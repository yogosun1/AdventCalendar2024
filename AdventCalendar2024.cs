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
            List<string> inputList = File.ReadAllLines(@"Input\Day8.txt").ToList();
            List<Day8Position> positionList = new List<Day8Position>();
            int x = 0, y = 0;
            foreach (string input in inputList)
            {
                x = 0;
                input.ToList().ForEach(e => positionList.Add(new Day8Position
                {
                    X = x++,
                    Y = y,
                    AntennaName = ((e == '.' || e == '#') ? null : e),
                    IsAntinode = false,
                    IsAntenna = ((e == '.' || e == '#') ? false : true)
                }));
                y++;
            }

            List<char> signalList = positionList.Where(w => w.IsAntenna).Select(s => (char)s.AntennaName).Distinct().ToList();
            foreach (char signal in signalList)
            {
                List<Day8Position> antennaList = positionList.Where(w => w.AntennaName == signal).ToList();
                foreach (Day8Position antenna in antennaList)
                {
                    foreach (Day8Position otherAntenna in antennaList.Where(w => !(w.X == antenna.X && w.Y == antenna.Y)))
                    {
                        Day8Position antinode = positionList.FirstOrDefault(w => w.X == (otherAntenna.X + otherAntenna.X - antenna.X)
                            && w.Y == (otherAntenna.Y + otherAntenna.Y - antenna.Y));
                        if (antinode != null)
                            antinode.IsAntinode = true;
                    }
                }
            }
            Debug.WriteLine(positionList.Count(c => c.IsAntinode));
        }

        private class Day8Position
        {
            public int X { get; set; }
            public int Y { get; set; }
            public char? AntennaName { get; set; }
            public bool IsAntenna { get; set; }
            public bool IsAntinode { get; set; }
        }

        [TestMethod]
        public void Day8_2()
        {
            List<string> inputList = File.ReadAllLines(@"Input\Day8.txt").ToList();
            List<Day8Position> positionList = new List<Day8Position>();
            int x = 0, y = 0;
            foreach (string input in inputList)
            {
                x = 0;
                input.ToList().ForEach(e => positionList.Add(new Day8Position
                {
                    X = x++,
                    Y = y,
                    AntennaName = ((e == '.' || e == '#') ? null : e),
                    IsAntinode = false,
                    IsAntenna = ((e == '.' || e == '#') ? false : true)
                }));
                y++;
            }

            List<char> signalList = positionList.Where(w => w.IsAntenna).Select(s => (char)s.AntennaName).Distinct().ToList();
            foreach (char signal in signalList)
            {
                List<Day8Position> antennaList = positionList.Where(w => w.AntennaName == signal).ToList();
                foreach (Day8Position antenna in antennaList)
                {
                    foreach (Day8Position otherAntenna in antennaList.Where(w => !(w.X == antenna.X && w.Y == antenna.Y)))
                    {
                        Day8Position antinode = otherAntenna;
                        int distanceX = otherAntenna.X - antenna.X;
                        int distanceY = otherAntenna.Y - antenna.Y;
                        while (antinode != null)
                        {
                            antinode.IsAntinode = true;
                            antinode = positionList.FirstOrDefault(w => w.X == antinode.X + distanceX && w.Y == antinode.Y + distanceY);
                        }
                        antinode = antenna;
                        while (antinode != null)
                        {
                            antinode.IsAntinode = true;
                            antinode = positionList.FirstOrDefault(w => w.X == antinode.X - distanceX && w.Y == antinode.Y - distanceY);
                        }
                    }
                }
            }
            Debug.WriteLine(positionList.Count(c => c.IsAntinode));
        }

        [TestMethod]
        public void Day9_1()
        {
            string input = File.ReadAllLines(@"Input\Day9.txt").First();
            List<int> diskData = new List<int>();
            int fileId = 1;
            int i = 0;
            foreach (char c in input)
            {
                int val = int.Parse(c.ToString());
                if (i % 2 == 0) // file
                {
                    for (int x = 0; x < val; x++)
                        diskData.Add(fileId);
                    fileId++;
                }
                else // free space
                    for (int x = 0; x < val; x++)
                        diskData.Add(0);
                i++;
            }
            while (diskData.Contains(0))
            {
                int last = diskData.Last();
                diskData.RemoveAt(diskData.Count() - 1);
                if (last > 0)
                {
                    int index = diskData.IndexOf(0);
                    diskData[index] = last;
                }
            }
            long checksum = 0;
            for (int x = 0; x < diskData.Count(); x++)
                checksum += x * (diskData[x] - 1);
            Debug.WriteLine(checksum);
        }

        [TestMethod]
        public void Day9_2()
        {
            string input = File.ReadAllLines(@"Input\Day9.txt").First();
            List<int> diskData = new List<int>();
            int fileId = 1;
            int i = 0;
            foreach (char c in input)
            {
                int val = int.Parse(c.ToString());
                if (i % 2 == 0) // file
                {
                    for (int x = 0; x < val; x++)
                        diskData.Add(fileId);
                    fileId++;
                }
                else // free space
                    for (int x = 0; x < val; x++)
                        diskData.Add(0);
                i++;
            }
            for (int x = diskData.Max(); x > 0; x--)
            {
                int currentLocation = diskData.IndexOf(x);
                int count = diskData.Where(w => w == x).Count();
                int emptyCount = 0;
                int moveIndex = -1;
                for (int y = 0; y < diskData.Count(); y++)
                {
                    if (currentLocation <= y)
                    {
                        emptyCount = 0;
                        moveIndex = -1;
                        break;
                    }
                    if (diskData[y] == 0)
                    {
                        if (emptyCount == 0)
                            moveIndex = y;
                        emptyCount++;
                    }
                    else
                    {
                        emptyCount = 0;
                        moveIndex = -1;
                    }
                    if (emptyCount == count)
                        break;
                }
                if (moveIndex > -1)
                {
                    for (int p = 0; p < count; p++)
                    {
                        diskData[moveIndex + p] = x;
                        diskData[diskData.LastIndexOf(x)] = 0;
                    }
                }
            }
            long checksum = 0;
            for (int x = 0; x < diskData.Count(); x++)
            {
                if (diskData[x] > 0)
                    checksum += x * (diskData[x] - 1);
            }
            Debug.WriteLine(checksum);
        }

        [TestMethod]
        public void Day10_1()
        {
            List<string> inputList = File.ReadAllLines(@"Input\Day10.txt").ToList();
            List<Day10Position> map = new List<Day10Position>();
            int x = 0, y = 0;
            foreach (string input in inputList)
            {
                x = 0;
                input.ToList().ForEach(e => map.Add(new Day10Position
                {
                    X = x++,
                    Y = y,
                    Height = int.Parse(e.ToString()),
                    Visisted = false
                }));
                y++;
            }
            int sum = 0;
            foreach (Day10Position trailhead in map.Where(w => w.Height == 0))
            {
                Day10Step(map, trailhead);
                sum += map.Count(c => c.Height == 9 && c.Visisted);
                //Debug.WriteLine(trailhead.X + " " + trailhead.Y + " " + map.Count(c => c.Height == 9 && c.Visisted));
                map.ForEach(e => e.Visisted = false);
            }
            Debug.WriteLine(sum);
        }

        private class Day10Position
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Height { get; set; }
            public bool Visisted { get; set; }
        }

        private void Day10Step(List<Day10Position> map, Day10Position currentPosition)
        {
            if (currentPosition.Height == 9)
            {
                currentPosition.Visisted = true;
                return;
            }
            List<Day10Position> possibleSteps = map.Where(w => Math.Abs(w.X - currentPosition.X) + Math.Abs(w.Y - currentPosition.Y) == 1
            && w.Height == (currentPosition.Height + 1) && !w.Visisted).ToList();
            foreach (Day10Position step in possibleSteps)
                Day10Step(map, step);
            return;
        }

        [TestMethod]
        public void Day10_2()
        {
            List<string> inputList = File.ReadAllLines(@"Input\Day10.txt").ToList();
            List<Day10_2Position> map = new List<Day10_2Position>();
            int x = 0, y = 0;
            foreach (string input in inputList)
            {
                x = 0;
                input.ToList().ForEach(e => map.Add(new Day10_2Position
                {
                    X = x++,
                    Y = y,
                    Height = int.Parse(e.ToString()),
                    VisitedCount = 0
                }));
                y++;
            }
            int sum = 0;
            foreach (Day10_2Position trailhead in map.Where(w => w.Height == 0))
            {
                Day10_2Step(map, trailhead);
                sum += map.Where(c => c.Height == 9).Sum(s => s.VisitedCount);
                Debug.WriteLine(trailhead.X + " " + trailhead.Y + " " + map.Where(c => c.Height == 9).Sum(s => s.VisitedCount));
                map.ForEach(e => e.VisitedCount = 0);
            }
            Debug.WriteLine(sum);
        }

        private class Day10_2Position
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Height { get; set; }
            public int VisitedCount { get; set; }
        }

        private void Day10_2Step(List<Day10_2Position> map, Day10_2Position currentPosition)
        {
            if (currentPosition.Height == 9)
            {
                currentPosition.VisitedCount++;
                return;
            }
            List<Day10_2Position> possibleSteps = map.Where(w => Math.Abs(w.X - currentPosition.X) + Math.Abs(w.Y - currentPosition.Y) == 1
            && w.Height == (currentPosition.Height + 1)).ToList();
            foreach (Day10_2Position step in possibleSteps)
                Day10_2Step(map, step);
            return;
        }

        [TestMethod]
        public void Day11_1()
        {
            string inputList = File.ReadAllLines(@"Input\Day11.txt").First();
            List<long> stoneList = inputList.Split(' ').Select(s => long.Parse(s)).ToList();
            for (int i = 0; i < 25; i++)
            {
                List<long> newStoneList = new List<long>();
                foreach (long stone in stoneList)
                {
                    if (stone == 0)
                        newStoneList.Add(1);
                    else if (stone.ToString().Count() % 2 == 0)
                    {
                        newStoneList.Add(long.Parse(stone.ToString().Substring(0, stone.ToString().Count() / 2)));
                        newStoneList.Add(long.Parse(stone.ToString().Substring(stone.ToString().Count() / 2)));
                    }
                    else
                        newStoneList.Add(stone * 2024);
                }
                stoneList = newStoneList;
            }
            Debug.WriteLine(stoneList.Count());
        }

        [TestMethod]
        public void Day11_2()
        {
            string inputList = File.ReadAllLines(@"Input\Day11.txt").First();
            List<long> stoneList = inputList.Split(' ').Select(s => long.Parse(s)).ToList();
            long stoneCount = 0;
            foreach (long stone in stoneList)
                stoneCount += Day11Blink(stone, 0);
            Debug.WriteLine(stoneCount); // 235571309320764
        }

        Dictionary<string, long> _day11_2KnownStones = new Dictionary<string, long>();

        private long Day11Blink(long stone, int blinks)
        {
            long knownValue;
            long stoneCount = 0;
            if (_day11_2KnownStones.TryGetValue(stone + "_" + blinks, out knownValue))
                return knownValue;
            if (blinks == 75)
                return 1;
            if (stone == 0)
                stoneCount = Day11Blink(stone + 1, blinks + 1);
            else if (stone.ToString().Count() % 2 == 0)
            {
                stoneCount += Day11Blink(long.Parse(stone.ToString().Substring(0, stone.ToString().Count() / 2)), blinks + 1);
                stoneCount += Day11Blink(long.Parse(stone.ToString().Substring(stone.ToString().Count() / 2)), blinks + 1);
            }
            else
                stoneCount = Day11Blink(stone * 2024, blinks + 1);
            _day11_2KnownStones.Add(stone + "_" + blinks, stoneCount);
            return stoneCount;
        }

        [TestMethod]
        public void Day12_1()
        {
            List<string> inputList = File.ReadAllLines(@"Input\Day12.txt").ToList();
            List<Day12GardenPlot> gardenPlotList = new List<Day12GardenPlot>();
            int x = 0, y = 0;
            foreach (string input in inputList)
            {
                x = 0;
                input.ToList().ForEach(e => gardenPlotList.Add(new Day12GardenPlot { X = x++, Y = y, Crop = e, RegionId = -1 }));
                y++;
            }
            int newRegionId = -1;
            while (gardenPlotList.Any(a => a.RegionId == -1))
            {
                newRegionId++;
                Day12GardenPlot plot = gardenPlotList.First(w => w.RegionId == -1);
                Day12DefineRegion(gardenPlotList, plot, newRegionId);
            }
            int sumCost = 0;
            foreach (int regionId in gardenPlotList.Select(s => s.RegionId).Distinct())
            {
                List<Day12GardenPlot> region = gardenPlotList.Where(w => w.RegionId == regionId).ToList();
                int area = region.Count();
                int perimiter = 0;
                foreach (Day12GardenPlot plot in region)
                    perimiter += 4 - region.Where(w => Math.Abs(w.X - plot.X) + Math.Abs(w.Y - plot.Y) == 1).Count();
                sumCost += area * perimiter;
                Debug.WriteLine("RegionId: " + regionId + " Crop: " + region.First().Crop + " Area: " + area + " Perimiter: " + perimiter + " Cost: " + area * perimiter);
            }
            Debug.WriteLine(sumCost);
        }

        private void Day12DefineRegion(List<Day12GardenPlot> gardenPlotList, Day12GardenPlot plot, int regionId)
        {
            plot.RegionId = regionId;
            List<Day12GardenPlot> neighbours = gardenPlotList.Where(w => Math.Abs(w.X - plot.X) + Math.Abs(w.Y - plot.Y) == 1 && w.Crop == plot.Crop && w.RegionId == -1).ToList();
            foreach (Day12GardenPlot neighbour in neighbours)
                Day12DefineRegion(gardenPlotList, neighbour, regionId);
        }

        public class Day12GardenPlot
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int RegionId { get; set; }
            public Char Crop { get; set; }
        }

        [TestMethod]
        public void Day12_2()
        {
            List<string> inputList = File.ReadAllLines(@"Input\Day12.txt").ToList();
            List<Day12GardenPlot> gardenPlotList = new List<Day12GardenPlot>();
            int x = 0, y = 0;
            foreach (string input in inputList)
            {
                x = 0;
                input.ToList().ForEach(e => gardenPlotList.Add(new Day12GardenPlot { X = x++, Y = y, Crop = e, RegionId = -1 }));
                y++;
            }
            int newRegionId = -1;
            while (gardenPlotList.Any(a => a.RegionId == -1))
            {
                newRegionId++;
                Day12GardenPlot plot = gardenPlotList.First(w => w.RegionId == -1);
                Day12DefineRegion(gardenPlotList, plot, newRegionId);
            }
            int sumCost = 0;
            foreach (int regionId in gardenPlotList.Select(s => s.RegionId).Distinct())
            {
                List<Day12GardenPlot> region = gardenPlotList.Where(w => w.RegionId == regionId).ToList();
                int area = region.Count();
                int sides = 0;
                for (y = region.Min(m => m.Y); y <= region.Max(m => m.Y); y++)
                {
                    bool topSideFound = false;
                    bool bottomSideFound = false;
                    for (x = region.Min(m => m.X); x <= region.Max(m => m.X); x++)
                    {
                        Day12GardenPlot plot = region.FirstOrDefault(w => w.X == x && w.Y == y);
                        if (plot == null)
                        {
                            topSideFound = false;
                            bottomSideFound = false;
                        }
                        else
                        {
                            bool topPlotExist = region.Any(w => w.X == plot.X && w.Y == (plot.Y - 1));
                            bool bottomPlotExist = region.Any(w => w.X == plot.X && w.Y == (plot.Y + 1));
                            if (!topSideFound && !topPlotExist)
                            {
                                sides++;
                                topSideFound = true;
                            }
                            if (!bottomSideFound && !bottomPlotExist)
                            {
                                sides++;
                                bottomSideFound = true;
                            }
                            if (topPlotExist)
                                topSideFound = false;
                            if (bottomPlotExist)
                                bottomSideFound = false;
                        }
                    }
                }
                for (x = region.Min(m => m.X); x <= region.Max(m => m.X); x++)
                {
                    bool leftSideFound = false;
                    bool rightSideFound = false;
                    for (y = region.Min(m => m.Y); y <= region.Max(m => m.Y); y++)
                    {
                        Day12GardenPlot plot = region.FirstOrDefault(w => w.X == x && w.Y == y);
                        if (plot == null)
                        {
                            leftSideFound = false;
                            rightSideFound = false;
                        }
                        else
                        {
                            bool leftPlotExist = region.Any(w => w.X == (plot.X - 1) && w.Y == plot.Y);
                            bool rightPlotExist = region.Any(w => w.X == (plot.X + 1) && w.Y == plot.Y);
                            if (!leftSideFound && !leftPlotExist)
                            {
                                sides++;
                                leftSideFound = true;
                            }
                            if (!rightSideFound && !rightPlotExist)
                            {
                                sides++;
                                rightSideFound = true;
                            }
                            if (leftPlotExist)
                                leftSideFound = false;
                            if (rightPlotExist)
                                rightSideFound = false;
                        }
                    }
                }
                sumCost += area * sides;
                Debug.WriteLine("RegionId: " + regionId + " Crop: " + region.First().Crop + " Area: " + area + " Sides: " + sides + " Cost: " + area * sides);
            }
            Debug.WriteLine(sumCost);
        }

        [TestMethod]
        public void Day13_1()
        {
            List<string> inputList = File.ReadAllLines(@"Input\Day13.txt").ToList();
            bool newMachine = true;
            List<Day13Machine> machineList = new List<Day13Machine>();
            Day13Machine machinePrep = null;
            foreach (string input in inputList)
            {
                if (newMachine)
                {
                    machinePrep = new Day13Machine();
                    machinePrep.MinTokens = long.MaxValue;
                    newMachine = false;
                    machineList.Add(machinePrep);
                }
                if (input.Contains("Button A"))
                {
                    machinePrep.ButtonA_X = int.Parse(new string(input.Split(',')[0].Where(w => char.IsDigit(w)).ToArray()));
                    machinePrep.ButtonA_Y = int.Parse(new string(input.Split(',')[1].Where(w => char.IsDigit(w)).ToArray()));
                }
                else if (input.Contains("Button B"))
                {
                    machinePrep.ButtonB_X = int.Parse(new string(input.Split(',')[0].Where(w => char.IsDigit(w)).ToArray()));
                    machinePrep.ButtonB_Y = int.Parse(new string(input.Split(',')[1].Where(w => char.IsDigit(w)).ToArray()));
                }
                else if (input.Contains("Prize"))
                {
                    machinePrep.PrizeX = int.Parse(new string(input.Split(',')[0].Where(w => char.IsDigit(w)).ToArray()));
                    machinePrep.PrizeY = int.Parse(new string(input.Split(',')[1].Where(w => char.IsDigit(w)).ToArray()));
                }
                else
                    newMachine = true;
            }
            foreach (Day13Machine machine in machineList)
            {
                long x = 0, y = 0;
                for (long b = 0; b <= 100; b++)
                {
                    for (long a = 0; a <= 100; a++)
                    {
                        x = b * machine.ButtonB_X + a * machine.ButtonA_X;
                        y = b * machine.ButtonB_Y + a * machine.ButtonA_Y;
                        if (x == machine.PrizeX && y == machine.PrizeY && ((a * 3 + b * 1) < machine.MinTokens))
                            machine.MinTokens = (a * 3 + b * 1);
                    }
                }
            }
            Debug.WriteLine(machineList.Where(w => w.MinTokens != int.MaxValue).Sum(s => s.MinTokens));
        }

        private class Day13Machine
        {
            public long ButtonA_X { get; set; }
            public long ButtonA_Y { get; set; }
            public long ButtonB_X { get; set; }
            public long ButtonB_Y { get; set; }
            public long PrizeX { get; set; }
            public long PrizeY { get; set; }
            public long MinTokens { get; set; }
        }

        [TestMethod]
        public void Day13_2()
        {
            List<string> inputList = File.ReadAllLines(@"Input\Day13.txt").ToList();
            bool newMachine = true;
            List<Day13Machine> machineList = new List<Day13Machine>();
            Day13Machine machinePrep = null;
            foreach (string input in inputList)
            {
                if (newMachine)
                {
                    machinePrep = new Day13Machine();
                    machinePrep.MinTokens = long.MaxValue;
                    newMachine = false;
                    machineList.Add(machinePrep);
                }
                if (input.Contains("Button A"))
                {
                    machinePrep.ButtonA_X = int.Parse(new string(input.Split(',')[0].Where(w => char.IsDigit(w)).ToArray()));
                    machinePrep.ButtonA_Y = int.Parse(new string(input.Split(',')[1].Where(w => char.IsDigit(w)).ToArray()));
                }
                else if (input.Contains("Button B"))
                {
                    machinePrep.ButtonB_X = int.Parse(new string(input.Split(',')[0].Where(w => char.IsDigit(w)).ToArray()));
                    machinePrep.ButtonB_Y = int.Parse(new string(input.Split(',')[1].Where(w => char.IsDigit(w)).ToArray()));
                }
                else if (input.Contains("Prize"))
                {
                    machinePrep.PrizeX = int.Parse(new string(input.Split(',')[0].Where(w => char.IsDigit(w)).ToArray())) + 10000000000000;
                    machinePrep.PrizeY = int.Parse(new string(input.Split(',')[1].Where(w => char.IsDigit(w)).ToArray())) + 10000000000000;
                }
                else
                    newMachine = true;
            }
            foreach (Day13Machine machine in machineList)
            {
                double delta = machine.ButtonA_X * machine.ButtonB_Y - machine.ButtonA_Y * machine.ButtonB_X;
                if (delta == 0)
                    continue;
                double aPresses = (machine.ButtonB_Y * machine.PrizeX - machine.ButtonB_X * machine.PrizeY) / delta;
                double bPresses = (machine.ButtonA_X * machine.PrizeY - machine.ButtonA_Y * machine.PrizeX) / delta;
                if (aPresses % 1 != 0 || bPresses % 1 != 0)
                    continue;
                machine.MinTokens = (long)aPresses * 3 + (long)bPresses * 1;
            }
            Debug.WriteLine(machineList.Where(w => w.MinTokens != long.MaxValue).Sum(s => s.MinTokens));
        }

        [TestMethod]
        public void Day14_1()
        {
            List<string> inputList = File.ReadAllLines(@"Input\Day14.txt").ToList();
            List<Day14Robot> robotList = new List<Day14Robot>();
            foreach (string input in inputList)
            {
                string[] inputSplit = input.Split(' ');
                robotList.Add(new Day14Robot
                {
                    X = int.Parse(new string(inputSplit[0].Split(',')[0].Skip(2).ToArray())),
                    Y = int.Parse(inputSplit[0].Split(',')[1]),
                    VX = int.Parse(new string(inputSplit[1].Split(',')[0].Skip(2).ToArray())),
                    VY = int.Parse(inputSplit[1].Split(',')[1]),
                });
            }
            int maxX = 101;
            int maxY = 103;
            for (int i = 0; i < 100; i++)
            {
                foreach (Day14Robot robot in robotList)
                {
                    robot.X = (robot.X + robot.VX) % maxX;
                    robot.Y = (robot.Y + robot.VY) % maxY;
                    if (robot.X < 0)
                        robot.X += maxX;
                    if (robot.Y < 0)
                        robot.Y += maxY;
                }
            }
            int sumRobots = robotList.Count(w => w.X < maxX / 2 && w.Y < maxY / 2);
            sumRobots *= robotList.Count(w => w.X < maxX / 2 && w.Y > maxY / 2);
            sumRobots *= robotList.Count(w => w.X > maxX / 2 && w.Y < maxY / 2);
            sumRobots *= robotList.Count(w => w.X > maxX / 2 && w.Y > maxY / 2);
            Debug.WriteLine(sumRobots);
        }

        private class Day14Robot
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int VX { get; set; }
            public int VY { get; set; }
        }

        [TestMethod]
        public void Day14_2()
        {
            List<string> inputList = File.ReadAllLines(@"Input\Day14.txt").ToList();
            List<Day14Robot> robotList = new List<Day14Robot>();
            foreach (string input in inputList)
            {
                string[] inputSplit = input.Split(' ');
                robotList.Add(new Day14Robot
                {
                    X = int.Parse(new string(inputSplit[0].Split(',')[0].Skip(2).ToArray())),
                    Y = int.Parse(inputSplit[0].Split(',')[1]),
                    VX = int.Parse(new string(inputSplit[1].Split(',')[0].Skip(2).ToArray())),
                    VY = int.Parse(inputSplit[1].Split(',')[1]),
                });
            }
            int maxX = 101;
            int maxY = 103;
            bool treeFound = false;
            int seconds = 0;
            while (!treeFound)
            {
                seconds++;
                foreach (Day14Robot robot in robotList)
                {
                    robot.X = (robot.X + robot.VX) % maxX;
                    robot.Y = (robot.Y + robot.VY) % maxY;
                    if (robot.X < 0)
                        robot.X += maxX;
                    if (robot.Y < 0)
                        robot.Y += maxY;
                }
                if (!robotList.GroupBy(g => new { g.X, g.Y }).Where(w => w.Count() > 1).Any())
                {
                    for (int y = 0; y < maxY; y++)
                    {
                        string row = string.Empty;
                        for (int x = 0; x < maxX; x++)
                            row += robotList.Any(a => a.X == x && a.Y == y) ? "X" : ".";
                        Debug.WriteLine(row + Environment.NewLine);
                    }
                    Debug.WriteLine("Seconds: " + seconds);
                    treeFound = true;
                }
            }
        }

        [TestMethod]
        public void Day15_1()
        {
            List<string> inputList = File.ReadAllLines(@"Input\Day15.txt").ToList();
            List<Day15Coordinate> warehouse = new List<Day15Coordinate>();
            List<char> instructions = new List<char>();
            int x = 0, y = 0;
            bool warehouseParsed = false;
            int robotX = 0, robotY = 0;
            foreach (string input in inputList)
            {
                if (string.IsNullOrWhiteSpace(input))
                {
                    warehouseParsed = true;
                    continue;
                }
                if (!warehouseParsed)
                {
                    x = 0;
                    foreach (char position in input)
                    {
                        if (position == '@')
                        {
                            robotX = x;
                            robotY = y;
                        }
                        warehouse.Add(new Day15Coordinate { X = x++, Y = y, IsBox = position == 'O', IsWall = position == '#' });
                    }
                    y++;
                }
                else
                    input.ToList().ForEach(e => instructions.Add(e));
            }
            foreach (char instruction in instructions)
            {
                Day15Coordinate position = warehouse.First(w => (instruction == '<' && w.X == robotX - 1 && w.Y == robotY)
                    || (instruction == '^' && w.X == robotX && w.Y == robotY - 1)
                    || (instruction == '>' && w.X == robotX + 1 && w.Y == robotY)
                    || (instruction == 'v' && w.X == robotX && w.Y == robotY + 1));
                if (position.IsWall)
                    continue;
                if (!position.IsBox)
                {
                    robotX = position.X;
                    robotY = position.Y;
                }
                else
                {
                    List<Day15Coordinate> line = warehouse.Where(w => (instruction == '<' && w.X < robotX && w.Y == robotY)
                        || (instruction == '^' && w.X == robotX && w.Y < robotY)
                        || (instruction == '>' && w.X > robotX && w.Y == robotY)
                        || (instruction == 'v' && w.X == robotX && w.Y > robotY)).ToList();
                    if (instruction == '<')
                        line = line.OrderByDescending(o => o.X).ToList();
                    else if (instruction == '^')
                        line = line.OrderByDescending(o => o.Y).ToList();
                    else if (instruction == 'v')
                        line = line.OrderBy(o => o.Y).ToList();
                    else if (instruction == '>')
                        line = line.OrderBy(o => o.X).ToList();
                    bool movePossible = false;
                    Day15Coordinate lastLinePosition = null;
                    foreach (Day15Coordinate linePosition in line)
                    {
                        if (linePosition.IsWall)
                            break;
                        if (!linePosition.IsBox)
                        {
                            movePossible = true;
                            lastLinePosition = linePosition;
                            break;
                        }
                    }
                    if (movePossible)
                    {
                        lastLinePosition.IsBox = true;
                        position.IsBox = false;
                        robotX = position.X;
                        robotY = position.Y;
                    }
                }
            }
            int sumGps = warehouse.Where(w => w.IsBox).Sum(s => s.Y * 100 + s.X);
            Debug.WriteLine(sumGps);
        }

        private void Day15Print(List<Day15Coordinate> warehouse)
        {
            int previousY = 0;
            string output = string.Empty;
            foreach (Day15Coordinate coordinate in warehouse.OrderBy(o => o.Y).ThenBy(t => t.X))
            {
                if (previousY != coordinate.Y)
                    output += Environment.NewLine;
                if (coordinate.IsBox)
                    output += "O";
                else if (coordinate.IsWall)
                    output += "#";
                else
                    output += ".";
                previousY = coordinate.Y;
            }
            Debug.WriteLine(output);
        }

        private class Day15Coordinate
        {
            public int X { get; set; }
            public int Y { get; set; }
            public bool IsWall { get; set; }
            public bool IsBox { get; set; }
        }

        private class Day15_2Coordinate
        {
            public int X { get; set; }
            public int Y { get; set; }
            public bool IsWall { get; set; }
            public bool IsLeftBox { get; set; }
            public bool IsRightBox { get; set; }
            public bool IsBox { get; set; }
            public int BoxId { get; set; }
        }

        [TestMethod]
        public void Day15_2()
        {
            List<string> inputList = File.ReadAllLines(@"Input\Day15.txt").ToList();
            List<Day15_2Coordinate> warehouse = new List<Day15_2Coordinate>();
            List<char> instructions = new List<char>();
            int x = 0, y = 0;
            bool warehouseParsed = false;
            int robotX = 0, robotY = 0;
            int boxId = 0;
            foreach (string input in inputList)
            {
                if (string.IsNullOrWhiteSpace(input))
                {
                    warehouseParsed = true;
                    continue;
                }
                if (!warehouseParsed)
                {
                    x = 0;
                    foreach (char position in input)
                    {
                        if (position == '@')
                        {
                            robotX = x;
                            robotY = y;
                            warehouse.Add(new Day15_2Coordinate { X = x++, Y = y, IsBox = false, IsWall = false, IsLeftBox = false, IsRightBox = false, BoxId = -1 });
                            warehouse.Add(new Day15_2Coordinate { X = x++, Y = y, IsBox = false, IsWall = false, IsLeftBox = false, IsRightBox = false, BoxId = -1 });
                        }
                        else
                        {
                            warehouse.Add(new Day15_2Coordinate { X = x++, Y = y, IsBox = position == 'O', IsLeftBox = position == 'O', IsRightBox = false, IsWall = position == '#', BoxId = position == 'O' ? boxId : -1 });
                            warehouse.Add(new Day15_2Coordinate { X = x++, Y = y, IsBox = position == 'O', IsLeftBox = false, IsRightBox = position == 'O', IsWall = position == '#', BoxId = position == 'O' ? boxId++ : -1 });
                        }
                    }
                    y++;
                }
                else
                    input.ToList().ForEach(e => instructions.Add(e));
            }
            int moveNr = 0;
            foreach (char instruction in instructions)
            {
                moveNr++;
                Day15_2Coordinate position = warehouse.First(w => (instruction == '<' && w.X == robotX - 1 && w.Y == robotY)
                    || (instruction == '^' && w.X == robotX && w.Y == robotY - 1)
                    || (instruction == '>' && w.X == robotX + 1 && w.Y == robotY)
                    || (instruction == 'v' && w.X == robotX && w.Y == robotY + 1));
                if (position.IsWall)
                    continue;
                if (!position.IsBox)
                {
                    robotX = position.X;
                    robotY = position.Y;
                }
                else
                {
                    if (Day15_2CanMove(warehouse, warehouse.Where(w => w.BoxId == position.BoxId).ToList(), instruction))
                    {
                        Day15_2Move(warehouse, warehouse.Where(w => w.BoxId == position.BoxId).ToList(), instruction);
                        robotX = position.X;
                        robotY = position.Y;
                    }
                }
                //Debug.WriteLine("MoveNr: " + moveNr);
                //Day15_2Print(warehouse, robotX, robotY);
            }
            int sumGps = warehouse.Where(w => w.IsBox && w.IsLeftBox).Sum(s => s.Y * 100 + s.X);
            Debug.WriteLine(sumGps); // 1522420
        }

        private void Day15_2Print(List<Day15_2Coordinate> warehouse, int robotX, int robotY)
        {
            int previousY = 0;
            string output = string.Empty;
            foreach (Day15_2Coordinate coordinate in warehouse.OrderBy(o => o.Y).ThenBy(t => t.X))
            {
                if (previousY != coordinate.Y)
                    output += Environment.NewLine;
                if (coordinate.IsLeftBox)
                    output += "[";
                else if (coordinate.IsRightBox)
                    output += "]";
                else if (coordinate.IsWall)
                    output += "#";
                else
                {
                    if (coordinate.X == robotX && coordinate.Y == robotY)
                        output += "@";
                    else
                        output += ".";
                }
                previousY = coordinate.Y;
            }
            Debug.WriteLine(output);
        }

        private bool Day15_2CanMove(List<Day15_2Coordinate> warehouse, List<Day15_2Coordinate> currentBox, char instruction)
        {
            int oldBoxId = currentBox.First().BoxId;
            List<Day15_2Coordinate> newPosition = new List<Day15_2Coordinate>();
            if (instruction == '<')
                newPosition = warehouse.Where(w => (w.X == currentBox.Min(m => m.X) - 1 || w.X == currentBox.Min(m => m.X)) && w.Y == currentBox.Min(m => m.Y)).ToList();
            else if (instruction == '^')
                newPosition = warehouse.Where(w => (w.X == currentBox.Max(m => m.X) || w.X == currentBox.Min(m => m.X)) && w.Y == currentBox.Min(m => m.Y) - 1).ToList();
            else if (instruction == 'v')
                newPosition = warehouse.Where(w => (w.X == currentBox.Max(m => m.X) || w.X == currentBox.Min(m => m.X)) && w.Y == currentBox.Min(m => m.Y) + 1).ToList();
            else
                newPosition = warehouse.Where(w => (w.X == currentBox.Max(m => m.X) + 1 || w.X == currentBox.Max(m => m.X)) && w.Y == currentBox.Min(m => m.Y)).ToList();
            if (newPosition.Any(a => a.IsWall))
                return false;
            bool canMove = true;
            foreach (int boxId in newPosition.Where(w => w.BoxId != -1 && w.BoxId != oldBoxId).Select(s => s.BoxId).Distinct())
                if (!Day15_2CanMove(warehouse, warehouse.Where(w => w.BoxId == boxId).ToList(), instruction))
                    canMove = false;
            return canMove;
        }

        private void Day15_2Move(List<Day15_2Coordinate> warehouse, List<Day15_2Coordinate> currentBox, char instruction)
        {
            int oldBoxId = currentBox.First().BoxId;
            List<Day15_2Coordinate> newPosition = new List<Day15_2Coordinate>();
            if (instruction == '<')
                newPosition = warehouse.Where(w => (w.X == currentBox.Min(m => m.X) - 1 || w.X == currentBox.Min(m => m.X)) && w.Y == currentBox.Min(m => m.Y)).ToList();
            else if (instruction == '^')
                newPosition = warehouse.Where(w => (w.X == currentBox.Max(m => m.X) || w.X == currentBox.Min(m => m.X)) && w.Y == currentBox.Min(m => m.Y) - 1).ToList();
            else if (instruction == 'v')
                newPosition = warehouse.Where(w => (w.X == currentBox.Max(m => m.X) || w.X == currentBox.Min(m => m.X)) && w.Y == currentBox.Min(m => m.Y) + 1).ToList();
            else
                newPosition = warehouse.Where(w => (w.X == currentBox.Max(m => m.X) + 1 || w.X == currentBox.Max(m => m.X)) && w.Y == currentBox.Min(m => m.Y)).ToList();
            foreach (int boxId in newPosition.Where(w => w.BoxId != -1 && w.BoxId != oldBoxId).Select(s => s.BoxId).Distinct())
                Day15_2Move(warehouse, warehouse.Where(w => w.BoxId == boxId).ToList(), instruction);
            Day15_2Coordinate leftBox = newPosition.OrderBy(o => o.X).First();
            Day15_2Coordinate rightBox = newPosition.OrderByDescending(o => o.X).First();
            currentBox.ForEach(e =>
            {
                e.IsBox = false;
                e.IsLeftBox = false;
                e.IsRightBox = false;
                e.BoxId = -1;
            });
            leftBox.BoxId = oldBoxId;
            leftBox.IsLeftBox = true;
            leftBox.IsRightBox = false;
            leftBox.IsBox = true;
            rightBox.BoxId = oldBoxId;
            rightBox.IsLeftBox = false;
            rightBox.IsRightBox = true;
            rightBox.IsBox = true;
        }

        [TestMethod]
        public void Day16_1()
        {
            List<string> inputList = File.ReadAllLines(@"Input\Day16.txt").ToList();
            List<Day16Pos> maze = new List<Day16Pos>();
            int x = 0, y = 0;
            foreach (string input in inputList)
            {
                x = 0;
                foreach (char c in input)
                {
                    if (c != '#')
                        maze.Add(new Day16Pos { X = x, Y = y, End = c == 'E', Start = c == 'S' });
                    x++;
                }
                y++;
            }
            Debug.WriteLine(Day16CalculateBestPath(maze).First().Points);
        }

        private List<Day16QueueItem> Day16CalculateBestPath(List<Day16Pos> maze)
        {
            PriorityQueue<Day16QueueItem, int> queue = new();
            Dictionary<string, int> knownLowestPoints = new Dictionary<string, int>();
            Day16Pos start = maze.First(w => w.Start);
            Day16Pos end = maze.First(w => w.End);
            queue.Enqueue(new Day16QueueItem { Pos = start, Direction = 3, Points = 0, Visited = new HashSet<string> { start.X + "-" + start.Y } }, 0);
            int dummy;
            int bestPoints = int.MaxValue;
            List<Day16QueueItem> bestPatchs = new List<Day16QueueItem>();
            Day16QueueItem pos;
            while (queue.TryDequeue(out pos, out dummy))
            {
                if (pos == null)
                    return bestPatchs;
                if (pos.Points > bestPoints)
                    continue;
                if (pos.Pos.End)
                {
                    if (bestPoints != pos.Points)
                        bestPatchs.Clear();
                    bestPatchs.Add(pos);
                    bestPoints = pos.Points;
                    Debug.WriteLine("New best points: " + bestPoints);
                    continue;
                }
                List<Day16Pos> possibleSteps = maze.Where(w => Math.Abs(w.X - pos.Pos.X) + Math.Abs(w.Y - pos.Pos.Y) == 1 && !pos.Visited.Contains(w.X + "-" + w.Y)).ToList();
                foreach (Day16Pos step in possibleSteps)
                {
                    // direction: 1 = left 2 = up 3 = right 4 = down
                    Day16QueueItem newQueueItem = new Day16QueueItem();
                    newQueueItem.Direction = step.X < pos.Pos.X ? 1 : step.X > pos.Pos.X ? 3 : step.Y < pos.Pos.Y ? 2 : 4;
                    newQueueItem.Points = newQueueItem.Direction == pos.Direction ? pos.Points + 1 : Math.Abs(newQueueItem.Direction - pos.Direction) != 2 ? pos.Points + 1001 : pos.Points + 2001;
                    int value;
                    string key = step.X + "-" + step.Y + "-" + newQueueItem.Direction;
                    if (knownLowestPoints.TryGetValue(step.X + "-" + step.Y + "-" + newQueueItem.Direction, out value))
                    {
                        if (value < newQueueItem.Points)
                            continue;
                        knownLowestPoints[key] = newQueueItem.Points;
                    }
                    else
                        knownLowestPoints.Add(key, newQueueItem.Points);
                    newQueueItem.Pos = step;
                    pos.Visited.ToList().ForEach(e => newQueueItem.Visited.Add(e));
                    newQueueItem.Visited.Add(step.X + "-" + step.Y);
                    queue.Enqueue(newQueueItem, (Math.Abs(end.X - step.X) + Math.Abs(end.Y - step.Y) * 5000) + newQueueItem.Points);
                }
            }
            return bestPatchs;
        }

        private class Day16Pos
        {
            public int X { get; set; }
            public int Y { get; set; }
            public bool End { get; set; }
            public bool Start { get; set; }
        }

        private class Day16QueueItem
        {
            public Day16Pos Pos { get; set; }
            public int Direction { get; set; }
            public int Points { get; set; }
            public HashSet<string> Visited = new HashSet<string>();
        }

        [TestMethod]
        public void Day16_2()
        {
            List<string> inputList = File.ReadAllLines(@"Input\Day16.txt").ToList();
            List<Day16Pos> maze = new List<Day16Pos>();
            int x = 0, y = 0;
            foreach (string input in inputList)
            {
                x = 0;
                foreach (char c in input)
                {
                    if (c != '#')
                        maze.Add(new Day16Pos { X = x, Y = y, End = c == 'E', Start = c == 'S' });
                    x++;
                }
                y++;
            }
            List<Day16QueueItem> bestPaths = Day16CalculateBestPath(maze);
            HashSet<string> posList = new HashSet<string>();
            foreach (Day16QueueItem path in bestPaths)
                foreach (string step in path.Visited.Where(w => !posList.Contains(w)))
                    posList.Add(step);
            Debug.WriteLine(posList.Count());
        }

        [TestMethod]
        public void Day17_1()
        {
            List<string> inputList = File.ReadAllLines(@"Input\Day17.txt").ToList();
            int registerA = 0;
            int registerB = 0;
            int registerC = 0;
            List<int> program = new List<int>();
            bool registerRead = false;
            foreach (string input in inputList)
            {
                if (string.IsNullOrWhiteSpace(input))
                {
                    registerRead = true;
                    continue;
                }
                if (!registerRead)
                {
                    if (input.Contains("Register A"))
                        registerA = int.Parse(input.Split(':')[1].Trim());
                    else if (input.Contains("Register B"))
                        registerB = int.Parse(input.Split(':')[1].Trim());
                    else if (input.Contains("Register C"))
                        registerC = int.Parse(input.Split(':')[1].Trim());
                }
                else
                    input.Where(w => char.IsDigit(w)).ToList().ForEach(e => program.Add(int.Parse(e.ToString())));
            }
            string output = Day17RunProgram(program, registerA, registerB, registerC);
            Debug.WriteLine(output);
        }

        private string Day17RunProgram(List<int> program, int registerA, int registerB, int registerC)
        {
            List<int> outputList = new List<int>();
            int instructionPointer = 0;
            while (true)
            {
                if (instructionPointer >= program.Count())
                    break;
                int opcode = program[instructionPointer];
                int literalOperand = program[instructionPointer + 1];
                int comboOperand = literalOperand == 4 ? registerA : literalOperand == 5 ? registerB : literalOperand == 6 ? registerC : literalOperand;
                if (opcode == 0)
                    registerA = (int)(registerA / Math.Pow(2, comboOperand));
                else if (opcode == 1)
                    registerB = registerB ^ literalOperand;
                else if (opcode == 2)
                    registerB = comboOperand % 8;
                else if (opcode == 3)
                {
                    if (registerA != 0)
                    {
                        instructionPointer = literalOperand;
                        continue;
                    }
                }
                else if (opcode == 4)
                    registerB = registerB ^ registerC;
                else if (opcode == 5)
                    outputList.Add(comboOperand % 8);
                else if (opcode == 6)
                    registerB = (int)(registerA / Math.Pow(2, comboOperand));
                else if (opcode == 7)
                    registerC = (int)(registerA / Math.Pow(2, comboOperand));
                instructionPointer += 2;
            }
            return string.Join(',', outputList);
        }

        [TestMethod]
        public void Day17_2()
        {
            List<string> inputList = File.ReadAllLines(@"Input\Day17.txt").ToList();
            int registerA = 0;
            int registerB = 0;
            int registerC = 0;
            List<int> program = new List<int>();
            bool registerRead = false;
            string originalProgram = string.Empty;
            foreach (string input in inputList)
            {
                if (string.IsNullOrWhiteSpace(input))
                {
                    registerRead = true;
                    continue;
                }
                if (!registerRead)
                {
                    if (input.Contains("Register A"))
                        registerA = int.Parse(input.Split(':')[1].Trim());
                    else if (input.Contains("Register B"))
                        registerB = int.Parse(input.Split(':')[1].Trim());
                    else if (input.Contains("Register C"))
                        registerC = int.Parse(input.Split(':')[1].Trim());
                }
                else
                {
                    originalProgram = input.Split(':')[1].Trim();
                    input.Where(w => char.IsDigit(w)).ToList().ForEach(e => program.Add(int.Parse(e.ToString())));
                }
            }
            string output = string.Empty;
            registerA = 308241087;
            int recordLength = 0;
            while (output != originalProgram)
            {
                registerA += 33554432;
                //Debug.Write("Testing A: " + registerA);
                output = Day17_2RunProgram(program, registerA, registerB, registerC, originalProgram);
                if (output.Length > recordLength)
                {
                    Debug.WriteLine("NewRecord: " + output + " RecordA: " + registerA);
                    recordLength = output.Length;
                }
            }
            //Day17_FindPattern(program, registerB, registerC, originalProgram);
            Debug.WriteLine(registerA);
        }

        private string Day17_2RunProgram(List<int> program, int registerA, int registerB, int registerC, string originalProgram)
        {
            string output = string.Empty;
            int instructionPointer = 0;
            bool firstOutput = true;
            int loops = 0;
            while (true)
            {
                loops++;
                if (instructionPointer >= program.Count())
                    break;
                int opcode = program[instructionPointer];
                int literalOperand = program[instructionPointer + 1];
                int comboOperand = literalOperand == 4 ? registerA : literalOperand == 5 ? registerB : literalOperand == 6 ? registerC : literalOperand;
                if (opcode == 0)
                    registerA = (int)(registerA / Math.Pow(2, comboOperand));
                else if (opcode == 1)
                    registerB = registerB ^ literalOperand;
                else if (opcode == 2)
                    registerB = comboOperand % 8;
                else if (opcode == 3)
                {
                    if (registerA != 0)
                    {
                        instructionPointer = literalOperand;
                        continue;
                    }
                }
                else if (opcode == 4)
                    registerB = registerB ^ registerC;
                else if (opcode == 5)
                {
                    if (!firstOutput)
                        output += ",";
                    else
                        firstOutput = false;
                    output += comboOperand % 8;
                    if (!originalProgram.StartsWith(output))
                        return output;
                }
                else if (opcode == 6)
                    registerB = (int)(registerA / Math.Pow(2, comboOperand));
                else if (opcode == 7)
                    registerC = (int)(registerA / Math.Pow(2, comboOperand));
                instructionPointer += 2;
                if (loops == 10000)
                    return output;
            }
            return output;
        }



        private List<int> Day17_FindPattern(List<int> program, int registerB, int registerC, string originalProgram)
        {
            bool patternFound = false;
            List<int> diffList = new List<int>();
            string diffOutput = string.Empty;
            List<int> aList = new List<int>();

            int startA = -1;
            int registerA = -1;
            while (!patternFound)
            {
                startA++;
                registerA = startA;
                string output = string.Empty;
                int instructionPointer = 0;
                bool firstOutput = true;
                int loops = 0;
                int originalA = registerA;
                while (true)
                {
                    loops++;
                    if (instructionPointer >= program.Count())
                        break;
                    int opcode = program[instructionPointer];
                    int literalOperand = program[instructionPointer + 1];
                    int comboOperand = literalOperand == 4 ? registerA : literalOperand == 5 ? registerB : literalOperand == 6 ? registerC : literalOperand;
                    if (opcode == 0)
                        registerA = (int)(registerA / Math.Pow(2, comboOperand));
                    else if (opcode == 1)
                        registerB = registerB ^ literalOperand;
                    else if (opcode == 2)
                        registerB = comboOperand % 8;
                    else if (opcode == 3)
                    {
                        if (registerA != 0)
                        {
                            instructionPointer = literalOperand;
                            continue;
                        }
                    }
                    else if (opcode == 4)
                        registerB = registerB ^ registerC;
                    else if (opcode == 5)
                    {
                        if (!firstOutput)
                            output += ",";
                        else
                            firstOutput = false;
                        output += comboOperand % 8;
                        if (originalProgram.StartsWith(output) && output.Length == 13)
                        {
                            if (aList.Count() > 0)
                                diffList.Add(originalA - aList.Last());
                            aList.Add(originalA);

                            Debug.WriteLine("Loop: " + loops + Environment.NewLine
                            + "originalA: " + originalA + Environment.NewLine
                            + "Output: " + output + Environment.NewLine
                            + "DiffList: " + string.Join(',', diffList) + Environment.NewLine
                            );

                            if (diffList.Count() > 40)
                            {
                                if (Regex.Matches(string.Join(',', diffList), string.Join(',', diffList.Take(10))).Count() == 4)
                                {
                                    return diffList.Take(10).ToList();
                                }
                            }
                        }
                        if (!originalProgram.StartsWith(output))
                            break;
                    }
                    else if (opcode == 6)
                        registerB = (int)(registerA / Math.Pow(2, comboOperand));
                    else if (opcode == 7)
                        registerC = (int)(registerA / Math.Pow(2, comboOperand));
                    instructionPointer += 2;
                }
            }
            return null;
        }

        [TestMethod]
        public void Day18_1()
        {
            //List<string> inputList = File.ReadAllLines(@"Input\Day18Test.txt").ToList();
            //int maxX = 6;
            //int maxY = 6;
            //int byteSteps = 12;
            List<string> inputList = File.ReadAllLines(@"Input\Day18.txt").ToList();
            int maxX = 70;
            int maxY = 70;
            int byteSteps = 1024;
            List<Day18Byte> byteList = new List<Day18Byte>();
            int fallStep = 1;
            foreach (string input in inputList)
                byteList.Add(new Day18Byte { X = int.Parse(input.Split(',')[0]), Y = int.Parse(input.Split(',')[1]), FallStep = fallStep++ });
            Debug.Write(Day18CalculateMinSteps(byteList, maxX, maxY, byteSteps));
        }

        Dictionary<string, long> _day18KnownSteps = new Dictionary<string, long>();
        private long Day18CalculateMinSteps(List<Day18Byte> byteList, int maxX, int maxY, int byteSteps)
        {
            PriorityQueue<Day18QueueItem, int> queue = new();
            queue.Enqueue(new Day18QueueItem { X = 0, Y = 0, Steps = 0 }, 0);
            int dummy;
            Day18QueueItem pos;
            long knownStep;
            long minSteps = int.MaxValue;
            while (queue.TryDequeue(out pos, out dummy))
            {
                if (_day18KnownSteps.TryGetValue(pos.X + "-" + pos.Y, out knownStep))
                {
                    if (knownStep <= pos.Steps)
                        continue;
                    _day18KnownSteps[pos.X + "-" + pos.Y] = pos.Steps;
                }
                else
                    _day18KnownSteps.Add(pos.X + "-" + pos.Y, pos.Steps);
                if (pos.X == maxX && pos.Y == maxY && pos.Steps < minSteps)
                    minSteps = pos.Steps;
                if (pos.Steps >= minSteps)
                    continue;

                int newX = pos.X + 1, newY = pos.Y;
                if (newX <= maxX && !byteList.Any(a => a.X == newX && a.Y == newY && a.FallStep <= byteSteps))
                    queue.Enqueue(new Day18QueueItem { X = newX, Y = newY, Steps = pos.Steps + 1 }, maxX - newX + maxY - newY + pos.Steps + 1);
                newX = pos.X - 1;
                newY = pos.Y;
                if (newX >= 0 && !byteList.Any(a => a.X == newX && a.Y == newY && a.FallStep <= byteSteps))
                    queue.Enqueue(new Day18QueueItem { X = newX, Y = newY, Steps = pos.Steps + 1 }, maxX - newX + maxY - newY + pos.Steps + 1);
                newX = pos.X;
                newY = pos.Y + 1;
                if (newY <= maxY && !byteList.Any(a => a.X == newX && a.Y == newY && a.FallStep <= byteSteps))
                    queue.Enqueue(new Day18QueueItem { X = newX, Y = newY, Steps = pos.Steps + 1 }, maxX - newX + maxY - newY + pos.Steps + 1);
                newX = pos.X;
                newY = pos.Y - 1;
                if (newY >= 0 && !byteList.Any(a => a.X == newX && a.Y == newY && a.FallStep <= byteSteps))
                    queue.Enqueue(new Day18QueueItem { X = newX, Y = newY, Steps = pos.Steps + 1 }, maxX - newX + maxY - newY + pos.Steps + 1);
            }
            long result;
            if (_day18KnownSteps.TryGetValue(maxX + "-" + maxY, out result))
                return result;
            else 
                return - 1;
        }

        private class Day18QueueItem
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Steps { get; set; }
        }

        private class Day18Byte
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int FallStep { get; set; }
        }

        [TestMethod]
        public void Day18_2()
        {
            //List<string> inputList = File.ReadAllLines(@"Input\Day18Test.txt").ToList();
            //int maxX = 6;
            //int maxY = 6;
            //int byteSteps = 12;
            List<string> inputList = File.ReadAllLines(@"Input\Day18.txt").ToList();
            int maxX = 70;
            int maxY = 70;
            int byteSteps = 1024;
            List<Day18Byte> byteList = new List<Day18Byte>();
            int fallStep = 1;
            foreach (string input in inputList)
                byteList.Add(new Day18Byte { X = int.Parse(input.Split(',')[0]), Y = int.Parse(input.Split(',')[1]), FallStep = fallStep++ });
            bool noExit = false;
            while (!noExit)
            {
                _day18KnownSteps = new Dictionary<string, long>();
                if (Day18CalculateMinSteps(byteList, maxX, maxY, byteSteps) == -1)
                    break;
                byteSteps++;
            }
            Day18Byte blockerByte = byteList[byteSteps - 1];
            Debug.Write(blockerByte.X + "," + blockerByte.Y);
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