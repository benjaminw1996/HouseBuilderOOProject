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
                Console.WriteLine("\nHere are the actions you can perform - \n\t1. Create a new house.\n\t2. Remove a house.\n\t3. Edit houses.\n\t4. Display Houses.\n\t5. Save the houses.\n\t6. End the program.");
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
                        EditHouses();
                        break;

                    case "4":
                        DisplayHouses();
                        break;

                    case "5":
                        SaveHouses();
                        break;

                    case "6":
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

            houseNumber = InputHouseNumber();

            houseName = InputHouseName();

            //A new house object is created using the values inputted by the user, this is then added to the list of houses*
            House newHouse = new House(houseNumber, houseName);

            while (creatingRooms) {
                newHouse.CreateRoom();

                Console.Write("\nWould you like to make another room? (y/n) ");
                creatingRooms = Utilities.ContinueLoop(); ;
            }

            houses.Add(newHouse);
        }

        private static int InputHouseNumber() { 
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

        private static string InputHouseName() { 
            string houseName;

            //This loop is used to repeat the new house name prompt until the user inputs a valid name
            while (true) {
                //The user is prompted to enter a name for the new house
                Console.Write("\nPlease enter the name of the new house: ");
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

        /// <summary>
        /// This function allows the user to edit houses. They are given the option to add or delete new houses, or then edit existing ones.
        /// The user is also given the option to edit rooms within the house and edit each rooms furniture, this is handled by a function within the house class.
        /// </summary>
        private static void EditHouses() {
            //Local variables to use within the function
            House houseToEdit;
            string userResponse;
            int roomNumber;
            bool editing = true;
            bool editingRooms = true;

            Console.WriteLine("\nEditing houses...");

            //The user is shown the list of numbers and names of the current houses
            Console.WriteLine("\nHere is the list of the current houses: ");

            foreach (House house in houses) {
                Console.WriteLine("\tNumber " + house.HouseNumber + " " + house.HouseName + "\n");
            }

            //The house they wish to edit is found using the FindHouse method
            houseToEdit = FindHouse();

            //If a house has been found then the code to edit it is run, if not the user is informed a house could not be found
            if (houseToEdit != null) {
                //The user is shown the options they have when editing the house
                Console.WriteLine("\nWhat would you like to edit about Number " + houseToEdit.HouseNumber + " " + houseToEdit.HouseName + "?\n\t1. The house number.\n\t2. The house name\n\t3. The rooms.");
                
                //This loop allows the user to keep editing a house without having to go back through from the main menu
                while (editing) {
                    //The user is promted to select an action to perform
                    Console.Write("\nPlease select an action to perform: ");
                    userResponse = Console.ReadLine();

                    //This switch case is used to process their response
                    switch (userResponse) {
                        case "1":
                            Console.WriteLine("\nEditing the house number...");
                            //This option is used to edit the house number, the user is prompted to enter a valid house number using the GetHouseNumber function
                            int newHouseNumber = InputHouseNumber();
                            houseToEdit.HouseNumber = newHouseNumber;
                            Console.WriteLine("The house number has been edited.");
                            //The user is then asked if they wish to continue editing the house, if no then the loop ends
                            Console.Write("Would you like to continue editing this house? (y/n) - ");
                            //The ContinueLoop function in the utilities class is used to process the users response and return a bool
                            editing = Utilities.ContinueLoop();
                            break;

                        case "2":
                            Console.WriteLine("\nEditing the house name...");
                            //This option is used to edit the house name, the user is prompted to enter a valid house name using the GetHouseName function
                            string newHouseName = InputHouseName();
                            houseToEdit.HouseName = newHouseName;
                            Console.WriteLine("The house name has been edited.");
                            //The user is then asked if they wish to continue editing the house, if no then the loop ends
                            Console.Write("Would you like to continue editing this house? (y/n) - ");
                            //The ContinueLoop function in the utilities class is used to process the users response and return a bool
                            editing = Utilities.ContinueLoop();
                            break;

                        case "3":
                            Console.WriteLine("\nEditing the rooms...");
                            //This option is used to edit the rooms, the first loop is used to allow the user to continually edit rooms without having to exit each time
                            while (editingRooms) {
                                //The list of current rooms within the house is displayed to the user
                                houseToEdit.DisplayRooms();

                                //The initial option is to add a new room to the house, if they select yes then the CreateRoom function within the house is called
                                Console.Write("Would you like to add a new room? (y/n) - ");
                                if (Utilities.ContinueLoop()) {
                                    houseToEdit.CreateRoom();
                                } else {
                                    //If they said no then they are asked to select the room they wish to edit
                                    while (true) {
                                        Console.Write("Please enter the number of the room you wish to edit: ");
                                        //This try catch is used to make sure the user enters a valid integer number for the desired room
                                        try {
                                            roomNumber = Int32.Parse(Console.ReadLine(), NumberStyles.None);
                                            break;
                                        } catch {
                                            Console.WriteLine("That is not a valid number.");
                                        }
                                    }

                                    //This if statement is used to make sure the number entered is within the constraints of the list
                                    if (roomNumber > 0 || roomNumber < houseToEdit.Rooms.Count + 1) {
                                        //The edit room function within the house is then called allowing the user to edit the specific house
                                        roomNumber --;
                                        houseToEdit.EditRoom(roomNumber);

                                    } else {
                                        //If the number was not within the size of the list then the user is informed that no room exists with that number
                                        Console.WriteLine("No room with that number exists.");
                                    }
                                }

                                //The user is asked if they wish to edit or create another room, if not then the loop ends
                                Console.Write("Would you like to edit or create another room? (y/n) - ");
                                editingRooms = Utilities.ContinueLoop();
                            }
                            break;

                        default:
                            //If an unkown option was slected then the user is informed of this 
                            Console.WriteLine("\nUnknown option selected!\n");
                            break;
                    }
                }

            } else {
                //If the house the user searched for to edit is not found, they are informed and asked to try again
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
