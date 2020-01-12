using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CableGuardian
{
    class WaveOutDevice
    {
        public string Name { get; private set; }
        public int Number { get; private set; }

        public WaveOutDevice(int number, string name)
        {            
            Number = number;
            Name = name;
        }

        /// <summary>
        /// Returns true if the other WaveOutDevice object is equal to this by content. (not necessarily by reference)
        /// Instead of overriding the Equals -method, I made this to avoid confusion and erroneous comparisons.
        /// </summary>
        /// <param name="another"></param>
        /// <returns></returns>
        public bool ValueEquals(WaveOutDevice another)
        {
            if (another != null)
            {
                return Name == another.Name;
            }
            return false;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
