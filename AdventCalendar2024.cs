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
            long registerA = 0;
            long registerB = 0;
            long registerC = 0;
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
            long additionSteps = 100;
            int currentMatches = 0;
            int currentMatchesSteps = 0;
            while (output != originalProgram)
            {
                registerA += additionSteps;
                output = Day17_2RunProgram(program, registerA, registerB, registerC, originalProgram);
                currentMatchesSteps++;
                int matches = 0;
                if (output.Length == originalProgram.Length)
                {
                    for (int i = output.Length - 1; i >= 0; i--)
                    {
                        if (output[i] == originalProgram[i])
                            matches++;
                        else
                            break;
                    }
                }
                if (matches > currentMatches)
                {
                    currentMatches = matches;
                    currentMatchesSteps = 0;
                    additionSteps /= 10;
                    if (additionSteps == 0)
                        additionSteps = 1;
                    Debug.WriteLine("RecordA: " + registerA + " Output: " + output + " outputCount: " + output.Length + " Matches: " + matches);
                }
                else if (currentMatches == matches)
                {
                    if (currentMatchesSteps >= 10000)
                    {
                        additionSteps *= 10;
                        currentMatchesSteps = 0;
                    }
                }
                else
                    currentMatches = matches;
            }
            Debug.WriteLine(registerA); // 236581108670061
        }

        private string Day17_2RunProgram(List<int> program, long registerA, long registerB, long registerC, string originalProgram)
        {
            string output = string.Empty;
            int instructionPointer = 0;
            bool firstOutput = true;
            long loops = 0;
            while (true)
            {
                loops++;
                if (instructionPointer >= program.Count())
                    break;
                int opcode = program[instructionPointer];
                int literalOperand = program[instructionPointer + 1];
                long comboOperand = literalOperand == 4 ? registerA : literalOperand == 5 ? registerB : literalOperand == 6 ? registerC : literalOperand;
                if (opcode == 0)
                    registerA = (long)(registerA / Math.Pow(2, comboOperand));
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
                    //if (!originalProgram.StartsWith(output) && output.Length < 13)
                    //    return output;
                }
                else if (opcode == 6)
                    registerB = (long)(registerA / Math.Pow(2, comboOperand));
                else if (opcode == 7)
                    registerC = (long)(registerA / Math.Pow(2, comboOperand));
                instructionPointer += 2;
            }
            return output;
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
                return -1;
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
            List<string> inputList = File.ReadAllLines(@"Input\Day19.txt").ToList();
            List<string> patterns = new List<string>();
            List<string> designs = new List<string>();
            bool towelsParsed = false;
            foreach (string input in inputList)
            {
                if (string.IsNullOrWhiteSpace(input))
                {
                    towelsParsed = true;
                    continue;
                }
                if (!towelsParsed)
                    input.Split(',').ToList().ForEach(e => patterns.Add(e.Trim()));
                else
                    designs.Add(input);
            }

            int possibleDesigns = 0;
            foreach (string design in designs)
            {
                bool isPossible = Day19IsDesignPossible(patterns, design);
                Debug.WriteLine("Design: " + design + " Possible?: " + isPossible);
                if (isPossible)
                    possibleDesigns++;

            }
            Debug.WriteLine(possibleDesigns);
        }

        Dictionary<string, bool> _day19KnownPatterns = new Dictionary<string, bool>();
        private bool Day19IsDesignPossible(List<string> patterns, string design)
        {
            if (string.IsNullOrWhiteSpace(design))
                return true;
            bool isPossible = false;
            foreach (string pattern in patterns.Where(w => design.StartsWith(w)))
            {
                string nextDesign = design.Substring(pattern.Length);
                bool knownPossible = false;
                if (_day19KnownPatterns.TryGetValue(nextDesign, out knownPossible))
                {
                    if (knownPossible)
                        return true;
                    else
                        continue;
                }
                if (Day19IsDesignPossible(patterns, nextDesign))
                {
                    _day19KnownPatterns.Add(nextDesign, true);
                    return true;
                }
                else
                    _day19KnownPatterns.Add(nextDesign, false);
            }
            return isPossible;
        }

        [TestMethod]
        public void Day19_2()
        {
            List<string> inputList = File.ReadAllLines(@"Input\Day19.txt").ToList();
            List<string> patterns = new List<string>();
            List<string> designs = new List<string>();
            bool towelsParsed = false;
            foreach (string input in inputList)
            {
                if (string.IsNullOrWhiteSpace(input))
                {
                    towelsParsed = true;
                    continue;
                }
                if (!towelsParsed)
                    input.Split(',').ToList().ForEach(e => patterns.Add(e.Trim()));
                else
                    designs.Add(input);
            }

            long possibleDesigns = 0;
            foreach (string design in designs)
            {
                long count = Day19_2IsDesignPossible(patterns, design);
                Debug.WriteLine("Design: " + design + " PossibleCount: " + count);
                possibleDesigns += count;
            }
            Debug.WriteLine(possibleDesigns);
        }

        Dictionary<string, long> _day19_2KnownPatterns = new Dictionary<string, long>();
        private long Day19_2IsDesignPossible(List<string> patterns, string design)
        {
            if (string.IsNullOrWhiteSpace(design))
                return 1;
            long possibleCount = 0;
            foreach (string pattern in patterns.Where(w => design.StartsWith(w)))
            {
                string nextDesign = design.Substring(pattern.Length);
                long knownPossible = 0;
                if (_day19_2KnownPatterns.TryGetValue(nextDesign, out knownPossible))
                    possibleCount += knownPossible;
                else
                {
                    long count = Day19_2IsDesignPossible(patterns, nextDesign);
                    possibleCount += count;
                    _day19_2KnownPatterns.Add(nextDesign, count);
                }
            }
            return possibleCount;
        }

        [TestMethod]
        public void Day20_1()
        {
            List<string> inputList = File.ReadAllLines(@"Input\Day20.txt").ToList();
            List<Day20Position> map = new List<Day20Position>();
            int x = 0, y = 0;
            int startX = 0, startY = 0;
            foreach (string input in inputList)
            {
                x = 0;
                foreach (char c in input)
                {
                    if (c == 'S')
                    {
                        startX = x;
                        startY = y;
                    }
                    if (c == '#')
                        x++;
                    else
                        map.Add(new Day20Position { X = x++, Y = y, IsEnd = c == 'E', IsWall = c == '#' });
                }
                y++;
            }
            Dictionary<string, int> knownSteps = Day20CalculateKnownNoCheatPathSteps(map, startX, startY);
            List<Day20Path> paths = Day20CalculatePaths(map, startX, startY, true, knownSteps.Count(), knownSteps, 2);
            var testList = paths.Select(s => s.Steps)
                .GroupBy(g => g).Select(t => new { Count = t.Count(), SavedTiles = knownSteps.Count() - t.Key }).ToList();
            foreach (var test in testList.OrderBy(o => o.SavedTiles))
                Debug.WriteLine("There are " + test.Count + " cheats that save " + test.SavedTiles + " picoseconds.");
            int result = paths.Where(w => (w.Steps + 100) <= knownSteps.Count()).Count();
            Debug.WriteLine(result); // 1367
        }

        private Dictionary<string, int> Day20CalculateKnownNoCheatPathSteps(List<Day20Position> map, int startX, int startY)
        {
            Dictionary<string, int> knownPaths = new Dictionary<string, int>();
            int steps = 0;
            Day20Position pos = map.First(w => w.X == startX && w.Y == startY);
            knownPaths.Add(pos.X + "-" + pos.Y, steps);
            while (true)
            {
                pos = map.First(w => Math.Abs(w.X - pos.X) + Math.Abs(w.Y - pos.Y) == 1 && !w.IsWall && !knownPaths.ContainsKey(w.X + "-" + w.Y));
                knownPaths.Add(pos.X + "-" + pos.Y, ++steps);
                if (pos.IsEnd)
                    break;
            }
            int maxSteps = knownPaths.Max(m => m.Value);
            foreach (KeyValuePair<string, int> path in knownPaths)
                knownPaths[path.Key] = maxSteps - path.Value;
            return knownPaths;
        }

        private class Day20Path
        {
            public int Steps { get; set; }
            public bool IsCheatUsed { get; set; }
            public int CurrentX { get; set; }
            public int CurrentY { get; set; }
            public int PreviousX { get; set; }
            public int PreviousY { get; set; }
        }

        private class Day20Position
        {
            public int X { get; set; }
            public int Y { get; set; }
            public bool IsWall { get; set; }
            public bool IsEnd { get; set; }
        }

        [TestMethod]
        public void Day20_2()
        {
            List<string> inputList = File.ReadAllLines(@"Input\Day20.txt").ToList();
            List<Day20Position> map = new List<Day20Position>();
            int x = 0, y = 0;
            int startX = 0, startY = 0;
            foreach (string input in inputList)
            {
                x = 0;
                foreach (char c in input)
                {
                    if (c == 'S')
                    {
                        startX = x;
                        startY = y;
                    }
                    if (c == '#')
                        x++;
                    else
                        map.Add(new Day20Position { X = x++, Y = y, IsEnd = c == 'E', IsWall = c == '#' });
                }
                y++;
            }
            Dictionary<string, int> knownSteps = Day20CalculateKnownNoCheatPathSteps(map, startX, startY);
            List<Day20Path> paths = Day20CalculatePaths(map, startX, startY, true, knownSteps.Count(), knownSteps, 20);
            //var testList = paths.Select(s => s.Steps)
            //    .GroupBy(g => g).Select(t => new { Count = t.Count(), SavedTiles = knownSteps.Count() - t.Key }).ToList();
            //foreach (var test in testList.OrderBy(o => o.SavedTiles))
            //    Debug.WriteLine("There are " + test.Count + " cheats that save " + test.SavedTiles + " picoseconds.");
            int result = paths.Where(w => (w.Steps + 100) <= knownSteps.Count()).Count();
            Debug.WriteLine(result);
        }

        private List<Day20Path> Day20CalculatePaths(List<Day20Position> map, int startX, int startY, bool allowCheat, int maxAllowedSteps, Dictionary<string, int> knownStepList, int maxAllowedCheatStep)
        {
            List<Day20Path> remainingPaths = new List<Day20Path>();
            Queue<Day20Path> queue = new();
            Day20Path startPath = new Day20Path { CurrentX = startX, CurrentY = startY, IsCheatUsed = !allowCheat, PreviousX = -1, PreviousY = -1, Steps = 1 };
            queue.Enqueue(startPath);
            List<Day20Path> successfullPaths = new List<Day20Path>();
            Day20Path path;
            int maxX = map.Max(m => m.X);
            int maxY = map.Max(m => m.Y);
            while (queue.TryDequeue(out path))
            {
                if (path == null)
                    break;
                if (path.Steps >= maxAllowedSteps)
                    continue;
                int knownSteps;
                if (path.IsCheatUsed && knownStepList.TryGetValue(path.CurrentX + "-" + path.CurrentY, out knownSteps))
                {
                    if (path.Steps + knownSteps > maxAllowedSteps)
                        continue;
                    Day20Path newPath = new Day20Path
                    {
                        CurrentX = path.CurrentX,
                        CurrentY = path.CurrentY,
                        IsCheatUsed = path.IsCheatUsed,
                        Steps = path.Steps + knownSteps,
                    };
                    successfullPaths.Add(newPath);
                    continue;
                }
                List<Day20Position> possiblePaths = map.Where(w => Math.Abs(w.X - path.CurrentX) + Math.Abs(w.Y - path.CurrentY) == 1
                    && !(w.X == path.PreviousX && w.Y == path.PreviousY)).ToList();
                if (!path.IsCheatUsed)
                    possiblePaths.AddRange(map.Where(w => Math.Abs(w.X - path.CurrentX) + Math.Abs(w.Y - path.CurrentY) <= maxAllowedCheatStep
                        && Math.Abs(w.X - path.CurrentX) + Math.Abs(w.Y - path.CurrentY) >= 2));
                foreach (Day20Position possiblePath in possiblePaths)
                {
                    Day20Path newPath = new Day20Path
                    {
                        CurrentX = possiblePath.X,
                        CurrentY = possiblePath.Y,
                        IsCheatUsed = path.IsCheatUsed || Math.Abs(possiblePath.X - path.CurrentX) + Math.Abs(possiblePath.Y - path.CurrentY) >= 2,
                        PreviousX = path.CurrentX,
                        PreviousY = path.CurrentY,
                        Steps = path.Steps + Math.Abs(possiblePath.X - path.CurrentX) + Math.Abs(possiblePath.Y - path.CurrentY),
                    };
                    if (possiblePath.IsEnd)
                        successfullPaths.Add(newPath);
                    else
                        queue.Enqueue(newPath);
                }
            }
            return successfullPaths;
        }

        [TestMethod]
        public void Day21_1()
        {
            List<string> doorCodes = File.ReadAllLines(@"Input\Day21.txt").ToList();
            List<Day21KeypadValue> numericKeypad = new List<Day21KeypadValue>
            {
                new Day21KeypadValue{ X = 0, Y = 0, Value = '7' },
                new Day21KeypadValue{ X = 1, Y = 0, Value = '8' },
                new Day21KeypadValue{ X = 2, Y = 0, Value = '9' },
                new Day21KeypadValue{ X = 0, Y = 1, Value = '4' },
                new Day21KeypadValue{ X = 1, Y = 1, Value = '5' },
                new Day21KeypadValue{ X = 2, Y = 1, Value = '6' },
                new Day21KeypadValue{ X = 0, Y = 2, Value = '1' },
                new Day21KeypadValue{ X = 1, Y = 2, Value = '2' },
                new Day21KeypadValue{ X = 2, Y = 2, Value = '3' },
                new Day21KeypadValue{ X = 1, Y = 3, Value = '0' },
                new Day21KeypadValue{ X = 2, Y = 3, Value = 'A' },
            };
            List<Day21KeypadValue> directionalKeypad = new List<Day21KeypadValue>
            {
                new Day21KeypadValue{ X = 1, Y = 0, Value = '^' },
                new Day21KeypadValue{ X = 2, Y = 0, Value = 'A' },
                new Day21KeypadValue{ X = 0, Y = 1, Value = '<' },
                new Day21KeypadValue{ X = 1, Y = 1, Value = 'v' },
                new Day21KeypadValue{ X = 2, Y = 1, Value = '>' },
            };
            string humanCode = string.Empty;
            int result = 0;
            foreach (string code in doorCodes)
            {
                List<string> robot1Combinations = Day21TranslateCode(code, numericKeypad);
                List<string> robot2Combinations = new List<string>();
                foreach (string combination in robot1Combinations)
                    robot2Combinations.AddRange(Day21TranslateCode(combination, directionalKeypad));
                int minLength = robot2Combinations.Min(m => m.Length);
                foreach (string s in robot2Combinations.Where(w => w.Length != minLength).ToArray())
                    robot2Combinations.Remove(s);
                List<string> robot3Combinations = new List<string>();
                foreach (string combination in robot2Combinations)
                    robot3Combinations.AddRange(Day21TranslateCode(combination, directionalKeypad));
                result += int.Parse(new string(code.Where(w => char.IsDigit(w)).ToArray())) * robot3Combinations.Min(m => m.Length);
            }
            Debug.WriteLine(result);
        }

        private class Day21KeypadValue
        {
            public int X { get; set; }
            public int Y { get; set; }
            public char Value { get; set; }
        }

        private List<string> Day21TranslateCode(string code, List<Day21KeypadValue> keypad)
        {
            char previousKey = 'A';
            List<string> possibleCombinations = new List<string>();
            possibleCombinations.Add(string.Empty);
            foreach (char key in code)
            {
                List<string> newCombinationList = new List<string>();
                foreach (string combination in possibleCombinations)
                    newCombinationList.AddRange(Day21TranslateKey(previousKey, keypad, key, combination));
                previousKey = key;
                possibleCombinations = newCombinationList;
            }
            return possibleCombinations;
        }

        private List<string> Day21TranslateKey(char currentLocationKey, List<Day21KeypadValue> keypad, char key, string translatedCode)
        {
            if (currentLocationKey == key)
                return new List<string> { translatedCode + 'A' };
            List<string> possibleTranslatedCodes = new List<string>();
            Day21KeypadValue currentLocation = keypad.First(w => w.Value == currentLocationKey);
            Day21KeypadValue target = keypad.First(w => w.Value == key);
            Day21KeypadValue previousLocation;
            previousLocation = currentLocation;
            int currentDistance = Math.Abs(target.X - currentLocation.X) + Math.Abs(target.Y - currentLocation.Y);
            List<Day21KeypadValue> possibleLocations = keypad.Where(w => (Math.Abs(w.X - currentLocation.X) + Math.Abs(w.Y - currentLocation.Y)) == 1
                && (currentDistance - (Math.Abs(target.X - w.X) + Math.Abs(target.Y - w.Y))) == 1).ToList();
            foreach (Day21KeypadValue location in possibleLocations)
            {
                char move = previousLocation.X - location.X == 1 ? '<' : previousLocation.X - location.X == -1 ? '>' : previousLocation.Y - location.Y == 1 ? '^' : 'v';
                if (location.Value == target.Value)
                    possibleTranslatedCodes.Add(translatedCode + move + "A");
                else
                    possibleTranslatedCodes.AddRange(Day21TranslateKey(location.Value, keypad, key, translatedCode + move));
            }
            return possibleTranslatedCodes;
        }

        [TestMethod]
        public void Day22_2()
        {
            List<long> buyerSecrets = File.ReadAllLines(@"Input\Day22.txt").Select(s => long.Parse(s)).ToList();
            long sum = 0;
            List<List<Day22Secret>> buyerList = new List<List<Day22Secret>>();
            foreach (long secret in buyerSecrets)
            {
                List<Day22Secret> secretList = new List<Day22Secret>();
                long secretNumber = secret;
                int? change1 = null, change2 = null, change3 = null, change4 = null;
                for (int i = 0; i < 2000; i++)
                {
                    long step1 = (secretNumber ^ (secretNumber * 64)) % 16777216;
                    long step2 = (step1 ^ (step1 / 32)) % 16777216;
                    long step3 = (step2 ^ (step2 * 2048)) % 16777216;
                    change1 = change2;
                    change2 = change3;
                    change3 = change4;
                    change4 = (int)((step3 % 10) - (secretNumber % 10));
                    secretList.Add(new Day22Secret
                    {
                        Value = step3,
                        Change = (int)((step3 % 10) - (secretNumber % 10)),
                        Price = (int)(step3 % 10)
                        ,
                        Changes4 = change1 != null ? (change1 + "," + change2 + "," + change3 + "," + change4) : string.Empty
                    });
                    secretNumber = step3;
                }
                sum += secretNumber;
                buyerList.Add(secretList);
            }
            int mostBananas = 0;
            string bestChange4 = string.Empty;
            List<string> changes4List = buyerList.SelectMany(s => s).Where(w => w.Price >= 9 && !string.IsNullOrWhiteSpace(w.Changes4)).Select(t => t.Changes4).Distinct().ToList();
            foreach (string change4 in changes4List)
            {
                int bananas = buyerList.Sum(w => (w.FirstOrDefault(t => t.Changes4 == change4)?.Price ?? 0));
                if (mostBananas < bananas)
                {
                    mostBananas = bananas;
                    bestChange4 = change4;
                }
            }
            Debug.WriteLine(bestChange4 + " " + mostBananas);
        }

        private class Day22Secret
        {
            public long Value { get; set; }
            public int Price { get; set; }
            public int Change { get; set; }
            public string Changes4 { get; set; }
        }

        [TestMethod]
        public void Day23_1()
        {
            List<string> inputList = File.ReadAllLines(@"Input\Day23.txt").ToList();
            List<Day23Computer> computers = new List<Day23Computer>();
            foreach (string input in inputList)
            {
                string computer1Name = input.Split('-')[0];
                string computer2Name = input.Split('-')[1];
                Day23Computer computer1 = computers.FirstOrDefault(w => w.Name == computer1Name);
                if (computer1 == null)
                {
                    computer1 = new Day23Computer();
                    computer1.Name = computer1Name;
                    computers.Add(computer1);
                }
                Day23Computer computer2 = computers.FirstOrDefault(w => w.Name == computer2Name);
                if (computer2 == null)
                {
                    computer2 = new Day23Computer();
                    computer2.Name = computer2Name;
                    computers.Add(computer2);
                }
                if (!computer1.Connections.Any(a => a == computer2))
                    computer1.Connections.Add(computer2);
                if (!computer2.Connections.Any(a => a == computer1))
                    computer2.Connections.Add(computer1);
            }
            List<string> groups = new List<string>();
            foreach (Day23Computer computer in computers.Where(w => w.Name.StartsWith('t')))
            {
                foreach (Day23Computer connection in computer.Connections)
                {
                    foreach (Day23Computer connectedConnection in connection.Connections.Where(w => computer.Connections.Contains(w)))
                    {
                        string group = string.Join(',', new List<Day23Computer> { computer, connection, connectedConnection }.OrderBy(o => o.Name).Select(s => s.Name).ToList());
                        if (!groups.Any(a => a == group))
                            groups.Add(group);
                    };
                }
            }
            Debug.WriteLine(groups.Count());
        }

        private class Day23Computer
        {
            public string Name { get; set; }
            public List<Day23Computer> Connections = new List<Day23Computer>();
        }

        [TestMethod]
        public void Day23_2()
        {
            List<string> inputList = File.ReadAllLines(@"Input\Day23.txt").ToList();
            List<Day23Computer> computers = new List<Day23Computer>();
            foreach (string input in inputList)
            {
                string computer1Name = input.Split('-')[0];
                string computer2Name = input.Split('-')[1];
                Day23Computer computer1 = computers.FirstOrDefault(w => w.Name == computer1Name);
                if (computer1 == null)
                {
                    computer1 = new Day23Computer();
                    computer1.Name = computer1Name;
                    computers.Add(computer1);
                }
                Day23Computer computer2 = computers.FirstOrDefault(w => w.Name == computer2Name);
                if (computer2 == null)
                {
                    computer2 = new Day23Computer();
                    computer2.Name = computer2Name;
                    computers.Add(computer2);
                }
                if (!computer1.Connections.Any(a => a == computer2))
                    computer1.Connections.Add(computer2);
                if (!computer2.Connections.Any(a => a == computer1))
                    computer2.Connections.Add(computer1);
            }
            List<Day23Computer> largestGroup = new List<Day23Computer>();
            int largestGroupSize = 0;
            foreach (Day23Computer computer in computers.OrderByDescending(d => d.Connections.Count()))
            {
                List<Day23Computer> group = (from g in computer.Connections select g).ToList();
                group.Add(computer);
                foreach (Day23Computer connection in computer.Connections.OrderByDescending(o => o.Connections.Where(w => group.Contains(w)).Count()))
                {
                    foreach (Day23Computer connectedConnection in group.Where(w => w != connection && !connection.Connections.Any(a => a == w)).ToArray())
                    {
                        if (connection.Connections.Where(w => group.Contains(w)).Count() > connectedConnection.Connections.Where(w => group.Contains(w)).Count())
                            group.Remove(connectedConnection);
                        else
                        {
                            group.Remove(connection);
                            break;
                        }
                    }
                }
                if (group.Count() > largestGroupSize)
                {
                    largestGroupSize = group.Count();
                    largestGroup = group;
                }
            }
            Debug.WriteLine(string.Join(',', largestGroup.Select(s => s.Name).OrderBy(o => o)));
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