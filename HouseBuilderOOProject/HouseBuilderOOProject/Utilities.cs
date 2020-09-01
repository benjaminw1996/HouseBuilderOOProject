using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseBuilderOOProject {
    public static class Utilities {

        public static bool ContinueLoop() {
            bool keepLooping = true;
            //This string holds the users response to the given question
            string userResponse = Console.ReadLine().ToLower();

            //These if/else statements look at the users response to determine if they wish to continue or not with the loop
            if (userResponse == "n" || userResponse == "no") {
                keepLooping = false;
            } else if (userResponse == "y" || userResponse == "yes") {
                keepLooping = true;
            } else {
                Console.WriteLine("Answer was not recognised...program will exit.");
                keepLooping = false;
            }

            //The result of the if/esle statements is returned
            return keepLooping;
        }
    }
}
