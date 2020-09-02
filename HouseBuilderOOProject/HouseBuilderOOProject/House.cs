﻿using System;
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
            bool run = true;
            string userReply;
            RoomType roomType = RoomType.none;
            Room newRoom = null;

            //This loop will run until the user gives a valid RoomType for the new room
            while (run) {
                //The user is prompted to select a type for the new room from the given list
                Console.Write("Please select a room type for the new room; 1. Kitchen, 2. Living Room, 3. Bathroom, 4. Bedroom, 5. Dining Room - ");
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
        /// This method loops through the rooms in the house and displays their room type and then calls the display furniture method of each room
        /// this then displays all the furniture in that room.
        /// </summary>
        public void DisplayRooms() {
            foreach (Room room in rooms) {
                Console.WriteLine("\t" + room.M_RoomType + "\n\tThat contains the following furniture - ");
                room.DisplayFurniture();
            }
        }
    }
}
