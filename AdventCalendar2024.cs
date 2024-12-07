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
            List<string> inputList = File.ReadAllLines(@"Input\Day5.txt").ToList();
            List<List<Day5Page>> printList = new List<List<Day5Page>>();
            List<Day5PrintRule> printRuleList = new List<Day5PrintRule>();
            foreach (string input in inputList)
            {
                if (input.Contains('|'))
                {
                    string[] split = input.Split('|');
                    printRuleList.Add(new Day5PrintRule { Page = int.Parse(split[0]), Rule = int.Parse(split[1]) });
                }
                else if (input.Length > 0)
                {
                    List<Day5Page> pageRow = new List<Day5Page>();
                    List<int> pageList = input.Split(',').Select(s => int.Parse(s)).ToList();
                    int pageIndex = 0;
                    pageList.ForEach(e => pageRow.Add(new Day5Page { Number = e, PrintIndex = pageIndex++ }));
                    printList.Add(pageRow);
                }
            }
            int middlePageSum = 0;
            foreach (List<Day5Page> pageRow in printList)
            {
                bool isValid = true;
                foreach (Day5Page page in pageRow)
                {
                    List<Day5PrintRule> pageRuleList = printRuleList.Where(w => w.Page == page.Number).ToList();
                    if (pageRow.Any(w => w.PrintIndex < page.PrintIndex && pageRuleList.Select(s => s.Rule).Contains(w.Number)))
                        isValid = false;
                }
                if (isValid)
                    middlePageSum += pageRow[pageRow.Count() / 2].Number;
            }
            Debug.WriteLine(middlePageSum);
        }

        private class Day5PrintRule
        {
            public int Page { get; set; }
            public int Rule { get; set; }
        }

        private class Day5Page
        {
            public int PrintIndex { get; set; }
            public int Number { get; set; }
        }

        [TestMethod]
        public void Day5_2()
        {
            List<string> inputList = File.ReadAllLines(@"Input\Day5.txt").ToList();
            List<List<Day5Page>> printList = new List<List<Day5Page>>();
            List<Day5PrintRule> printRuleList = new List<Day5PrintRule>();
            foreach (string input in inputList)
            {
                if (input.Contains('|'))
                {
                    string[] split = input.Split('|');
                    printRuleList.Add(new Day5PrintRule { Page = int.Parse(split[0]), Rule = int.Parse(split[1]) });
                }
                else if (input.Length > 0)
                {
                    List<Day5Page> pageRow = new List<Day5Page>();
                    List<int> pageList = input.Split(',').Select(s => int.Parse(s)).ToList();
                    int pageIndex = 0;
                    pageList.ForEach(e => pageRow.Add(new Day5Page { Number = e, PrintIndex = pageIndex++ }));
                    printList.Add(pageRow);
                }
            }
            int middlePageSum = 0;
            foreach (List<Day5Page> pageRow in printList)
            {
                bool isValid = true;
                foreach (Day5Page page in pageRow)
                {
                    List<Day5PrintRule> pageRuleList = printRuleList.Where(w => w.Page == page.Number).ToList();
                    if (pageRow.Any(w => w.PrintIndex < page.PrintIndex && pageRuleList.Select(s => s.Rule).Contains(w.Number)))
                        isValid = false;
                }
                if (!isValid)
                {
                    List<Day5Page> orderedPageRow = new List<Day5Page>();
                    foreach (Day5Page page in pageRow)
                    {
                        List<int> rulePages = printRuleList.Where(w => w.Page == page.Number).Select(s => s.Rule).ToList();
                        int insertIndex = orderedPageRow.Where(w => rulePages.Contains(w.Number)).OrderBy(o => o.PrintIndex).Select(s => (int?)s.PrintIndex).FirstOrDefault() ?? orderedPageRow.Count;
                        orderedPageRow.Where(w => w.PrintIndex >= insertIndex).ToList().ForEach(e => e.PrintIndex++);
                        orderedPageRow.Insert(insertIndex, new Day5Page { Number = page.Number, PrintIndex = insertIndex });
                    }
                    middlePageSum += orderedPageRow[orderedPageRow.Count() / 2].Number;
                }
            }
            Debug.WriteLine(middlePageSum);
        }

        [TestMethod]
        public void Day6_1()
        {
            List<string> inputList = File.ReadAllLines(@"Input\Day6.txt").ToList();
            List<Day6Position> positionList = new List<Day6Position>();
            int x = 0, y = 0;
            int startX = 0, startY = 0;
            int currentDirectionX = 0, currentDirectionY = 0;
            foreach (string input in inputList)
            {
                x = 0;
                input.ToList().ForEach(e =>
                {
                    if (new char[] { '<', '>', '^', 'v' }.Contains(e))
                    {
                        startX = x;
                        startY = y;
                        if (e == '<')
                            currentDirectionX = -1;
                        else if (e == '>')
                            currentDirectionX = 1;
                        else if (e == '^')
                            currentDirectionY = -1;
                        else if (e == 'v')
                            currentDirectionY = 1;
                    }
                    positionList.Add(new Day6Position { X = x++, Y = y, IsObstacle = e == '#', Visited = false });
                });
                y++;
            }

            Day6Position currentPos = positionList.First(w => w.X == startX && w.Y == startY);
            while (currentPos != null)
            {
                currentPos.Visited = true;
                Day6Position nextPos = positionList.FirstOrDefault(w => w.X == (currentPos.X + currentDirectionX) 
                    && w.Y == (currentPos.Y + currentDirectionY));
                while (nextPos != null && nextPos.IsObstacle)
                {
                    if (currentDirectionX == -1)
                    {
                        currentDirectionX = 0;
                        currentDirectionY = -1;
                    }
                    else if (currentDirectionX == 1)
                    {
                        currentDirectionX = 0;
                        currentDirectionY = 1;
                    }
                    else if (currentDirectionY == -1)
                    {
                        currentDirectionX = 1;
                        currentDirectionY = 0;
                    }
                    else if (currentDirectionY == 1)
                    {
                        currentDirectionX = -1;
                        currentDirectionY = 0;
                    }
                    nextPos = positionList.FirstOrDefault(w => w.X == (currentPos.X + currentDirectionX)
                        && w.Y == (currentPos.Y + currentDirectionY));
                }
                currentPos = nextPos;
            }
            Debug.WriteLine(positionList.Count(c => c.Visited));
        }

        private class Day6Position
        {
            public int X { get; set; }
            public int Y { get; set; }
            public bool IsObstacle { get; set; }
            public bool Visited { get; set; }
        }

        private class Day6Position2
        {
            public int X { get; set; }
            public int Y { get; set; }
            public bool IsObstacle { get; set; }
            public List<string> VisitedList { get; set; }
        }

        [TestMethod]
        public void Day6_2() // Needs optimization
        {
            List<string> inputList = File.ReadAllLines(@"Input\Day6.txt").ToList();
            List<Day6Position2> positionList = new List<Day6Position2>();
            int x = 0, y = 0;
            int startX = 0, startY = 0;
            int startDirectionX = 0, startDirectionY = 0;
            int currentDirectionX = 0, currentDirectionY = 0;
            int maxX = inputList.First().Count();
            int maxY = inputList.Count();
            foreach (string input in inputList)
            {
                x = 0;
                input.ToList().ForEach(e =>
                {
                    if (new char[] { '<', '>', '^', 'v' }.Contains(e))
                    {
                        startX = x;
                        startY = y;
                        if (e == '<')
                            startDirectionX = -1;
                        else if (e == '>')
                            startDirectionX = 1;
                        else if (e == '^')
                            startDirectionY = -1;
                        else if (e == 'v')
                            startDirectionY = 1;
                    }
                    positionList.Add(new Day6Position2 { X = x++, Y = y, IsObstacle = e == '#', VisitedList = new List<string>() });
                });
                y++;
            }

            int loopCount = 0;
            Day6Position2 startPos = positionList.First(w => w.X == startX && w.Y == startY);
            Day6Position2 nextPos = null;
            Day6Position2 currentPos = null;
            foreach (Day6Position2 newObstacle in positionList.Where(w => !w.IsObstacle && !(w.X == startPos.X && w.Y == startPos.Y))
                .OrderBy(o => o.Y).ThenBy(t => t.X))
            {
                positionList.ForEach(e => e.VisitedList.Clear());
                currentPos = startPos;
                newObstacle.IsObstacle = true;
                currentDirectionX = startDirectionX;
                currentDirectionY = startDirectionY;
                while (currentPos != null)
                {
                    if (currentPos.VisitedList.Contains(currentDirectionX.ToString() + currentDirectionY.ToString()))
                    {
                        loopCount++;
                        break;
                    }
                    currentPos.VisitedList.Add(currentDirectionX.ToString() + currentDirectionY.ToString());
                    nextPos = positionList.FirstOrDefault(w => w.X == (currentPos.X + currentDirectionX) && w.Y == (currentPos.Y + currentDirectionY));
                    while (nextPos != null && nextPos.IsObstacle)
                    {
                        if (currentDirectionX == -1)
                        {
                            currentDirectionX = 0;
                            currentDirectionY = -1;
                        }
                        else if (currentDirectionX == 1)
                        {
                            currentDirectionX = 0;
                            currentDirectionY = 1;
                        }
                        else if (currentDirectionY == -1)
                        {
                            currentDirectionX = 1;
                            currentDirectionY = 0;
                        }
                        else if (currentDirectionY == 1)
                        {
                            currentDirectionX = -1;
                            currentDirectionY = 0;
                        }
                        nextPos = positionList.FirstOrDefault(w => w.X == (currentPos.X + currentDirectionX)
                            && w.Y == (currentPos.Y + currentDirectionY));
                    }
                    currentPos = nextPos;
                }
                newObstacle.IsObstacle = false;
            }
            Debug.WriteLine(loopCount); // 1705
        }

        [TestMethod]
        public void Day7_1()
        {
            List<string> inputList = File.ReadAllLines(@"Input\Day7.txt").ToList();
            List<Day7Test> testList = new List<Day7Test>();
            foreach (string input in inputList)
            {
                Day7Test test = new Day7Test();
                string[] inputSplit = input.Split(':');
                test.Result = long.Parse(inputSplit[0]);
                test.ValueList = inputSplit[1].Trim().Split(' ').Select(s => int.Parse(s)).ToList();
                testList.Add(test);
            }
            long sum = 0;
            foreach (Day7Test test in testList)
            {
                if (Day7_1CalculationTest(test.Result, test.ValueList.First(), 1, test.ValueList.Skip(1).ToList())
                    || Day7_1CalculationTest(test.Result, test.ValueList.First(), 2, test.ValueList.Skip(1).ToList()))
                    sum += test.Result;
            }
            Debug.WriteLine(sum);
        }

        private bool Day7_1CalculationTest(long result, long currentValue, int operation, List<int> remainingValues)
        {
            if (remainingValues.Count == 0)
                return result == currentValue;
            if (currentValue > result)
                return false;
            int nextValue = remainingValues.First();
            if (operation == 1) // +
                currentValue += nextValue;
            else // *
                currentValue *= nextValue;
            bool test = Day7_1CalculationTest(result, currentValue, 1, remainingValues.Skip(1).ToList());
            if (!test)
                test = Day7_1CalculationTest(result, currentValue, 2, remainingValues.Skip(1).ToList());
            return test;
        }

        private class Day7Test
        {
            public long Result { get; set; }
            public List<int> ValueList { get; set; }
        }

        [TestMethod]
        public void Day7_2()
        {
            List<string> inputList = File.ReadAllLines(@"Input\Day7.txt").ToList();
            List<Day7Test> testList = new List<Day7Test>();
            foreach (string input in inputList)
            {
                Day7Test test = new Day7Test();
                string[] inputSplit = input.Split(':');
                test.Result = long.Parse(inputSplit[0]);
                test.ValueList = inputSplit[1].Trim().Split(' ').Select(s => int.Parse(s)).ToList();
                testList.Add(test);
            }
            long sum = 0;
            foreach (Day7Test test in testList)
            {
                if (Day7_2CalculationTest(test.Result, test.ValueList.First(), 1, test.ValueList.Skip(1).ToList())
                    || Day7_2CalculationTest(test.Result, test.ValueList.First(), 2, test.ValueList.Skip(1).ToList())
                    || Day7_2CalculationTest(test.Result, test.ValueList.First(), 3, test.ValueList.Skip(1).ToList()))
                    sum += test.Result;
            }
            Debug.WriteLine(sum);
        }

        private bool Day7_2CalculationTest(long result, long currentValue, int operation, List<int> remainingValues)
        {
            if (remainingValues.Count == 0)
                return result == currentValue;
            if (currentValue > result)
                return false;
            int nextValue = remainingValues.First();
            if (operation == 1) // +
                currentValue += nextValue;
            else if (operation == 2) // *
                currentValue *= nextValue;
            else // ||
                currentValue = long.Parse(currentValue.ToString() + nextValue.ToString());
            bool test = Day7_2CalculationTest(result, currentValue, 1, remainingValues.Skip(1).ToList());
            if (!test)
                test = Day7_2CalculationTest(result, currentValue, 2, remainingValues.Skip(1).ToList());
            if (!test)
                test = Day7_2CalculationTest(result, currentValue, 3, remainingValues.Skip(1).ToList());
            return test;
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