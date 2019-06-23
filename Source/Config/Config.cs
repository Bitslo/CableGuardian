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
        public static readonly Color CGColor = Color.FromArgb(86, 184, 254);
        public static readonly  Color CGErrorColor = Color.FromArgb(254, 84, 84);
        public static readonly Color CGBackColor = Color.FromArgb(15, 15, 15);                
        public static string ProfilesFile { get; private set; }
        public static string OculusHomeProcessName { get; private set; } = "oculusclient";
        public static string SteamVRProcessName { get; private set; } = "vrserver";        
        public static bool MinimizeAtUserStartup { get; set; } = false;
        public static bool MinimizeAtWindowsStartup { get; set; } = false;        
        public static bool NotifyWhenVRConnectionLost { get; set; } = true;
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
        /// <summary>
        /// backwards compatibility only
        /// </summary>
        public static VRAPI LegacyAPI { get; private set; } = VRAPI.OculusVR;
        public static bool IsLegacyConfig { get; private set; } = false;


        static Config()
        {   
            ProfilesFile = Program.ExeFolder + $@"\{ProfilesName}.xml";
        }

        public static void WriteWindowsStartupToRegistry(bool startWithWindows)
        {
            using (RegistryKey reg = Registry.CurrentUser.OpenSubKey(RegistryPathForStartup, true))
            {
                if (startWithWindows)
                    reg.SetValue(Program.ConfigName, "\"" + Program.ExeFile + "\" winstartup");
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
            string exeFolder = Program.ExeFolder;

            // Write baked in default sounds to disk if missing. A bit of double waste of space but IIRC they have to be on disk due to the audio implementation.
            string wavePath = exeFolder + $@"\TickTock.wav";
            if (!File.Exists(wavePath))
            {
                var fileStream = File.Create(wavePath);
                CableGuardian.Properties.Resources.TickTock.Seek(0, SeekOrigin.Begin);
                CableGuardian.Properties.Resources.TickTock.CopyTo(fileStream);
                fileStream.Close();
                WaveComboRefreshRequired = true; // a bit unfortunate gimmick, but whatever
            }
            wavePath = exeFolder + $@"\Bilibom.wav";
            if (!File.Exists(wavePath))
            {
                var fileStream = File.Create(wavePath);
                CableGuardian.Properties.Resources.Bilibom.Seek(0, SeekOrigin.Begin);
                CableGuardian.Properties.Resources.Bilibom.CopyTo(fileStream);
                fileStream.Close();
                WaveComboRefreshRequired = true;
            }
            wavePath = exeFolder + $@"\Beep_loud.wav";
            if (!File.Exists(wavePath))
            {
                var fileStream = File.Create(wavePath);
                CableGuardian.Properties.Resources.Beep_loud.Seek(0, SeekOrigin.Begin);
                CableGuardian.Properties.Resources.Beep_loud.CopyTo(fileStream);
                fileStream.Close();
                WaveComboRefreshRequired = true;
            }

            wavePath = exeFolder + $@"\CG_Jingle.wav";
            if (!File.Exists(wavePath))
            {
                var fileStream = File.Create(wavePath);
                CableGuardian.Properties.Resources.CG_Jingle.Seek(0, SeekOrigin.Begin);
                CableGuardian.Properties.Resources.CG_Jingle.CopyTo(fileStream);
                fileStream.Close();
                WaveComboRefreshRequired = true;
            }

            wavePath = exeFolder + $@"\CG_ConnLost.wav";
            if (!File.Exists(wavePath))
            {
                var fileStream = File.Create(wavePath);
                CableGuardian.Properties.Resources.CG_ConnLost.Seek(0, SeekOrigin.Begin);
                CableGuardian.Properties.Resources.CG_ConnLost.CopyTo(fileStream);
                fileStream.Close();
                WaveComboRefreshRequired = true;
            }

            wavePath = exeFolder + $@"\TurnLeft.wav";
            if (!File.Exists(wavePath))
            {
                var fileStream = File.Create(wavePath);
                CableGuardian.Properties.Resources.TurnLeft.Seek(0, SeekOrigin.Begin);
                CableGuardian.Properties.Resources.TurnLeft.CopyTo(fileStream);
                fileStream.Close();
                WaveComboRefreshRequired = true;
            }

            wavePath = exeFolder + $@"\TurnRight.wav";
            if (!File.Exists(wavePath))
            {
                var fileStream = File.Create(wavePath);
                CableGuardian.Properties.Resources.TurnRight.Seek(0, SeekOrigin.Begin);
                CableGuardian.Properties.Resources.TurnRight.CopyTo(fileStream);
                fileStream.Close();
                WaveComboRefreshRequired = true;
            }

            // default alarm:
            Alarm.Wave = "TickTock";
            Alarm.Pan = 0;
            Alarm.Volume = 100;
            Alarm.LoopCount = 3;

            // default jingle:
            Jingle.Wave = "CG_Jingle";
            Jingle.Pan = 0;
            Jingle.Volume = 50;
            Jingle.LoopCount = 1;

            // Connection lost sound:
            ConnLost.Wave = "CG_ConnLost";
            ConnLost.Pan = 0;
            ConnLost.Volume = 70;
            ConnLost.LoopCount = 1;
        }

        public static void WriteConfigToFile()
        {
            XDocument xCableGuardian =
                    new XDocument(
                        new XDeclaration("1.0", "UTF-8", "yes"),
                        GetConfigXml());                        
                        
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
                    MinimizeAtWindowsStartup = MinimizeAtUserStartup;
                }
                else
                {
                    MinimizeAtUserStartup = xConfig.GetElementValueBool("MinimizeAtUserStartup");
                    MinimizeAtWindowsStartup = xConfig.GetElementValueBool("MinimizeAtWindowsStartup");
                }
                
                NotifyWhenVRConnectionLost = xConfig.GetElementValueBool("NotifyWhenVRConnectionLost", true);
                NotifyOnAPIQuit = xConfig.GetElementValueBool("NotifyOnAPIQuit");
                TrayMenuNotifications = xConfig.GetElementValueBool("TrayMenuNotifications", true);
                PlaySoundOnHMDinteractionStart = xConfig.GetElementValueBool("PlaySoundOnHMDinteractionStart");
                ShowResetMessageBox = xConfig.GetElementValueBool("ShowResetMessageBox", true);
                LastSessionProfileName = xConfig.GetElementValueTrimmed("LastSessionProfileName");

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

        public static XElement GetConfigXml()
        {
            return new XElement(Program.ConfigName, 
                                new XElement("MinimizeAtUserStartup", MinimizeAtUserStartup),
                                new XElement("MinimizeAtWindowsStartup", MinimizeAtWindowsStartup),
                                new XElement("NotifyWhenVRConnectionLost", NotifyWhenVRConnectionLost),
                                new XElement("NotifyOnAPIQuit", NotifyOnAPIQuit),
                                new XElement("TrayMenuNotifications", TrayMenuNotifications),
                                new XElement("PlaySoundOnHMDinteractionStart", PlaySoundOnHMDinteractionStart),
                                new XElement("ShowResetMessageBox", ShowResetMessageBox),
                                new XElement("LastSessionProfileName", ActiveProfile?.Name),
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
