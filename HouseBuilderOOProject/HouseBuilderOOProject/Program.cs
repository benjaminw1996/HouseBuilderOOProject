using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace HouseBuilderOOProject {
    class Program {

        public static List<House> houses = new List<House>();

        static void Main(string[] args) {
            Console.WriteLine("Hello, here's to a new beginning...");

            CreateNewHouse();

            Console.Write("\nPress any key to end program...");
            Console.ReadKey();
        }

        /// <summary>
        /// This method is used to create a new House object,
        /// The user will be prompted for a name and number for the new house.
        /// </summary>
        static private void CreateNewHouse() {
            //Variables for the new house number and name
            int houseNumber = 0;
            string houseName = "";

            //This loop is used to repeat the new house number prompt until the user inputs a valid number
            while (true) {
                //The user is prompted to enter a number for the new house
                Console.Write("\nPlease enter the house number of the new house: ");
                //A try catch block is used to parse the user input as a positive integer, if they dont they are informed they have entered an invalid number
                try {
                    houseNumber = Int32.Parse(Console.ReadLine(), NumberStyles.None);

                    if (houseNumber == 0) {
                        Console.WriteLine("The house number must be above 0.");
                    } else {
                        break;
                    }
                } catch {
                    Console.WriteLine("That is not a valid house number.");
                }
            }
            
            //The user is prompted to enter a name for the new house
            Console.Write("Please enter the name of the new house: ");
            houseName = Console.ReadLine();

            //A new house object is created using the values inputted by the user, this is then added to the list of houses*
            House newHouse = new House(houseNumber, houseName);
            houses.Add(newHouse);
        }
    }
}
