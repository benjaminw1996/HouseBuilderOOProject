using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuilderOOProject {
    class Furniture {

        private string furnitureName;

        public Furniture() {
            furnitureName = "";
        }

        public Furniture(string newFurnitureName) {
            FurnitureName = newFurnitureName;
        }

        public string FurnitureName {
            get { return furnitureName; }
            set { furnitureName = value; }
        }
    }
}
