using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuOmegaOfir
{

    /* Class Board represent board Of sudoku with dynamic Size
     The Sudoku game involves a grid of edgeSize * edgeSize cells. 
     The grid is divided into edgeSize number of boxes, each containing edgeSize cells. 
     This class is a part From The Model */

    public class Board
    {

        // Two dimensional array of sudoku cells - the grid of the board
        private Cell[,] cells;
        // The edge size and box size of the board 
        private readonly int edgeSize, boxSize;

        public Board(string boardStr, int edgeSize)
        {
            // Input: boardStr - string that represent a sudoku board values 
            //          size   - the edge size of the board
            // Output: The function builds The Board from the values of boardStr

            this.edgeSize = edgeSize;

            this.boxSize = (int)Math.Sqrt(this.edgeSize);

            // Init the cells Array
            this.cells = new Cell[edgeSize, edgeSize];
            // the index in boardStr that i need to read the char from
            int countStr = 0;
            for (int row = 0; row < this.edgeSize; row++)
            {
                for (int col = 0; col < this.edgeSize; col++)
                {
                    // read a char from the string board
                    char value = boardStr[countStr++];
                    // build new cell in [row,col] indexes with the value from the boardStr
                    this.cells[row, col] = new Cell(value, row, col);
                }
            }
        }

        /* Input: row and col as index in cells array
           Outpt: return the cell in row,col index in cells array */
        public Cell GetCell(int row, int col)
        {
            return cells[row, col];
        }

        // Getter to this.edgeSize
        public int GetEdgeSize()
        {
            return this.edgeSize;
        }

        /* Input: get number of row and value       
           Output: The function return true if there is a cell in the row with val Value */
        private bool IsInRow(int row, char val)
        {
            for (int i = 0; i < edgeSize; i++)
            {
                if (cells[row, i].GetValue() == val)
                    return true;
            }
            return false;
        }

        /* Input: get number of col and value       
           Output: The function return true if there is a cell in the col with val Value */
        private bool IsInCol(int col, char val)
        {
            for (int i = 0; i < edgeSize; i++)
            {
                if (cells[i, col].GetValue() == val)
                    return true;
            }
            return false;
        }

        /* Input: get number of row, col and value       
           Output: The function return true if There is a cell 
           with value of : val , In the box that the cell with indexes row and col is In */
        private bool IsInBox(int row, int col, char val)
        {
            // find the left high corner of the box indexes
            int r = row - row % boxSize;
            int c = col - col % boxSize;

            // search in the box 
            for (int i = r; i < r + boxSize; i++)
                for (int j = c; j < c + boxSize; j++)
                    // if we find a cell with value of val in the current box return true
                    if (cells[i, j].GetValue() == val)
                        return true;
            return false;
        }

        /* Input: get number of row, col and value       
           Output: return true if it is safe to put val in the cell with index row,col */
        public bool IsSafe(int row, int col, char val)
        {
            return !IsInRow(row, val) && !IsInCol(col, val) && !IsInBox(row, col, val);
        }

        /* Input: get array of booleans     
           Output: this function change by ref all the values in the array to be false*/
        private void ClearArray(bool[] shows)
        {
            for (int i = 0; i < shows.Length; i++)
            {
                shows[i] = false;
            }
        }


        /*Input:  Get row,col and array of bool called shows  
          Output: This function return true, if the flag in shows in index that represent 
                  the value in cells[row,col] is true. else update him to true (unless the cell is empty) and return false  */
        private bool CheckExistCell(int row, int col, bool[] shows)
        {
            // get the value of the cell in row,col indexes
            char numberValue = cells[row, col].GetValue();

            // Check if the cell is not Empty
            if (numberValue != '0')
            {
                // Check if we met this value already 
                // in the current checked row or col or box
                // by cheking this value flag in shows array 

                if (shows[numberValue - '0' - 1])
                    return true;

                // set the flag to true
                // in order to avoid duplicate of this numberValue 
                // in the current checked row or col or box
                shows[numberValue - '0' - 1] = true;
            }
            return false;
        }


        /* Output: This function return LegalSudoku.Legal if the this board represent
           Legal sudoku according to the rules of the game:       
           each of the edgeSize blocks has to contain all the numbers ('1' - edgeSize asci value) within its cells. 
           Each number can only appear once in a row, column or box.
           else return LegalSudoku.ILegal */
        public LegalSudoku IsLegalSudoku()
        {
            // Init array of shows in the size of edgeSize
            // The cell in index i represent if the char (i+1) seen in the cheked row col or box.
            bool[] shows = new bool[this.edgeSize];

            // check duplicates chars in the rows
            for (int row = 0; row < edgeSize; row++)
            {
                for (int col = 0; col < edgeSize; col++)
                {
                    if (CheckExistCell(row, col, shows))
                        // find duplicate, return Ilegal Board
                        return LegalSudoku.Ilegal;
                }
                ClearArray(shows);
            }

            // check duplicates numbers in the cols
            for (int col = 0; col < edgeSize; col++)
            {
                for (int row = 0; row < edgeSize; row++)
                {
                    if (CheckExistCell(row, col, shows))
                        // find duplicate in a row, return Ilegal Board
                        return LegalSudoku.Ilegal;
                }
                ClearArray(shows);
            }

            // check duplicates numbers in the boxes
            for (int row = 0; row < this.edgeSize; row += boxSize)
            {
                for (int col = 0; col < this.edgeSize; col += boxSize)
                {
                    // row,col are indexes of left high corner of a Box
                    // search in all the boxes for duplicate values
                    for (int sr = row; sr < row + boxSize; sr++)
                    {
                        for (int sc = col; sc < col + boxSize; sc++)
                        {
                            if (CheckExistCell(sr, sc, shows))
                                // find duplicate in the box, return Ilegal Board
                                return LegalSudoku.Ilegal;
                        }
                    }
                    ClearArray(shows);
                }
            }


            // the board is Legal according to the rules
            return LegalSudoku.Legal;
        }


        /*
       Input: the function get startRow and startCol as indexes of cell in the cells array of this board
       Output:The function checks
              if there are Empty cells (value = '0') in the board 
              from (startRow,startCol) indexes until the right low corner (the last element in the String input)
              The search: exploring all the chars row after row
              if Empty cells not founded the function will return null
              else, return the first empty cell found
       */
        public Cell SearchEmptyCell(int startRow, int startCol)
        {
            int col;
            // loop over all the cells of the board
            for (int row = startRow; row < this.edgeSize; row++)
            {
                // if we in the row of the start row, we need to search only 
                // the cells in with equal and higher col indexes until the end of the row
                if (row == startRow)
                {
                    col = startCol;
                }
                // we need to search the entire row
                else
                {
                    col = 0;
                }
                // looping over all the cells in all the rows
                for (; col < this.edgeSize; col++)
                {
                    if (!cells[row, col].IsFilled())
                    {
                        return cells[row, col];
                    }
                }
            }
            // not found any empty cell 
            return null;
        }

       
        /* Method that overrides the base class Tostring (System.Object) implementation.
           Return a string of with info about this Board  */
        public override string ToString()
        {
            // Build the string with info about this object

            string oneLineBoard = "The board without Indentations: \n";
            string boardStr = "\nThe Board with Indentations is: \n";

            // loop over the cells of the sudoku
            for (int i = 0; i < edgeSize; i++)
            {
                for (int j = 0; j < edgeSize; j++)
                {
                    // add '  ' in order to add spaces between box to box
                    if (j % boxSize == 0 && j > 0)
                        boardStr += "  ";
                    // add the value of the cells to string
                    boardStr += "[" + cells[i, j].GetValue() + "]  ";
                    oneLineBoard += cells[i, j].GetValue();
                }
                boardStr += "\n";
                if (i % boxSize == boxSize - 1)
                    // add '\n' in order to add spaces between box to box
                    boardStr += "\n";
            }
            return oneLineBoard + boardStr;
        }

    }
}

