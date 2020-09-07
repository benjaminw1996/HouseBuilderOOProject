using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

namespace HouseBuilderOOProject {
    class Program {

        public static List<House> houses = new List<House>();
        public static string housesJsonFile;
        public static string filePath = @"C:\Users\Benjamin\Documents\Visual Studio 2019\Projects\HouseBuilderOOProject\HouseBuilderOOProject\Houses.txt";

        static void Main(string[] args) {
            //Console.WriteLine("Hello, here's to a new beginning...");

            //Local variables to use during the running of the program
            bool run = true;
            string userResponse;

            LoadHouses();

            //Displaying to the user how many houses there are currently existing
            Console.WriteLine("There are currently " + houses.Count + " houses.");

            //This loop is used to run the program until the user wishes to exit, this option is given at the end of the loop
            while (run) {
                Console.WriteLine("\nHere are the actions you can perform - \n\t1. Create a new house.\n\t2. Remove a house.\n\t3. Display Houses.\n\t4. Save the houses.\n\t5. End the program.");
                Console.Write("Please select an action to perform - ");
                userResponse = Console.ReadLine();

                switch (userResponse) {
                    case "1":
                        CreateNewHouse();
                        break;

                    case "2":
                        RemoveHouse();
                        break;

                    case "3":
                        DisplayHouses();
                        break;

                    case "4":
                        SaveHouses();
                        break;

                    case "5":
                        Exit();
                        run = false;
                        break;

                    default:
                        Console.WriteLine("\nUnknown option selected!\n");
                        break;
                }
            }

            Console.WriteLine("\nYou created the following houses - ");
            DisplayHouses();

            Console.Write("\nPress any key to end program...");
            Console.ReadKey();
        }

        /// <summary>
        /// This method is used to create a new House object,
        /// The user will be prompted for a name and number for the new house.
        /// </summary>
        private static void CreateNewHouse() {
            //Variables for the new house number and name
            int houseNumber;
            string houseName;
            bool creatingRooms = true;

            Console.WriteLine("\nCreating a new house...");

            houseNumber = GetHouseNumber();

            houseName = GetHouseName();

            //A new house object is created using the values inputted by the user, this is then added to the list of houses*
            House newHouse = new House(houseNumber, houseName);

            while (creatingRooms) {
                newHouse.CreateRoom();

                Console.Write("\nWould you like to make another room? (y/n) ");
                creatingRooms = Utilities.ContinueLoop(); ;
            }

            houses.Add(newHouse);
        }

        private static int GetHouseNumber() {
            int houseNumber;

            //This loop is used to repeat the new house number prompt until the user inputs a valid number
            while (true) {
                //The user is prompted to enter a number for the new house
                Console.Write("\nPlease enter the house number you wish to use: ");
                //A try catch block is used to parse the user input as a positive integer, if they dont they are informed they have entered an invalid number
                try {
                    houseNumber = Int32.Parse(Console.ReadLine(), NumberStyles.None);

                    //If the house number is 0 then the user is informed that it must be above 0, if it is above 0 then is checked against the existing house numbers as duplicates arnt allowed
                    if (houseNumber == 0) {
                        Console.WriteLine("The house number must be above 0.");
                    } else if (CheckInUse(houseNumber, "") == false) {
                        break;
                    } else {
                        Console.WriteLine("That house number is already in use.");
                    }
                } catch {
                    Console.WriteLine("That is not a valid house number.");
                }
            }

            return houseNumber;
        }

        private static string GetHouseName() {
            string houseName;

            //This loop is used to repeat the new house name prompt until the user inputs a valid name
            while (true) {
                //The user is prompted to enter a name for the new house
                Console.Write("Please enter the name of the new house: ");
                houseName = Console.ReadLine();

                //The new name is checked against the existing names as duplicates arnt allowed
                if (CheckInUse(0, houseName) == false) {
                    break;
                } else {
                    Console.WriteLine("That house name is already in use.");
                }
            }

            return houseName;
        }

