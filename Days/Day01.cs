namespace AdventOfCode2022.Days
{
    internal static class Day01
    {
        public static void Run()
        {
            string[] lines = File.ReadAllLines("../../../Assets/Day01.txt");
            int current = 0;
            int max = 0;
            int[] top3 = new int[3] { 0, 0, 0 };
            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line))
                {
                    if (current > max)
                    {
                        max = current;
                    }
                    if (current > top3.Min())
                    {
                        top3[Array.IndexOf(top3, top3.Min())] = current;
                    }

                    current = 0;
                    continue;
                }
                current += int.Parse(line);
            }

            Console.WriteLine($"Max: {max}");
            Console.WriteLine($"Top 3: {top3.Sum()}");
            Console.ReadLine();
        }
    }
}
