using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace kattis
{
    class Program
    {
        static void Main(string[] args)
        {
            //string line;
            //List<string> strings = new List<string>();
            //while ((line = Console.ReadLine()) != null)
            //{
            //    strings.Add(line);
            //}
            //string[] nums = strings.Split(' ');
            List<string> nums = new List<string>();
            using (StreamReader sr = new StreamReader(@"C:\Users\pogo\documents\visual studio 2017\Projects\kattis\TextFile2.txt"))
            {

                string line;
                // Read and display lines from the file until the end of 
                // the file is reached.
                while ((line = sr.ReadLine()) != null)
                {
                    nums.Add(line);
                }
            }
            nums.Sort();

            // dictionary med en int key som svarer til den pågældende guards nummer(#). Valuen er en liste af 60 intergers. en for hvert minut mellem 00:00 og 00:59.
            Dictionary<int, List<int>> guards = new Dictionary<int, List<int>>();
            foreach (string item in nums)
            {
                Console.WriteLine(item);
            }
            int currGuard = new int();
            int min = new int();

            foreach (string item in nums)
            {
                // hvis en guard starter en vagt for første gang bliver han føjet til guard dictionary og currGuard bliver sat til den pågældene guard nummer(#).
                if (item[19].ToString() == "G")
                {
                    string[] substringy = item.Substring(26).Split(' ');
                    int guardNumber = int.Parse(substringy[0]);

                    if (!guards.ContainsKey(guardNumber))
                    {
                        List<int> schedule = new List<int>();
                        for (int i = 0; i < 60; i++)
                        {
                            schedule.Add(0);
                        }
                        guards.Add(guardNumber, schedule);
                    }

                    currGuard = guardNumber;
                }
                //sætter det minut guarden falder i søvn
                else if (item[19].ToString() == "f")
                {

                    min = int.Parse(item.Substring(15, 2));

                }

                else if (item[19].ToString() == "w")
                {
                    int fellAsleep = min;
                    int wokenUp = int.Parse(item.Substring(15, 2));
                    int minSlept = wokenUp - min;
                    for (int i = 0; i < minSlept; i++)
                    {
                        guards[currGuard][fellAsleep] += 1;
                        fellAsleep++;
                    }
                }
            }
            // finder den guard der sover mest
            int sleepyGuard = 0;
            int mostsleeps = 0;
            foreach (KeyValuePair<int, List<int>> item in guards)
            {

                int guardsHours = 0;
                foreach (int item2 in item.Value)
                {
                    guardsHours += item2;
                }
                if (guardsHours > mostsleeps)
                {
                    mostsleeps = guardsHours;
                    sleepyGuard = item.Key;
                }
            }

            // finder det minut 
            int sleepyMin = 0;
            int sleepyIndex = 0;
            foreach (int item in guards[sleepyGuard])
            {
                if (item > sleepyMin)
                {
                    sleepyMin = item;
                    sleepyIndex = guards[sleepyGuard].IndexOf(item);
                }
            }

            //finder den guard der har sovet mest på sammme minut og hvilket minut det er.
            int maxSleepOnMin = 0;
            int guard = 0;
            int minSleptMost = 0;
            foreach (KeyValuePair<int, List<int>> item in guards)
            {

                int guardsHours = 0;
                foreach (int item2 in item.Value)
                {
                    if (item2 > guardsHours)
                    {
                        guardsHours = item2;
                    }
                }
                if (guardsHours > maxSleepOnMin)
                {
                    maxSleepOnMin = guardsHours;
                    guard = item.Key;
                    minSleptMost = guards[guard].IndexOf(guardsHours);
                }
            }
            Console.WriteLine((sleepyGuard * sleepyIndex));
            Console.WriteLine((guard * minSleptMost));
            Console.ReadKey();
        }


    }
}

