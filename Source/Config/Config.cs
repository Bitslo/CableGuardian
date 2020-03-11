using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using Microsoft.Win32;
using System.Drawing;

namespace CableGuardian
{
    static class Config
    {        
        public const string ProfilesName = "CGProfiles";
        public const string RegistryPathForStartup = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";
        public const string ProgramTitle = "Cable Guardian";
        public const string ManifestAppKey = "cableguardian";
        public static string ManifestPath { get { return Program.ExeFolder + "\\CableGuardian.vrmanifest"; } }
        public static string ManifestContents { get; private set; }
        public static readonly Color CGColor = Color.FromArgb(86, 184, 254);
        public static readonly  Color CGErrorColor = Color.FromArgb(254, 84, 84);
        public static readonly Color CGBackColor = Color.FromArgb(15, 15, 15);                
        public static string ProfilesFile { get; private set; }
        public static string OculusHomeProcessName { get; private set; } = "oculusclient";
        public static string SteamVRProcessName { get; private set; } = "vrserver";        
        public static bool MinimizeAtUserStartup { get; set; } = false;
        public static bool MinimizeAtAutoStartup { get; set; } = true;        
        public static bool NotifyWhenVRConnectionLost { get; set; } = true;
        public static bool ConnLostNotificationIsSticky { get; set; } = true;
        public static bool NotifyOnAPIQuit { get; set; } = false;
        public static bool TrayMenuNotifications { get; set; } = true;
        public static bool ShowResetMessageBox { get; set; } = true;
        public static CGActionWave Alarm { get; private set; } = new CGActionWave(FormMain.WaveOutPool);
        public static CGActionWave Jingle { get; private set; } = new CGActionWave(FormMain.WaveOutPool);
        public static CGActionWave ConnLost { get; private set; } = new CGActionWave(FormMain.WaveOutPool);
        public static bool PlaySoundOnHMDinteractionStart { get; set; } = false;
        public static List<Profile> Profiles { get; private set; } = new List<Profile>();
        public static Profile StartUpProfile { get; set; }
        public static Profile ActiveProfile { get; private set; }
        public static int ProfilesFileBackupCount { get; set; } = 5;
        public static bool WaveComboRefreshRequired { get; set; }  = false;
        public static bool ConfigFileMissingAtStartup { get; private set; } = false;
        public static bool ProfilesFileMissingAtStartup { get; private set; } = false;
        public static string LastSessionProfileName { get; private set; } = "";
        public static uint LastExitSeconds { get; private set; } = 0;
        public static int LastHalfTurn { get; private set; } = 0;
        public static double LastYawValue { get; private set; } = 0;
        public static int TurnCountMemoryMinutes { get; set; } = -1;        
        /// <summary>
        /// backwards compatibility only
        /// </summary>
        public static VRAPI LegacyAPI { get; private set; } = VRAPI.OculusVR;
        public static bool IsLegacyConfig { get; private set; } = false;


        static Config()
        {   
            ProfilesFile = Program.ExeFolder + $@"\{ProfilesName}.xml";

            ManifestContents = Properties.Resources.CableGuardianVrManifest;
            ManifestContents = ManifestContents.Replace("$APPKEY$", ManifestAppKey);
            ManifestContents = ManifestContents.Replace("$ARGS$", Program.Arg_SteamVRStartup);
            ManifestContents = ManifestContents.Replace("$EXEPATH$", Program.ExeFile.Replace("\\", "\\\\"));
        }

        public static void WriteWindowsStartupToRegistry(bool startWithWindows)
        {
            using (RegistryKey reg = Registry.CurrentUser.OpenSubKey(RegistryPathForStartup, true))
            {
                if (startWithWindows)
                    reg.SetValue(Program.ConfigName, "\"" + Program.ExeFile + "\" " + Program.Arg_WinStartup);
                else
                    reg.DeleteValue(Program.ConfigName, false);
            }
        }

