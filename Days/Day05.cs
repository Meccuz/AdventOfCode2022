using System.Text.RegularExpressions;

namespace AdventOfCode2022.Days
{
    internal static class Day05
    {
        public static void Run()
        {
            string[] lines = File.ReadAllLines("../../../Assets/Day05.txt");
            var cratesV1 = new List<List<char>>();
            bool readingCrates = true;
            var commands = new List<List<int>>();
            var commandRegex = new Regex(@"move (\d*) from (\d*) to (\d*)");
            foreach (string line in lines)
            {
                if (readingCrates)
                {
                    readingCrates = ReadCrates(cratesV1, line);
                }
                else if (commandRegex.IsMatch(line))
                {
                    var match = commandRegex.Match(line);
                    commands.Add(new List<int> {
                        int.Parse(match.Groups[1].Value),
                        int.Parse(match.Groups[2].Value),
                        int.Parse(match.Groups[3].Value)
                    });
                }
            }

            var cratesV2 = new List<List<char>>(cratesV1);

            foreach (var command in commands)
            {
                // DoCommandV1(cratesV1, command);
                DoCommandV2(cratesV2, command);
            }

            foreach (var crate in cratesV2)
            {
                Console.Write(crate[crate.Count - 1]);
            }
            Console.ReadLine();
        }

        private static void DoCommandV1(List<List<char>> crates, List<int> command)
        {
            for (int i = 0; i < command[0]; i++)
            {
                var origin = crates[command[1] - 1];
                var destination = crates[command[2] - 1];
                int index = origin.Count - 1;
                var item = origin[index];
                destination.Add(item);
                origin.RemoveAt(index);
            }
        }

        private static void DoCommandV2(List<List<char>> crates, List<int> command)
        {
            var origin = crates[command[1] - 1];
            var destination = crates[command[2] - 1];
            var toAdd = new List<char>();
            for (int i = 0; i < command[0]; i++)
            {
                int index = origin.Count - 1;
                var item = origin[index];
                origin.RemoveAt(index);
                toAdd.Insert(0, item);
            }
            destination.AddRange(toAdd);
        }

        private static bool ReadCrates(List<List<char>> arr, string line)
        {
            for (int i = 1; i < line.Length; i += 4)
            {
                var index = (i - 1) / 4;
                if (arr.Count == index)
                {
                    arr.Add(new List<char>());
                }
                var arrCol = arr[index];

                char item = line[i];
                if (!string.IsNullOrWhiteSpace(item.ToString()))
                {
                    if (int.TryParse(item.ToString(), out int tmp))
                    {
                        return false;
                    }
                    arrCol.Insert(0, item);
                }
            }

            return true;
        }
    }
}
