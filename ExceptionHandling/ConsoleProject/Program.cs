using System;
using StringToIntConverter;

namespace ConsoleProject
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isRunning = true;
            try {
                while (isRunning)
                {
                    Console.WriteLine("Enter the  text:");
                    var sentence = FirstElementGetter.GetFirstElement(Console.ReadLine());
                    Console.WriteLine($"First symbol:{sentence}");
                    Console.WriteLine("*****************************************************************");

                    Console.WriteLine("Continue? Y/N");
                    isRunning = FirstElementGetter.GetAnswer(Console.ReadLine());
                }
            } catch (ArgumentNullException e) {
                Console.WriteLine($"ArgumentNullExcepton: {e.Message}");
            } catch (ArgumentException e) {
                Console.WriteLine($"ArgumentException: {e.Message}");
            }
               
            Console.WriteLine("Press any button...");
            Console.ReadKey(true);
        }
    }
}
