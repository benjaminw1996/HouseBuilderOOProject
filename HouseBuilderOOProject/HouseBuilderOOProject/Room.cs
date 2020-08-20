using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuilderOOProject {
    class Room {

        public RoomType roomType;

        public List<Furniture> furniture;

        public Room() {
            roomType = RoomType.none;

            furniture = new List<Furniture>();
        }

        public Room(RoomType newRoomType) {
            roomType = newRoomType;

            furniture = new List<Furniture>();
        }
    }
}
