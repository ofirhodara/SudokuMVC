using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuOmegaOfir
{
    
    /* 
     * This Interface is Interface for sudoku View 
     * any kind of View for sudoku project (doesn't matter the input fromat for Example)
     * will have to implement those functions 
     */

    public interface ISudokuView
    {      
        /*Present the solved board to the User*/
        public void SolvedBoardMessage(Board solvedBoard);

        /*send to User Error messages like:*/

        /*unsolvable board message*/
        public void CantSolveMessage();

        /*Unvalid board message*/
        public void ILegalBoardMessage();

        /*Unvalid size of the board message*/
        public void UnValidSizeMessage();
        
        /*Unvalid char in the board message*/
        public void UnValidCharMessage();
   
    }
}
