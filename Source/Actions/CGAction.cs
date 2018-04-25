using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CableGuardian
{
    abstract class CGAction
    {   
        public abstract bool Enabled { get; set; }

        public void Run()
        {
            // add common actions here...

            RunImplementation();        
            
            // add common actions here...
        }

        protected abstract void RunImplementation();

        public virtual void LoadFromXml(XElement xCGAction)
        {
            if (xCGAction != null)
            {
                // nothing here atm
            }
        }

        public virtual XElement GetXml()
        {
            return new XElement("CGAction", "");
        }

        public void Delete()
        {
            DeleteImplementation();
        }

        protected abstract void DeleteImplementation();
    }
}
