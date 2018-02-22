using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking2._0GRP
{
    public class Car : Vehicle
    {
        public Car(string regNr) : base(regNr)
        {
            base.Size = 4;
            base.ArrivalTime = DateTime.Now;
        }

        public override string ToString()
        {
            return "Car:" + base.RegNr;
        }

    }


   
}
