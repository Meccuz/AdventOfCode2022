namespace AdventOfCode2022.Days
{
    internal static class Day10
    {
        public static void Run()
        {
            string[] lines = File.ReadAllLines("../../../Assets/Day10.txt");

            var currentVal = 1;
            var res1 = 0;
            var calcRes1 = (int i) =>
            {
                if ((i - 20) % 40 == 0)
                {
                    res1 += (i * currentVal);
                }
                //Console.WriteLine($"{i + 1}: {currentVal} ({res1})");

                var tmp = (i - 1) % 40;
                if (currentVal == tmp || currentVal == tmp - 1 || currentVal == tmp + 1)
                {
                    Console.Write("#");
                }
                else
                {
                    Console.Write(".");
                }
                if (i % 40 == 0)
                {
                    Console.WriteLine();
                }
            };
            int cycle = 1;
            for (int i = 0; i < lines.Count(); i++)
            {
                calcRes1(cycle);
                var command = lines[i].Split(" ");
                cycle++;
                if (command[0] == "addx")
                {
                    calcRes1(cycle);
                    currentVal += int.Parse(command[1]);
                    cycle++;
                }
            }

            Console.WriteLine(res1);
            Console.ReadLine();
        }
    }
}
