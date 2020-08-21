using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuilderOOProject {
    class Furniture {
        //Class attributes
        private string furnitureName;

        /// <summary>
        /// Default initialiser for the class
        /// </summary>
        public Furniture() {
            furnitureName = "";
        }

        /// <summary>
        /// Initialiser for the class that allows values to be input
        /// </summary>
        /// <param name="newFurnitureName"></param>
        public Furniture(string newFurnitureName) {
            FurnitureName = newFurnitureName;
        }

        //Get/Set for the FurnitureName
        public string FurnitureName {
            get { return furnitureName; }
            set { furnitureName = value; }
        }
    }
}
