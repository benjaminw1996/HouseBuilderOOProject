using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuilderOOProject {
    class House {

        private int houseNumber;
        private string houseName;

        private List<Room> rooms;

        public House() {
            HouseNumber = 1;
            HouseName = "";

            rooms = new List<Room>();
        }

        public House(int newNumber, string newName) {
            HouseNumber = newNumber;
            HouseName = newName;

            rooms = new List<Room>();
        }

        public int HouseNumber {
            get { return houseNumber; }
            set { houseNumber = value;  }
        }

        public string HouseName {
            get { return houseName; }
            set { houseName = value;  }
        }

        public List<Room> Rooms {
            get { return rooms;  }
        }

        private addRoom(Room newRoom) {
            if (newRoom != null) {
                rooms.Add(newRoom);
            }
        }
    }
}
