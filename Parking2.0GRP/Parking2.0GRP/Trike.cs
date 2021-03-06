﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking2._0GRP
{
    public class Trike : Vehicle
    {
        public Trike(string regNr) : base(regNr)
        {
            this.Size = 3;
            ArrivalTime = DateTime.Now;
        }

        public override string ToString()
        {
            return "Trike:" + base.RegNr;
        }
    }

}
