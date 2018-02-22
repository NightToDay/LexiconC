using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking2._0GRP
{
    public class Vehicle
    {
        private string regNr;
        private DateTime arrivalTime;
        private int size;
        public Vehicle(string regNr)
        {
            this.regNr = regNr;      
        }
        public string RegNr
        {
            get { return this.regNr; }
            set { this.regNr = value; }
        }
        public int Size
        {
            get { return this.size; }
            set { this.size = value; }
        }
        public DateTime ArrivalTime
        {
            get { return this.arrivalTime; }
            set { this.arrivalTime = value; }
        }
    }
    
}
