using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace MarsRover
{
    class Program
    {
        private static readonly Regex PlateauCheckInput = new Regex(@"([LR][0-9]$)");
        private static string[] Split(string str, int chunkSize)
        {
            return Enumerable.Range(0, str.Length / chunkSize)
                .Select(i => str.Substring(i * chunkSize, chunkSize)).ToArray();
        }

        static void Main(string[] args)
        {
            Plateau p = new Plateau(40, 30);

            // Initialise rover and set position
            Rover r = new Rover(p);
            r.SetPosition(10, 10, "N");

            var input = "";
            while (input != null)
            {
                // display rover current position
                var names = r.CurrentPosition();
                Console.WriteLine($"\nCurrent Position {names.Item1} {names.Item2} Direction {names.Item3}");

                // accept rovercommand line input 
                Console.Write("Input command (blank to exit): ");
                input = Console.ReadLine();

                if (String.IsNullOrEmpty(input)) return;

                // parse rover input commands
                var output = ParseInput(input);

                if (output != null)
                {
                    foreach (var word in output)
                    {
                        // move rover according to command
                        if (!r.MoveRover(word[0],
                                    word[1] - '0'))
                        {
                            Console.WriteLine("Rover fell off plateau.");
                            return;
                        }
                    }
                }
            }
        }

        // method to parse and validate rover input commands
        // returns a string array of rover commands
        private static string[] ParseInput(string input)
        {
            if (input.Length == 0)
            {
                Console.WriteLine("Empty string");
                return null;
            }

            using (var reader = new StringReader(input))
            {
                if (PlateauCheckInput.Match(input).Success)
                {
                    string[] words = Split(input, 2);

                    return words;
                }
                else
                {
                    Console.WriteLine("Input data is in wrong format \n");
                }

                return null;
            }
        }
    }
}
