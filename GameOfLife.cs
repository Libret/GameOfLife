using System;
using System.Threading;


namespace GameOfLife
{
    public enum State
    {
        Live,
        Dead
    }

    public class ClassWithMain
    {
        public static void Main()
        {
            bool isRepeat = true;
            while(isRepeat)
            {
                PrintConditionsOfPlace();
                var (length, height) = GetLengthAndHeight();

                PrintConditionsOfIterations();
                int numOfIteration = GetNumOfIteration();

                var cellsArray = GetCellsArry(height, length);
                PrintCellsArray(cellsArray);

                for(int i = 0; i < numOfIteration; i++)
                {
                    cellsArray = GetNextCellsArray(cellsArray);
                    PrintCellsArray(cellsArray);
                }
                isRepeat = ContinueOrNot();
            }
        }
        public static (int length, int height) GetLengthAndHeight() 
        {
            bool isRepeat = true;
            int length = 0;
            int height = 0;
            while (isRepeat)
                    {
                        try
                        {
                            int tempLength = int.Parse(Console.ReadLine());
                            int tempHeigh = int.Parse(Console.ReadLine());
                            if (tempLength >= 5 && tempLength <= 50 && tempHeigh >= 5 && tempHeigh <= 50)
                            {
                                isRepeat = false;
                                length = tempLength;
                                height = tempHeigh;
                            }
                            if (tempLength < 5 || tempLength > 50 || tempHeigh < 5 || tempHeigh > 50)
                                Console.WriteLine("Wrong input. Please enter length and than heigth.");
                        }
                        catch
                        {
                            Console.WriteLine("Ups! Something wrong, try again.");
                        }
                    }
            return (length, height);
        }

