using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arithmetic;
using Power;

namespace Scientific_Calculator1
{
    internal class Program
    {
        static string staticVar1 = "degree";
        static string staticVar2 = "E-F";
        static string staticVar3 = "";

        public static void Main(string[] args)
        {
            Console.SetCursorPosition(0, 10);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Key Mapping");            
            KeyMapping();
            


            // Move the cursor to a specific position
            Console.SetCursorPosition(0, 0);

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("                                         ---welcome to Scientific World---");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"{staticVar1} | {staticVar2} | {staticVar3}");

            StringBuilder inputExpression = new StringBuilder();
            StringBuilder number = new StringBuilder("0");
            StringBuilder calculate = new StringBuilder();

            ConsoleKeyInfo keyInfo;
            char inputChar;
            double currentNumber = 0;
            double result = 0;
            string memoryValue=string.Empty;
            // char op = '+';
            Console.Write(currentNumber);

            do
            {
                keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.Delete)
                {
                    break;
                }
                else if (keyInfo.Key == ConsoleKey.Backspace)
                {

                    if (number.Length > 0)
                    {
                        number.Length -= 1;
                        Console.Write("\b \b");
                    }
                }
                else if (keyInfo.Key == ConsoleKey.E && keyInfo.Modifiers == ConsoleModifiers.Control)
                {
                    staticVar2 = "F-E";
                    ClearStatusLine(staticVar2);
                }
                else if (keyInfo.Key == ConsoleKey.F && keyInfo.Modifiers == ConsoleModifiers.Control)
                {
                    staticVar2 = "E-F";
                    ClearStatusLine(staticVar2);
                }

                else if (keyInfo.Key == ConsoleKey.P && keyInfo.Modifiers == ConsoleModifiers.Control)
                {

                    staticVar1 = "radian";
                    ClearStatusLine(staticVar1);

                }
                else if (keyInfo.Key == ConsoleKey.Q && keyInfo.Modifiers == ConsoleModifiers.Control)
                {

                    staticVar1 = "degree";
                    ClearStatusLine(staticVar1);

                }
                else if (keyInfo.Key == ConsoleKey.R && keyInfo.Modifiers == ConsoleModifiers.Control)
                {

                    staticVar1 = "gradian";
                    ClearStatusLine(staticVar1);
                }

                else if (keyInfo.Key == ConsoleKey.A && keyInfo.Modifiers == ConsoleModifiers.Control)
                {
                    inputExpression.Clear();
                    Console.Clear();
                    Console.WriteLine("Thanks for Using Sanjeev's Calculator");
                    Environment.Exit(0);
                }
                else if (keyInfo.Key == ConsoleKey.Z && keyInfo.Modifiers == ConsoleModifiers.Control)
                {
                    inputExpression.Clear();
                    number.Clear();
                    calculate.Clear();
                    currentNumber = 0;
                    ClearConsoleExceptFirstTwoLine();
                }
                else if (keyInfo.Key == ConsoleKey.M && keyInfo.Modifiers == ConsoleModifiers.Control)
                {
                    staticVar3 = "M";
                    ClearStatusLine(staticVar3);
                    memoryValue = result.ToString();

                }
                else if (keyInfo.Key == ConsoleKey.N && keyInfo.Modifiers == ConsoleModifiers.Control)
                {
                    staticVar3 = "";
                    ClearStatusLine(staticVar3);
                    memoryValue =null;

                }
                else if (keyInfo.Key == ConsoleKey.B && keyInfo.Modifiers == ConsoleModifiers.Control)
                {
                    staticVar3 = "MR";
                    ClearStatusLine(staticVar3);
                    number.Append(memoryValue);
                    Console.Write(memoryValue);
                }


