using System;

namespace TicTacToe
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            /*
                X | O | X | X   	 
                ---------------	 
                O | X | O | O
                ---------------
                X | X | X | O
                ---------------
                X | X | O | X
             */

            char[,] array = new char[4, 4];
            //row 1
            array[0, 0] = 'X';
            array[0, 1] = 'O';
            array[0, 2] = 'X';
            array[0, 3] = 'X';

            //row 2
            array[1, 0] = 'O';
            array[1, 1] = 'X';
            array[1, 2] = 'O';
            array[1, 3] = 'O';

            //row 3
            array[2, 0] = 'X';
            array[2, 1] = 'X';
            array[2, 2] = 'X';
            array[2, 3] = 'O';

            //row 4
            array[3, 0] = 'X';
            array[3, 1] = 'X';
            array[3, 2] = 'O';
            array[3, 3] = 'X';

            char playerWinner = DetermineWinner(array);

            if(!playerWinner.Equals('X'))
                Console.WriteLine("Test Failed.");

            Console.WriteLine("Test Complete.");
        }

        public static char DetermineWinner(char[,] array)
        {
            char noWinner = 'N';
            char winnerToCheckHorizontal = noWinner;
            char winnerToCheckVeriticle = noWinner;
            char winnerToCheckAcrossGoingForward = noWinner;
            char winnerToCheckAcrossGoingBackward= noWinner;

            int lengthOfArray = Convert.ToInt32(Math.Sqrt(array.Length));

            bool acrossForwardCheck = true;
            bool acrossBackwardCheck = true;

            for (int rowIndex = 0; rowIndex < lengthOfArray; rowIndex++)
            {
                bool horizontalCheck = true;
                bool veritcleCheck = true;

                for (int columnIndex = 0; columnIndex < lengthOfArray; columnIndex++)
                {
                    if (horizontalCheck)
                    {
                        if (columnIndex == 0)
                            winnerToCheckHorizontal = array[rowIndex, columnIndex];
                        else
                        {
                            if (array[rowIndex, columnIndex] != winnerToCheckHorizontal)
                            {
                                winnerToCheckHorizontal = noWinner;
                                horizontalCheck = false;
                            }
                        }
                    }

                    if (veritcleCheck)
                    {
                        if (winnerToCheckVeriticle == noWinner && (rowIndex == 0 || columnIndex == 0))
                            winnerToCheckVeriticle = array[columnIndex, rowIndex];
                        else
                        {
                            if (array[columnIndex, rowIndex] != winnerToCheckVeriticle)
                            {
                                winnerToCheckVeriticle = noWinner;
                                veritcleCheck = false;
                            }
                        }
                    }

                    if (rowIndex == 0 && columnIndex == 0)
                    {
                        winnerToCheckAcrossGoingForward = array[rowIndex, columnIndex];
                        winnerToCheckAcrossGoingBackward = array[rowIndex, lengthOfArray - 1];
                    }
                    else if (rowIndex == columnIndex)
                    {
                        if (acrossForwardCheck && array[rowIndex, columnIndex] != winnerToCheckAcrossGoingForward)
                        {
                            winnerToCheckAcrossGoingForward = noWinner;
                            acrossForwardCheck = false;
                        }

                        if (acrossBackwardCheck && array[rowIndex, lengthOfArray - 1 - columnIndex] != winnerToCheckAcrossGoingBackward)
                        {
                            winnerToCheckAcrossGoingBackward = noWinner;
                            acrossBackwardCheck = false;
                        }
                    }
                }

                if (winnerToCheckHorizontal != noWinner ||
                    winnerToCheckVeriticle != noWinner)
                    break;
            }

            if (winnerToCheckHorizontal != noWinner)
                return winnerToCheckHorizontal;
            if (winnerToCheckVeriticle != noWinner)
                return winnerToCheckVeriticle;
            if (winnerToCheckAcrossGoingForward != noWinner)
                return winnerToCheckAcrossGoingForward;
            if (winnerToCheckAcrossGoingBackward != noWinner)
                return winnerToCheckAcrossGoingBackward;

            return noWinner;
        }
    }
}
