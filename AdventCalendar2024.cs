using System.Diagnostics;
using System.Text.RegularExpressions;

namespace AdventCalendar2024
{
    [TestClass]
    public class AdventCalendar2024
    {
        [TestMethod]
        public void Day1_1()
        {
            List<string> inputList = File.ReadAllLines(@"Input\Day1.txt").ToList();
            List<int> leftList = new List<int>();
            List<int> rightList = new List<int>();
            foreach (string inputRow in inputList)
            {
                string[] split = inputRow.Split("   ");
                leftList.Add(int.Parse(split[0]));
                rightList.Add(int.Parse(split[1]));
            }
            leftList = leftList.OrderBy(o => o).ToList();
            rightList = rightList.OrderBy(o => o).ToList();
            int sum = 0;
            for (int i = 0; i < leftList.Count; i++)
                sum += Math.Abs(leftList[i] - rightList[i]);
            Debug.WriteLine(sum);
        }

        [TestMethod]
        public void Day1_2()
        {
            List<string> inputList = File.ReadAllLines(@"Input\Day1.txt").ToList();
            List<int> leftList = new List<int>();
            List<int> rightList = new List<int>();
            foreach (string inputRow in inputList)
            {
                string[] split = inputRow.Split("   ");
                leftList.Add(int.Parse(split[0]));
                rightList.Add(int.Parse(split[1]));
            }
            int sum = 0;
            leftList.ForEach(e => sum += e * rightList.Count(c => c == e));
            Debug.WriteLine(sum);
        }

        [TestMethod]
        public void Day2_1()
        {
            List<string> inputList = File.ReadAllLines(@"Input\Day2.txt").ToList();
            List<List<int>> reportList = new List<List<int>>();
            inputList.ForEach(e => reportList.Add(e.Split(" ").Select(s => int.Parse(s)).ToList()));
            int safeCount = 0;
            foreach (List<int> report in reportList)
            {
                int direction = 0; // 1 = increasing, 2 = decreasing
                bool isSafe = true;
                for (int i = 0; i < (report.Count - 1); i++)
                {
                    if (report[i] == report[i + 1] || Math.Abs(report[i] - report[i + 1]) > 3)
                        isSafe = false;
                    if (i == 0)
                    {
                        direction = report[i] < report[i + 1] ? 1 : 2;
                        continue;
                    }
                    if (direction == 1 && report[i] - report[i + 1] > 0)
                        isSafe = false;
                    if (direction == 2 && report[i] - report[i + 1] < 0)
                        isSafe = false;
                }
                if (isSafe)
                    safeCount++;
            }
            Debug.WriteLine(safeCount);
        }

        [TestMethod]
        public void Day2_2()
        {
            List<string> inputList = File.ReadAllLines(@"Input\Day2.txt").ToList();
            List<List<int>> reportList = new List<List<int>>();
            inputList.ForEach(e => reportList.Add(e.Split(" ").Select(s => int.Parse(s)).ToList()));
            int safeCount = 0;
            foreach (List<int> report in reportList)
            {
                string reportText = string.Join(' ', report);
                int direction = (report.Take(report.Count / 2).Sum() / (report.Count / 2))
                    < (report.Skip(report.Count / 2).Sum() / (report.Skip(report.Count / 2).Count()))
                    ? 1 : 2; // 1 = increasing, 2 = decreasing
                bool isSafe = Day2IsSafe(report, direction);
                if (!isSafe)
                {
                    for (int i = 0; i < report.Count; i++)
                    {
                        int[] reportAttempt = new int[report.Count];
                        report.CopyTo(reportAttempt);
                        List<int> reportAttemptList = reportAttempt.ToList();
                        reportAttemptList.RemoveAt(i);
                        isSafe = Day2IsSafe(reportAttemptList, direction);
                        if (isSafe)
                            break;
                    }
                }
                if (isSafe)
                    safeCount++;
            }
            Debug.WriteLine(safeCount); // 373
        }

        private bool Day2IsSafe(List<int> report, int direction)
        {
            for (int i = 0; i < (report.Count - 1); i++)
            {
                if ((report[i] == report[i + 1])
                    || (Math.Abs(report[i] - report[i + 1]) > 3)
                    || (direction == 1 && report[i] - report[i + 1] > 0)
                    || (direction == 2 && report[i] - report[i + 1] < 0))
                    return false;
            }
            return true;
        }

