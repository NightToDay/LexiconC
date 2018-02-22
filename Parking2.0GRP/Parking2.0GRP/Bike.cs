using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking2._0GRP
{
    public class Bike : Vehicle
    {
        public Bike(string regNr) : base(regNr)
        {
            this.Size = 1;
            ArrivalTime = DateTime.Now;
        }
        public override string ToString()
        {
            return "Bike:" + base.RegNr;
        }

    }
}
