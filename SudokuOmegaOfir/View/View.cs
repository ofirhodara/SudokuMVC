using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SudokuOmegaOfir
{
    /*
     This class is the View part in Mvc design Pattern of my application.
     This view in not a classic one because i didnt write a GUI to the program.
     This function target is to get the input from the user and show the 
     user the results by printing or write to a asked file.
     */

    public class View : ISudokuView
    {

        // boolean flag that check if we the user want to continue 
        // send the solver boards
        private bool running = true;

        // Icontroller reference to the controller 
        private IController controller;

        // path of file to write results from the model,
        // if textFile is equal to Empty string, we need to print to the console else, write to file 
        private string textFile = string.Empty;


        /* none arguments constructor to View,builds a controller */
        public View()
        {
            // Build the controller 
            this.controller = new Controller(this);
        }

        /* Get the board input from the user
         * return the board as a string */
        private string InputStr()
        {
            Console.WriteLine("Please press the sudoku Board as one line without spaces:");
            string boardStr = Console.ReadLine();
            return boardStr;
        }

        /* Output: The Function return a string board from a file path: this.testFile, 
         * if not succsees to read from the file return null */
        private string GetBoardFromFile()
        {
            string boardInput = null;
            try
            {
                // try to read the text from the file 
                boardInput = File.ReadAllText(this.textFile);
            }
            catch (Exception e)
            {
                Console.WriteLine("Sorry,can't open Or read this file !");
                Console.WriteLine(e.Message);
            }
            return boardInput;
        }

        /* Output: The function get input from the user for this.textFile */
        private void InputFileName()
        {
            Console.WriteLine("Please the path of the text file:");
            this.textFile = Console.ReadLine();
        }

        /* Print the menu Of the game */
        private void Menu()
        {
            Console.WriteLine("\nPress console - for keyBoard input of the Board ");
            Console.WriteLine("Press file - for input from text file");
            Console.WriteLine("Press exit - for Close The Program! \n");
        }

        /* The Game main Function */
        public void SodukoGame()
        {
            Console.WriteLine("Hello! This is my Sudoko solver :)\n");
            string board, chosenInput;

            // until the user wants to end the game
            while (running)
            {
                // print the menu
                Menu();

                // Get from the user the wanted command 
                chosenInput = Console.ReadLine();
                if (chosenInput == "exit")
                {
                    // set the flag to flase in order to end the game
                    this.running = false;
                }
                else
                {
                    if (chosenInput == "console")
                    {
                        // Get input from the console
                        board = InputStr();

                        // send to controller the board 
                        controller.Solve(board);

                    }
                    else
                    {
                        if (chosenInput == "file")
                        {
                            // Get from the user the file path
                            InputFileName();
                            // read the board from the file
                            board = GetBoardFromFile();

                            // check if the read succeeded
                            if (board != null)
                            {
                                // send to controller the board 
                                controller.Solve(board);
                            }

                            // Change the text file again to empty string  
                            // Because next board we will not use this file path  
                            textFile = string.Empty;
                        }
                        else
                        {
                            Console.WriteLine("There is No such Input format option !");
                        }

                    }
                }
            }
            Console.WriteLine("\nThe Game is Over!");
        }

        /* Display Error that the input board is Ilegal Sudoku board */
        public void ILegalBoardMessage()
        {
            // build the error message
            string IlegalError = "Your board in Unvalid Board. \n";          
            IlegalError += "Some of the numbers appeared more the 1 time in one of the row/cols/boxes of the board!";
            // send to display message function 
            displayMessage(IlegalError);
            
        }

        /* Display Error that one of the char the input board is unvalid */
        public void UnValidCharMessage()
        {
            string CharError = "One of the char in your input is not in the range of numbers of your board! \n";
            CharError += "For example: 4*4 Grid and you entered: '7'";
            // send to display message function 
            displayMessage(CharError);
            
        }

        /* Display Error that the size of the input board is unvalid */
        public void UnValidSizeMessage()
        {
            string sizeError = "Sorry, you inserted unvalid size of suduko Grid: \n";
            sizeError += "option A: bigger then the max value in my program: 625 (25 * 25 grid) \n";
            sizeError += "option B: the lenght in not a forth square of any number!";
            // send to display message function 
            displayMessage(sizeError);
        }

        /* Display Error that the program cant solve the input board - the board in unsolvable*/
        public void CantSolveMessage()
        {

            string cantSolveError = "Sorry,This sudoku grid is not slovable";
            // send to display message function 
            displayMessage(cantSolveError);
     
        }

        /* print/write to A file solved boards */
        public void SolvedBoardMessage(Board solvedBoard)
        {
            displayMessage(solvedBoard.ToString());
        }

        /*
         Input: string message with the answer from the controller after try to solve the input board from a file with path: this.textFile
         Output: This function write the message in the end of the file
        */
        private void WriteToAFile(string message)
        {
            try
            {
                // Write the meesage in the end of the file with path: this.textFile 
                using (StreamWriter sw = File.AppendText(this.textFile))
                {
                    sw.WriteLine(message);
                }
                Console.WriteLine("The answer is in the end on your file :)");
            }
            catch
            {
                // catch exception during the opening or writing to the file
                Console.WriteLine("I dont succsess to write to your file.");
                Console.WriteLine("But The answer is: " + message);
            }
        }


        /* Input:The function gets a message to display To the user
           Output:The function print/ write to a file the message
                according to the format the board has inserted */
        private void displayMessage(string message)
        {
            // check the format of the board inserted 
            if (this.textFile == string.Empty)
                // display the answer in console
                Console.WriteLine("\n" + message);
            // write to a File
            else WriteToAFile("\n" + message);
        }
    }
}
