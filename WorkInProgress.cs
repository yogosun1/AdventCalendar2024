using System.Collections;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.ExceptionServices;

namespace AdventCalendar2024
{
    [TestClass]
    public class WorkInProgress
    {
        private class Day21KeypadValue
        {
            public int X { get; set; }
            public int Y { get; set; }
            public char Value { get; set; }
        }

        [TestMethod]
        public void Day21_2()
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
            long result = 0;
            foreach (string code in doorCodes)
            {
                List<string> robot1Combinations = Day21_2TranslateCode(code, numericKeypad);
                Debug.WriteLine("I: " + 0 + " Length: " + robot1Combinations.Min(m => m.Length) + " Count: " + robot1Combinations.Count());
                //List<List<Day21_2Move>> moveCombinationList = new List<List<Day21_2Move>>();
                //Dictionary<string, List<string>> knownMoves = Day21_2GenerateKnownMoves(directionalKeypad);
                //foreach (string combination in robot1Combinations)
                //{
                //    List<Day21_2Move> moveList = new List<Day21_2Move>();
                //    for (int i = 0; i < combination.Length - 1; i++)
                //    {
                //        string move = combination[i] + "-" + combination[i + 1];
                //        Day21_2Move moveItem = moveList.FirstOrDefault(w => w.Move == move);
                //        if (moveItem == null)
                //        {
                //            moveItem = new Day21_2Move();
                //            moveItem.Move = move;
                //            moveList.Add(moveItem);
                //        }
                //        moveItem.Count++;
                //    }
                //    moveCombinationList.Add(moveList);
                //}

                //moveList = Day21_2Step(moveList, knownMoves);
                //moveList = Day21_2Step(moveList, knownMoves);



                //List<string> robot1Combinations = Day21_2TranslateCode(code.Substring(0, 1), numericKeypad);
                //Debug.WriteLine("I: " + 0 + " Length: " + robot1Combinations.Min(m => m.Length) + " Count: " + robot1Combinations.Count());
                //List<string> robot2Combinations = Day21RobotCombinations(robot1Combinations, directionalKeypad);
                //Debug.WriteLine("I: " + 1 + " Length: " + robot2Combinations.Min(m => m.Length) + " Count: " + robot2Combinations.Count());
                //List<string> robot3Combinations = Day21RobotCombinations(robot2Combinations, directionalKeypad);
                //Debug.WriteLine("I: " + 2 + " Length: " + robot3Combinations.Min(m => m.Length) + " Count: " + robot3Combinations.Count());
                //List<string> robot4Combinations = Day21RobotCombinations(robot3Combinations, directionalKeypad);
                //Debug.WriteLine("I: " + 3 + " Length: " + robot4Combinations.Min(m => m.Length) + " Count: " + robot4Combinations.Count());
                //List<string> robot5Combinations = Day21RobotCombinations(robot4Combinations, directionalKeypad);
                //Debug.WriteLine("I: " + 4 + " Length: " + robot5Combinations.Min(m => m.Length) + " Count: " + robot5Combinations.Count());

                //double lenMultiplyer = robot3Combinations.Min(m => (double)m.Length) / robot2Combinations.Min(m => (double)m.Length);
                //double combinationLength = robot3Combinations.Min(m => m.Length);
                //for (int i = 0; i < 23; i++)
                //    combinationLength *= lenMultiplyer;


                //result += int.Parse(new string(code.Where(w => char.IsDigit(w)).ToArray())) * robot3Combinations.Min(m => m.Length);
                //result += long.Parse(new string(code.Where(w => char.IsDigit(w)).ToArray())) * (long)combinationLength;
            }
            //foreach (string code in doorCodes)
            //{
            //    string robot1Combinations = Day21TranslateCodeOnlyFirstMathcing(code, numericKeypad);
            //    //string robot2Combinations = Day21TranslateCodeOnlyFirstMathcing(robot1Combinations, directionalKeypad);
            //    //string robot3Combinations = Day21TranslateCodeOnlyFirstMathcing(robot2Combinations, directionalKeypad);

