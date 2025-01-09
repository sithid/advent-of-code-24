using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
                        case 2: DayTwo(); Console.Clear(); break;
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
                
                Console.WriteLine();
                Console.WriteLine("Input data from ./Assets/Day1/input.txt:");
                Console.WriteLine();

                List<int> numbersX = new List<int>();
                List<int> numbersY = new List<int>();
                List<int> absXY = new List<int>();
                int distance = 0;

                string[] lines = File.ReadAllLines(fullPath);

                foreach (string line in lines)
                {
                    Console.WriteLine(line);

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

                for( int i = 0; i < absXY.Count; i++ )
                {
                    distance += absXY[i];
                }

                Console.WriteLine($"Total Distance: {distance}");
                Console.WriteLine();
                Console.ReadKey();

                DayOnePartTwo( numbersX, numbersY );
            }
        }

        private void DayOnePartTwo( List<int> numbersX, List<int> numbersY )
        {
            Console.WriteLine("Day 1: Historian Hysteria");
            Console.WriteLine("Day 1: Part Two");
            Console.WriteLine("What is the similarity score of the left and right lists from part one?");
            Console.WriteLine();

            Console.WriteLine();
            Console.WriteLine("Simularity data from the previous lists:");
            Console.WriteLine();

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

            Console.WriteLine("All unlisted numbers have a similarity score of 0 and will not affect the similarity score.");

            foreach ( KeyValuePair<int,int> kvp in similarityScores )
            {
                if (kvp.Value == 0)
                    continue;
                else
                    Console.WriteLine($"Localtion: {kvp.Key}, Similarity Score: {kvp.Value}");
            }

            int similarityScoreTotal = 0;

            foreach( int simScore in similarityScores.Values )
            {
                similarityScoreTotal += simScore;
            }

            Console.WriteLine($"The similarity score of the two lists is: {similarityScoreTotal}");
            Console.WriteLine("Press any key to continue to the advent menu.");
            Console.WriteLine();
            Console.ReadKey();
        }

        private void DayTwo()
        {
            DayTwoPartOne();
        }

        private void DayTwoPartOne()
        {
            Console.WriteLine("Day 2: Historian Hysteria");
            Console.WriteLine("Day 2: Part Two");
            Console.WriteLine("Analyze the unusual data from the engineers found in 'input.txt'. How many reports are safe?");
            
            Console.WriteLine();
            Console.WriteLine("Input data from ./Assets/Day2/input.txt:");
            Console.WriteLine();


            List<int[]> reports = new List<int[]>();

            int safeReports = 0;
            string fullPath = AppDomain.CurrentDomain.BaseDirectory + "/Assets/Day2/input.txt";

            if (File.Exists(fullPath))
            {
                string[] lines = File.ReadAllLines(fullPath);

                foreach (string line in lines)
                {
                    string[] parts = line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                    int[] nums = new int[parts.Length];

                    for( int i = 0; i < parts.Length; i++ )
                    {
                        nums[i] = int.Parse(parts[i]);
                    }

                    reports.Add(nums);
                }

                foreach (int[] arr in reports )
                {
                    for( int i = 0; i < arr.Length; i++ )
                    {

                        Console.Write($"{arr[i]} ");
                    }

                    Console.WriteLine();
                }

                foreach (int[] arr in reports)
                {
                    bool invalidReport = true;

                    if (!IsConsistent(arr))
                    {
                        Console.WriteLine("InConsistent report!");
                        continue;
                    }

                    // offset start and stop position such that start is one right of first index and stop is one left of last index.
                    for (int i = 1; i < arr.Length-1; i++)
                    {
                        int absX, absY;

                        absX = Math.Abs(arr[i] - arr[i - 1]);
                        absY = Math.Abs(arr[i] - arr[i + 1]);

                        if( absX >= 1 && absX <= 3 )
                        {
                            if ((arr[i] > arr[i - 1] && arr[i] < arr[i + 1]) || arr[i] < arr[i - 1] && arr[i] > arr[i + 1] )
                            {
                                if (absY >= 1 && absY <= 3)
                                {
                                    invalidReport = false;
                                }
                            }
                        }

                        if( !invalidReport )
                        {
                            safeReports += 1;
                        }
                    }
                }

                Console.WriteLine($"Total number of reports found 'safe': {safeReports}");
                Console.WriteLine("Press any key to continue to the advent menu.");
                Console.WriteLine();
                Console.ReadKey();
            }
        }

        private bool IsConsistent(int[] arr)
        {
            int last = 0;
            int current = 0;
            int next = 0;

            if (arr[0] > arr[1])
            {
                for (int i = 1; i < arr.Length - 1; i++)
                {
                    last = arr[i - 1];
                    current = arr[i];
                    next = arr[i + 1];

                    if (last > current && current > next)
                    {
                        continue;
                    }
                    else
                        return false;
                }
            }
            else if (arr[0] < arr[1])
            {
                for (int i = 1; i < arr.Length - 1; i++)
                {
                    last = arr[i - 1];
                    current = arr[i];
                    next = arr[i + 1];

                    if (last < current && current < next)
                    {
                        continue;
                    }
                    else
                        return false;
                }
            }


            return true;
        }
        private void DayTwoPartTwo()
        {
        }

        private void NotImplemented()
        {
            Console.WriteLine("The solution to that puzzle has not been implemented.");
            Console.WriteLine();
            Console.ReadKey();
        }
    }
}