        public static int GetNumOfIteration()
        {
            int numOfIteration = 0;
            bool isRepit = true;
            while (isRepit)
            {
                try
                {
                    numOfIteration = int.Parse(Console.ReadLine());
                    isRepit = false;
                }
                catch (ArgumentNullException ex)
                {
                    Console.WriteLine("This fild can`t be empty.");
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("Try to enter number.");
                }
                catch (OverflowException ex)
                {
                    Console.WriteLine("Nope. This number is too much!");
                }
            }
            

            return numOfIteration;
        }
        public static State[,] GetNextCellsArray(State[,] previousArray)
        {
            var nextState = new State[previousArray.GetLength(0), previousArray.GetLength(1)];

            for(int h = 0; h < previousArray.GetLength(0); h++)
            {
                for(int l = 0; l < previousArray.GetLength(1); l++)
                {
                    var numberOfLiveNeighbors = NumberOfLifeNeighbors(previousArray, h, l);

                    if (previousArray[h, l] == State.Live)
                    {
                        if (numberOfLiveNeighbors < 2)
                        {
                            nextState[h, l] = State.Dead;
                        }
                        else if (numberOfLiveNeighbors == 2 || numberOfLiveNeighbors == 3)
                        {
                            nextState[h, l] = State.Live;
                        }
                        else
                        {
                            nextState[h, l] = State.Dead;
                        }
                    }
                    else if (previousArray[h, l] == State.Dead)
                    {
                        if (numberOfLiveNeighbors == 3)
                        {
                            nextState[h, l] = State.Live;
                        }
                        else
                        {
                            nextState[h, l] = State.Dead;
                        }
                    }
                }
            }

            return nextState;
        }
        public static int NumberOfLifeNeighbors(State[,] previousState, int heightIndex, int lengthIndex)
        {
            var neighborToTheRight = 0;
            var neighborToTheLeft = 0;
            var neighborToTheBottom = 0;
            var neighborToTheTop = 0;
            
            // Check if we have a neighbor to the right
            if (lengthIndex + 1 != previousState.GetLength(1))
            {
                neighborToTheRight = previousState[heightIndex, lengthIndex + 1] == State.Live ? 1 : 0;
            }
            // Check if we have a neighbor to the left
            if (lengthIndex - 1 != -1)
            {
                neighborToTheLeft = previousState[heightIndex, lengthIndex - 1] == State.Live ? 1 : 0;
            }
            // Check if we have a neighbor to the bottom
            if (heightIndex + 1 != previousState.GetLength(0))
            {
                neighborToTheBottom = previousState[heightIndex + 1, lengthIndex] == State.Live ? 1 : 0;
            }
            // Check if we have a neighbor to the top
            if (heightIndex - 1 != -1)
            {
                neighborToTheTop = previousState[heightIndex - 1, lengthIndex] == State.Live ? 1 : 0;
            }
            
            var numberOfLiveNeighbors = neighborToTheTop + neighborToTheRight + neighborToTheBottom + neighborToTheLeft;

            var diagonalTopLeft = 0;
            var diagonalTopRight = 0;
            var diagonalBottomLeft = 0;
            var diagonalBottomRight = 0;
            // Check if we have a neighbor to the top left
            if (heightIndex - 1 != -1 && lengthIndex - 1 != -1)
            {
                diagonalTopLeft = previousState[heightIndex - 1, lengthIndex - 1] == State.Live ? 1 : 0;
            }
            // Check if we have a neighbor to the top right
            if (heightIndex - 1 != -1 && lengthIndex + 1 != previousState.GetLength(1))
            {
                diagonalTopRight = previousState[heightIndex - 1, lengthIndex + 1] == State.Live ? 1 : 0;
            }
            // Check if we have a neighbor to the bottom left
            if (heightIndex + 1 != previousState.GetLength(0) && lengthIndex - 1 != -1)
            {
                diagonalBottomLeft = previousState[heightIndex + 1, lengthIndex - 1] == State.Live ? 1 : 0;
            }
            // Check if we have a neighbor to the bottom right
            if (heightIndex + 1 != previousState.GetLength(0) && lengthIndex + 1 != previousState.GetLength(1))
            {
                diagonalBottomRight = previousState[heightIndex + 1, lengthIndex + 1] == State.Live ? 1 : 0;
            }
            
            numberOfLiveNeighbors += diagonalTopLeft + diagonalTopRight + diagonalBottomRight + diagonalBottomLeft;
                
            return numberOfLiveNeighbors;
        }
        public static void PrintCellsArray(State[,] cellsArray)
        {
            Console.Clear();
            for (int h = 0; h < cellsArray.GetLength(0); h++)
            {
                for(int l = 0; l < cellsArray.GetLength(1); l++)
                {
                    Console.Write(cellsArray[h, l] == State.Live ? "+" : "-");
                }
                Console.WriteLine();
            }
            Thread.Sleep(2000);
        }

        public static State[,] GetCellsArry(int height, int length)
        {
            var cellsArray = new State[height, length];
            var random = new Random();
            for (int h = 0; h < height; h++)
            {
                for(int l = 0; l < length; l++)
                {
                    int nextRandomValue = random.Next(0, 2);
                    cellsArray[h, l] = nextRandomValue == 1 ? State.Live : State.Dead;
                }
            }

            return cellsArray;
        }
        
        public static void PrintConditionsOfIterations()
        {
            Console.WriteLine("Please, enter the number of repetitions of your life game.");
        }
        public static void PrintConditionsOfPlace()
        {
            Console.WriteLine("Please, enter length and than heigth of your life game.");
            Console.WriteLine("Place of life game can`t be less than 5x5 and more than 50x50.");
        }
        public static bool ContinueOrNot()
        {
            Console.WriteLine("If you want to continue please enter Yes, else enter No.");
            string answer = Console.ReadLine();

            bool isRepit = true;
            while(isRepit)
            {
                switch(answer)
                {
                    case "Yes":
                        return true;
                        isRepit = false;
                        break;
                    case "yes":
                        return true;
                        isRepit = false;
                        break;
                    case "No":
                        return false;
                        isRepit = false;
                        break;
                    case "no":
                        return false;
                        isRepit = false;
                        break;
                    default:
                        Console.WriteLine("Please enter Yes or No.");
                        isRepit = true;
                        break;
                }
            }
            return false;
        }
    }
}