                else if (IsValidInputKey(keyInfo.KeyChar))
                {

                    inputChar = keyInfo.KeyChar;

                    if (IsOperatorKey(inputChar))
                    {
                        if (currentNumber == 0)
                            calculate.Append(number).Append(inputChar);
                        else
                            calculate.Append(currentNumber).Append(inputChar);

                        inputExpression.Append(number.ToString());
                        if (inputExpression.Length != 0 && inputExpression[inputExpression.Length - 1] == '=')
                        {
                            ClearConsoleExceptFirstTwoLine();
                            inputExpression.Remove(inputExpression.Length - 1, 1);
                            inputExpression.Append(inputChar);


                            //  inputExpression.Clear();

                            //  Console.Write(calculate);
                            Console.Write($"\n{currentNumber}");
                            calculate.Append(currentNumber).Append(inputChar);

                            // op = inputChar;
                        }
                        else if (inputExpression.Length != 0 && IsOperatorKey(inputExpression[inputExpression.Length - 1]))
                        {
                            ClearConsoleExceptFirstTwoLine();
                            inputExpression.Remove(inputExpression.Length - 1, 1);
                            inputExpression.Append(inputChar);
                            Console.Write(inputExpression);
                        }
                        else
                        {
                            inputExpression.Append(inputChar);
                            ClearConsoleExceptFirstTwoLine();
                            Console.WriteLine(inputExpression);
                            //op = inputChar;

                        }
                        number.Clear();
                        currentNumber = 0;
                    }


                    else if (char.IsDigit(inputChar) || inputChar == '.')
                    {
                        if (number.Length == 0)
                        {
                            ClearCurrentConsoleLine();
                        }

                        if (number.Length > 0 && number[0] == '0')
                        {
                            number.Clear();
                            ClearCurrentConsoleLine();
                        }
                        if (inputExpression.Length != 0 && inputExpression[inputExpression.Length - 1] == '=')
                        {

                            ClearConsoleExceptFirstTwoLine();
                            number.Clear();
                            inputExpression.Clear();
                            calculate.Clear();
                            currentNumber = 0;
                            //  op = '+';
                        }
                        number.Append(inputChar);
                        Console.Write(inputChar);

                        result = Convert.ToDouble(number.ToString());

                    }

                    else if (inputChar == '=')
                    {
                        if (inputExpression.Length != 0 && inputExpression[inputExpression.Length - 1] == '=')
                        {
                            continue;
                        }
                        if (currentNumber == 0)
                            calculate.Append(number);
                        else
                            calculate.Append(currentNumber);
                        ClearConsoleExceptFirstTwoLine();
                        inputExpression.Append(number.ToString()).Append(inputChar);
                        Console.Write(inputExpression);

                        result = ArithmeticOperations.Bodmas(calculate.ToString(), staticVar2);
                        calculate.Clear();
                        calculate.Append(result);
                        number.Clear();
                        inputExpression.Clear();
                        inputExpression.Append(result);
                       
                    }

                    else if (inputChar == 's' || inputChar == 'c' || inputChar == 't' || inputChar == 'L' || inputChar == 'O'
                        || inputChar == 'S' || inputChar == 'C' || inputChar == 'T' || inputChar == 'Q' || inputChar == '#' || inputChar == 'q' ||
                        inputChar == 'Z' || inputChar == 'z' || inputChar == 'b' || inputChar == 'f' || inputChar == 'i' || inputChar == 'A' ||
                        inputChar == 'e' || inputChar == 'E' || inputChar == 'p' || inputChar == 'F' || inputChar == 'B' || inputChar == 'd' || inputChar == 'r'
                        || inputChar == 'g' || inputChar == 'G' || inputChar == 'h' || inputChar == 'H' || inputChar == 'k' || inputChar == 'K' || inputChar == 'l'
                        || inputChar == 'm' || inputChar == 'n')
                    {
                        if (inputExpression.Length != 0 && inputExpression[inputExpression.Length - 1] == '=')
                        {
                            number.Append(result);
                            inputExpression.Clear();

                        }

                        IsTrigonometricFunctionKey(inputChar);
                    }


                }
            } while (true);

            bool IsValidInputKey(char input)
            {
                char[] allowedKeys = { 'Q', 'M', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '.', '+',
                    '-', '*', '/', '=', 'S', 's', 'C', 'c', 'T', 't', ')', 'L', 'O', '=','Q','#','q','Z','z'
                    ,'b','f','i','A','e','p','E','F','B','d','r','J','G','g','h','H','k','K','l','m','n'};

                return Array.IndexOf(allowedKeys, input) != -1;
            }

