using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuilderOOProject {
    class Room {
        //Class attributes
        private RoomType m_RoomType;

        private List<Furniture> m_Furniture;

        /// <summary>
        /// Default initialiser for the class
        /// </summary>
        public Room() {
            M_RoomType = RoomType.none;

            m_Furniture = new List<Furniture>();
        }

        /// <summary>
        /// Initialiser for the class that allows values to be input
        /// </summary>
        /// <param name="newRoomType">Value for the house number given by the user</param>
        public Room(RoomType newRoomType) {
            roomType = newRoomType;

            furniture = new List<Furniture>();
        }

        //Get/Set for the RoomType
        public RoomType M_RoomType {
            get { return m_RoomType; }
            set { m_RoomType = value; }
        }

        //Get/Set for the Furniture list
        public List<Furniture> M_Furniture {
            get { return m_Furniture; }
        }

        //Method for adding furniture to the list
        private addFurniture(Furniture newFurniture) {
            if (newFurniture != null) {
                m_Furniture.Add(newFurniture);
            }
        }
    }
}