            //    string combination = robot1Combinations;
            //    for (int i = 0; i < 25; i++)
            //    {
            //        combination = Day21TranslateCodeOnlyFirstMathcing(combination, directionalKeypad);
            //        Debug.WriteLine("I: " + i + " Length: " + combination.Length + " " + combination);
            //    }
            //    //result += int.Parse(new string(code.Where(w => char.IsDigit(w)).ToArray())) * robot3Combinations.Min(m => m.Length);
            //}
            Debug.WriteLine(result); // 128339891365302 too low
        }

        private List<string> Day21RobotCombinations(List<string> previousRobotCombinations, List<Day21KeypadValue> keypad)
        {
            Dictionary<string, int> combinationLengths = new Dictionary<string, int>();
            foreach (string combination in previousRobotCombinations)
                combinationLengths.Add(combination, Day21CombinationLength(combination, keypad));
            //List<string> returnCombinations = new List<string>();
            //foreach (string combination in combinationLengths.Where(w => w.Value == combinationLengths.Min(m => m.Value)).Select(s => s.Key))
            //    returnCombinations.AddRange(Day21_2TranslateCode(combination, keypad));
            List<string> returnCombinations = new List<string>();
            int minLength = combinationLengths.Min(m => m.Value);
            Debug.WriteLine(combinationLengths.Where(w => w.Value == minLength).Count());
            foreach (string combination in combinationLengths.Where(w => w.Value == minLength).Select(s => s.Key))
            {
                List<string> newCombinations = Day21_2TranslateCode(combination, keypad);
                int newMinLength = newCombinations.Min(m => m.Length);
                if (newMinLength > minLength)
                    continue;
                else if (newMinLength == minLength)
                    returnCombinations.AddRange(newCombinations.Where(w => w.Length == minLength));
                else
                {
                    minLength = newMinLength;
                    returnCombinations = new List<string>();
                    returnCombinations.AddRange(newCombinations.Where(w => w.Length == minLength));
                }
            }
            return returnCombinations.Distinct().ToList();
        }

        private int Day21CombinationLength(string code, List<Day21KeypadValue> keypad)
        {
            string translation = string.Empty;
            char previousKey = 'A';
            foreach (char key in code)
            {
                translation += Day21_2TranslateKeyOnlyFirstMatching(previousKey, keypad, key, string.Empty);
                previousKey = key;
            }
            return translation.Length;
        }

        private string Day21TranslateCodeOnlyFirstMatching(string code, List<Day21KeypadValue> keypad)
        {
            string translation = string.Empty;
            char previousKey = 'A';
            foreach (char key in code)
            {
                translation += Day21_2TranslateKeyOnlyFirstMatching(previousKey, keypad, key, string.Empty);
                previousKey = key;
            }
            return translation;
        }

        private string Day21_2TranslateKeyOnlyFirstMatching(char currentLocationKey, List<Day21KeypadValue> keypad, char key, string translatedCode)
        {
            if (currentLocationKey == key)
                return translatedCode + 'A';
            Day21KeypadValue currentLocation = keypad.First(w => w.Value == currentLocationKey);
            Day21KeypadValue target = keypad.First(w => w.Value == key);
            Day21KeypadValue previousLocation;
            previousLocation = currentLocation;
            int currentDistance = Math.Abs(target.X - currentLocation.X) + Math.Abs(target.Y - currentLocation.Y);
            Day21KeypadValue location = keypad.Where(w => (Math.Abs(w.X - currentLocation.X) + Math.Abs(w.Y - currentLocation.Y)) == 1
                && (currentDistance - (Math.Abs(target.X - w.X) + Math.Abs(target.Y - w.Y))) == 1).First();
            char move = previousLocation.X - location.X == 1 ? '<' : previousLocation.X - location.X == -1 ? '>' : previousLocation.Y - location.Y == 1 ? '^' : 'v';
            if (location.Value == target.Value)
                return translatedCode + move + "A";
            else
                return Day21_2TranslateKeyOnlyFirstMatching(location.Value, keypad, key, translatedCode + move);
        }

        private List<string> Day21_2TranslateCode(string code, List<Day21KeypadValue> keypad)
        {
            char previousKey = 'A';
            List<string> possibleCombinations = new List<string>();
            possibleCombinations.Add(string.Empty);
            foreach (char key in code)
            {
                List<string> newCombinationList = new List<string>();
                foreach (string combination in possibleCombinations)
                    newCombinationList.AddRange(Day21_2TranslateKey(previousKey, keypad, key, combination));
                previousKey = key;
                possibleCombinations = newCombinationList;
            }
            return possibleCombinations;
        }

