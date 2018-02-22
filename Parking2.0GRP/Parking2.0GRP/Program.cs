using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking2._0GRP
{
    class Program
    {
        static void Main(string[] args)
        {
            ParkingLot parking = new ParkingLot();
            while (true)

            {
                PrintState(parking);
                if (Meny(parking))
                    break;
                Console.ReadLine();
                Console.Clear();
                
            }
        }


        static public bool Meny(ParkingLot park)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("                                    Welcome to Prague Parking2,0!");
            Console.WriteLine("Choose one");
            Console.WriteLine("1. ADD");
            Console.WriteLine("2. REMOVE");
            Console.WriteLine("3. SEARCH");
            Console.WriteLine("4. MOVE");
            Console.WriteLine("5. Exit");
            int userChoice = Int32.Parse(Console.ReadLine());
            int place;
            switch (userChoice)
            {
                case 1://ADD
                    {
                        Vehicle vec;
                        Console.WriteLine("Choose the vehicle type you wish to park:");
                        Console.WriteLine("1. CAR");
                        Console.WriteLine("2. MC");
                        Console.WriteLine("3. TRIKE");
                        Console.WriteLine("4. BIKE");
                        int userInput = Int32.Parse(Console.ReadLine());
                        Console.WriteLine("Enter registernumber:");
                        string regNr = Console.ReadLine().ToUpper();
                        switch (userInput)
                        {
                            case 1:
                                {                                    
                                    vec = new Car(regNr);
                                    place = park.Add(vec);
                                    Console.WriteLine("Put the Car into place: {0}", place+1);
                                    break;
                                }   
                                
                            case 2:
                                {                                    
                                    vec = new MC(regNr);
                                    place = park.Add(vec);
                                    Console.WriteLine("Put the MC into place: {0}", place + 1);
                                    break;
                                }
                            case 3:
                                {                                    
                                    vec = new Trike(regNr);
                                    place = park.Add(vec);
                                    Console.WriteLine("Put the Trike into place: {0}", place + 1);
                                    break;
                                }
                            case 4:
                                {                                   
                                    vec = new Bike(regNr);
                                    place = park.Add(vec);
                                    Console.WriteLine("Put the Bike into place: {0}", place + 1);
                                    break;
                                }
                            default:
                                Console.WriteLine("Enter number between 1 to 4");
                                break;
                        }
                        break;
                    }
                case 2://REMOVE
                    {
                        Console.WriteLine("Enter registernumber:");
                        string regNr = Console.ReadLine().ToUpper();
                        Console.WriteLine(park.Remove(regNr) );
                        break;
                    }
                    
                    
                case 3://SEARCH
                    {
                        Console.WriteLine("Enter registernumber:");
                        string regNr = Console.ReadLine().ToUpper();
                        place = park.Search(regNr);
                        if(place < 0)
                            Console.WriteLine("Can´t find the vehicle");
                        else
                            Console.WriteLine("The vehicle is in slot: {0}", place +1);
                        break;
                    }
                    
                    
                case 4://MOVE
                    {
                        Console.WriteLine("Enter registernumber:");
                        string regNr = Console.ReadLine().ToUpper();
                        Console.WriteLine("Where do you want to put the vehicle?");
                        int slot =int.Parse(Console.ReadLine());
                        bool b = park.Move(regNr, slot-1);
                        if (b) Console.WriteLine("Moved to spot {0}", slot);
                        else Console.WriteLine("Can't move vehicle");                        
                        break;
                    }
                    
                    
                case 5://EXIT
                    {
                        Console.WriteLine("Are you sure you want to exit the program? (Y/N)");
                        string input = Console.ReadLine().ToUpper();
                        if (input == "Y")
                        {
                            Console.WriteLine("Goodbye!");
                            Console.ReadLine();
                            userChoice = -1;
                            Console.Clear();
                         
                        }
                        return true;                     
                        
                    }
                default:
                    {
                        Console.WriteLine("Enter number between 1 to 5");
                        break;
                    }


            }

            Console.ForegroundColor = ConsoleColor.White;
            return false;
        }


              
        static void PrintState(ParkingLot p)
        {
            int index = 0;
            string[] parking = p.Content();
            for (int i = 0; i < 33; i++)
            {
                for (int x = 0; x < 3; x++)
                {
                    Console.CursorLeft = x * 50;
                    if (index < 9) Console.Write(" ");
                    if (index < 99) Console.Write(" ");

                    if (parking[index].Equals(""))
                        Console.Write("{0}: [Spot Empty]", index + 1);
                    if (!parking[index].Equals(""))
                        Console.Write("{0}: {1} ", index + 1, parking[index].ToString());

                    index++;
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    
       
    }
}
        