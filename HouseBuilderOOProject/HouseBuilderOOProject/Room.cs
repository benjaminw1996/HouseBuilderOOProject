using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuilderOOProject {
    class Room {

        private RoomType m_RoomType;

        private List<Furniture> m_Furniture;

        public Room() {
            M_RoomType = RoomType.none;

            m_Furniture = new List<Furniture>();
        }

        public Room(RoomType newRoomType) {
            roomType = newRoomType;

            furniture = new List<Furniture>();
        }

        public RoomType M_RoomType {
            get { return m_RoomType; }
            set { m_RoomType = value; }
        }

        public List<Furniture> M_Furniture {
            get { return m_Furniture; }
        }

        private addFurniture(Furniture newFurniture) {
            if (newFurniture != null) {
                m_Furniture.Add(newFurniture);
            }
        }
    }
}