        [TestMethod]
        public void Day3_1()
        {
            string input = File.ReadAllText(@"Input\Day3.txt");
            MatchCollection matches = Regex.Matches(input, @"mul\(\d{1,3},\d{1,3}\)");
            int sum = 0;
            foreach (var match in matches.ToList())
            {
                string[] splitString = match.Value.Split(',');
                int value1 = int.Parse(new string(splitString[0].Where(w => char.IsDigit(w)).ToArray()));
                int value2 = int.Parse(new string(splitString[1].Where(w => char.IsDigit(w)).ToArray()));
                sum += value1 * value2;
            }
            Debug.WriteLine(sum);
        }

        [TestMethod]
        public void Day3_2()
        {
            string input = File.ReadAllText(@"Input\Day3.txt");
            MatchCollection matches = Regex.Matches(input, @"mul\(\d{1,3},\d{1,3}\)|do\(\)|don't\(\)");
            int sum = 0;
            bool doCalculation = true;
            foreach (var match in matches.ToList())
            {
                if (match.Value == "do()")
                    doCalculation = true;
                else if (match.Value == "don't()")
                    doCalculation = false;
                else if (doCalculation)
                {
                    string[] splitString = match.Value.Split(',');
                    int value1 = int.Parse(new string(splitString[0].Where(w => char.IsDigit(w)).ToArray()));
                    int value2 = int.Parse(new string(splitString[1].Where(w => char.IsDigit(w)).ToArray()));
                    sum += value1 * value2;
                }
            }
            Debug.WriteLine(sum);
        }

        [TestMethod]
        public void Day4_1()
        {
            List<string> inputList = File.ReadAllLines(@"Input\Day4.txt").ToList();
            List<Day4Char> wordList = new List<Day4Char>();
            int i = 0, j = 0;
            foreach (string input in inputList)
            {
                i = 0;
                input.ToList().ForEach(e => { wordList.Add(new Day4Char { x = i, y = j, Value = e }); i++; });
                j++;
            }
            int wordCount = 0;
            string wordSearch = "XMAS";
            foreach (Day4Char day4Char in wordList.Where(w => w.Value == wordSearch[0]))
            {
                List<Day4Char> matchList = wordList.Where(w => (Math.Abs(w.x - day4Char.x) == 1 || Math.Abs(w.x - day4Char.x) == 0)
                    && (Math.Abs(w.y - day4Char.y) == 1 || Math.Abs(w.y - day4Char.y) == 0)
                        && w.Value == wordSearch[1]).ToList();
                if (matchList.Count == 0)
                    continue;
                foreach (Day4Char match in matchList)
                {
                    int directionX = match.x - day4Char.x;
                    int directionY = match.y - day4Char.y;
                    if (Day4PositionWordSearch(wordList, match, wordSearch, 1, directionX, directionY))
                        wordCount++;
                }
            }
            Debug.WriteLine(wordCount);
        }

        private bool Day4PositionWordSearch(List<Day4Char> wordList, Day4Char position, string wordSearch, int wordIndex, int directionX, int directionY)
        {
            wordIndex++;
            Day4Char match = wordList.FirstOrDefault(w => w.x == (position.x + directionX) && w.y == (position.y + directionY) && w.Value == wordSearch[wordIndex]);
            if (match != null)
            {
                if (wordIndex == (wordSearch.Count() - 1))
                    return true;
                else
                    return Day4PositionWordSearch(wordList, match, wordSearch, wordIndex, directionX, directionY);
            }
            else
                return false;
        }

        private class Day4Char
        {
            public int x { get; set; }
            public int y { get; set; }
            public Char Value { get; set; }
        }

