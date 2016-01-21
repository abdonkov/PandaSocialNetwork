using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PandaSocialNetworkLibrary;

namespace PandaSocialNetworkApplication
{
    public class Program
    {
        public static void Main()
        {
            Panda PandaShrek = new Panda("Shrek","shrek@pandamail.com",Gender.Male);
            Console.WriteLine(PandaShrek.IsFemale == false);
            Console.WriteLine(PandaShrek.PandaEmail == "shrek@pandamail.com");
        }
    }
}
