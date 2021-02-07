using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuOmegaOfir
{
    // This interface target is that the view class 
    // will know only Solve function of The Controller
    // In addition, any implement of Sudoku controller will have to implement Solve
    // function 

    public interface IController
    {
        /*Function that get numbersInput string that represnt sudoku board
         and send it to the model, after update the view about the solving attempt */
        public void Solve(string numbersInput);  
        
    }
}
