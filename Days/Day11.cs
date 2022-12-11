using System.Numerics;
using System.Text.RegularExpressions;

namespace AdventOfCode2022.Days
{
    internal static class Day11
    {
        public static void Run()
        {
            string lines = File.ReadAllText("../../../Assets/Day11.txt");

            var regex = new Regex(@"Monkey (?<index>\d*):\r\n" +
                @"  Starting items: (?<startingItems>.*)\r\n" +
                @"  Operation: new = (?<operation>.*)\r\n" +
                @"  Test: divisible by (?<testDivisible>\d*)\r\n" +
                @"    If true: throw to monkey (?<destIfTrue>\d*)\r\n" +
                @"    If false: throw to monkey (?<destIfFalse>\d*)");

            var matches = regex.Matches(lines);

            var monkeys = new List<Monkey>();
            var lcm = 1;
            foreach (Match m in matches)
            {
                var monkey = new Monkey(m);
                monkeys.Add(monkey);
                lcm *= monkey.TestDivisible;
            }


            //for (int i = 0; i < 20; i++)
            for (int i = 1; i <= 10000; i++)
            {
                foreach (var monkey in monkeys)
                {
                    monkey.DoBusiness(monkeys, lcm);
                }

                if (i % 1000 == 0 || i / 20.0 == 1.0)
                {
                    Console.WriteLine($"=== After round {i} ===");
                    foreach (var m in monkeys)
                    {
                        Console.WriteLine($"Monkey {m.Index} inspected {m.ItemsInspected}");
                    }
                }
            }

            var top2 = monkeys.Select(x => x.ItemsInspected)
                .OrderByDescending(x => x)
                .Take(2)
                .ToArray();

            Console.WriteLine(top2[0] * top2[1]);
            Console.ReadLine();
        }
    }

    public class Monkey
    {
        public Monkey(Match m)
        {
            Index = int.Parse(m.Groups["index"].Value);
            Items = m.Groups["startingItems"].Value.Split(", ").Select(x => BigInteger.Parse(x)).ToList();
            Operation = m.Groups["operation"].Value;
            TestDivisible = int.Parse(m.Groups["testDivisible"].Value);
            DestIfTrue = int.Parse(m.Groups["destIfTrue"].Value);
            DestIfFalse = int.Parse(m.Groups["destIfFalse"].Value);
            ItemsInspected = 0;
        }

        public int Index { get; }
        public List<BigInteger> Items { get; }
        public string Operation { get; }
        public int TestDivisible { get; }
        public int DestIfTrue { get; }
        public int DestIfFalse { get; }
        public BigInteger ItemsInspected { get; private set; }

        internal void DoBusiness(List<Monkey> monkeys, int lcm)
        {
            foreach (var item in Items)
            {
                BigInteger res = DoOperation(item, lcm);
                // res = (BigInteger)Math.Floor(res / 3.0);
                if (res % TestDivisible == 0)
                {
                    monkeys.First(x => x.Index == DestIfTrue).GetItem(res);
                }
                else
                {
                    monkeys.First(x => x.Index == DestIfFalse).GetItem(res);
                }
                ItemsInspected++;
            }

            Items.Clear();
        }

        public void GetItem(BigInteger item)
        {
            Items.Add(item);
        }

        private BigInteger DoOperation(BigInteger item, int lcm)
        {
            var ops = Operation.Split(" ");
            var op2 = GetOp2(ops[2], item);
            return ops[1] switch
            {
                "*" => (item * op2) % lcm,
                "+" => (item + op2) % lcm,
                _ => throw new Exception("Unknown operation"),
            };
        }

        private BigInteger GetOp2(string v, BigInteger item)
        {
            return v switch
            {
                "old" => item,
                _ => BigInteger.Parse(v),
            };
        }
    }
}
