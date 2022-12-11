namespace AdventOfCode2022.Days
{
    internal static class Day02
    {
        public static void Run()
        {
            string[] lines = File.ReadAllLines("../../../Assets/Day02.txt");
            // Rock A X 1
            // Paper B Y 2
            // Sciss C Z 3

            int score1 = 0;
            int score2 = 0;
            foreach (string line in lines)
            {
                score1 += GetMyHandScore(line[2]);
                score1 += GetMatchResult1(line[0], line[2]);
            }
            foreach (string line in lines)
            {
                score2 += GetMatchResult2(line[0], line[2]);
            }

            Console.WriteLine(score1);
            Console.WriteLine(score2);
            Console.ReadLine();
        }

        private static int GetMatchResult2(char opponent, char result)
        {
            switch (opponent)
            {
                case 'A': // opponent plays rock
                    switch (result)
                    {
                        case 'X': return 3; // 3 for sciss + 0 for loss
                        case 'Y': return 4; // 1 for rock + 3 for draw
                        case 'Z': return 8; // 2 for paper + 6 for win
                        default: throw new Exception();
                    }
                case 'B': // opponent plays paper
                    switch (result)
                    {
                        case 'X': return 1; // 1 for rock + 0 for loss
                        case 'Y': return 5; // 2 for paper + 3 for draw
                        case 'Z': return 9; // 3 for sciss + 6 for win
                        default: throw new Exception();
                    }
                case 'C': // opponent plays sciss
                    switch (result)
                    {
                        case 'X': return 2; // 2 for paper + 0 for loss
                        case 'Y': return 6; // 3 for sciss + 3 for draw
                        case 'Z': return 7; // 1 for rock + 6 for win
                        default: throw new Exception();
                    }
                default: throw new Exception();
            }
        }

        private static int GetMatchResult1(char opponent, char me)
        {
            switch (me)
            {
                case 'X':
                    switch (opponent)
                    {
                        case 'A': return 3;
                        case 'B': return 0;
                        case 'C': return 6;
                        default: throw new Exception();
                    }
                case 'Y':
                    switch (opponent)
                    {
                        case 'A': return 6;
                        case 'B': return 3;
                        case 'C': return 0;
                        default: throw new Exception();
                    }
                case 'Z':
                    switch (opponent)
                    {
                        case 'A': return 0;
                        case 'B': return 6;
                        case 'C': return 3;
                        default: throw new Exception();
                    }
                default: throw new Exception();
            }
        }

        private static int GetMyHandScore(char me)
        {
            return me switch
            {
                'X' => 1,
                'Y' => 2,
                'Z' => 3,
                _ => throw new Exception(),
            };
        }
    }
}
