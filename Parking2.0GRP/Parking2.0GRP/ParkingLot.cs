using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking2._0GRP
{
    class ParkingLot
    {

        const int parkingSize = 100;
        private ParkingSlot[] parkingSlots = new ParkingSlot[parkingSize];
        public int numFreeSlots = parkingSize;
        private int numFreeSizeUnits = 400;
        
        public ParkingLot()
        {
            SeedArray();
        }

        public int Add(Vehicle vehicle)
        {
            if (numFreeSizeUnits < vehicle.Size) return -1;

            for (int i = 0; i < parkingSlots.Length; i++)
            {
                int to = Add(vehicle, i);
                if (to > -1) return to;
            }
            return -1;
        }

        public int Add(Vehicle vehicle, int i)
        {
            if (parkingSlots[i].freeSize >= vehicle.Size)
            {
                parkingSlots[i].Add(vehicle);
                numFreeSizeUnits -= vehicle.Size;
                return i;
            }
            return -1;
        }

        public bool Move(string regNr, int slot)
        {
            int from = Search(regNr);
            if (from != -1)
            {
                Vehicle v = parkingSlots[from].Remove(regNr);
                if (v == null) return false;
                else
                {
                    int to = Add(v, slot);
                    if (to == -1)
                    {
                        to = Add(v, from);
                        return false;
                    }
                    return true;
                }
            }
            return false;
        }


        public string Remove(string regNr)
        {
            int plats, minute = 0, hour = 0;
            Vehicle tempV;
            DateTime now = DateTime.Now;
            plats = Search(regNr);
            if (plats >= 0)
            {
                now = now.AddHours(1);
                tempV = parkingSlots[plats].Remove(regNr);
                hour = now.Hour - tempV.ArrivalTime.Hour;
                minute = now.Minute - tempV.ArrivalTime.Hour;
                if (minute < 0)
                    minute *= -1;
                return "Remove the vehicle at " + (plats + 1) + " and the parked time is hour:" + hour + " minute:" + minute + "";
            }
            return "Can´t find the vehicle you want to remove.";
        }

        public int Search(string regNr)
        {
            for (int i = 0; i < parkingSize; i++)
            {
                //Om det hittar annars return -1
                if (parkingSlots[i].Search(regNr))
                {
                    return i;
                }
            }
            return -1;
        }

        public void CountFreeSlot()
        {
            int count = 0;

            foreach (ParkingSlot slot in parkingSlots)
            {
                if (slot.freeSize == 4)
                    count++;
            }
            numFreeSlots = count;            
        }

        public void CountFreeSizeUnit()
        {
            int count = 0;

            foreach (ParkingSlot slot in parkingSlots)
            {
                //Kollar om det inte är tom eller fulla.
                if (slot.freeSize < 4 && slot.freeSize > 0)
                    count++;
            }
            numFreeSizeUnits = count;

        }
        public void SeedArray()
        {
            Random rd = new Random(DateTime.Now.Millisecond);
            int index = rd.Next(0, 100);

            for (int i = 0; i < parkingSize; i++)
            {
                parkingSlots[i] = new ParkingSlot();
            }

            Vehicle vec;
            //skapar Cars
            for (int i = 0; i < 4; i++)
            {
                if (parkingSlots[index].freeSize == 4)
                {
                    Task.Delay(3000);
                    vec = new Car(CreateVehicleReg(rd));
                    parkingSlots[index].Add(vec);                    
                    index = rd.Next(0, 100);
                }
                else
                    i--;
            }
            //skapar MC
            for (int i = 0; i < 4; i++)
            {
                index = rd.Next(0, 100);
                if (parkingSlots[index].freeSize >= 2)
                {
                    Task.Delay(3000);
                    vec = new MC(CreateVehicleReg(rd));
                    parkingSlots[index].Add(vec);
                    index = rd.Next(0, 100);
                }
                else
                {
                    i--;
                }
            }

            //Skapar Trikes
            for (int i = 0; i < 4; i++)
            {
                index = rd.Next(0, 100);
                if (parkingSlots[index].freeSize >= 3)
                {
                    Task.Delay(3000);
                    vec = new Trike(CreateVehicleReg(rd));
                    parkingSlots[index].Add(vec);
                    index = rd.Next(0, 100);                    
                }
                else
                {
                    i--;
                }
            }

            //Skapar Bikes
            for (int i = 0; i < 4; i++)
            {
                index = rd.Next(0, 100);
                if (parkingSlots[index].freeSize >= 1)
                {
                    Task.Delay(3000);
                    vec = new Bike("NR" + i);
                    parkingSlots[index].Add(vec);
                    index = rd.Next(0, 100);                  
                }
                else
                {
                    i--;
                }
            }
            //Räknar hur många tomma slot.
            CountFreeSizeUnit();
            CountFreeSlot();
        }

        static string CreateVehicleReg(Random r)
        {

            string regNr = "";
            char c;
            //Skapar a-z 3 bokstäver
            for (int i = 0; i < 3; i++)
            {
                c = (char)(r.Next(65, 90));
                Task.Delay(3000);
                regNr = regNr.Insert(regNr.Length, c.ToString());
            }
            //Skapar 3 nummer
            for (int i = 0; i < 3; i++)
            {
                c = (char)r.Next(48, 57);
                Task.Delay(3000);
                regNr = regNr.Insert(regNr.Length, c.ToString());
            }

            return regNr;
        }

        public string[] Content()
        {
            string[] allSlot = new string[parkingSize];

            for (int i = 0; i < parkingSize; i++)
            {
                allSlot[i] = parkingSlots[i].Content();
            }
            return allSlot;
        }
    }
}
