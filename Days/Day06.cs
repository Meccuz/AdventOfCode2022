namespace AdventOfCode2022.Days
{
    internal static class Day06
    {
        public static void Run()
        {
            string[] lines = File.ReadAllLines("../../../Assets/Day06.txt");

            string line = lines[0];
            int res1 = Calculate(line, 4);
            int res2 = Calculate(line, 14);

            Console.WriteLine(res1);
            Console.WriteLine(res2);
            Console.ReadLine();
        }

        private static int Calculate(string line, int length)
        {
            for (int i = 0; i < line.Length; i++)
            {
                var current = line.Substring(i, length);
                if (current.Distinct().Count() == length)
                {
                    return i + length;
                }
            }
            throw new Exception();
        }
    }
}
