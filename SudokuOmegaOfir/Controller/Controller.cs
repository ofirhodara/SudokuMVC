using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuOmegaOfir
{
    /* General:
     This class is the controller of the application in the Model–view–controller 
     software design pattern.
     The controller responds to the user input and performs interactions on the data model objects. 
     The controller receives the input,
     optionally validates it and then passes the input to the model.

     Specific infromation:
     This class get the input from the view, check the validation 
     of the board. if it is valid, send to the Game Solver to solve the game.
     in the end the controller update the view to show the suitable message.
     implement the IController interface.*/

    public class Controller : IController
    {
        // The view of the application ISudokuView Reference
        private ISudokuView view;

        public Controller(ISudokuView view)
        {
            // Build Contoller with a View
            this.view = view;
        }
        /*Empty consractor*/
        public Controller()
        {
            // For the unit test i Create Controller seperate from A View
            // in order to check all the each part of the application without dependency.
        }

        /* Input:  This function get potensial board as a string- 'numbersInput'  
           Output: The function checks the validation and legalization of the input by send it to the model.
                   The function updates the view about the results of solving attempt. */
        public void Solve(string numbersInput)
        {
            // Validate the size of numbersInput 
            int edgeSize = InputValidator.GetValidEdge(numbersInput.Length);

            // if the size of numbersInput is valid
            if (edgeSize > 0)
            {
                // Checking the values of numbersInput according to edgeSize  
                if (InputValidator.IsCharsInRange((char)(edgeSize + '0'), numbersInput))
                {
                    // build a board from numbersInput
                    Board validBoard = new Board(numbersInput, edgeSize);

                    // checks to Legalization of the board according to sudoku rules
                    if (validBoard.IsLegalSudoku() == LegalSudoku.Legal)
                    {
                        GameSolver solver = new GameSolver(validBoard);

                        // Try to solve The sudoku
                        if (solver.SolveGame())
                        {
                            // send the solved board to the view
                            view.SolvedBoardMessage(validBoard);
                        }
                        else
                        {
                            // send to view That the program didn't sucseed to solve the sudoku 
                            view.CantSolveMessage();
                        }
                    }
                    else
                    {
                        // send to view That the numbersInput represent unvalid sudoku board
                        // there is duplicates in the rows/cols/boxes
                        view.ILegalBoardMessage();
                    }
                }
                else
                {
                    // send to view that there is char in numbersInput string that 
                    // not in the range of the valid chars in the board

                    view.UnValidCharMessage();
                }
            }
            else
            {
                // send to view that the size of numbersInput is not valid in order to build board from
                view.UnValidSizeMessage();
            }


        }



        /*Alert: I build this function in order to test the controller
                 without connection to the View. This function is similiar to Solve function in this class.                    
          Input:  This function get potensial board as a string- 'numbersInput'  
          Output: This function return true if it success to build and solve a sudoku board from numbersInput
                  else return false. */
        public bool SolveForTest(string numbersInput)
        {
            // Validate the size of numbersInput 
            int edgeSize = InputValidator.GetValidEdge(numbersInput.Length);

            // if the size of numbersInput is valid
            if (edgeSize > 0)
            {
                // Checking the values of numbersInput according to edgeSize  
                if (InputValidator.IsCharsInRange((char)(edgeSize + '0'), numbersInput))
                {

                    Board validBoard = new Board(numbersInput, edgeSize);

                    // checks to Legalization of the board according to sudoku rules
                    if (validBoard.IsLegalSudoku() == LegalSudoku.Legal)
                    {
                        GameSolver solver = new GameSolver(validBoard);
                        // return the result of the sudoku attempt
                        return solver.SolveGame();
                    }
                }
            }
            // dont succseed to solve a sudoku from numbersInput
            return false;
        }

    }
}
