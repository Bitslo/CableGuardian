using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.IO;
using System.Diagnostics;

namespace CableGuardian
{
    static class Program
    {
        public static string ExeFile { get; private set; }
        public static string ExeFolder { get; private set; }
        public const string ConfigName = "CGConfig";        
        public static string LogFile { get; private set; }
        public static string ConfigFile { get; private set; }        
        public static bool IsWindowsStartup { get; private set; } = false;
        public static bool IsSteamVRStartup { get; private set; } = false;
        public static bool IsAutoStartup { get { return IsWindowsStartup || IsSteamVRStartup; } }
        public static int WindowsStartupWaitInSeconds { get; private set; } = 30;
        public static string CmdArgsLCase { get; private set; } = "";

        public const string Arg_Maximized = "maximized";
        public const string Arg_Minimized = "minimized";
        public const string Arg_WinStartup = "winstartup";
        public const string Arg_SteamVRStartup = "steamvrstartup";
        public const string Arg_IsRestart = "isrestart";


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            ExeFile = System.Reflection.Assembly.GetEntryAssembly().Location;
            ExeFolder = Path.GetDirectoryName(ExeFile);
            ConfigFile = ExeFolder + $@"\{ConfigName}.xml";
            LogFile = ExeFolder + $@"\CGLog.txt";
            
            Environment.CurrentDirectory = ExeFolder; // always run from exe folder to avoid problems with dlls                        

            if (args.Count() > 0)
            {
                CmdArgsLCase = String.Concat(args).ToLower();
            }

            try
            {
                ReadEarlyConfig();
            }
            catch (Exception)
            {
                // intentionally ignore
            }

            // Windows startup
            if (CmdArgsLCase.Contains(Arg_WinStartup))
            {
                IsWindowsStartup = true;
                System.Threading.Thread.Sleep(WindowsStartupWaitInSeconds * 1000); // wait for audio devices
            }

            // SteamVR startup
            if (CmdArgsLCase.Contains(Arg_SteamVRStartup))
            {
                IsSteamVRStartup = true;                
            }

            // Exit if already running (autostart only)
            if (IsAutoStartup)
            {                
                string cgName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
                Process current = Process.GetCurrentProcess();
                if (Process.GetProcessesByName(cgName).Where
                    (
                        p => p.Id != current.Id
                        &&
                        String.Compare(p.MainModule.FileName, current.MainModule.FileName, true) == 0 // only check instances from the same location
                    ).Any())
                {
                    Environment.Exit(0);
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
    }
}
