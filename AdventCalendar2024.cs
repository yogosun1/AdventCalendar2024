using System.Diagnostics;

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

        }

        [TestMethod]
        public void Day2_2()
        {

        }

        [TestMethod]
        public void Day3_1()
        {

        }

        [TestMethod]
        public void Day3_2()
        {

        }

        [TestMethod]
        public void Day4_1()
        {

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