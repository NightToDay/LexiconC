using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking2._0GRP
{
    class ParkingSlot
    {

        public const int size = 4;
        public int freeSize = 4;
        private List<Vehicle> vehicles = new List<Vehicle>();

        public void Add(Vehicle vehicle)
        {
            vehicles.Add(vehicle);
            freeSize = freeSize - vehicle.Size;
        }

        public Vehicle Remove(string regNr)
        {
            Vehicle tmp = null;
            
            foreach (Vehicle aVehicle in vehicles)
            {
                //Om den hittar fordon med regNr
                if (aVehicle.RegNr == regNr)
                {
                    tmp = aVehicle;
                    this.freeSize = tmp.Size + this.freeSize;
                    vehicles.Remove(tmp);
                    return tmp;
                }
            }
            return tmp;
        }
        public bool Search(string regNr)
        {
            foreach (Vehicle aVehicle in vehicles)
            {
                //Om den hittar fordon med regNr
                if (aVehicle.RegNr == regNr)
                {
                    return true;
                }
            }
            return false;
        }

        public String Content()
        {
            string allVehicle = "";
            //kollar om den inte är tom
            if (vehicles.Count > 0)
            {
                allVehicle = "|";
                foreach (Vehicle v in vehicles)
                {
                    allVehicle = allVehicle + v.ToString() + "|";
                }
            }
            return allVehicle;
        }
    }
}
