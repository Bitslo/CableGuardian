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
        public override string ToString()
        {
            return Name;
        }
    }
}
