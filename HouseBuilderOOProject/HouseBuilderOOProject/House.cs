using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuilderOOProject {
    class House {
        //Class attributes
        private int houseNumber;
        private string houseName;

        private List<Room> rooms;

        /// <summary>
        /// Default initialiser for the class
        /// </summary>
        public House() {
            HouseNumber = 1;
            HouseName = "";

            rooms = new List<Room>();
        }

        /// <summary>
        /// Initialiser for the class that allows values to be input
        /// </summary>
        /// <param name="newNumber">Value for the house number given by the user</param>
        /// <param name="newName">Value for the house name given by the user</param>
        public House(int newNumber, string newName) {
            HouseNumber = newNumber;
            HouseName = newName;

            rooms = new List<Room>();
        }

        //Get/Set for the HouseNumber
        public int HouseNumber {
            get { return houseNumber; }
            set { houseNumber = value;  }
        }

        //Get/Set for the HouseName
        public string HouseName {
            get { return houseName; }
            set { houseName = value;  }
        }

        //Get for the list of rooms
        public List<Room> Rooms {
            get { return rooms;  }
        }

        //Method for adding rooms to the list
        private void AddRoom(Room newRoom) {
            if (newRoom != null) {
                rooms.Add(newRoom);
            }
        }

        /// <summary>
        /// This method is used to create rooms for the house
        /// The user is asked for the type of room they'd like to create, this is then checked through using a switch case.
        /// </summary>
        public void CreateRoom() {
            //Local variables to use throughout the function
            bool run;
            RoomType roomType;
            Room newRoom = null;

            roomType = GetRoomType();

            //If the room type is valid the room is made, if not the user is informed the room could not be created
            if (roomType != RoomType.none) {
                newRoom = new Room(roomType);

                run = true;
                while (run) {
                    newRoom.CreateFurniture();

                    Console.Write("Would you like to add more furniture to the room? (y/n) ");
                    run = Utilities.ContinueLoop();
                }

            } else {
                Console.WriteLine("New room could not be created, please try again.");
            }

            //If the new room is not null it is added to the list of rooms for the house
            if(newRoom != null) {
                AddRoom(newRoom);
                Console.WriteLine("\nA new " + roomType + " was created and added to the house.\nThis room contains the following furniture -");
                newRoom.DisplayFurniture();
            }
        }

        /// <summary>
        /// This function is used to get a valid room type from the user,
        /// this code was moved to its own function to be reused by the edit rooms function
        /// </summary>
        /// <returns>The room type selected by the user</returns>
        private RoomType GetRoomType() {
            //Local variables to use in the function
            string userReply;
            RoomType roomType = RoomType.none;
            bool run = true;

            //This loop will run until the user gives a valid RoomType for the new room
            while (run) {
                //The user is prompted to select a type for the new room from the given list
                Console.Write("Please select the type of room from this list; 1. Kitchen, 2. Living Room, 3. Bathroom, 4. Bedroom, 5. Dining Room - ");
                userReply = Console.ReadLine();

                //This switch case goes through all the possible options and creates a new room with the selected type
                switch (userReply) {
                    case "1":
                        roomType = RoomType.Kitchen;
                        run = false;
                        break;
                    case "2":
                        roomType = RoomType.LivingRoom;
                        run = false;
                        break;
                    case "3":
                        roomType = RoomType.Bathroom;
                        run = false;
                        break;
                    case "4":
                        roomType = RoomType.Bedroom;
                        run = false;
                        break;
                    case "5":
                        roomType = RoomType.DiningRoom;
                        run = false;
                        break;
                    default:
                        Console.WriteLine("Unknown option selected, please try again.");
                        break;
                }
            }

            //returns the roomtype the user selected
            return roomType;
        }

        /// <summary>
        /// This method is used to edit the rooms of the house, the user will be given some options they can choose from.
        /// These options allow them to change the room type, add or remove furniture to the room or delete the room entirely.
        /// </summary>
        /// <param name="roomIndex">The method is given the index for the room that is going to be edited.</param>
        public void EditRoom(int roomIndex) {
            //Local variables to use in the function
            string userResponse;
            RoomType newRoomType;
            Furniture tempFurniture;
            bool run;

            //The user is shown the room and its contents that they are editing
            Console.WriteLine("You are editing the " + rooms[roomIndex].M_RoomType + "\nThis room contains -");
            rooms[roomIndex].DisplayFurniture();

            //The user is shown the options they can perform and prompted to select one
            Console.WriteLine("\nWhat would you like to edit about the room?\n\t1. Change the room type.\n\t2. Add furniture to the room.\n\t3. Remove furniture from the room\n\t4. Delete the room.");
            userResponse = Console.ReadLine();

            //This switch case is used to process their response
            switch (userResponse) {
                case "1":
                    Console.WriteLine("Editing the room type...");
                    //This option is used to edit the room type, the GetRoomType method is called to get a new room type from the user
                    newRoomType = GetRoomType();
                    rooms[roomIndex].M_RoomType = newRoomType;
                    Console.WriteLine("The room type was changed.");
                    break;

                case "2":
                    Console.WriteLine("Adding furniture to the room...");
                    //This option is used to add furniture to the room
                    run = true;
                    while (run) {
                        rooms[roomIndex].CreateFurniture();

                        Console.Write("Would you like to add more furniture to the room? (y/n) ");
                        run = Utilities.ContinueLoop();
                    }
                    break;

                case "3":
                    Console.WriteLine("Removing furniture...");
                    //This option is used to remove furniture from the room. The user inputs the name of the furniture they want to remove, if it can be found then it is removed
                    tempFurniture = rooms[roomIndex].FindFurniture();
                    if (tempFurniture != null) {
                        rooms[roomIndex].M_Furniture.Remove(tempFurniture);
                        Console.WriteLine("The furniture was removed from the room.");
                    } else {
                        Console.WriteLine("No furniture with that name could be found to remove.");
                    }
                    break;

                case "4":
                    Console.WriteLine("Removing a room...");
                    //This option is used to remove the chosen room from the house
                    rooms.RemoveAt(roomIndex);
                    Console.WriteLine("The selected room was removed.");
                    break;

                default:
                    Console.WriteLine("\nUnknown option selected!\n");
                    break;
            }
        }

        /// <summary>
        /// This method loops through the rooms in the house and displays their room type and then calls the display furniture method of each room
        /// this then displays all the furniture in that room.
        /// </summary>
        public void DisplayRooms() {
            foreach (Room room in rooms) {
                Console.WriteLine("\t1. " + room.M_RoomType + "\n\tThat contains the following furniture - ");
                room.DisplayFurniture();
            }
        }
    }
}
