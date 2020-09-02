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
            m_RoomType = newRoomType;

            m_Furniture = new List<Furniture>();
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
        private void AddFurniture(Furniture newFurniture) {
            if (newFurniture != null) {
                m_Furniture.Add(newFurniture);
            }
        }

        /// <summary>
        /// This method is used to create new furniture to add to the room, the user gives a name for the new furniture which is then created and added to the list.
        /// </summary>
        public void CreateFurniture() {
            Furniture newFurniture;
            string furnitureName;

            //The user is prompted to give the new furniture a name, the users response is then read
            Console.Write("\nPlease enter a name for the new furniture you wish to add to the room: ");
            furnitureName = Console.ReadLine();

            //A new furniture is then created using the given name
            newFurniture = new Furniture(furnitureName);

            //The furniture is added to the list in the room
            AddFurniture(newFurniture);
        }

        /// <summary>
        /// This method loops through the list of furniture and displays the contents to the console app.
        /// </summary>
        public void DisplayFurniture() {
            foreach (Furniture furniture in m_Furniture) {
                Console.WriteLine("\t\t" + furniture.FurnitureName);
            }
        }
    }
}
