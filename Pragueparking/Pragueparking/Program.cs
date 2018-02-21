using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Pragueparking
{
	class Program
	{
		const int MAXSPACE = 100;
		static string[] arParking = new string[MAXSPACE];
		const string Seperator = ":";
		const string EmptySpace = "";
		const string strTitle = "Prague Parking 1.0";

		static void Main(string[] args)
		{
			Console.Title = strTitle;
			ParkingInitArray();
			ParkingInitArrayRandom();

			do
			{
				switch (UIMenu())
				{
					case '1':   // Add Car
						UIAdd(true);
						break;
					case '2':   // Add MC
						UIAdd(false);
						break;
					case '3':   // Remove
						UIRemove();
						break;
					case '4':   // Search
						UISearch();
						break;
					case '5':   // Move
						UIMove();
						break;
					case '6':   // Quit
						return;
				}

			} while (true);
		}


		static char UIMenu()
		{
			Console.BackgroundColor = ConsoleColor.DarkBlue;
			Console.Clear();

			// Add Header
			UIDrawBox(0, 0, 120, 1, ConsoleColor.DarkCyan); // Title box
			Console.ForegroundColor = ConsoleColor.White;
			Console.SetCursorPosition((Console.WindowWidth - strTitle.Length) / 2, 0);
			Console.Write(strTitle);

			// Parking Status
			UIShowParkingStatus();

			// StatusBar
			UIDrawBox(0, 21, 120, 1, ConsoleColor.DarkCyan);
			Console.SetCursorPosition(0, 21);
			Console.ForegroundColor = ConsoleColor.White;
			Console.Write(ParkingStat());   // Display Parking status

			// Show Options bar
			UIDrawBox(0, 22, 120, 1, ConsoleColor.Black);

			// Add Car
			UIDrawBox(5, 22, 16, 1, ConsoleColor.DarkBlue);
			Console.SetCursorPosition(5, 22);
			Console.ForegroundColor = ConsoleColor.Red;
			Console.Write("1");
			Console.ForegroundColor = ConsoleColor.Gray;
			Console.Write(" Add Car");

			// Add MC
			UIDrawBox(23, 22, 17, 1, ConsoleColor.DarkBlue);
			Console.SetCursorPosition(23, 22);
			Console.ForegroundColor = ConsoleColor.Red;
			Console.Write("2");
			Console.ForegroundColor = ConsoleColor.Gray;
			Console.Write(" Add MC");

			// Remove
			UIDrawBox(42, 22, 17, 1, ConsoleColor.DarkBlue);
			Console.SetCursorPosition(42, 22);
			Console.ForegroundColor = ConsoleColor.Red;
			Console.Write("3");
			Console.ForegroundColor = ConsoleColor.Gray;
			Console.Write(" Remove");


			// Search
			UIDrawBox(61, 22, 17, 1, ConsoleColor.DarkBlue);
			Console.SetCursorPosition(61, 22);
			Console.ForegroundColor = ConsoleColor.Red;
			Console.Write("4");
			Console.ForegroundColor = ConsoleColor.Gray;
			Console.Write(" Search");

			// Move
			UIDrawBox(80, 22, 17, 1, ConsoleColor.DarkBlue);
			Console.SetCursorPosition(80, 22);
			Console.ForegroundColor = ConsoleColor.Red;
			Console.Write("5");
			Console.ForegroundColor = ConsoleColor.Gray;
			Console.Write(" Move");

			// Quit
			UIDrawBox(99, 22, 16, 1, ConsoleColor.DarkBlue);
			Console.SetCursorPosition(99, 22);
			Console.ForegroundColor = ConsoleColor.Red;
			Console.Write("6");
			Console.ForegroundColor = ConsoleColor.Gray;
			Console.Write(" Quit");

			// Draw box for text to enter
			UIDrawBox(0, 23, 120, 6, ConsoleColor.Black);

			Console.SetCursorPosition(0, 24);
			Console.ForegroundColor = ConsoleColor.White;
			Console.Write("Please, select on option > ");

			return Console.ReadKey(true).KeyChar;
		}


		static void UIAdd(bool bIsCar)
		{
			// Get Registration number
			string strRegNo = UIGetRegNo("Please, enter registration number for " + ((bIsCar) ? "Car" : "MC") + " > ");

			if (ParkingSearch(strRegNo) != -1)  // Check for duplicate value
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Duplicate Value.");
				Console.ForegroundColor = ConsoleColor.White;
				Console.ReadKey(true);
				return;
			}

			// Add Parking and Check for possible Error
			if (ParkingAdd(strRegNo, bIsCar) == -1)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Parking is full!");
				Console.ForegroundColor = ConsoleColor.White;
				Console.ReadKey(true);
			}

		}


		static void UIRemove()
		{
			string strRegNo = UIGetRegNo("Please, enter registration number to remove > ");
			if (ParkingRemove(strRegNo) == -1)
			{
				Console.WriteLine("Registration number not found");
				Console.ReadKey(true);
			}
			else
			{
				Console.WriteLine($"Registration number: {strRegNo} removed successfully.");
			}
		}


		static void UISearch()
		{
			string strRegNo = UIGetRegNo("Please, enter registration number to search > ");
			int Index = ParkingSearch(strRegNo);
			if (Index == -1)
			{
				Console.WriteLine("Registration number not found");
			}
			else
			{
				Console.WriteLine($"Registration number located in {Index + 1}.");
			}


			Console.ForegroundColor = ConsoleColor.Gray;
			Console.WriteLine("Press any key to countine...");
			Console.ReadKey(true);
		}


		static void UIMove()
		{
			string strRegNo = UIGetRegNo("Please, enter registration number to move > ");
			Console.Write("Enter new place number > ");
			string sNewPlace = Console.ReadLine().Trim();

			int iNewPlace;
			if (Int32.TryParse(sNewPlace, out iNewPlace))
			{
				// Validate for range
				if (iNewPlace < 1 || iNewPlace > MAXSPACE)
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("Value is out of range.");
					Console.ForegroundColor = ConsoleColor.White;
					Console.ReadKey(true);
					return;
				}

				// "Move" and check for possible error
				if (ParkingMove(strRegNo, iNewPlace - 1) == -1)
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("Move is invalid");
					Console.ForegroundColor = ConsoleColor.White;
					Console.ReadKey(true);
					return;
				}
			}
			else
			{
				Console.Write("Value is not number.");
				Console.ReadKey(true);
				return;
			}
		}


		static void UIShowParkingStatus()
		{
			//Console.BackgroundColor = ConsoleColor.Gray;
			UIDrawBox(5, 1, 110, 20, ConsoleColor.Gray);

			int iMaxX = Console.WindowWidth;
			int iMaxY = Console.WindowHeight;

			int left = 5, top = 1;

			for (int i = 0; i < arParking.Length; i++)
			{
				left = Math.Min(iMaxX - 1, left);
				top = Math.Min(iMaxY - 1, top);

				Console.SetCursorPosition(left, top);

				UIDrawBox(left, top, 3, 2, ConsoleColor.White);


				Console.SetCursorPosition(left, top);   // Reset cursor place again

				Console.ForegroundColor = ConsoleColor.Red;
				Console.Write($"{i + 1}");

				Console.BackgroundColor = ConsoleColor.Gray;
				Console.ForegroundColor = (arParking[i].IndexOf(Seperator) == -1) ? ConsoleColor.DarkBlue : ConsoleColor.DarkGreen;

				string[] str = arParking[i].Split(Seperator, StringSplitOptions.RemoveEmptyEntries);

				int iDiff = 0;
				foreach (string item in str)
				{
					Console.SetCursorPosition(left + 3, top + iDiff);

					Console.Write(UITruncateString(item, 8));
					iDiff++;
				}

				Console.SetCursorPosition(left, top);   // Reset cursor place again

				// Set position
				top += 2;
				if ((top - 1) % 20 == 0)
				{
					left += 11;
					top = 1;
				}
			}
		}


		static string UIGetRegNo(String strText)
		{
			// Set text prompt
			UIDrawBox(0, 24, 120, 4, ConsoleColor.Black);
			Console.SetCursorPosition(0, 24);
			Console.ForegroundColor = ConsoleColor.White;
			Console.Write(strText);

			bool bIsValid = false;
			string strRegNo = String.Empty;

			do
			{
				strRegNo = Console.ReadLine().Trim();

				// Validate for Alphabet and numbers
				if (!Regex.IsMatch(strRegNo, @"^[a-zA-Z0-9]+$"))
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.Write("Just letters and numbers. try again> ");
					Console.ForegroundColor = ConsoleColor.White;
					continue;
				}

				// Validate for string length
				if ((strRegNo.Length < 2 || strRegNo.Length > 25))
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.Write("Invalid registration number. try again> ");
					Console.ForegroundColor = ConsoleColor.White;
				}
				else
				{
					bIsValid = true;
				}
			} while (!bIsValid);

			return strRegNo.ToUpper();
		}


		static void UIDrawBox(int left, int top, int x, int y, ConsoleColor color)
		{
			Console.SetCursorPosition(left, top);
			Console.BackgroundColor = color;
			Console.ForegroundColor = color;

			for (int i = 0; i < x; i++)
			{
				for (int j = 0; j < y; j++)
				{
					Console.SetCursorPosition(left + i, top + j);
					Console.Write(" ");
				}
			}
		}


		static string UITruncateString(string str, int iMaxLen)
		{
			if (str.Length > iMaxLen)
			{
				return str.Substring(0, iMaxLen - 1) + "#";
			}
			else
				return str;
		}


		static int ParkingAdd(string RegNo, bool bIsCar)
		{
			int Index = ParkingGetEmptySpace(bIsCar);

			// if there is no empty space
			if (Index == -1)
				return -1;

			if (arParking[Index] == EmptySpace)    // car and mc
			{
				arParking[Index] = (bIsCar) ? RegNo : RegNo + Seperator;
			}
			else
			{
				// Just for mc, with one mc already in place
				arParking[Index] += RegNo;
			}

			return Index;
		}



		static int ParkingSearch(string RegNo)
		{
			for (int i = 0; i < arParking.Length; i++)
			{
				string[] str = arParking[i].Split(Seperator, StringSplitOptions.RemoveEmptyEntries);

				foreach (string item in str)
					if (item == RegNo)
						return i;
			}

			return -1;   // Not found
		}


		static int ParkingRemove(string RegNo)
		{
			int iIndex = ParkingSearch(RegNo);

			// if not found
			if (iIndex == -1)
				return -1;

			string strCurrent = arParking[iIndex];

			// Car or single mc
			if (strCurrent.IndexOf(Seperator) == -1 || strCurrent.EndsWith(Seperator))
			{
				arParking[iIndex] = EmptySpace;
			}
			// double mc
			else
			{
				arParking[iIndex] = strCurrent.Replace(RegNo, String.Empty).Replace(Seperator, String.Empty) + Seperator;
			}
			return iIndex;
		}


		static int ParkingMove(string RegNo, int NewPlace)
		{
			int OldPlace = ParkingSearch(RegNo);
			if (OldPlace != -1)
			{
				bool bIsCar = (arParking[OldPlace].IndexOf(Seperator) == -1) ? true : false;

				if (bIsCar)	//Car
				{
					if (arParking[NewPlace] != EmptySpace)  // If it is not suitable for car
						return -1;
					arParking[NewPlace] = arParking[OldPlace];	// Copy to new place
					arParking[OldPlace] = EmptySpace;			// Delete from old place
				}
				else // MC
				{
					if (arParking[NewPlace] == EmptySpace || arParking[NewPlace].EndsWith(Seperator))
					{
						ParkingRemove(RegNo);
						arParking[NewPlace] += (arParking[NewPlace].IndexOf(Seperator) == -1) ? (RegNo + Seperator) : RegNo;
					}
					else
					{
						return -1;
					}

				}
			}
			else
			{
				return -1;
			}

			return NewPlace;
		}


		static int ParkingGetEmptySpace(bool bIsCar)
		{
			// Just for motorcyckel
			if (!bIsCar)
			{
				for (int i = 0; i < arParking.Length; i++)
				{
					if (arParking[i].EndsWith(Seperator))
					{
						return i;
					}
				}
			}

			// For car or when there is no shared place for a motorcyckle
			for (int j = 0; j < arParking.Length; j++)
			{
				if (arParking[j] == EmptySpace)
					return j;
			}

			return -1;  // If nothing found
		}


		static string ParkingStat()
		{
			int iCar = 0;
			int iMC = 0;
			int iSpaceForCar = 0;
			int iSpaceForMC = 0;
			foreach (string item in arParking)
			{
				if (item == EmptySpace)	// No Vehicle, Just empty space
				{
					iSpaceForCar++;
					iSpaceForMC += 2;
				}
				else // There is something in Space
				{
					if (item.IndexOf(Seperator) == -1)  // For cars
					{
						iCar++;
					}
					else
					{
						if (item.EndsWith(Seperator)) // One MC
						{
							iMC++;
							iSpaceForMC++;
						}
						else // Two MC
						{
							iMC += 2;
						}
					}
				}
			}

			return String.Format($"Cars: {iCar}, MC: {iMC}, Space for cars: {iSpaceForCar}, Space for MC: {iSpaceForMC}");
		}


		static void ParkingInitArray()
		{
			for (int i = 0; i < arParking.Length; i++)
				arParking[i] = EmptySpace;
		}


		static void ParkingInitArrayRandom()
		{
			Random random = new Random();
			int iNumVehicle = 100;
			for (int i = 0; i < iNumVehicle; i++)
			{
				bool bIsCar = (random.Next(0, 2) == 1) ? true : false;
				int iLen = random.Next(4, 8);

				string strRandRegNo = Path.GetRandomFileName();
				strRandRegNo = strRandRegNo.Replace(".", "");  // remove '.' from file extension
				strRandRegNo = strRandRegNo.Substring(0, iLen);
				ParkingAdd(strRandRegNo.ToUpper(), bIsCar);
			}

		}

	}
}
