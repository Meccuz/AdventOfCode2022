namespace AdventOfCode2022.Days
{
    internal static class Day08
    {
        public static void Run()
        {
            string[] lines = File.ReadAllLines("../../../Assets/Day08.txt");

            HashSet<Tree> trees = new HashSet<Tree>();

            CalculateVisibilityFromTopAndLeft(lines, trees);
            CalculateVisibilityFromBottomAndRight(lines, trees);
            int bestScore = CalculateScenicScore(lines);

            Console.WriteLine(trees.Count(x => x.IsVisible));
            Console.WriteLine(bestScore);
            Console.ReadLine();
        }

        private static int CalculateScenicScore(string[] lines)
        {
            var calcScoreLeft = (int x, int y) =>
            {
                // Console.Write($"L {x + 1};{y + 1} => ");
                var res = 0;
                var currentHeight = int.Parse(lines[y][x].ToString());
                for (int x1 = x - 1; x1 >= 0; x1--)
                {
                    res++;
                    if (int.Parse(lines[y][x1].ToString()) >= currentHeight)
                    {
                        break;
                    }
                }
                // Console.WriteLine(res);
                return res;
            };
            var calcScoreRight = (int x, int y) =>
            {
                // Console.Write($"L {x + 1};{y + 1} => ");
                var res = 0;
                var currentHeight = int.Parse(lines[y][x].ToString());
                for (int x1 = x + 1; x1 < lines[0].Length; x1++)
                {
                    res++;
                    if (int.Parse(lines[y][x1].ToString()) >= currentHeight)
                    {
                        break;
                    }
                }
                // Console.WriteLine(res);
                return res;
            };
            var calcScoreTop = (int x, int y) =>
            {
                // Console.Write($"L {x + 1};{y + 1} => ");
                var res = 0;
                var currentHeight = int.Parse(lines[y][x].ToString());
                for (int y1 = y - 1; y1 >= 0; y1--)
                {
                    res++;
                    if (int.Parse(lines[y1][x].ToString()) >= currentHeight)
                    {
                        break;
                    }
                }
                // Console.WriteLine(res);
                return res;
            };
            var calcScoreBottom = (int x, int y) =>
            {
                // Console.Write($"L {x + 1};{y + 1} => ");
                var res = 0;
                var currentHeight = int.Parse(lines[y][x].ToString());
                for (int y1 = y + 1; y1 < lines.Length; y1++)
                {
                    res++;
                    if (int.Parse(lines[y1][x].ToString()) >= currentHeight)
                    {
                        break;
                    }
                }
                // Console.WriteLine(res);
                return res;
            };

            int best = 0;
            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    var scoreLeft = calcScoreLeft(x, y);
                    var scoreRight = calcScoreRight(x, y);
                    var scoreTop = calcScoreTop(x, y);
                    var scoreBottom = calcScoreBottom(x, y);
                    var score = scoreLeft * scoreRight * scoreTop * scoreBottom;
                    if (score > best)
                    {
                        Console.WriteLine($"Score: {score}; X: {x + 1}; Y: {y + 1}; L: {scoreLeft}; R: {scoreRight}; T: {scoreTop}; B: {scoreBottom}");
                        best = score;
                    }
                }
            }
            return best;
        }

        private static void CalculateVisibilityFromBottomAndRight(string[] lines, HashSet<Tree> trees)
        {
            var tallestByCol = new List<int>(lines[0].Length);
            for (int i = 0; i < lines[0].Length; i++) { tallestByCol.Add(0); }

            for (int y = lines.Length - 1; y >= 0; y--)
            {
                var line = lines[y];
                var tallestOnLine = 0;
                for (int x = line.Length - 1; x >= 0; x--)
                {
                    var currentHeight = line[x];
                    var tree = trees.FirstOrDefault(tree => tree.X == x && tree.Y == y);
                    if (tree == null)
                    {
                        tree = new Tree(x, y, int.Parse(currentHeight.ToString()));
                        trees.Add(tree);
                    }
                    tree.IsVisibleFromRight = currentHeight > tallestOnLine;
                    if (tree.IsVisibleFromRight)
                    {
                        tallestOnLine = currentHeight;
                    }
                    tree.IsVisibleFromBottom = currentHeight > tallestByCol[x];
                    if (tree.IsVisibleFromBottom)
                    {
                        tallestByCol[x] = currentHeight;
                    }
                }
            }
        }

        private static void CalculateVisibilityFromTopAndLeft(string[] lines, HashSet<Tree> trees)
        {
            var tallestByCol = new List<int>(lines[0].Length);
            for (int i = 0; i < lines[0].Length; i++) { tallestByCol.Add(0); }

            for (int y = 0; y < lines.Length; y++)
            {
                var line = lines[y];
                var tallestOnLine = 0;
                for (int x = 0; x < line.Length; x++)
                {
                    var currentHeight = line[x];
                    var tree = trees.FirstOrDefault(tree => tree.X == x && tree.Y == y);
                    if (tree == null)
                    {
                        tree = new Tree(x, y, int.Parse(currentHeight.ToString()));
                        trees.Add(tree);
                    }
                    tree.IsVisibleFromLeft = currentHeight > tallestOnLine;
                    if (tree.IsVisibleFromLeft)
                    {
                        tallestOnLine = currentHeight;
                    }
                    tree.IsVisibleFromTop = currentHeight > tallestByCol[x];
                    if (tree.IsVisibleFromTop)
                    {
                        tallestByCol[x] = currentHeight;
                    }
                }
            }
        }
    }

    internal class Tree
    {
        public int X { get; }
        public int Y { get; }
        public int Height { get; }
        public bool IsVisibleFromRight { get; set; }
        public bool IsVisibleFromLeft { get; set; }
        public bool IsVisibleFromTop { get; set; }
        public bool IsVisibleFromBottom { get; set; }
        public bool IsVisible => IsVisibleFromRight || IsVisibleFromLeft || IsVisibleFromTop || IsVisibleFromBottom;

        public Tree(int x, int y, int height)
        {
            X = x;
            Y = y;
            Height = height;
        }
    }
}