        private Dictionary<string, List<string>> _day21KnownMovements = new Dictionary<string, List<string>>();
        private List<string> Day21_2TranslateKey(char currentLocationKey, List<Day21KeypadValue> keypad, char key, string translatedCode)
        {
            if (currentLocationKey == key)
                return new List<string> { translatedCode + 'A' };

            List<string> knownTranslations = null;
            if (_day21KnownMovements.TryGetValue(currentLocationKey + "-" + key, out knownTranslations))
            {

            }

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
                    possibleTranslatedCodes.AddRange(Day21_2TranslateKey(location.Value, keypad, key, translatedCode + move));
            }
            return possibleTranslatedCodes;
        }

        private List<string> Day21RobotCombinations2(List<string> previousRobotCombinations, List<Day21KeypadValue> keypad)
        {
            Dictionary<string, int> combinationLengths = new Dictionary<string, int>();
            foreach (string combination in previousRobotCombinations)
                combinationLengths.Add(combination, Day21CombinationLength(combination, keypad));
            //List<string> returnCombinations = new List<string>();
            //foreach (string combination in combinationLengths.Where(w => w.Value == combinationLengths.Min(m => m.Value)).Select(s => s.Key))
            //    returnCombinations.AddRange(Day21_2TranslateCode(combination, keypad));
            List<string> returnCombinations = new List<string>();
            int minLength = combinationLengths.Min(m => m.Value);
            Debug.WriteLine(combinationLengths.Where(w => w.Value == minLength).Count());
            foreach (string combination in combinationLengths.Where(w => w.Value == minLength).Select(s => s.Key))
            {
                List<string> newCombinations = Day21_2TranslateCode2(combination, keypad);
                int newMinLength = newCombinations.Min(m => m.Length);
                if (newMinLength > minLength)
                    continue;
                else if (newMinLength == minLength)
                    returnCombinations.AddRange(newCombinations.Where(w => w.Length == minLength));
                else
                {
                    minLength = newMinLength;
                    returnCombinations = new List<string>();
                    returnCombinations.AddRange(newCombinations.Where(w => w.Length == minLength));
                }
            }
            return returnCombinations.Distinct().ToList();
        }

        private List<string> Day21_2TranslateCode2(string code, List<Day21KeypadValue> keypad)
        {
            char currentKey = 'A';
            List<string> possibleCombinations = new List<string>();
            possibleCombinations.Add(string.Empty);
            foreach (char key in code)
            {
                List<string> newCombinationList = new List<string>();
                List<string> moveList = Day21_2TranslateMove(currentKey, keypad, key);
                foreach (string move in moveList)
                    foreach (string combination in possibleCombinations)
                        newCombinationList.Add(combination + move);
                currentKey = key;
                possibleCombinations = newCombinationList;
            }
            return possibleCombinations;
        }

        private List<string> Day21_2TranslateMove(char currentKey, List<Day21KeypadValue> keypad, char key)
        {
            if (currentKey == key)
                return new List<string> { "A" };
            List<string> knownTranslations = null;
            if (_day21KnownMovements.TryGetValue(currentKey + "-" + key, out knownTranslations))
                return knownTranslations;
            List<string> possibleTranslatedCodes = new List<string>();
            Day21KeypadValue current = keypad.First(w => w.Value == currentKey);
            Day21KeypadValue target = keypad.First(w => w.Value == key);
            Day21KeypadValue previous;
            previous = current;
            int currentDistance = Math.Abs(target.X - current.X) + Math.Abs(target.Y - current.Y);
            List<Day21KeypadValue> possibleLocations = keypad.Where(w => (Math.Abs(w.X - current.X) + Math.Abs(w.Y - current.Y)) == 1
                && (currentDistance - (Math.Abs(target.X - w.X) + Math.Abs(target.Y - w.Y))) == 1).ToList();
            foreach (Day21KeypadValue location in possibleLocations)
            {
                char move = previous.X - location.X == 1 ? '<' : previous.X - location.X == -1 ? '>' : previous.Y - location.Y == 1 ? '^' : 'v';
                if (location.Value == target.Value)
                    possibleTranslatedCodes.Add(move + "A");
                else
                    possibleTranslatedCodes.AddRange(Day21_2TranslateMove(location.Value, keypad, key).Select(s => move + s));
            }
            _day21KnownMovements.Add(currentKey + "-" + key, possibleTranslatedCodes);
            return possibleTranslatedCodes;
        }



        private Dictionary<string, List<string>> Day21_2GenerateKnownMoves(List<Day21KeypadValue> keypad)
        {
            Dictionary<string, List<string>> knownMoves = new Dictionary<string, List<string>>();
            foreach (Day21KeypadValue key in keypad)
                foreach (Day21KeypadValue moveTo in keypad.Where(w => w.Value != key.Value))
                    knownMoves.Add(key.Value + "-" + moveTo.Value, Day21_2TranslateMove(key.Value, keypad, moveTo.Value));
            return knownMoves;
        }

        //private List<Day21_2Move> Day21_2Step(List<Day21_2Move> moveList, Dictionary<string, List<string>> knownMoves)
        //{
        //    List<Day21_2Move> newMoveList = new List<Day21_2Move>();
        //    foreach (Day21_2Move move in moveList)
        //    {
        //        List<string> newMoves = knownMoves.First(w => w.Key == move.Move).Value;
        //        foreach (string newMove in newMoves)
        //        {
        //            Day21_2Move moveItem = newMoveList.FirstOrDefault(w => w.Move == move.Move);
        //            if (moveItem == null)
        //            {
        //                moveItem = new Day21_2Move();
        //                moveItem.Move = newMove;
        //                newMoveList.Add(moveItem);
        //            }
        //            moveItem.Count += move.Count;
        //        }
        //    }
        //    return newMoveList;
        //}

        //private List<List<Day21_2Move>> Day21_2Step(List<List<Day21_2Move>> moveCombinationList, Dictionary<string, List<string>> knownMoves)
        //{
        //    List<List<Day21_2Move>> newMoveCombinationList = new List<List<Day21_2Move>>();
        //    foreach (List<Day21_2Move> moveList in moveCombinationList)
        //    {
        //        List<Day21_2Move> newMoveList = new List<Day21_2Move>();
        //        foreach (Day21_2Move oldMove in moveList)
        //        {
        //            List<string> newMoves = knownMoves.First(w => w.Key == oldMove.Move).Value;
        //            foreach (string newMove in newMoves)
        //            {
        //                Day21_2Move moveItem = newMoveList.FirstOrDefault(w => w.Move == oldMove.Move);
        //                if (moveItem == null)
        //                {
        //                    moveItem = new Day21_2Move();
        //                    moveItem.Move = newMove;
        //                    newMoveList.Add(moveItem);
        //                }
        //                moveItem.Count += oldMove.Count;
        //            }
        //        }
        //        newMoveCombinationList.Add(newMoveList);
        //    }
        //    return newMoveCombinationList;
        //}

        //private class Day21_2MoveCombination
        //{
        //    public List<Day21_2Move> MoveList = new List<Day21_2Move>();
        //}

        //private class Day21_2Move
        //{
        //    public string Move { get; set; }
        //    public long Count { get; set; }
        //}



        [TestMethod]
        public void Day24_2()
        {
            List<string> inputList = File.ReadAllLines(@"Input\Day24Test.txt").ToList();
            List<Day24Wire> wires = new List<Day24Wire>();
            List<Day24Gate> gates = new List<Day24Gate>();
            List<string> outputWireNames = new List<string>();
            bool isWiresRead = false;
            foreach (string input in inputList)
            {
                if (string.IsNullOrWhiteSpace(input))
                {
                    isWiresRead = true;
                    continue;
                }
                if (!isWiresRead)
                    wires.Add(new Day24Wire { Name = input.Split(':')[0], Value = int.Parse(new string(input.Split(':')[1].Trim())) == 1 });
                else
                {
                    string[] inputSplit = input.Split(' ');
                    gates.Add(new Day24Gate { LeftWireName = inputSplit[0], Operation = inputSplit[1], RightWireName = inputSplit[2], OutputWireName = inputSplit[4] });
                    outputWireNames.Add(inputSplit[4]);
                }
            }
            long x = Convert.ToInt64(new string(wires.Where(w => w.Name.Contains('x')).OrderByDescending(o => o.Name)
                .Select(s => s.Value ? '1' : '0').ToArray()), 2);
            long y = Convert.ToInt64(new string(wires.Where(w => w.Name.Contains('y')).OrderByDescending(o => o.Name)
                .Select(s => s.Value ? '1' : '0').ToArray()), 2);
            long z = 0;
            List<Day24Wire> outputWires = null;
            List<Day24Gate> newGates = new List<Day24Gate>();
            bool swapFound = false;
            List<string> swappedGateOutputNames = new List<string>();

            outputWireNames = new List<string>();
            outputWires = Day24CalculateWireValues(wires, (from g in gates select g).ToList(), true);
            List<bool> zResultList = Convert.ToString(x + y, 2).PadLeft(outputWires.Count(), '0').Select(s => s == '1').Reverse().ToList();
            foreach (Day24Gate gate in gates.Where(w => w.OutputWireName.Contains('z')))
            {
                bool expectedResult = zResultList[int.Parse(new string(gate.OutputWireName.Skip(1).ToArray()))];
                bool currentResult = outputWires.First(w => w.Name == gate.OutputWireName).Value;
                if (expectedResult != currentResult)
                {
                    List<string> tempWireList = new List<string>();
                    if (!outputWireNames.Contains(gate.OutputWireName))
                        tempWireList.Add(gate.OutputWireName);
                    while (tempWireList.Any())
                    {
                        string tempWire = tempWireList.First();
                        outputWireNames.Add(tempWire);
                        tempWireList.Remove(tempWire);
                        Day24Gate tempGate = gates.First(w => w.OutputWireName == tempWire);
                        if (tempGate.Operation == "AND" && expectedResult)
                        {
                            Day24Wire leftWire = outputWires.First(w => w.Name == tempGate.LeftWireName);
                            Day24Wire rightWire = outputWires.First(w => w.Name == tempGate.RightWireName);
                            if (!leftWire.Value && !tempGate.LeftWireName.StartsWith('x') && !tempGate.LeftWireName.StartsWith('y') && !outputWireNames.Contains(gate.LeftWireName))
                                tempWireList.Add(tempGate.LeftWireName);
                            if (!rightWire.Value && !tempGate.RightWireName.StartsWith('x') && !tempGate.RightWireName.StartsWith('y') && !outputWireNames.Contains(gate.RightWireName))
                                tempWireList.Add(tempGate.RightWireName);
                        }
                        else
                        {
                            if (!tempGate.LeftWireName.StartsWith('x') && !tempGate.LeftWireName.StartsWith('y') && !outputWireNames.Contains(gate.LeftWireName))
                                tempWireList.Add(tempGate.LeftWireName);
                            if (!tempGate.RightWireName.StartsWith('x') && !tempGate.RightWireName.StartsWith('y') && !outputWireNames.Contains(gate.RightWireName))
                                tempWireList.Add(tempGate.RightWireName);
                        }
                    }
                }
            }


            while (!swapFound)
            {
                for (int i1 = 0; i1 < outputWireNames.Count() && !swapFound; i1++)
                {
                    for (int i2 = i1 + 1; i2 < outputWireNames.Count() && !swapFound; i2++)
                    {
                        for (int i3 = 0; i3 < outputWireNames.Count() && !swapFound; i3++)
                        {
                            if (i3 == i1 || i3 == i2)
                                continue;
                            for (int i4 = i3 + 1; i4 < outputWireNames.Count() && !swapFound; i4++)
                            {
                                if (i4 == i1 || i4 == i2)
                                    continue;
                                for (int i5 = 0; i5 < outputWireNames.Count() && !swapFound; i5++)
                                {
                                    if (i5 == i1 || i5 == i2 || i5 == i3 || i5 == i4)
                                        continue;
                                    for (int i6 = i5 + 1; i6 < outputWireNames.Count() && !swapFound; i6++)
                                    {
                                        if (i6 == i1 || i6 == i2 || i6 == i3 || i6 == i4)
                                            continue;
                                        for (int i7 = 0; i7 < outputWireNames.Count() && !swapFound; i7++)
                                        {
                                            if (i7 == i1 || i7 == i2 || i7 == i3 || i7 == i4 || i7 == i5 || i7 == i6)
                                                continue;
                                            for (int i8 = i7 + 1; i8 < outputWireNames.Count() && !swapFound; i8++)
                                            {
                                                if (i8 == i1 || i8 == i2 || i8 == i3 || i8 == i4 || i8 == i5 || i8 == i6)
                                                    continue;
                                                newGates = (from g in gates
                                                            select new Day24Gate
                                                            {
                                                                LeftWireName = g.LeftWireName,
                                                                Operation = g.Operation,
                                                                OutputWireName = g.OutputWireName,
                                                                RightWireName = g.RightWireName
                                                            }).ToList();
                                                string tempGate;
                                                tempGate = newGates[i1].OutputWireName;
                                                newGates[i1].OutputWireName = newGates[i2].OutputWireName;
                                                newGates[i2].OutputWireName = tempGate;

                                                tempGate = newGates[i3].OutputWireName;
                                                newGates[i3].OutputWireName = newGates[i4].OutputWireName;
                                                newGates[i4].OutputWireName = tempGate;

                                                Debug.WriteLine("Swaps: " + i1 + " " + i2 + " " + i3 + " " + i4 + " " + i5 + " " + i6 + " " + i7 + " " + i8);

                                                tempGate = newGates[i5].OutputWireName;
                                                newGates[i5].OutputWireName = newGates[i6].OutputWireName;
                                                newGates[i6].OutputWireName = tempGate;

                                                tempGate = newGates[i7].OutputWireName;
                                                newGates[i7].OutputWireName = newGates[i8].OutputWireName;
                                                newGates[i8].OutputWireName = tempGate;
                                                //if (i1 == 0 && i2 == 5)
                                                //{

                                                //}
                                                outputWires = Day24CalculateWireValues(wires, newGates, false);
                                                if (outputWires == null)
                                                    continue;
                                                z = Convert.ToInt64(new string(outputWires.Where(w => w.Name.Contains('z')).OrderByDescending(o => o.Name)
                                                    .Select(s => s.Value ? '1' : '0').ToArray()), 2);
                                                //if (i1 == 0 && i2 == 5)
                                                //{

                                                //}
                                                if (x + y == z)
                                                {
                                                    swapFound = true;
                                                    swappedGateOutputNames = new List<string>
                                                    {
                                                        gates[i1].OutputWireName, gates[i2].OutputWireName,gates[i3].OutputWireName,
                                                        gates[i4].OutputWireName,gates[i5].OutputWireName,gates[i6].OutputWireName,
                                                        gates[i7].OutputWireName,gates[i8].OutputWireName
                                                    };
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            Debug.WriteLine(z);
        }

        private List<Day24Wire> Day24CalculateWireValues(List<Day24Wire> wires, List<Day24Gate> gates, bool includeAllWiresInResult)
        {
            List<Day24Wire> outputWires = (from w in wires select w).ToList();
            while (gates.Any())
            {
                Day24Gate gate = gates.FirstOrDefault(w => outputWires.Any(a => a.Name == w.LeftWireName && outputWires.Any(n => n.Name == w.RightWireName)));
                if (gate == null)
                    return null;
                Day24Wire leftWire = outputWires.First(w => w.Name == gate.LeftWireName);
                Day24Wire rightWire = outputWires.First(w => w.Name == gate.RightWireName);
                if (gate.Operation == "AND")
                    outputWires.Add(new Day24Wire { Name = gate.OutputWireName, Value = leftWire.Value && rightWire.Value });
                else if (gate.Operation == "OR")
                    outputWires.Add(new Day24Wire { Name = gate.OutputWireName, Value = leftWire.Value || rightWire.Value });
                else
                    outputWires.Add(new Day24Wire { Name = gate.OutputWireName, Value = leftWire.Value != rightWire.Value });
                gates.Remove(gate);
            }
            if (includeAllWiresInResult)
                return outputWires;
            else
                return outputWires.Where(w => w.Name.StartsWith('z')).ToList();
        }

        public class Day24Wire
        {
            public string Name { get; set; }
            public bool Value { get; set; }
        }

        public class Day24Gate
        {
            public string LeftWireName { get; set; }
            public string RightWireName { get; set; }
            public string OutputWireName { get; set; }
            public string Operation { get; set; }
        }
    }
}
