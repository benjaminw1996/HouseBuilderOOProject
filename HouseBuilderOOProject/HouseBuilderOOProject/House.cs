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
        private addRoom(Room newRoom) {
            if (newRoom != null) {
                rooms.Add(newRoom);
            }
        }
    }
}
