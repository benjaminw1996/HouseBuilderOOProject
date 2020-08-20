using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuilderOOProject {
    class House {

        public int houseNumber;
        public string houseName;

        public List<Room> rooms;

        public House() {
            houseNumber = 1;
            houseName = "";

            rooms = new List<Room>();
        }

        public House(int newNumber, string newName) {
            houseNumber = newNumber;
            houseName = newName;

            rooms = new List<Room>();
        }

    }
}
