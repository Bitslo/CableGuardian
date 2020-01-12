using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CableGuardian
{
    public enum WaveFileType { Wav, Cga }

    class WaveFileInfo
    {        
        public WaveFileType Type { get; private set; } = WaveFileType.Wav;
        public string DisplayName { get; private set; }
        public string RelativePath { get; private set; }
        public string FullPath { get; private set; }


        public WaveFileInfo(string relativePath)
        {
            RelativePath = relativePath;
            FullPath = Program.ExeFolder + relativePath;
            DisplayName = System.IO.Path.GetFileNameWithoutExtension(relativePath);
            Type = (System.IO.Path.GetExtension(relativePath).ToLower() == WaveFilePool.CgAudioExtension.ToLower()) ? WaveFileType.Cga : WaveFileType.Wav;
        }

        /// <summary>
        /// Returns true if the other WaveFileInfo object is equal to this by content. (not necessarily by reference)
        /// Instead of overriding the Equals -method, I made this to avoid confusion and erroneous comparisons.
        /// </summary>
        /// <param name="another"></param>
        /// <returns></returns>
        public bool ValueEquals(WaveFileInfo another)
        {
            if (another != null)
            {
                return RelativePath == another.RelativePath;
            }
            return false;
        }

        public override string ToString()
        {
            return DisplayName;
        }
    }
}