        public static bool ReadWindowsStartupFromRegistry()
        {
            using (RegistryKey reg = Registry.CurrentUser.OpenSubKey(RegistryPathForStartup, true))
            {
                return (reg.GetValue(Program.ConfigName) != null);
            }
        }

        public static uint GetCurrentSeconds()
        {
            try
            {
                DateTime zero = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                return (uint)Math.Floor((DateTime.Now.ToUniversalTime() - zero).TotalSeconds);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static int GetInitialHalfTurn()
        {
            if (TurnCountMemoryMinutes == 0)
            {
                return LastHalfTurn;
            }
            else if (TurnCountMemoryMinutes > 0) // -1 = disabled
            {
                if (LastExitSeconds > 0)
                {
                    uint elapsedSeconds = GetCurrentSeconds() - LastExitSeconds;
                    if (elapsedSeconds <= (TurnCountMemoryMinutes * 60))
                    {
                        return LastHalfTurn;
                    }
                }
                else
                {
                    WriteLog($"Last exit time was not recorded properly. Unable to check elapsed time. Returning last half turn ({LastHalfTurn}).");
                    return LastHalfTurn;
                }                
            }
            return 0;            
        }

        public static void WriteLog(string message)
        {
            try
            {
                if (File.Exists(Program.LogFile))
                {
                    FileInfo i = new System.IO.FileInfo(Program.LogFile);
                    if (i.Length > 1000000) // delete log at about 1 MB. (probably never grows that big, since normally nothing is written there)
                    {
                        File.Delete(Program.LogFile);
                    }
                }

                File.AppendAllText(Program.LogFile, DateTime.Now.ToString() + Environment.NewLine +
                                                 message + Environment.NewLine + Environment.NewLine);
            }
            catch (Exception)
            {
                // intentionally ignore... 
            }
        }

        public static void SetActiveProfile(Profile profile)
        {
            if (ActiveProfile != null)
            {
                ActiveProfile.DeActivate();
            }

            profile.Activate();
            ActiveProfile = profile;            
        }

        public static void AddProfile(Profile profile)
        {            
            Profiles.Add(profile);
        }

        public static void RemoveProfile(Profile profile)
        {
            Profiles.Remove(profile);            
        }

        public static void CheckDefaultSounds()
        {
            if (Alarm.Wave == null)
            {
                // default alarm:
                Alarm.SetWave(new WaveFileInfo(WaveFilePool.DefaultAudioFolder_Rel + "\\CG_TickTock" + WaveFilePool.CgAudioExtension));
                Alarm.Pan = 0;
                Alarm.Volume = 100;
                Alarm.LoopCount = 3;
            }
            
            if (Jingle.Wave == null)
            {
                // default jingle:
                Jingle.SetWave(new WaveFileInfo(WaveFilePool.DefaultAudioFolder_Rel + "\\CG_Jingle" + WaveFilePool.CgAudioExtension));
                Jingle.Pan = 0;
                Jingle.Volume = 50;
                Jingle.LoopCount = 1;
            }

            // Connection lost sound:
            ConnLost.SetWave(new WaveFileInfo(WaveFilePool.DefaultAudioFolder_Rel + "\\CG_ConnLost" + WaveFilePool.CgAudioExtension)); 
            ConnLost.Pan = 0;
            ConnLost.Volume = 70;
            ConnLost.LoopCount = 1;
        }

        public static void WriteManifestFile()
        {
            File.WriteAllText(ManifestPath, ManifestContents);
        }

        public static void WriteConfigToFile(bool isExit = false)
        {
            XDocument xCableGuardian =
                    new XDocument(
                        new XDeclaration("1.0", "UTF-8", "yes"),
                        GetConfigXml(isExit));                        
                        
            // UTF-8 (with BOM):
            xCableGuardian.Save(Program.ConfigFile);
        }

        public static void WriteProfilesToFile()
        {
            XDocument xCableGuardian =
                    new XDocument(
                        new XDeclaration("1.0", "UTF-8", "yes"),
                        GetProfilesXml());

            // backup previous version to .001 etc
            FileIO.CreateNumberedBackup(ProfilesFile, ProfilesFileBackupCount);

            // UTF-8 (with BOM):
            xCableGuardian.Save(ProfilesFile);
        }

        public static void ReadConfigFromFile()
        {
            if (File.Exists(Program.ConfigFile))
            {
                XDocument XmlConfig = XDocument.Load(Program.ConfigFile, LoadOptions.PreserveWhitespace);

                if (XmlConfig != null)
                {
                    XElement xBase = XmlConfig.Element(Program.ConfigName);
                    if (xBase != null)
                    {
                        LoadConfigFromXml(xBase);
                    }
                }
            }
            else
            {
                ConfigFileMissingAtStartup = true;
            }
        }
           
        public static void ReadProfilesFromFile()
        {
            XDocument XmlProfiles;

            if (File.Exists(ProfilesFile))
            {
                XmlProfiles = XDocument.Load(ProfilesFile, LoadOptions.PreserveWhitespace);
            }
            else
            {
                ProfilesFileMissingAtStartup = true;
                
                // try to detect Oculus HMD and set default profile accordingly
                if (FormMain.OculusConn.OculusHMDConnected())
                    XmlProfiles = XDocument.Parse(CableGuardian.Properties.Resources.CGProfiles_Default_Oculus, LoadOptions.PreserveWhitespace);
                else
                    XmlProfiles = XDocument.Parse(CableGuardian.Properties.Resources.CGProfiles_Default_OpenVR, LoadOptions.PreserveWhitespace);
            }

            if (XmlProfiles != null)
            {
                XElement xBase = XmlProfiles.Element(ProfilesName);
                if (xBase != null)
                {
                    LoadProfilesFromXml(xBase);
                }
            }
        }

        public static void LoadConfigFromXml(XElement xConfig)
        {
            if (xConfig != null)
            {                
                if (xConfig.GetElementValueOrNull("API") != null) // backwards compatibility
                {
                    IsLegacyConfig = true;
                    if (Enum.TryParse(xConfig.GetElementValueTrimmed("API"), out VRAPI a))
                        LegacyAPI = a;                    
                }

                if (xConfig.GetElementValueOrNull("StartMinimized") != null) // backwards compatibility
                {
                    MinimizeAtUserStartup = xConfig.GetElementValueBool("StartMinimized");
                    MinimizeAtAutoStartup = MinimizeAtUserStartup;
                }
                else
                {
                    MinimizeAtUserStartup = xConfig.GetElementValueBool("MinimizeAtUserStartup");

                    if (xConfig.GetElementValueOrNull("MinimizeAtWindowsStartup") != null) // backwards compatibility
                    {
                        MinimizeAtAutoStartup = xConfig.GetElementValueBool("MinimizeAtWindowsStartup");
                    }
                    else
                    {
                        MinimizeAtAutoStartup = xConfig.GetElementValueBool("MinimizeAtAutoStartup");
                    }   
                }
                
                NotifyWhenVRConnectionLost = xConfig.GetElementValueBool("NotifyWhenVRConnectionLost", true);
                ConnLostNotificationIsSticky = xConfig.GetElementValueBool("ConnLostNotificationIsSticky", true);
                NotifyOnAPIQuit = xConfig.GetElementValueBool("NotifyOnAPIQuit");
                TrayMenuNotifications = xConfig.GetElementValueBool("TrayMenuNotifications", true);
                PlaySoundOnHMDinteractionStart = xConfig.GetElementValueBool("PlaySoundOnHMDinteractionStart");
                ShowResetMessageBox = xConfig.GetElementValueBool("ShowResetMessageBox", true);
                LastSessionProfileName = xConfig.GetElementValueTrimmed("LastSessionProfileName");
                LastExitSeconds = xConfig.GetElementValueUInt("LastExitSeconds");                
                LastHalfTurn = xConfig.GetElementValueInt("LastHalfTurn");
                LastYawValue = xConfig.GetElementValueDouble("LastYawValue");
                
                if (xConfig.GetElementValueOrNull("TurnCountMemoryMinutes") != null)
                    TurnCountMemoryMinutes = xConfig.GetElementValueInt("TurnCountMemoryMinutes");
                else
                    TurnCountMemoryMinutes = -1;

                XElement xAlarm = xConfig.Element("Alarm");               
                Alarm.LoadFromXml(xAlarm?.Element("CGActionWaveFile"));

                XElement xJingle = xConfig.Element("Jingle");
                Jingle.LoadFromXml(xJingle?.Element("CGActionWaveFile"));

                XElement cons = xConfig.Element("CONSTANTS");
                if (cons != null)
                {
                    string temp = cons.GetElementValueTrimmed("OculusHomeProcessName");
                    if (!String.IsNullOrWhiteSpace(temp))
                        OculusHomeProcessName = temp;

                    temp = cons.GetElementValueTrimmed("SteamVRProcessName");
                    if (!String.IsNullOrWhiteSpace(temp))
                        SteamVRProcessName = temp;
                }
            }
        }

        public static void LoadProfilesFromXml(XElement xProfiles)
        {
            if (xProfiles != null)
            {   
                foreach (var prof in xProfiles.Descendants().Where(element => element.Name == "Profile"))
                {
                    Profile newProf = new Profile();
                    newProf.LoadFromXml(prof);
                    Profiles.Add(newProf);
                    newProf.DeActivate(); // important 
                }
                StartUpProfile = Profiles.Where(p => p.Name == xProfiles.GetElementValueTrimmed("StartupProfile")).FirstOrDefault();                
            }
        }

        public static XElement GetConfigXml(bool isExit = false)
        {
            return new XElement(Program.ConfigName, 
                                new XElement("MinimizeAtUserStartup", MinimizeAtUserStartup),
                                new XElement("MinimizeAtAutoStartup", MinimizeAtAutoStartup),
                                new XElement("NotifyWhenVRConnectionLost", NotifyWhenVRConnectionLost),
                                new XElement("ConnLostNotificationIsSticky", ConnLostNotificationIsSticky),
                                new XElement("NotifyOnAPIQuit", NotifyOnAPIQuit),
                                new XElement("TrayMenuNotifications", TrayMenuNotifications),
                                new XElement("PlaySoundOnHMDinteractionStart", PlaySoundOnHMDinteractionStart),
                                new XElement("ShowResetMessageBox", ShowResetMessageBox),
                                new XElement("LastSessionProfileName", ActiveProfile?.Name),
                                new XElement("LastExitSeconds", (isExit) ? GetCurrentSeconds() : 0), 
                                new XElement("LastHalfTurn", FormMain.Tracker.CurrentHalfTurn.ToString(System.Globalization.CultureInfo.InvariantCulture)),
                                new XElement("LastYawValue", FormMain.Tracker.YawValue.ToString(System.Globalization.CultureInfo.InvariantCulture)),
                                new XElement("TurnCountMemoryMinutes", TurnCountMemoryMinutes.ToString(System.Globalization.CultureInfo.InvariantCulture)),
                                new XElement("Alarm", Alarm.GetXml()),
                                new XElement("Jingle", Jingle.GetXml()),
                                new XElement("CONSTANTS",
                                new XComment("Wait time when starting with Windows. The purpose is to ensure that all audio devices have been initialized before using them."),
                                new XElement("WindowsStartupWaitInSeconds", Program.WindowsStartupWaitInSeconds),
                                new XComment("DO NOT TOUCH. These are for future proofing. In case SteamVR or Oculus Home processes are named differently in an update (unlikely)."),
                                new XElement("OculusHomeProcessName", OculusHomeProcessName),
                                new XElement("SteamVRProcessName", SteamVRProcessName))
                                );
        }

        public static XElement GetProfilesXml()
        {
            return new XElement(ProfilesName,
                                new XElement("StartupProfile", StartUpProfile?.Name),
                                new XElement("Profiles", from Profile u in Profiles select u.GetXml()));
        }
    }
}
