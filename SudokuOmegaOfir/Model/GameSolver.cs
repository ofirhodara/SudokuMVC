using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuOmegaOfir
{

    /*GameSolver class is The main class of the Model (the barin of the project).
    The model is responsible for managing the data of the application. 
    It directly manages the data, logic and rules of the application.
    this class goal is to solve legal sudoku board*/

    public class GameSolver
    {
        // board to solve
        private Board board;
        // edge size of the board
        private int edgeSize;
        // max char value can be set in the board
        private char maxValue;

        // GameSolver constructor gets Board and create Game solver for this board
        public GameSolver(Board board)
        {
            this.board = board;
            this.edgeSize = board.GetEdgeSize();

            // calculate The max value of the board
            this.maxValue = (char)(this.edgeSize + '0');
        }

        /* This function try solve the sudoku grid of this.board 
           Output: the function return true if the function succsees to solve this.board grid else return false */
        public bool SolveGame()
        {
            Cell currentCell;
            // Create the lists of possible values to all the empty cells in the board
            for (int row = 0; row < this.edgeSize; row++)
            {
                for (int col = 0; col < this.edgeSize; col++)
                {
                    currentCell = board.GetCell(row, col);
                    // call SearchPossibleValues in order to build the list
                    // possible values to currentCell              
                    currentCell.BuildPossibleValues(board, this.maxValue);
                }
            }
            // call SolveBoard function and return the result of the solving attempt
            return SolveBoard();
        }


        /* Input: The function set correct values in all the cells
         *        starting in [startRow,startCol] until the end of the board
         *        in order to solve this.board
         *        The default value are startRow = 0, startCol = 0 because we want to solve ALL the board
         * Outpt: This recursive function using backtracking algorithm to solve the sudoku board
         *        return true if success to solve else, return false
         */
        private bool SolveBoard(int startRow = 0, int startCol = 0)
        {
            // Get reference to Empty cell in board 
            Cell emptyCell = board.SearchEmptyCell(startRow, startCol);

            // There is not empty cells '0' in the board - We solved the board
            if (emptyCell == null)
            {
                return true;
            }

            // get the indexes of currentCell in this.board
            int rowIndex = emptyCell.GetRowIndex();
            int colIndex = emptyCell.GetColIndex();

            // get the list of possible Values we can set in this cell
            List<char> possibleValues = emptyCell.GetPossibleValues();

            // loop over the possible values
            foreach (char numValue in possibleValues)
            {
                // check if num can set in rowIndex,colndex index in board
                if (board.IsSafe(rowIndex, colIndex, numValue))
                {
                    // set numValue our emptyCell
                    emptyCell.SetValue(numValue);

                    // call the function SolveGame with indexes values of the 
                    // "next" cell in the board          
                    if (SolveBoard(rowIndex, colIndex + 1))
                    {
                        // if we get here we finished To explore all 
                        // the empty cells of the board and solved the Game
                        return true;
                    }
                    else
                    {
                        // numValue in this Empty Cell is not lead us to solve the board
                        // try with the next value in the list                         
                        emptyCell.Reset();
                    }
                }

            }
            // the sudoku board has no corrent answer - the board is unsolvable
            return false;
        }

    }
}
