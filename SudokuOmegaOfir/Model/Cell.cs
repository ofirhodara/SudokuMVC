using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuOmegaOfir
{
    // This class reprsenet Cell in the sudoku board 
    public class Cell
    {
        // The value of the cell
        private char value;

        // the indexes of this cell in the array of cells in the board 
        private int rowIndex, colIndex;

        // list of chars with all the possible values
        // that can set on this cell
        // if the cell value is not '0' this list will always be empty
        private List<char> possibleValues = new List<char>();

        /* constractor get Value of this cell and indexes of the cell in the board  */
        public Cell(char value, int row, int col)
        {
            this.value = value;
            this.rowIndex = row;
            this.colIndex = col;
        }

        // getter to rowIndex
        public int GetRowIndex()
        { return this.rowIndex; }

        // getter to colIndex
        public int GetColIndex()
        { return this.colIndex; }

        // getter to possibleValues list
        public List<char> GetPossibleValues()
        { return this.possibleValues; }


        /*Input: the function get the board that this cell is in his grid 
         *       and maxAsci char: the max Value can be set in this board 
         *Output:The function search the safe values can be set in this cell
         *       and add them to a this.possibleValues list
         *       in addition, if there is only one value can be set 
         *       the function set this value to this cell
         */
        public void BuildPossibleValues(Board board, char maxAsci)
        {
            // if this cell is not empty Cell, the function dont do anything
            // because we cant change value of not empty cells
            if (value == '0')
            {
                // loop over all the values can be set in this board
                for (char numValue = '1'; numValue <= maxAsci; numValue++)
                {
                    // check if numValue is safe value for this cell
                    if (board.IsSafe(this.rowIndex, this.colIndex, numValue))
                        // add numValue to the list of possible Values
                        this.possibleValues.Add(numValue);
                }
                // if only one value possible set this numValue to this.value 
                if (possibleValues.Count == 1)
                {
                    this.value = possibleValues[0];
                }
            }
        }

        /* Output: return True if the cells value is filled, else return false */
        public bool IsFilled()
        {
            // filled cell is cell with value diffrent from '0'
            return value != '0';
        }

        // value setter
        public void SetValue(char value)
        {
            this.value = value;
        }

        // value getter function 
        public char GetValue()
        {
            return value;
        }

        // This function reset the value of this cell to '0' (empty cell)
        public void Reset()
        {
            this.value = '0';
        }
    }
}
