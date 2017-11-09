using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace RetroRabbit
{
    class Helper
    {

        //out list fot the individual strings in the csv
        private static List<string> rabitlist = new List<string>();
        
        //Generate RabitList of strings for the search
        public void GenerateList(string location)
        {
            List<string> retroDict = new List<string>();
            Random randomchar = new Random();
            //charecters to use during file generation
            var alphabet = new[]
            {
                "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U",
                "V", "W", "X", "Y", "Z"
            };

            try
            {
                //Lets Cleanup for new file generation
                if (File.Exists(location))
                    File.Delete(location);

                //shuffel alphabet and generate string for csv 
                for (var x = 0; x < 50; x++)
                {
                    var randomSelection = (from c in alphabet orderby randomchar.Next() select c).Take(5);
                    var dictEntryList = randomSelection.ToList();
                    retroDict.Add(dictEntryList[0] + dictEntryList[1] + dictEntryList[2] + dictEntryList[3] +
                                  dictEntryList[4]);
                }


                //combine and format for CSV from alphabet strings
                var csv = new StringBuilder();
                foreach (var value in retroDict.Distinct())
                {
                    var record = value;
                    var delimiter = ",";
                    var newRecord = string.Format("{0}", record);
                    csv.AppendLine(string.Concat(newRecord, delimiter));

                }
                File.WriteAllText(location, csv.ToString().Substring(0, csv.Length - 3));
                //Open generated csv in Notepad
                var openGeneratedDict = new ProcessStartInfo(location)
                {
                    WindowStyle = ProcessWindowStyle.Maximized,
                    Arguments = location
                };
                Process.Start(openGeneratedDict);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                Console.ReadLine();
            }
        }


        //Lets get the generated text file and split it to a list
        public List<string> SplitFile(string location)
        {
            string line;
            try
            {
                using (var sr = new StreamReader(location))
                {
                    while (!sr.EndOfStream)
                    {
                        line = sr.ReadLine();
                        if (line != null)
                            foreach (var s in line.Split(','))
                            {
                                if (!string.IsNullOrEmpty(s))
                                {
                                    rabitlist.Add(s);
                                }
                            }
                        // Console.WriteLine(line);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                Console.ReadLine();
            }


            return rabitlist;
        }

        //we have a list with values in csv now lets find a match
        public bool SearchList(List<string> seperatedlist, string input)
        {
            try
            {
               
                foreach (string x in seperatedlist)
                {

                    var source = input.ToLookup(inputitem => inputitem);
                    var dest = x.ToLookup(lineItem => lineItem);
                    var output = source.SelectMany(i => i.Take(i.Count() - dest[i.Key].Count())).ToArray();
                    //Check input length if not 5 exit
                    if (input.Length != 5)
                    {
                        
                        return false; 
                    }
                    if (output.Length == 0)
                    {

                       
                        return true;

                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return false;

        }
        //Get the value enterd in the console
        public  string Searchinput()
        {

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("Use one of the Carrots(string) in the bucket and to throw it in the Rabbit Hole! \nLet's see if we can find the Rabbit !!! \n\n");
            Console.Write("Please enter 5 Characters to search\nRabit Hole :  ");
            Console.ForegroundColor = ConsoleColor.White;
            var search = Console.ReadLine() ?? "";
            return search.ToUpper();
        }

        //Success ASCCI
        public void RabbitImage()
        {
            string rabbit =
                @"  ** **" + "\n" +
                @" * *   **" + "\n" +
                @" **   **         ****" + "\n" +
                @" * *   **       **   ****" + "\n" +
                @" **  **       *   **   **" + "\n" +
                @" * *  *      *  **  ***  **" + "\n" +
                @"   **  *    *  **     **  *" + "\n" +
                @"   * * **  ** **        **" + "\n" +
                @"    **   **  **" + "\n" +
                @"   *           *" + "\n" +
                @"  *             *" + "\n" +
                @" *    0     0    *" + "\n" +
                @" *   /   @   \   *" + "\n" +
                @" *   \__/ \__/   *" + "\n" +
                @"   *     W     *" + "\n" +
                @"    * *     **" + "\n" +
                @"       *****" + "\n";

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(rabbit);
            Console.WriteLine("\nYou found the Rabbit!!!\n\n\nDone !\nPress any key to exit.");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
