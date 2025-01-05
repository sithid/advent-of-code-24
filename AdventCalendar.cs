using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    public class AdventCalendar
    {
        private int day;

        public void Run()
        {
            string input = string.Empty;

            Console.WriteLine("***************************************");
            Console.WriteLine("* Advent Of Code, 2024!");
            Console.WriteLine("* Puzzle solutions by Day:");
            Console.WriteLine("* Day 1: Historian Hysteria");
            Console.WriteLine("* Day 2: Red-Nosed Reports");
            Console.WriteLine("* Day 3: Mull It Over");
            Console.WriteLine("* Day 4: Ceres Search");
            Console.WriteLine("* Day 5: Print Queue");
            Console.WriteLine("*");
            Console.WriteLine("* q: Quit");
            Console.Write("* Run solution for which day? ");
            
            input = Console.ReadLine();

            while ( !string.Equals(input, "q"))
            {
                Console.WriteLine("***************************************\n\r");

                if (int.TryParse(input, out day))
                {
                    switch (day)
                    {
                        case 1: DayOne(); Console.Clear(); break;
                        default: NotImplemented(); Console.Clear(); break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid selection.");
                    Console.ReadKey();
                    Console.Clear();
                }

                Console.WriteLine("***************************************");
                Console.WriteLine("* Advent Of Code, 2024!");
                Console.WriteLine("* Puzzle solutions by Day:");
                Console.WriteLine("* Day 1: Historian Hysteria");
                Console.WriteLine("* Day 2: Red-Nosed Reports");
                Console.WriteLine("* Day 3: Mull It Over");
                Console.WriteLine("* Day 4: Ceres Search");
                Console.WriteLine("* Day 5: Print Queue");
                Console.WriteLine("*");
                Console.WriteLine("* q: Quit");
                Console.Write("* Run solution for which day? ");

                input = Console.ReadLine();
            }
        }

        private void DayOne()
        {
            DayOnePartOne();
        }
        private void DayOnePartOne()
        {
            string fullPath = AppDomain.CurrentDomain.BaseDirectory + "/Assets/Day1/input.txt";

            if (File.Exists(fullPath))
            {
                Console.WriteLine("Day 1: Historian Hysteria");
                Console.WriteLine("Day 1: Part One");
                Console.WriteLine("Find the distance between the locations from 'input.txt'.");

                List<int> numbersX = new List<int>();
                List<int> numbersY = new List<int>();
                List<int> absXY = new List<int>();

                string[] lines = File.ReadAllLines(fullPath);

                foreach (string line in lines)
                {
                    string[] parts = line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                    numbersX.Add(int.Parse(parts[0]));
                    numbersY.Add(int.Parse(parts[1]));
                }

                numbersX.Sort();
                numbersY.Sort();

                if ( numbersX.Count != numbersY.Count )
                {
                    Console.WriteLine("An error occured while parsing the input data: " +
                                      "There should be exactly the same number of X locations as Y locations.");
                }

                for( int i = 0; i < numbersX.Count; i++)
                {
                    absXY.Add(Math.Abs(numbersX[i] - numbersY[i]));
                }

                Console.WriteLine("List X and Y parsed from ./Assets/Day1/input.txt");

                int distance = 0;

                for( int i = 0; i < absXY.Count; i++ )
                {
                    distance += absXY[i];
                }

                Console.WriteLine($"Total Distance: {distance}");
                Console.WriteLine("Press any key to continue to part two.");
                Console.ReadKey();

                DayOnePartTwo( numbersX, numbersY );
            }
        }

        private void DayOnePartTwo( List<int> numbersX, List<int> numbersY )
        {
            Console.WriteLine("Day 1: Historian Hysteria");
            Console.WriteLine("Day 1: Part Two");
            Console.WriteLine("What is the similarity score of the left and right lists?");

            Dictionary<int, int> similarityScores = new Dictionary<int, int>(); // dictionary<number, score>

            for( int x = 0;x < numbersX.Count; x++ )
            {
                int numX = numbersX[x];
                int found = 0;

                for(int y = 0; y < numbersY.Count; y++)
                {
                    if (numX == numbersY[y])
                        ++found;
                }

                similarityScores.Add(numX, numX * found);
            }

            int similarityScoreTotal = 0;

            foreach( int simScore in similarityScores.Values )
            {
                similarityScoreTotal += simScore;
            }

            Console.WriteLine($"The similarity score of the two lists is: {similarityScoreTotal}");
            Console.WriteLine("Press any key to return to the advent menu.");
            Console.ReadKey();
        }

        private void NotImplemented()
        {
            Console.WriteLine("The solution to that puzzle has not been implemented.");
            Console.ReadKey();
        }
    }
}