        [TestMethod]
        public void Day4_2()
        {
            List<string> inputList = File.ReadAllLines(@"Input\Day4.txt").ToList();
            List<Day4Char> wordList = new List<Day4Char>();
            int i = 0, j = 0;
            foreach (string input in inputList)
            {
                i = 0;
                input.ToList().ForEach(e => { wordList.Add(new Day4Char { x = i, y = j, Value = e }); i++; });
                j++;
            }
            int wordCount = 0;
            int maxX = wordList.Max(m => m.x);
            int maxY = wordList.Max(m => m.y);
            foreach (Day4Char day4Char in wordList.Where(w => w.Value == 'A' && w.x > 0 && w.x < maxX && w.y > 0 && w.y < maxY))
            {
                bool line1 = false, line2 = false;
                if ((wordList.Any(w => w.x == (day4Char.x - 1) && w.y == (day4Char.y - 1) && w.Value == 'M') &&
                    wordList.Any(w => w.x == (day4Char.x + 1) && w.y == (day4Char.y + 1) && w.Value == 'S'))
                    ||
                    (wordList.Any(w => w.x == (day4Char.x - 1) && w.y == (day4Char.y - 1) && w.Value == 'S') &&
                    wordList.Any(w => w.x == (day4Char.x + 1) && w.y == (day4Char.y + 1) && w.Value == 'M')))
                    line1 = true;
                if ((wordList.Any(w => w.x == (day4Char.x - 1) && w.y == (day4Char.y + 1) && w.Value == 'M') &&
                    wordList.Any(w => w.x == (day4Char.x + 1) && w.y == (day4Char.y - 1) && w.Value == 'S'))
                    ||
                    (wordList.Any(w => w.x == (day4Char.x - 1) && w.y == (day4Char.y + 1) && w.Value == 'S') &&
                    wordList.Any(w => w.x == (day4Char.x + 1) && w.y == (day4Char.y - 1) && w.Value == 'M')))
                    line2 = true;
                if (line1 && line2)
                    wordCount++;
            }
            Debug.WriteLine(wordCount);
        }

        [TestMethod]
        public void Day5_1()
        {
            List<string> inputList = File.ReadAllLines(@"Input\Day5Test.txt").ToList();
            List<List<int>> printList = new List<List<int>>();
            List<Day5PrintRule> printRuleList = new List<Day5PrintRule>();
            foreach (string input in inputList)
            {
                if (input.Contains('|'))
                {
                    string[] split = input.Split('|');
                    printRuleList.Add(new Day5PrintRule { Page = int.Parse(split[0]), Rule = int.Parse(split[1]) });
                }
                else if (input.Length > 0)
                    printList.Add(input.Split(',').Select(s => int.Parse(s)).ToList());
            }
        }

        private class Day5PrintRule
        {
            public int Page { get; set; }
            public int Rule { get; set; }
        }



        [TestMethod]
        public void Day5_2()
        {

        }

        [TestMethod]
        public void Day6_1()
        {

        }

        [TestMethod]
        public void Day6_2()
        {

        }

        [TestMethod]
        public void Day7_1()
        {

        }

        [TestMethod]
        public void Day7_2()
        {

        }

        [TestMethod]
        public void Day8_1()
        {

        }

        [TestMethod]
        public void Day8_2()
        {

        }

        [TestMethod]
        public void Day9_1()
        {

        }

        [TestMethod]
        public void Day9_2()
        {

        }

        [TestMethod]
        public void Day10_1()
        {

        }

        [TestMethod]
        public void Day10_2()
        {

        }

        [TestMethod]
        public void Day11_1()
        {

        }

        [TestMethod]
        public void Day11_2()
        {

        }

        [TestMethod]
        public void Day12_1()
        {

        }

        [TestMethod]
        public void Day13_1()
        {

        }

        [TestMethod]
        public void Day13_2()
        {

        }

        [TestMethod]
        public void Day14_1()
        {

        }

        [TestMethod]
        public void Day14_2()
        {

        }

        [TestMethod]
        public void Day15_1()
        {

        }

        [TestMethod]
        public void Day15_2()
        {

        }

        [TestMethod]
        public void Day16_1()
        {

        }

        [TestMethod]
        public void Day16_2()
        {

        }

        [TestMethod]
        public void Day17_1()
        {

        }

        [TestMethod]
        public void Day17_2()
        {

        }

        [TestMethod]
        public void Day18_1()
        {

        }

        [TestMethod]
        public void Day18_2()
        {

        }

        [TestMethod]
        public void Day19_1()
        {

        }

        [TestMethod]
        public void Day19_2()
        {

        }

        [TestMethod]
        public void Day20_1()
        {

        }

        [TestMethod]
        public void Day20_2()
        {

        }

        [TestMethod]
        public void Day21_1()
        {

        }

        [TestMethod]
        public void Day21_2()
        {

        }

        [TestMethod]
        public void Day22_1()
        {

        }

        [TestMethod]
        public void Day22_2()
        {

        }

        [TestMethod]
        public void Day23_1()
        {

        }

        [TestMethod]
        public void Day23_2()
        {

        }

        [TestMethod]
        public void Day24_1()
        {

        }

        [TestMethod]
        public void Day24_2()
        {

        }

        [TestMethod]
        public void Day25()
        {

        }
    }
}