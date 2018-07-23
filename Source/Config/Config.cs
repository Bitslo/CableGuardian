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
        public const string ConfigName = "CGConfig";
        public const string ProfilesName = "CGProfiles";
        public const string RegistryPathForStartup = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";
        public const string ProgramTitle = "Cable Guardian";
        public static readonly Color CGColor = Color.FromArgb(86, 184, 254);
        public static readonly  Color CGErrorColor = Color.FromArgb(254, 84, 84);
        public static readonly Color CGBackColor = Color.FromArgb(15, 15, 15);
        public static string ExeFile { get; private set; }
        public static string ExeFolder { get; private set; }
        public static string ConfigFile { get; private set; }
        public static string ProfilesFile { get; private set; }
        public static string OculusHomeProcessName { get; private set; } = "oculusclient";
        public static string SteamVRProcessName { get; private set; } = "vrserver";

        public static bool StartMinimized { get; set; } = false;
        public static bool RequireHome { get; set; } = false;
        public static bool NotifyWhenVRConnectionLost { get; set; } = true;
        public static bool TrayMenuNotifications { get; set; } = true;
        public static CGActionWave Alarm { get; private set; } = new CGActionWave(FormMain.WaveOutPool);        
        public static VRAPI API { get; set; } = VRAPI.OculusVR;
        public static List<Profile> Profiles { get; private set; } = new List<Profile>();
        public static Profile StartUpProfile { get; set; }
        static Profile ActiveProfile { get; set; }
        public static int ProfilesFileBackupCount { get; set; } = 5;
        public static bool WaveComboRefreshRequired { get; set; }  = false;
        
        static Config()
        {
            ExeFile = System.Reflection.Assembly.GetEntryAssembly().Location;
            ExeFolder = Path.GetDirectoryName(ExeFile);
            ConfigFile = ExeFolder + $@"\{ConfigName}.xml";
            ProfilesFile = ExeFolder + $@"\{ProfilesName}.xml";
        }

        public static void WriteWindowsStartupToRegistry(bool startWithWindows)
        {
            using (RegistryKey reg = Registry.CurrentUser.OpenSubKey(RegistryPathForStartup, true))
            {
                if (startWithWindows)
                    reg.SetValue(ConfigName, ExeFile);
                else
                    reg.DeleteValue(ConfigName, false);
            }
        }

        public static bool ReadWindowsStartupFromRegistry()
        {
            using (RegistryKey reg = Registry.CurrentUser.OpenSubKey(RegistryPathForStartup, true))
            {
                return (reg.GetValue(ConfigName) != null);
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
            // default sounds
            string wavePath = ExeFolder + $@"\TickTock.wav";
            if (!File.Exists(wavePath))
            {
                var fileStream = File.Create(wavePath);
                CableGuardian.Properties.Resources.TickTock.Seek(0, SeekOrigin.Begin);
                CableGuardian.Properties.Resources.TickTock.CopyTo(fileStream);
                fileStream.Close();
                WaveComboRefreshRequired = true; // a bit unfortunate gimmick, but whatever
            }
            wavePath = ExeFolder + $@"\Bilibom.wav";
            if (!File.Exists(wavePath))
            {
                var fileStream = File.Create(wavePath);
                CableGuardian.Properties.Resources.Bilibom.Seek(0, SeekOrigin.Begin);
                CableGuardian.Properties.Resources.Bilibom.CopyTo(fileStream);
                fileStream.Close();
                WaveComboRefreshRequired = true;
            }
            wavePath = ExeFolder + $@"\Beep_classic.wav";
            if (!File.Exists(wavePath))
            {
                var fileStream = File.Create(wavePath);
                CableGuardian.Properties.Resources.Beep_classic.Seek(0, SeekOrigin.Begin);
                CableGuardian.Properties.Resources.Beep_classic.CopyTo(fileStream);
                fileStream.Close();
                WaveComboRefreshRequired = true;
            }

            // default alarm:
            Alarm.Wave = "TickTock";
            Alarm.Pan = 0;
            Alarm.Volume = 100;
            Alarm.LoopCount = 3;
        }

        public static void WriteConfigToFile()
        {
            XDocument xCableGuardian =
                    new XDocument(
                        new XDeclaration("1.0", "UTF-8", "yes"),
                        GetConfigXml());                        
                        
            // UTF-8 (with BOM):
            xCableGuardian.Save(ConfigFile);
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
            if (File.Exists(ConfigFile))
            {
                XDocument XmlConfig = XDocument.Load(ConfigFile, LoadOptions.PreserveWhitespace);

                if (XmlConfig != null)
                {
                    XElement xBase = XmlConfig.Element(ConfigName);
                    if (xBase != null)
                    {
                        LoadConfigFromXml(xBase);
                    }
                }
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
                // default profile              
                XmlProfiles = XDocument.Parse(CableGuardian.Properties.Resources.CGProfiles, LoadOptions.PreserveWhitespace);                
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
                StartMinimized = xConfig.GetElementValueBool("StartMinimized");
                RequireHome = xConfig.GetElementValueBool("RequireHome");
                NotifyWhenVRConnectionLost = xConfig.GetElementValueBool("NotifyWhenVRConnectionLost", true);
                TrayMenuNotifications = xConfig.GetElementValueBool("TrayMenuNotifications", true);

                XElement xAlarm = xConfig.Element("Alarm");               
                Alarm.LoadFromXml(xAlarm?.Element("CGActionWaveFile"));
                                

                XElement cons = xConfig.Element("CONSTANTS");
                string temp = cons.GetElementValueTrimmed("OculusHomeProcessName");
                if (!String.IsNullOrWhiteSpace(temp))
                    OculusHomeProcessName = temp;

                temp = cons.GetElementValueTrimmed("SteamVRProcessName");
                if (!String.IsNullOrWhiteSpace(temp))
                    SteamVRProcessName = temp;

                if (Enum.TryParse(xConfig.GetElementValueTrimmed("API"), out VRAPI a))
                    API = a;
                else
                    API = VRAPI.OculusVR;
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
            return new XElement(ConfigName, 
                                new XElement("StartMinimized", StartMinimized),
                                new XElement("RequireHome", RequireHome),
                                new XElement("NotifyWhenVRConnectionLost", NotifyWhenVRConnectionLost),
                                new XElement("TrayMenuNotifications", TrayMenuNotifications),
                                new XElement("API", API),
                                new XElement("Alarm", Alarm.GetXml()),                                
                                new XElement("CONSTANTS",
                                new XComment("These are for future proofing. In case SteamVR or Oculus Home processes are named differently in an update (unlikely)."),
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
