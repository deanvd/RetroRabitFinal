using System;
using System.IO;
using System.Reflection;

namespace RetroRabbit
{
    class Program
    {

        //could add this to app config but not needed for this
        private static readonly string Location = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\RetroRabitDictResult.txt";
        static void Main()
        {
            //Instansiate the helper class
            var csHelper = new Helper();
            //Lets generate a list with values for the search
            csHelper.GenerateList(Location);
            //Ok now we have a the csv with values lets split the csv to a list and pass it on to the search and see if we get a match
            if (csHelper.SearchList(csHelper.SplitFile(Location), csHelper.Searchinput()))
            {
                csHelper.RabbitImage();
            }
            //If we fail to find a match5
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Can't find the Rabbit.\n\nDone !\nPress any key to exit.");
            }

                Console.ReadLine();
                Environment.Exit(0);
        }
    }
}
