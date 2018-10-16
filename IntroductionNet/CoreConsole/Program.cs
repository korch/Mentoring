using System.Collections.Generic;
using System.Linq;
using Helper;
using HelperProject;
using static System.Console;

namespace CoreConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var helper = new GreetingHelper();

            WriteLine("Enter the name:");
            var name = ReadLine();
            
            Loop(helper, name);
        }

        private static void Loop(GreetingHelper helper, string name)
        {
            var isRunning = true;
            while (isRunning) {
                WriteLine("***********************************************************");
                WriteLine("Choose languge to say hello: ");
                WriteLine("1. Default (English - Hello)");
                WriteLine("2. Other language");
                var choice = int.Parse(ReadLine());

                if (choice == 1) {
                    WriteLine(helper.Greeting(name));
                } else {
                    ChooseCountry(helper, name);
                }
                  
                WriteLine();
                WriteLine("Wanna leave the program? 1 - Yes, 2 - No");
                var leave = int.Parse(ReadLine());
                if (leave == 1)
                    isRunning = false;
            }
        }

        private static void ChooseCountry(GreetingHelper helper, string name)
        {
            WriteLine("Choose country:");
            var index = 1;
            var helloEntries = helper.HelloWords.ToList();
            helloEntries.ForEach(h => {
                WriteLine($"{index}. {h.Key}");
                index++;
            });

            var choice = int.Parse(ReadLine());
            if (choice > 0 && choice <= helloEntries.Count) {
                helper.ChooseHelloType(helloEntries[choice - 1].Key);
                WriteLine(helper.Greeting(name));
            } else {
                WriteLine("Incorrect choise");
            }
        }
    }
}
