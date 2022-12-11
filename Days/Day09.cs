namespace AdventOfCode2022.Days
{
    internal static class Day09
    {
        public static void Run()
        {
            string[] lines = File.ReadAllLines("../../../Assets/Day09.txt");

            var rope = new List<Knot>();
            var hs = new HashSet<(int, int)>();
            int ropeLength = 10;

            for (int i = 0; i < ropeLength; i++) rope.Add(new Knot());

            foreach (var line in lines)
            {
                var p = line.Split(" ");
                string dir = p[0];
                int moves = int.Parse(p[1]);

                for (int i = 0; i < moves; i++)
                {
                    rope[0].UpdateHead(dir);

                    for (int j = 1; j < rope.Count; j++)
                    {
                        rope[j].UpdateTail(rope[j - 1]);
                    }

                    hs.Add(rope[rope.Count - 1].position);
                }
            }

            Console.WriteLine(hs.Count);
        }
    }

    public class Knot
    {
        public (int x, int y) position;
        Dictionary<string, int> direction = new Dictionary<string, int>
        {
            {"U", 1},
            {"D", -1},
            {"L", -1},
            {"R", 1},
        };

        public static bool IsFar((int x, int y) head, (int x, int y) tail) => Math.Max(Math.Abs(head.x - tail.x), Math.Abs(head.y - tail.y)) > 1;

        public void UpdateHead(string dir)
        {
            int val = direction[dir];

            if (dir == "U" || dir == "D")
            {
                position.y += val;
            }
            else if (dir == "L" || dir == "R")
            {
                position.x += val;
            }
        }
        public void UpdateTail(Knot head)
        {
            if (!IsFar(head.position, position))
                return;

            if (head.position.y > position.y) position.y++;
            if (head.position.y < position.y) position.y--;

            if (head.position.x > position.x) position.x++;
            if (head.position.x < position.x) position.x--;
        }
    }
}