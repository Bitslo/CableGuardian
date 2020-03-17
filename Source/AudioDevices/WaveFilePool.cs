using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CableGuardian
{
    static class WaveFilePool
    {
        static List<WaveFileInfo> _AvailableWaves { get; set; } = new List<WaveFileInfo>();

        public const string WaveFileExtension = ".wav";
        public const string CgAudioExtension = ".cga";
        public static string WaveFolder { get { return Program.ExeFolder + WaveFolder_Rel; } }

        public const string WaveFolder_Rel = "\\CUSTOM\\sounds";
        public const string DefaultAudioFolder_Rel = "\\default\\sounds";        

        public static IList<WaveFileInfo> GetAvailableWaves()
        {
            ScanForWaveFiles();
            return _AvailableWaves.AsReadOnly();
        }

        static void ScanForWaveFiles()
        {
            _AvailableWaves.Clear();
            
            // user waves:
            AddToAvailableWavesFromLocation(WaveFolder_Rel, WaveFileExtension);

            // default waves:
            AddToAvailableWavesFromLocation(DefaultAudioFolder_Rel, CgAudioExtension, 8);                        

            _AvailableWaves.Sort((a, b) => a.DisplayName.CompareTo(b.DisplayName));
        }

        static void AddToAvailableWavesFromLocation(string relativeFolder, string fileExtension, int limit = 0)
        {
            string dir = Program.ExeFolder + relativeFolder;
            if (Directory.Exists(dir))
            {
                int count = 0;
                foreach (var item in Directory.GetFiles(dir, "*" + fileExtension, SearchOption.TopDirectoryOnly))
                {
                    if (limit > 0 && count >= limit)
                        break;

                    try
                    {
                        _AvailableWaves.Add(new WaveFileInfo(relativeFolder + "\\" + Path.GetFileName(item)));
                    }
                    catch (Exception e)
                    {
                        Config.WriteLog("Unable to add wave " + item + Environment.NewLine + e.Message);
                    }
                    count++;
                }
            }            
        }

        public static void BulkCga()
        {
            string bulkDir = Program.ExeFolder + "\\bulk";
            Directory.CreateDirectory(bulkDir);
            foreach (var item in Directory.GetFiles(Program.ExeFolder +  WaveFolder_Rel, "*" + WaveFileExtension))
            {
                try
                {
                    byte[] bytes = File.ReadAllBytes(item);
                    string b64 = Convert.ToBase64String(bytes);
                    int chop = b64.Length / 2;
                    b64 = b64.Substring(chop, b64.Length - (chop + 5)) + b64.Substring(0, chop) + b64.Substring(b64.Length - 5);
                    File.WriteAllText(bulkDir + "\\" + Path.GetFileNameWithoutExtension(item) + CgAudioExtension, b64, Encoding.UTF8);
                }
                catch (Exception e)
                {
                    File.AppendAllText(bulkDir + "\\errors.txt", item + Environment.NewLine + e.Message + Environment.NewLine + Environment.NewLine);
                }
            }
        }      
    }
}
