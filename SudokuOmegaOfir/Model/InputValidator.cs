using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuOmegaOfir
{
    /* InputValidator is a static class with 
       functions that check the string input validation from the user
       before create new objcet of Board */
    public static class InputValidator
    {
        /* I decided that the max box edge that my program will handle is 5
           it's mean that the max length of valid sudoku input is: 5^4 = 625 */
        private const int MaxBoxEdge = 5;

        /* Input:  This function get sudukoInput string represnt potensial sudoku board
           Output: If the length of the string is valid sudoku board size, The function return
                   the edgeSize of the potnesial board, else return -1 */
        public static int GetValidEdge(int boardLength)
        {      
            // find the edge size of the board
            double edgeSize = Math.Sqrt(boardLength);

            // find the box size of the board
            double boxSize = Math.Sqrt(edgeSize);

            // check if the box size if in the valid range, and if the box size is round number 
            if (boxSize % 1 == 0 && boxSize <= MaxBoxEdge && boxSize > 0)
                return (int)edgeSize;

            return -1;

        }



        /* Input:  This function get sudukoInput string represnt potensial sudoku board 
                   and maxAsci char that represnt the max char in the board 
                   for example for 9*9 board the maxAsci is '9'          
           Output: return true if all the chars in sudukoInput are between '0' and maxAsci 
                   else return false */
        public static bool IsCharsInRange(char maxAsci, string sudukoInput)
        {        
            // loop over the chars of sudukoInput
            for (int i = 0; i < sudukoInput.Length; i++)
            {
                if (sudukoInput[i] < '0' || sudukoInput[i] > maxAsci)
                {
                    // find unvalid char in the potensial board
                    return false;
                }
            }
            // all the chars in this string are between '0' and maxAsci of 
            // the board is valid board
            return true;
        }
    }
}
