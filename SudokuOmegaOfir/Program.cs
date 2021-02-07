using System;

namespace SudokuOmegaOfir
{
    // Enum that present if A built board is Legal or Ilegal according to sudoku rules
    public enum LegalSudoku { Ilegal, Legal };


    public class Program
    {     
        public static void Main(string[] args)
        {

            // Create View 
            View view = new View();
            // Start the Game 
            view.SodukoGame();
        
        }
    }
}

