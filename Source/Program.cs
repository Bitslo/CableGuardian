using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.IO;

namespace CableGuardian
{
    static class Program
    {
        public static string ExeFile { get; private set; }
        public static string ExeFolder { get; private set; }
        public const string ConfigName = "CGConfig";
        public static string ConfigFile { get; private set; }
        public static bool IsWindowsStartup { get; private set; } = false;
        public static int WindowsStartupWaitInSeconds { get; private set; } = 30;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            ExeFile = System.Reflection.Assembly.GetEntryAssembly().Location;
            ExeFolder = Path.GetDirectoryName(ExeFile);
            ConfigFile = ExeFolder + $@"\{ConfigName}.xml";
            Environment.CurrentDirectory = ExeFolder; // always run from exe folder to avoid problems with dlls            

            try
            {
                ReadEarlyConfig();
            }
            catch (Exception)
            {
                // intentionally ignore
            }            

            if (args.Count() > 0 && args[0].ToLower() == "winstartup")
            {
                IsWindowsStartup = true;
                System.Threading.Thread.Sleep(WindowsStartupWaitInSeconds * 1000); // wait for audio devices
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
