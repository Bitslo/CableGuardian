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
        const string ManifestAppKeyBase = "cableguardian";
        static string ManifestAppKey = null; // will be generated
        public static string ManifestPath { get { return Program.ExeFolder + "\\CableGuardian.vrmanifest"; } }        
        public static readonly Color CGColor = Color.FromArgb(86, 184, 254);
        public static readonly  Color CGErrorColor = Color.FromArgb(254, 84, 84);
        public static readonly Color CGBackColor = Color.FromArgb(15, 15, 15);
        static string ZeroVer = "0.0.0.0";
        public static Version LoadedVersion { get; private set; } = new Version(ZeroVer);
        public static string ProfilesFile { get; private set; }
        public static string OculusHomeProcessName { get; private set; } = "oculusclient";
        public static string SteamVRProcessName { get; private set; } = "vrserver";
        public static bool StartMinimized { get; set; } = false;
        public static bool NotifyStartMinimized { get; set; } = true;
        public static bool NotifyWhenVRConnectionLost { get; set; } = true;
        public static bool ConnLostNotificationIsSticky { get; set; } = true;
        public static bool NotifyOnAPIQuit { get; set; } = false;
        public static bool TrayMenuNotifications { get; set; } = true;
        public static bool ShowResetMessageBox { get; set; } = true;
        public static CGActionWave Alarm { get; private set; } = new CGActionWave(FormMain.WaveOutPool);
        public static XElement JingleXML_Legacy { get; private set; }
        public static CGActionWave ConnLost { get; private set; } = new CGActionWave(FormMain.WaveOutPool);
        public static bool PlayMountingSound_Legacy { get; set; } = false;
        public static List<Profile> Profiles { get; private set; } = new List<Profile>();
        public static Profile StartUpProfile { get; set; }
        public static Profile ActiveProfile { get; private set; }
        public static int ProfilesFileBackupCount { get; set; } = 5;
        public static bool WaveComboRefreshRequired { get; set; }  = false;
        public static bool ConfigFileMissingAtStartup { get; private set; } = false;
        public static bool ProfilesFileMissingAtStartup { get; private set; } = false;
        public static bool ProfilesFileLoadFailed { get; private set; } = false;
        public static bool ProfilesLoadedFromBackup { get; private set; } = false;
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
        public static bool SaveProfilesAtStartup { get; set; } = false;                
        public static bool ExitWithSteamVR { get; set; } = false;
        public static bool WelcomeFormClosed { get; set; } = false;
        public static bool UseRawCoordinatesInOpenVR { get; set; } = true;
        
        public static bool UseSimpleMode { get; set; } = false;
        public static uint SimpleModeThreshold { get; set; } = 3;
        public static SimpleNotifType SimpleModeNotifType { get; set; } = SimpleNotifType.Beep;
        public static int SimpleModeVolume { get; set; } = 75;
        public static bool SimpleModeResetOnMount { get; set; } = false;
        public static bool SimpleModePlayMountingSound { get; set; } = false;
        


        static Config()
        {   
            ProfilesFile = Program.ExeFolder + $@"\{ProfilesName}.xml";
        }

        /// <summary>
        /// returns an appkey that is _likely_ unique between CG installations
        /// </summary>
        /// <returns></returns>
        public static string GetManifestAppKey()
        {
            if (ManifestAppKey != null)
                return ManifestAppKey;

            // backwards compatibility with pre-hotfix version:
            string prev = GetPreviousManifestAppKey();
            if (prev != null && prev.Length > "cableguardian".Length)
                return ManifestAppKey = prev;

            string ext = Program.ExeFolder.Length.ToString();
                        
            uint code = 0;
            foreach (char c in Program.ExeFolder)
            {                
                code += c;
            }

            ext += ((int)code).ToString();

            return ManifestAppKey = ManifestAppKeyBase + ext;
        }

        static string GetPreviousManifestAppKey()
        {
            try
            {
                if (File.Exists(ManifestPath))
                {
                    string[] lines = File.ReadAllLines(ManifestPath);
                    string appkey = lines.Where(l => l.Contains("app_key")).FirstOrDefault();
                    appkey = appkey.Substring(appkey.IndexOf("cableguardian"));
                    return appkey.Substring(0, appkey.IndexOf("\""));                    
                }
            }
            catch (Exception)
            {
                // intentionally ignore
            }
            return null;
        }

        public static Profile GetProfileByName(string name)
        {
            return Profiles.Where(p => p.Name == name).FirstOrDefault();
        }

        public static void SortProfilesByName()
        {
            Profiles.Sort((a, b) => a.Name.CompareTo(b.Name));
        }


        static string GetLatestProfilesBackupFile()
        {
            List<string> files = Directory.GetFiles(Program.ExeFolder, ProfilesName + ".*", SearchOption.TopDirectoryOnly).ToList();
            files?.Sort();
            return files?.Where(str => str.ToUpper().Contains("XML") == false).FirstOrDefault();
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
                ActiveProfile.Deactivate();
            }

            profile.Activate();
            ActiveProfile = profile;            
        }

        public static void AddProfile(Profile profile)
        {            
            Profiles.Add(profile);
            SortProfilesByName();
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
                Alarm.LoopCount = 2;
            }

            // Connection lost sound:
            ConnLost.SetWave(new WaveFileInfo(WaveFilePool.DefaultAudioFolder_Rel + "\\CG_ConnLost" + WaveFilePool.CgAudioExtension)); 
            ConnLost.Pan = 0;
            ConnLost.Volume = 70;
            ConnLost.LoopCount = 1;
        }

        public static void WriteManifestFile()
        {
            string manifestContents;

            if (Program.IsSteamInstallation)
                manifestContents = Properties.Resources.CableGuardianVrManifest;
            else
                manifestContents = Properties.Resources.CableGuardianVrManifestNoSteam;

            manifestContents = manifestContents.Replace("$APPKEY$", GetManifestAppKey());
            manifestContents = manifestContents.Replace("$ARGS$", Program.Arg_SteamVRStartup);
            manifestContents = manifestContents.Replace("$EXEPATH$", Program.ExeFile.Replace("\\", "\\\\"));

            File.WriteAllText(ManifestPath, manifestContents);
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
                UseSimpleMode = true;
            }
        }

        public static void ReadProfilesFromFile()
        {
            XDocument XmlProfiles = null;
            bool error = false;

            if (File.Exists(ProfilesFile))
            {
                try
                {
                    XmlProfiles = XDocument.Load(ProfilesFile, LoadOptions.PreserveWhitespace);
                }
                catch (Exception e)
                {
                    XmlProfiles = null;
                    error = true;
                    WriteLog($"Error loading {ProfilesFile}. {Environment.NewLine}{e.Message}");
                }
            }
            if (XmlProfiles == null)
            {
                string latestBackup = GetLatestProfilesBackupFile();
                if (latestBackup != null && File.Exists(latestBackup))
                {
                    try
                    {
                        XmlProfiles = XDocument.Load(latestBackup, LoadOptions.PreserveWhitespace);
                        ProfilesLoadedFromBackup = (XmlProfiles != null);
                    }
                    catch (Exception e)
                    {
                        XmlProfiles = null;
                        error = true;
                        WriteLog($"Error loading {latestBackup}. {Environment.NewLine}{e.Message}");
                    }
                }
                else
                {
                    ProfilesFileMissingAtStartup = true;
                }
            }

            if (XmlProfiles == null)
            {
                ProfilesFileLoadFailed = error;

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
                string ver = xConfig.GetElementValueTrimmed("Version");
                if (String.IsNullOrWhiteSpace(ver))
                    ver = ZeroVer;

                try
                {
                    LoadedVersion = new Version(ver);
                }
                catch (Exception)
                {
                    LoadedVersion = new Version(ZeroVer);                    
                }


                if (xConfig.GetElementValueOrNull("API") != null) // backwards compatibility
                {
                    IsLegacyConfig = true;
                    SaveProfilesAtStartup = true;
                    if (Enum.TryParse(xConfig.GetElementValueTrimmed("API"), out VRAPI a))
                        LegacyAPI = a;                    
                }

                if (xConfig.GetElementValueOrNull("StartMinimized") == null) // backwards compatibility
                {                    
                    StartMinimized = xConfig.GetElementValueBool("MinimizeAtUserStartup");
                }
                else
                {
                    StartMinimized = xConfig.GetElementValueBool("StartMinimized");
                }

                NotifyStartMinimized = xConfig.GetElementValueBool("NotifyStartMinimized", true);
                NotifyWhenVRConnectionLost = xConfig.GetElementValueBool("NotifyWhenVRConnectionLost", true);
                ConnLostNotificationIsSticky = xConfig.GetElementValueBool("ConnLostNotificationIsSticky", true);
                NotifyOnAPIQuit = xConfig.GetElementValueBool("NotifyOnAPIQuit");
                TrayMenuNotifications = xConfig.GetElementValueBool("TrayMenuNotifications", true);
                PlayMountingSound_Legacy = xConfig.GetElementValueBool("PlaySoundOnHMDinteractionStart");
                ShowResetMessageBox = xConfig.GetElementValueBool("ShowResetMessageBox", true);
                LastSessionProfileName = xConfig.GetElementValueTrimmed("LastSessionProfileName");
                LastExitSeconds = xConfig.GetElementValueUInt("LastExitSeconds");                
                LastHalfTurn = xConfig.GetElementValueInt("LastHalfTurn");
                LastYawValue = xConfig.GetElementValueDouble("LastYawValue");
                                
                ExitWithSteamVR = xConfig.GetElementValueBool("ExitWithSteamVR", false);

                if (xConfig.GetElementValueOrNull("TurnCountMemoryMinutes") != null)
                    TurnCountMemoryMinutes = xConfig.GetElementValueInt("TurnCountMemoryMinutes");
                else
                    TurnCountMemoryMinutes = -1;

                XElement xAlarm = xConfig.Element("Alarm");               
                Alarm.LoadFromXml(xAlarm?.Element("CGActionWaveFile"));

                JingleXML_Legacy = xConfig.Element("Jingle");

                WelcomeFormClosed = xConfig.GetElementValueBool("WelcomeFormClosed", false);
                // Do not show the welcome form if an earlier version of the app was already in use
                if (LoadedVersion < new Version("1.3.3.3")) 
                    WelcomeFormClosed = true;

                UseSimpleMode = xConfig.GetElementValueBool("UseSimpleMode", false);
                SimpleModeThreshold = xConfig.GetElementValueUInt("SimpleModeThreshold", 3);
                if (Enum.TryParse(xConfig.GetElementValueTrimmed("SimpleModeNotifType"), out SimpleNotifType nType) == false)
                    nType = SimpleNotifType.Beep;

                SimpleModeNotifType = nType;
                SimpleModeVolume = xConfig.GetElementValueInt("SimpleModeVolume", 75);
                SimpleModeResetOnMount = xConfig.GetElementValueBool("SimpleModeResetOnMount", false);

                if (xConfig.GetElementValueOrNull("SimpleModePlayMountingSound") != null) // backwards compatibility                
                    SimpleModePlayMountingSound = xConfig.GetElementValueBool("SimpleModePlayMountingSound", false);
                else
                    SimpleModePlayMountingSound = SimpleModeResetOnMount;

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
                    newProf.Deactivate(); // important 
                }
                SortProfilesByName();
                StartUpProfile = Profiles.Where(p => p.Name == xProfiles.GetElementValueTrimmed("StartupProfile")).FirstOrDefault();                
            }
        }                

        public static XElement GetConfigXml(bool isExit = false)
        {
            Version ver = System.Reflection.Assembly.GetEntryAssembly().GetName().Version;            

            return new XElement(Program.ConfigName,
                                new XElement("Version", ver.ToString()),
                                new XElement("StartMinimized", StartMinimized),
                                new XElement("NotifyStartMinimized", NotifyStartMinimized),
                                new XElement("NotifyWhenVRConnectionLost", NotifyWhenVRConnectionLost),
                                new XElement("ConnLostNotificationIsSticky", ConnLostNotificationIsSticky),
                                new XElement("NotifyOnAPIQuit", NotifyOnAPIQuit),
                                new XElement("TrayMenuNotifications", TrayMenuNotifications),                                
                                new XElement("ShowResetMessageBox", ShowResetMessageBox),
                                new XElement("LastSessionProfileName", ActiveProfile?.Name),
                                new XElement("LastExitSeconds", (isExit) ? GetCurrentSeconds() : 0), 
                                new XElement("LastHalfTurn", FormMain.Tracker.CurrentHalfTurn.ToString(System.Globalization.CultureInfo.InvariantCulture)),
                                new XElement("LastYawValue", FormMain.Tracker.YawValue.ToString(System.Globalization.CultureInfo.InvariantCulture)),
                                new XElement("TurnCountMemoryMinutes", TurnCountMemoryMinutes.ToString(System.Globalization.CultureInfo.InvariantCulture)),
                                new XElement("Alarm", Alarm.GetXml()),                                                           
                                new XElement("ExitWithSteamVR", ExitWithSteamVR),
                                new XElement("WelcomeFormClosed", WelcomeFormClosed),
                                new XElement("UseSimpleMode", UseSimpleMode),
                                new XElement("SimpleModeThreshold", SimpleModeThreshold),
                                new XElement("SimpleModeNotifType", SimpleModeNotifType),
                                new XElement("SimpleModeVolume", SimpleModeVolume),
                                new XElement("SimpleModeResetOnMount", SimpleModeResetOnMount),
                                new XElement("SimpleModePlayMountingSound", SimpleModePlayMountingSound),
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
