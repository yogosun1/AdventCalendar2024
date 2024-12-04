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
            List<string> inputList = File.ReadAllLines(@"Input\Day4Test.txt").ToList();
            //List<List<char>> wordList = new List<List<char>>();
            //foreach (string input in inputList)
            //{
            //    List<char> row = new List<char>();
            //    input.ToList().ForEach(e => row.Add(e));
            //    wordList.Add(row);
            //}
            //int wordCount = 0;
            //string wordSearch = "XMAS";
            //wordCount += Day4SearchWord(wordList, wordSearch);
            //wordList = MathHelpers.Rotate2DArray(wordList);
            //wordCount += Day4SearchWord(wordList, wordSearch);
            //wordList = MathHelpers.Rotate2DArray(wordList);
            //wordCount += Day4SearchWord(wordList, wordSearch);
            //wordList = MathHelpers.Rotate2DArray(wordList);
            //wordCount += Day4SearchWord(wordList, wordSearch);


            List<List<char>> wordList = new List<List<char>>();
            foreach (string input in inputList)
            {
                List<char> row = new List<char>();
                input.ToList().ForEach(e => row.Add(e));
                wordList.Add(row);
            }
            int wordCount = 0;
            string wordSearch = "XMAS";
            char[,] array = wordList.ToMultiArray();
            wordCount += Day4SearchWord(array, wordSearch);
            char[,] arrayRotated = MathHelpers.Rotate45Degrees(array);
            wordCount += Day4SearchWord(array, wordSearch);
            array = MathHelpers.Rotate45Degrees(array);
            wordCount += Day4SearchWord(array, wordSearch);
            array = MathHelpers.Rotate45Degrees(array);
            wordCount += Day4SearchWord(array, wordSearch);
            array = MathHelpers.Rotate45Degrees(array);
            wordCount += Day4SearchWord(array, wordSearch);
            array = MathHelpers.Rotate45Degrees(array);
            wordCount += Day4SearchWord(array, wordSearch);
            array = MathHelpers.Rotate45Degrees(array);
            wordCount += Day4SearchWord(array, wordSearch);
            array = MathHelpers.Rotate45Degrees(array);
            wordCount += Day4SearchWord(array, wordSearch);
            array = MathHelpers.Rotate45Degrees(array);

            Debug.WriteLine(wordCount);
        }

        private void Day4Print(List<List<char>> wordList)
        {
            wordList.ForEach(e => Debug.WriteLine(new string(e.ToArray())));
        }

        private void Day4Print(char[,] array)
        {
            for (int r = 0; r < array.GetLength(0); r++)
            {
                string row = string.Empty;
                for (int c = 0; c < array.GetLength(1); c++)
                    row += array[r, c];
                Debug.WriteLine(row);
            }
        }

        private int Day4SearchWord(char[,] wordList, string wordSearch)
        {
            int wordCount = 0;
            int currentWordIndex = 0;
            for (int row = 0; row < wordList.GetLength(0); row++)
            {
                for (int column = 0; column < wordList.GetLength(1); column++)
                {
                    if (wordList[row,column] == wordSearch[currentWordIndex])
                    {
                        if ((currentWordIndex + 1) == wordSearch.Count())
                        {
                            wordCount++;
                            currentWordIndex = 0;
                        }
                        else if ((currentWordIndex + 1) < wordSearch.Count())
                            currentWordIndex++;
                    }
                    else if (wordList[row,column] == wordSearch[0])
                        currentWordIndex = 1;
                    else
                        currentWordIndex = 0;
                }
            }
            return wordCount;
        }

        [TestMethod]
        public void Day4_2()
        {

        }

        [TestMethod]
        public void Day5_1()
        {

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