        /// <summary>
        /// This method is used to check if a house name or number is already in use by another house.
        /// If the method is called and given an empty house name then the check is performed on the number, else it checks the name instead.
        /// </summary>
        /// <param name="houseNumber">The house number to check.</param>
        /// <param name="houseName">The house name to check, give "" if you want to check the number instead.</param>
        /// <returns>The method returns a boolean value, true if the name/number is in use or false if not.</returns>
        private static bool CheckInUse(int houseNumber, string houseName) {
            bool exists = false;

            //The list of houses is looped through to see if the desired name or number is already in use, if the name is in use the loops exits and true is returned
            foreach (House house in houses) {
                //If the name given is empty then the number is compared, else the name is compared
                if (houseName == "" && house.HouseNumber == houseNumber) {
                        exists = true;
                        break;
                } else {
                    if (house.HouseName == houseName) {
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
        private static House FindHouse() {
            House tempHouse = null;
            int houseNumber;
            string houseName = "";

            //The user is then prompted to enter the number of the house they wish to remove, the number is checked to ensure its a number and it repeats until a valid number is entered
            while (true) {
                Console.Write("Please enter the house number of the house you wish to select: ");

                try {
                    houseNumber = Int32.Parse(Console.ReadLine(), NumberStyles.None);
                    break;
                } catch {
                    Console.WriteLine("That is not a valid house number.");
                }
            }

            //The list of houses is looped through, the given name or number is then compared to locate the desired house object
            foreach (House house in houses) {
                if (houseName == "" && houseNumber == house.HouseNumber) {
                    tempHouse = house;
                }else if (houseName == house.HouseName) {
                    tempHouse = house;
                }
            }

            //The located house object is then returned, null is returned if no house was found with the name/number.
            return tempHouse;
        }

        /// <summary>
        /// This method is used to remove houses from the list. The user is shown the list of house numbers and names, they are then prompted to enter the number of the house they
        /// wish to remove. Checks are performed on the number entered to ensure it is indeed a number. 
        /// The user is informed if the house could be removed or not.
        /// </summary>
        private static void RemoveHouse() {
            //Local variables to use within the method
            House houseToRemove;

            Console.WriteLine("\nRemoving a house...");
            
            //The user is shown the list of numbers and names of the current houses
            Console.WriteLine("\nHere is the list of the current houses -");

            foreach (House house in houses) {
                Console.WriteLine("\tNumber " + house.HouseNumber + " " + house.HouseName +"\n");
            }

            //The house which they wish to remove is located
            houseToRemove = FindHouse();
            //If the house exists then it is removed and the user is informed it was successful, if it doesnt then the user is told no house could be removed
            if (houseToRemove != null) { 
                houses.Remove(houseToRemove);
                Console.WriteLine("\nThe house was successfully removed.");
            } else {
                Console.WriteLine("\nNo house was found to remove, please try again.");
            }
        }

        /// <summary>
        /// This method loops through the list of houses and displays the contents, each house displays its rooms which in turn display their furniture.
        /// </summary>
        private static void DisplayHouses() {
            if (houses.Count > 0) {
                foreach (House house in houses) {
                    Console.WriteLine("Number " + house.HouseNumber + " " + house.HouseName + "\nThis house contains the following rooms - ");
                    house.DisplayRooms();
                }
            } else {
                Console.WriteLine("\nThere are no houses to display.");
            }
        }

        private static void EditHouses() {
            House houseToEdit;
            string userResponse;

            Console.WriteLine("\nEditing houses...");

            //The user is shown the list of numbers and names of the current houses
            Console.WriteLine("\nHere is the list of the current houses: ");

            foreach (House house in houses) {
                Console.WriteLine("\tNumber " + house.HouseNumber + " " + house.HouseName + "\n");
            }

            houseToEdit = FindHouse();

            if (houseToEdit != null) {

                Console.WriteLine("What would you like to do with this house?\n\t1. Edit the house number.\n\t2. Edit the house name\n\t3. Edit the rooms.");
                Console.Write("Please select an action to perform: ");
                userResponse = Console.ReadLine();

                switch (userResponse) {
                    case "1":
                        int newHouseNumber = GetHouseNumber();
                        houseToEdit.HouseNumber = newHouseNumber;
                        break;

                    case "2":
                        string newHouseName = GetHouseName();
                        houseToEdit.HouseName = newHouseName;
                        break;

                    case "3":
                        break;
                }

            } else {
                Console.WriteLine("No house was found to edit, please try again.");
            }
        }

        /// <summary>
        /// This method converts the list of houses to a json file format, then saves that to a txt document in the project folder.
        /// </summary>
        private static void SaveHouses() {
            Console.WriteLine("\nSaving houses...");

            //The JsonSerialiser converts the list of houses into json data that can then be used to rebuild the list of houses later on
            housesJsonFile = JsonSerializer.Serialize(houses);

            //The try catch block is used to catch a potential error that may occur if someone other than the author runs the code
            try {
                //The file is saved, and if successful then the user is informed of this
                File.WriteAllText(filePath, housesJsonFile);
                Console.WriteLine("The houses were saved.");

            } catch (DirectoryNotFoundException) {
                //This occurs if the path to the file is not valid, this probably is due to the path being hard coded for my personal computer
                Console.WriteLine("Invalid file path, please check that the hard coded file path is written to find your hard drive and documents.");

            } catch {

                Console.WriteLine("There was an error when attempting to save the houses, please try again.");

            }
        }

        /// <summary>
        /// This method is used to load in a list of houses from a specific text file, the list is stored as json data.
        /// </summary>
        private static void LoadHouses() {
            //The whole process is done inside the try catch block in case there is any problems with the file path or the file itself
            try {
                //The json data from the txt file is read into a string
                housesJsonFile = File.ReadAllText(filePath);
                //This is then used to rebuild the list of houses and their contents
                houses = JsonSerializer.Deserialize<List<House>>(housesJsonFile);

            } catch (DirectoryNotFoundException) {
                //This occurs if the path to the file is not valid, this probably is due to the path being hard coded for my personal computer
                Console.WriteLine("Invalid file path, please check that the hard coded file path is written to find your hard drive and documents.");

            } catch (FileNotFoundException) {
                //This occurs if the file does not yet exist, e.g this is the first time running the program
                Console.WriteLine("There is no file containing the houses, try saving some houses to the file and then load them in.");

            } catch {

                Console.WriteLine("There was an error when attempting to load the houses, please try again.");

            }
        }

        /// <summary>
        /// This method is used to give the user the option to save their houses before they end the program.
        /// </summary>
        private static void Exit() {
            bool save;
            //The user is asked if they wish to save their houses
            Console.Write("Would you like to save the list of houses before you end the program? (y/n) - ");
            //The utilities continue loop method is used to process their response
            save = Utilities.ContinueLoop();

            //If they wish to save then the save method is called
            if (save == true) {
                SaveHouses();
            }

            Console.WriteLine("The program will now end.");
        }
    }
}