            bool IsOperatorKey(char c)
            {
                return c == '+' || c == '-' || c == '*' || c == '/';
            }
            void IsTrigonometricFunctionKey(char c)
            {
                StringBuilder s = inputExpression;



                switch (c)
                {
                    case 's':
                        ClearConsoleExceptFirstTwoLine();
                        number.Insert(0, "sin(").Append(")");
                        Console.Write(s.ToString() + number.ToString());
                        currentNumber = PowerOperations.Sine(CalculateTrigonometricValue(result, staticVar1));
                        break;
                    /* ClearConsoleExceptFirstTwoLine();
                     inputExpression.Insert(0, "sine(" + number).Append(")");
                     Console.Write(inputExpression);
                     currentNumber = PowerOperations.Sine(CalculateTrigonometricValue(result, staticVar1));
                     break;*/

                    case 'c':
                        ClearConsoleExceptFirstTwoLine();
                        number.Insert(0, "cos(").Append(")");
                        Console.Write(s.ToString() + number.ToString());
                        currentNumber = PowerOperations.Cosine(CalculateTrigonometricValue(result, staticVar1));
                        break;
                    case 't':
                        ClearConsoleExceptFirstTwoLine();
                        number.Insert(0, "tan(").Append(")");
                        Console.Write(s.ToString() + number.ToString());
                        currentNumber = PowerOperations.Tangent(CalculateTrigonometricValue(result, staticVar1));
                        break;
                    case 'S':
                        ClearConsoleExceptFirstTwoLine();
                        number.Insert(0, "Sin^(-1)(").Append(")");
                        Console.Write(s.ToString() + number.ToString());
                        currentNumber = PowerOperations.SineInverse(CalculateTrigonometricValue(result, staticVar1));
                        break;
                    case 'C':
                        ClearConsoleExceptFirstTwoLine();
                        number.Insert(0, "cos^(-1)(").Append(")");
                        Console.Write(s.ToString() + number.ToString());
                        currentNumber = PowerOperations.CosineInverse(CalculateTrigonometricValue(result, staticVar1));
                        break;
                    case 'T':
                        ClearConsoleExceptFirstTwoLine();
                        number.Insert(0, "Tan^(-1)(").Append(")");
                        Console.Write(s.ToString() + number.ToString());
                        currentNumber = PowerOperations.TangentInverse(CalculateTrigonometricValue(result, staticVar1));
                        break;
                    case 'g':
                        ClearConsoleExceptFirstTwoLine();
                        number.Insert(0, "Cosec(").Append(")");
                        Console.Write(s.ToString() + number.ToString());
                        currentNumber = PowerOperations.Cosec(CalculateTrigonometricValue(result, staticVar1));
                        break;
                    case 'G':
                        ClearConsoleExceptFirstTwoLine();
                        number.Insert(0, "Cosec^(-1)(").Append(")");
                        Console.Write(s.ToString() + number.ToString());
                        currentNumber = PowerOperations.CosecInverse(CalculateTrigonometricValue(result, staticVar1));
                        break;
                    case 'h':
                        ClearConsoleExceptFirstTwoLine();
                        number.Insert(0, "Sec(").Append(")");
                        Console.Write(s.ToString() + number.ToString());
                        currentNumber = PowerOperations.Sec(CalculateTrigonometricValue(result, staticVar1));
                        break;
                    case 'H':
                        ClearConsoleExceptFirstTwoLine();
                        number.Insert(0, "Sec^(-1)(").Append(")");
                        Console.Write(s.ToString() + number.ToString());
                        currentNumber = PowerOperations.SecInverse(CalculateTrigonometricValue(result, staticVar1));
                        break;
                    case 'k':
                        ClearConsoleExceptFirstTwoLine();
                        number.Insert(0, "Cot(").Append(")");
                        Console.Write(s.ToString() + number.ToString());
                        currentNumber = PowerOperations.Cot(CalculateTrigonometricValue(result, staticVar1));
                        break;
                    case 'K':
                        ClearConsoleExceptFirstTwoLine();
                        number.Insert(0, "Cot^(-1)(").Append(")");
                        Console.Write(s.ToString() + number.ToString());
                        currentNumber = PowerOperations.CotInverse(CalculateTrigonometricValue(result, staticVar1));
                        break;
                    case 'l':
                        ClearConsoleExceptFirstTwoLine();
                        number.Insert(0, "Sinh(").Append(")");
                        Console.Write(s.ToString() + number.ToString());
                        currentNumber = PowerOperations.SineHyp(CalculateTrigonometricValue(result, staticVar1));
                        break;
                    case 'm':
                        ClearConsoleExceptFirstTwoLine();
                        number.Insert(0, "Cosh(").Append(")");
                        Console.Write(s.ToString() + number.ToString());
                        currentNumber = PowerOperations.CosineHyp(CalculateTrigonometricValue(result, staticVar1));
                        break;
                    case 'n':
                        ClearConsoleExceptFirstTwoLine();
                        number.Insert(0, "tanh(").Append(")");
                        Console.Write(s.ToString() + number.ToString());
                        currentNumber = PowerOperations.TangentHyp(CalculateTrigonometricValue(result, staticVar1));
                        break;




                    case 'L':
                        ClearConsoleExceptFirstTwoLine();
                        number.Insert(0, "log(").Append(")");
                        Console.Write(s.ToString() + number.ToString());
                        currentNumber = PowerOperations.Log(result, 10);
                        break;
                    case 'O':
                        ClearConsoleExceptFirstTwoLine();
                        number.Insert(0, "ln(").Append(")");
                        Console.Write(s.ToString() + number.ToString());
                        currentNumber = PowerOperations.Ln(result);
                        break;

                    case 'Q':
                        ClearConsoleExceptFirstTwoLine();
                        number.Insert(0, "sqr(").Append(")");
                        Console.Write(s.ToString() + number.ToString());
                        currentNumber = PowerOperations.Exponentiation(result, 2);
                        break;
                    case '#':
                        ClearConsoleExceptFirstTwoLine();
                        number.Insert(0, "cube(").Append(")");
                        Console.Write(s.ToString() + number.ToString());
                        currentNumber = PowerOperations.Exponentiation(result, 3);
                        break;
                    case 'q':
                     

                        ClearConsoleExceptFirstTwoLine();
                        number.Insert(0, "√(").Append(")");
                        Console.Write(s.ToString() + number.ToString());
                        currentNumber = PowerOperations.SquareRoot(result);
                        break;
                    case 'Z':
                        ClearConsoleExceptFirstTwoLine();
                        number.Insert(0, "cuberoot(").Append(")");
                        Console.Write(s.ToString() + number.ToString());
                        currentNumber = PowerOperations.CubeRoot(result);
                        break;
                    case 'z':
                        ClearConsoleExceptFirstTwoLine();
                        number.Insert(0, "10^(").Append(")");
                        Console.Write(s.ToString() + number.ToString());
                        currentNumber = PowerOperations.Exponentiation(10, result);
                        break;
                    case 'b':
                        ClearConsoleExceptFirstTwoLine();
                        number.Insert(0, "2^(").Append(")");
                        Console.Write(s.ToString() + number.ToString());
                        currentNumber = PowerOperations.Exponentiation(2, result);
                        break;
                    case 'f':
                        ClearConsoleExceptFirstTwoLine();
                        number.Insert(0, "fact(").Append(")");
                        Console.Write(s.ToString() + number.ToString());
                        currentNumber = PowerOperations.Factorial(result);
                        break;
                    case 'i':
                        ClearConsoleExceptFirstTwoLine();
                        number.Insert(0, "1/(").Append(")");
                        Console.Write(s.ToString() + number.ToString());
                        currentNumber = PowerOperations.inverse(result);
                        break;
                    case 'A':
                        ClearConsoleExceptFirstTwoLine();
                        number.Insert(0, "abs(").Append(")");
                        Console.Write(s.ToString() + number.ToString());
                        currentNumber = PowerOperations.AbsoluteFunction(result);
                        break;
                    case 'E':
                        ClearConsoleExceptFirstTwoLine();
                        number.Insert(0, "e^(").Append(")");
                        Console.Write(s.ToString() + number.ToString());
                        currentNumber = PowerOperations.ePowerx(result);
                        break;
                    case 'e':
                        ClearConsoleExceptFirstTwoLine();
                        inputExpression.Append("2.7182818284590452353602874713527");
                        currentNumber = 2.7182818284590452353602874713527;
                        break;
                    case 'p':
                        ClearConsoleExceptFirstTwoLine();
                        inputExpression.Append("3.1415926535897932384626433832795");
                        currentNumber = 3.1415926535897932384626433832795;
                        break;
                    case 'r':
                        ClearConsoleExceptFirstTwoLine();
                        inputExpression.Append(PowerOperations.RandomFunction().ToString());
                        currentNumber = PowerOperations.RandomFunction();
                        break;
                    case 'F':
                        ClearConsoleExceptFirstTwoLine();
                        number.Insert(0, "floor(").Append(")");
                        Console.Write(s.ToString() + number.ToString());
                        currentNumber = PowerOperations.FloorFunction(result);
                        break;
                    case 'B':
                        ClearConsoleExceptFirstTwoLine();
                        number.Insert(0, "ceil(").Append(")");
                        Console.Write(s.ToString() + number.ToString());
                        currentNumber = PowerOperations.CeilingFunction(result);
                        break;


                    case 'd':
                        ClearConsoleExceptFirstTwoLine();
                        number.Insert(0, "dms(").Append(")");
                        Console.Write(s.ToString() + number.ToString());
                        currentNumber = PowerOperations.ConvertToDMS(result);
                        break;



                }
                Console.Write($"\n{currentNumber}");
                //  Console.Write(inputExpression);
                // number.Clear();
                result = currentNumber;
                //  number= currentNumber;

            }



        }

        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }
        static void ClearConsoleExceptFirstTwoLine()
        {
            Console.SetCursorPosition(0, 2); // Move cursor to the beginning of the second line
            int currentLineCursor = Console.CursorTop;
            for (int i = 2; i <= currentLineCursor + 3; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write(new string(' ', Console.WindowWidth)); // Clear each line from the second line onward
            }
            Console.SetCursorPosition(0, 2); // Move cursor back to the beginning of the second line
        }

        public static void ClearStatusLine(String status)
        {
            int originalLeft = Console.CursorLeft;
            int originalTop = Console.CursorTop;
            Console.SetCursorPosition(0, 1);

            // Clear the line and write new content
            Console.Write(new string(' ', Console.WindowWidth - 1)); // Clear the line
            Console.SetCursorPosition(0, Console.CursorTop); // Move back to the beginning of the line
            Console.Write($"{staticVar1} | {staticVar2} | {staticVar3}");
            // Restore the original cursor position
            Console.SetCursorPosition(originalLeft, originalTop);
        }



        static double CalculateTrigonometricValue(double angle, string inputUnit)
        {
            double angleRad;

            switch (inputUnit)
            {
                case "radian":
                    angleRad = angle;
                    break;
                case "degree":
                    angleRad = Math.PI * angle / 180.0;
                    break;
                case "gradian":
                    angleRad = Math.PI * angle * 0.9 / 180.0;
                    break;
                default:
                    throw new ArgumentException("Invalid unit. Supported units are 'radian', 'degree', and 'gradian'.");
            }
            return angleRad;


        }

        public static void KeyMapping()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            KeyMapping1("Radian   -  ctrl+P", "Degree    -  ctrl+Q", "Gradian   - ctrl+R   M+    - ctrl+M     MR   -  ctrl+B  ", 12);
            KeyMapping1("F-E      -  ctrl+E", "!F-E      -  ctrl+F", "Exist     - ctrl+A   M-   -  ctrl+N            ", 13);
            Console.ForegroundColor = ConsoleColor.Red;
            KeyMapping1("Sin   -  s", "Sin^(-1)   -  S", "Sinh -  l", 15);
            KeyMapping1("Cos   -  c", "Cos^(-1)   -  C", "Cosh -  m", 16);
            KeyMapping1("tan   -  t", "tan^(-1)   -  T", "tanh -  n", 17);
            
            KeyMapping1("Cosec -  g", "Cosec^(-1) -  G", " ", 18);
            KeyMapping1("Sec   -  h", "Sec^(-1)   -  H", " ", 19);
            KeyMapping1("Cot   -  k", "Cot^(-1)   -  K", " ", 20);
            Console.ForegroundColor = ConsoleColor.Cyan;
            KeyMapping1("Log   -  L", "ln  - O", " ", 22);
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            KeyMapping1("Square      -  Q", "Cube    -  #", "SquareRoot - q", 23);
            KeyMapping1("CubeRoot    -  Z", "10^x    -  z", "2^x        - b", 24);
            KeyMapping1("Factorial   -  f", "1/X     -  i", "Absolute   - A", 25);
            KeyMapping1("e^x         -  E", "floor   -  F", "Ceil       - B", 26);
            KeyMapping1("dms         -  d", "  ", "  ", 27);
            KeyMapping1("Pie   -  p", "Random    -  r", "e   - e", 29);
            Console.ForegroundColor = ConsoleColor.White;


        }
        public static void KeyMapping1(string c1, string c2, string c3, int row)
        {
            Console.SetCursorPosition(0, row);
            Console.Write($"{c1}");
            Console.SetCursorPosition(30, row);
            Console.Write($"{c2}");
            Console.SetCursorPosition(60, row);
            Console.Write($"{c3}");

        }
    }
}