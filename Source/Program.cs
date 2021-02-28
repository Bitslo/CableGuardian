using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.IO;
using System.Diagnostics;
using System.Reflection;

namespace CableGuardian
{
    static class Program
    {
        public static string ExeFile { get; private set; }
        public static string ExeFolder { get; private set; }        
        public const string ConfigName = "CGConfig";        
        public static string LogFile { get; private set; }
        public static string ConfigFile { get; private set; }        
        public static bool IsSteamInstallation { get; private set; } = true;
        public static bool IsWindowsStartup { get; private set; } = false;                
        public static bool IsRestart { get; private set; } = false;
        public static bool IsSteamLaunch { get; private set; } = false;
        public static bool AnotherInstanceExists { get; private set; } = false;
        public static string InstanceCheckErrorMsg { get; private set; }
        public static int WindowsStartupWaitInSeconds { get; private set; } = 30;
        public static string CmdArgsLCase { get; private set; } = "";

        public const string Arg_Maximized = "maximized";
        public const string Arg_Minimized = "minimized";
        public const string Arg_WinStartup = "winstartup";
        public const string Arg_SteamVRStartup = "steamvrstartup";
        public const string Arg_SteamLaunch = "steamlaunch";
        public const string Arg_IsRestart = "isrestart";
        public const string Arg_NoSteam = "nosteam";

        static bool BypassSteam { get; set; } = false;
        public static string SteamRestartErrorMsg { get; private set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            ExeFile = System.Reflection.Assembly.GetEntryAssembly().Location;
            ExeFolder = Path.GetDirectoryName(ExeFile);
            Environment.CurrentDirectory = ExeFolder; // always run from exe folder to avoid problems with dlls         

            ConfigFile = ExeFolder + $@"\{ConfigName}.xml";
            LogFile = ExeFolder + $@"\CGLog.txt";            

            if (args.Count() > 0)
            {
                CmdArgsLCase = String.Concat(args).ToLower();
            }

            if (CmdArgsLCase.Contains("bulkcga"))
            {
                WaveFilePool.BulkCga();
                Environment.Exit(0);
            }

            try
            {
                if (File.Exists(ExeFolder + $@"\CableGuardian.ghb"))
                    IsSteamInstallation = false;
            }
            catch (Exception)
            {
                // intentionally ignore
            }


            try
            {
                ReadEarlyConfig();
            }
            catch (Exception)
            {
                // intentionally ignore
            }

            if (CmdArgsLCase.Contains(Arg_IsRestart))
                IsRestart = true;

            if (CmdArgsLCase.Contains(Arg_SteamLaunch))
                IsSteamLaunch = true;

            if (CmdArgsLCase.Contains(Arg_NoSteam))
                BypassSteam = true;

            // Windows startup
            if (CmdArgsLCase.Contains(Arg_WinStartup))
            {
                IsWindowsStartup = true;
                System.Threading.Thread.Sleep(WindowsStartupWaitInSeconds * 1000); // wait for audio devices
            }

            AnotherInstanceExists = CheckExistingInstance();

            // restart if started from Steam and user does not want Steam in-app status
            if (BypassSteam && IsSteamLaunch && !AnotherInstanceExists)
            {
                try
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo(ExeFile, Arg_IsRestart);
                    Process.Start(startInfo);
                    Environment.Exit(0);
                }
                catch (Exception e)
                {
                    SteamRestartErrorMsg = "Failed to prevent Steam in-app status." + Environment.NewLine + e.Message;
                    Config.WriteLog(SteamRestartErrorMsg);
                }
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }

        static void ReadEarlyConfig()
        {
            // Ended up duplicating some xml reading here to have configurable startup wait time.
            if (File.Exists(ConfigFile))
            {
                XDocument XmlConfig = XDocument.Load(ConfigFile, LoadOptions.PreserveWhitespace);

                if (XmlConfig != null)
                {
                    XElement xBase = XmlConfig.Element(ConfigName);
                    if (xBase != null)
                    {
                        XElement cons = xBase.Element("CONSTANTS");
                        if (cons != null)
                            WindowsStartupWaitInSeconds = cons.GetElementValueInt("WindowsStartupWaitInSeconds", 30);
                    }
                }
            }
        }

        /// <summary>
        /// Returns true if another instance FROM THE SAME LOCATION is running
        /// </summary>
        /// <returns></returns>
        static bool CheckExistingInstance()
        {
            // if this is a restarted instance, we know there is/was another instance
            // skip actual check, because it can throw an exception when the previous instance closes mid-check
            if (IsRestart)
                return true;

            try
            {
                string cgName = Assembly.GetExecutingAssembly().GetName().Name;
                Process current = Process.GetCurrentProcess();
                if (Process.GetProcessesByName(cgName).Where
                    (
                        p => p.Id != current.Id
                        &&
                        String.Compare(p.MainModule.FileName, current.MainModule.FileName, true) == 0 // only from the same location
                    ).Any())
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                InstanceCheckErrorMsg = "Unexpected error when checking for an existing instance. " + Environment.NewLine + e.Message;
                Config.WriteLog(InstanceCheckErrorMsg);
            }

            return false;
        }
    }
}
