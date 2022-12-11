using System.Text.RegularExpressions;

namespace AdventOfCode2022.Days
{
    internal static class Day04
    {
        public static void Run()
        {
            string[] lines = File.ReadAllLines("../../../Assets/Day04.txt");

            var regex = new Regex(@"(\d*)-(\d*),(\d*)-(\d*)");
            var res1 = 0;
            var res2 = 0;
            foreach (string line in lines)
            {
                var match = regex.Match(line);
                int l1 = int.Parse(match.Groups[1].Value);
                int u1 = int.Parse(match.Groups[2].Value);
                int l2 = int.Parse(match.Groups[3].Value);
                int u2 = int.Parse(match.Groups[4].Value);
                res1 += RangeFullyOverlapping(l1, u1, l2, u2) ? 1 : 0;
                res2 += RangeOverlapping(l1, u1, l2, u2) ? 1 : 0;
            }

            Console.WriteLine(res1);
            Console.WriteLine(res2);
            Console.ReadLine();
        }

        private static bool RangeOverlapping(int l1, int u1, int l2, int u2)
        {
            return (l1 <= u2 && u1 >= l2) || (l1 >= u2 && u1 <= l2);
        }

        private static bool RangeFullyOverlapping(int l1, int u1, int l2, int u2)
        {
            return (l1 >= l2 && u1 <= u2) || (l1 <= l2 && u1 >= u2);
        }
    }
}
