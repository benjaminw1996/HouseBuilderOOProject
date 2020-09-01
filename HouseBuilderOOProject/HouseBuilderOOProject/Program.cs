using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace HouseBuilderOOProject {
    class Program {

        public static List<House> houses = new List<House>();

        static void Main(string[] args) {
            Console.WriteLine("Hello, here's to a new beginning...");

            //Local variables to use during the running of the program
            bool run = true;
            bool creatingRooms = true;
            string userResponse;
            House tempHouse;

            //This loop is used to run the program until the user wishes to exit, this option is given at the end of the loop
            while (run) {
                tempHouse = new House();
                tempHouse = CreateNewHouse();

                while (creatingRooms) {
                    tempHouse.CreateRoom();

                    Console.Write("\nWould you like to make another room? (y/n) ");
                    creatingRooms = Utilities.ContinueLoop(); ;
                }

                Console.Write("\nWould you like to make another house? (y/n) ");
                run = Utilities.ContinueLoop(); ;
            }

            Console.Write("\nPress any key to end program...");
            Console.ReadKey();
        }

        /// <summary>
        /// This method is used to create a new House object,
        /// The user will be prompted for a name and number for the new house.
        /// </summary>
        static private House CreateNewHouse() {
            //Variables for the new house number and name
            int houseNumber = 0;
            string houseName = "";

            //This loop is used to repeat the new house number prompt until the user inputs a valid number
            while (true) {
                //The user is prompted to enter a number for the new house
                Console.Write("\nPlease enter the house number of the new house: ");
                //A try catch block is used to parse the user input as a positive integer, if they dont they are informed they have entered an invalid number
                try {
                    houseNumber = Int32.Parse(Console.ReadLine(), NumberStyles.None);

                    //If the house number is 0 then the user is informed that it must be above 0, if it is above 0 then is checked against the existing house numbers as duplicates arnt allowed
                    if (houseNumber == 0) {
                        Console.WriteLine("The house number must be above 0.");
                    } else if(CheckInUse(houseNumber,"") == false) {
                        break;
                    } else {
                        Console.WriteLine("That house number is already in use.");
                    }
                } catch {
                    Console.WriteLine("That is not a valid house number.");
                }
            }

            //This loop is used to repeat the new house name prompt until the user inputs a valid name
            while (true) {
                //The user is prompted to enter a name for the new house
                Console.Write("Please enter the name of the new house: ");
                houseName = Console.ReadLine();

                //The new name is checked against the existing names as duplicates arnt allowed
                if(CheckInUse(0,houseName) == false) {
                    break;
                } else {
                    Console.WriteLine("That house name is already in use.");
                }
            }

            //A new house object is created using the values inputted by the user, this is then added to the list of houses*
            House newHouse = new House(houseNumber, houseName);
            houses.Add(newHouse);

            return newHouse;
        }

        /// <summary>
        /// This method is used to check if a house name or number is already in use by another house.
        /// If the method is called and given an empty house name then the check is performed on the number, else it checks the name instead.
        /// </summary>
        /// <param name="houseNumber">The house number to check.</param>
        /// <param name="houseName">The house name to check, give "" if you want to check the number instead.</param>
        /// <returns>The method returns a boolean value, true if the name/number is in use or false if not.</returns>
        static private bool CheckInUse(int houseNumber, string houseName) {
            bool exists = false;

            //The list of houses is looped through to see if the desired name or number is already in use, if the name is in use the loops exits and true is returned
            foreach (House house in houses) {
                //If the name given is empty then the number is compared, else the name is compared
                if(houseName == "" && house.HouseNumber == houseNumber) {
                        exists = true;
                        break;
                } else {
                    if(house.HouseName == houseName) {
                        exists = true;
                        break;
                    }
                }
            }

            return exists;
        }

        /// <summary>
        /// This method is used to find a house given its name or number.
        /// </summary>
        /// <param name="houseNumber">Number of a house to find.</param>
        /// <param name="houseName">Name of a house to find.</param>
        /// <returns>The house object that was found in the list of houses.</returns>
        static private House FindHouse(int houseNumber, string houseName) {
            House tempHouse = null;

            //The list of houses is looped through, the given name or number is then compared to locate the desired house object
            foreach (House house in houses) {
                if(houseName == "" && houseNumber == house.HouseNumber) {
                    tempHouse = house;
                }else if (houseName == house.HouseName) {
                    tempHouse = house;
                }
            }

            //The located house object is then returned, null is returned if no house was found with the name/number.
            return tempHouse;
        }
    }
}
