using System.Diagnostics;

namespace AdventOfCode2022.Days
{
    internal static class Day03
    {
        public static void Run()
        {
            Stopwatch sw = new Stopwatch();
            string[] lines = File.ReadAllLines("../../../Assets/Day03.txt");
            sw.Start();

            var sum1 = 0;
            var sum2 = 0;
            var backpacks = new List<string>();
            foreach (var line in lines)
            {
                char common = GetCompartmentsCommonItem(line);
                sum1 += GetPriority(common);

                backpacks.Add(line);
                if (backpacks.Count == 3)
                {
                    common = backpacks[0].Intersect(backpacks[1]).Intersect(backpacks[2]).First();
                    sum2 += GetPriority(common);
                    backpacks.Clear();
                }
            }

            Console.WriteLine(sum1);
            Console.WriteLine(sum2);
            sw.Stop();
            Console.WriteLine($"Execution time: {sw.ElapsedMilliseconds}");
            Console.ReadLine();
        }

        private static char GetCompartmentsCommonItem(string line)
        {
            var half = line.Length / 2;
            var c1 = line.Substring(0, half);
            var c2 = line.Substring(half);
            var common = c1.Intersect(c2).First();
            return common;
        }

        private static int GetPriority(char common)
        {
            if (char.IsUpper(common))
            {
                return common - 'A' + 27;
            }
            else
            {
                return common - 'a' + 1;
            }
        }
    }
}
