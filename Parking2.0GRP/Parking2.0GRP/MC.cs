using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking2._0GRP
{
    public class MC : Vehicle
    {
        public MC(string regNr) : base(regNr)
        {
            this.Size = 2;
            base.ArrivalTime = DateTime.Now;
        }
        public override string ToString()
        {
            return "MC:" + base.RegNr;
        }

    }

    
}
