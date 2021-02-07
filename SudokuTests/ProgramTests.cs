using Microsoft.VisualStudio.TestTools.UnitTesting;
using SudokuOmegaOfir;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SudokuOmegaOfir.Tests
{
    /*
    This Class is The Test class to my sudoku solver project.   
    The functions build in are tests to most of my project impotrant functions.
    My function are build in AAA test principle.
    In addition, I wrote function to Test 
    the parts of my project in MVC design pattern, without dependency TO each other
    */


    /*
    Function name include (Microsoft convension):
    1.The name of the method being tested.
    2.The scenario under which it's being tested.
    3.The expected behavior when the scenario is invoked.
    */

    [TestClass()]
    public class ProgramTests
    {
        // controller to Use in my unit tests Functions 
        private Controller GameController = new Controller();

        /* Tests to Controller:
         * I test here the SolveForTest that start trying to solve the board
         * Only after validations in input Validator 
         * The input of this function is input from the user represent sudoku board
           the input is not have to be valid or legal sudoku board, it can be any string */


        /* Try to Solve board with Ilegal char in it  */
        [TestMethod()]
        public void SolveForTest_IlegalCharsBoard_ReturnFalse()
        {

            // Arrange
            // create 4 on 4 board 
            string testBoard = "1234" +
                               "0000" +
                               "2013" +
                               "000!";


            // Act
            // try to solve the board
            bool solved = GameController.SolveForTest(testBoard);

            // Assert
            // the function should return false
            // because in the board there is '!' 
            // char that not belongs to 4 on 4 boards

            Assert.AreEqual(solved, false);
        }

        /* Try to Solve board with ilegal size  */
        [TestMethod()]
        public void SolveForTest_IlegalSizeBoard_ReturnFalse()
        {

            // Arrange
            // create 18 chars board 
            string testBoard = "500080049" +
                               "500500030";


            // Act
            // try to solve the board
            bool solved = GameController.SolveForTest(testBoard);

            // Assert
            // the function should return false
            // the lenght of the board is not forth square of any Number
            Assert.AreEqual(solved, false);
        }

        /* Try to Solve valid 9*9 board  */
        [TestMethod()]
        public void SolveForTest_Legal9X9_ReturnTrue()
        {
            // Arrange
            // create valid 9 on 9 sudoku Board 
            string testBoard = "500080049" +
                               "000500030" +
                               "067300001" +
                               "150000000" +
                               "000208000" +
                               "000000018" +
                               "700004150" +
                               "030002000" +
                               "490050000";

            // Act
            // try to solve the testBoard by sending it to controller
            // the controller will send the board to the Model and get if
            // the model success to solve the sudoku
            bool solved = GameController.SolveForTest(testBoard);

            // Assert
            Assert.AreEqual(solved, true);
        }     

        /* Try to Solve valid 16*16 board  */
        [TestMethod()]
        public void SolveForTest_Legal6X16_ReturnTrue()
        {
            // Arrange
            // Arrange
            // create valid 16 on 16 sudoku Board                 
            string testBoard = "6007:1004>2?800=" +
                               "00@>0760<900004?" +
                               "0;=0008<07000069" +
                               "<00400>0036:0720" +
                               "96001000>:050300" +
                               "8<0;9002?@000100" +
                               "04:0>005201;000<" +
                               "10000:00009<?2;0" +
                               "?020007=0109;008" +
                               "300008007=400000" +
                               "0071@<000;005002" +
                               "0@083510000064<0" +
                               "0?05000000020=00" +
                               "2180;0=70?0000>0" +
                               "0060<000900=0000" +
                               "@04:?290;5300080";
            // Act
            bool solved = GameController.SolveForTest(testBoard);

            // Assert
            // the function should success slove the board and return true
            Assert.AreEqual(solved, true);
        }

        /* Try to Solve valid 4*4 board  */
        [TestMethod()]
        public void SolveForTest_Legal4X4_ReturnTrue()
        {
            // Arrange
            // create valid 4 on 4 sudoku Board 
            string testBoard = "3100" +
                               "0200" +
                               "0020" +
                               "0013";

            // Act
            bool solved = GameController.SolveForTest(testBoard);

            // Assert
            // the function should success slove the board and return true
            Assert.AreEqual(solved, true);
        }

        /* Try to Solve Unvalid board by sending it To the controller */
        [TestMethod()]
        public void SolveForTest_HugeBoard_ReturnFalse()
        {

            // Arrange
            // create huge Empty Board 

            string testBoard = new string('0', 120000);
            // Act 
            // Try to solve the board
            bool solved = GameController.SolveForTest(testBoard);

            // Assert
            // The function should return false
            // because my project can handle max 25*25 boards

            Assert.AreEqual(solved, false);
        }

        /* Try to Solve Ilegal 9*9 board  */
        [TestMethod()]
        public void SolveForTest_ILegal9X9_ReturnFalse()
        {
            // Arrange
            // create Ilegal 9 on 9 sudoku Board 
            string testBoard = "500080049" +
                               "000500039" +
                               "067300001" +
                               "150000000" +
                               "000208000" +
                               "000000018" +
                               "700004158" +
                               "030002000" +
                               "490050000";

            // Act
            bool solved = GameController.SolveForTest(testBoard);

            // Assert
            Assert.AreEqual(solved, false);
        }





        /* Model Tests: */

        /* Input Validator class Tests: */

        /*GetValidEdge Tests*/

        /*This function Tests the GetEdge function in InputValidator class
          with unvalid size of string board - big from the solver can solve */
        [TestMethod()]
        public void GetValidEdge_HugeBoard_ReturnNotValid()
        {
            // Arrange
            int testLength = 1000;
            // Act
            int edgeSize = InputValidator.GetValidEdge(testLength);
            // Assert
            // The function need to return -1 if the length is not valid
            Assert.AreEqual(edgeSize, -1);

        }

        /*This function Tests the GetEdge function in InputValidator class
          with unvalid size of string board : not forth square of any number */

        [TestMethod()]
        public void GetValidEdge_CantSquared_ReturnNotValid()
        {
            // Arrange
            int testLength = 82;
            // Act
            int edgeSize = InputValidator.GetValidEdge(testLength);
            // Assert
            // The function need to return -1 if the length is not valid
            Assert.AreEqual(edgeSize, -1);
        }


        /*This function Tests the GetEdge function in InputValidator class
          with valid size of string board  */
        [TestMethod()]
        public void GetValidEdge_Valid25X25_Return25()
        {
            // Arrange
            int testLength = 25 * 25;
            // Act
            int edgeSize = InputValidator.GetValidEdge(testLength);

            //Assert

            // if testLength is Valid the function GetEdge
            // will return the edge size of the board from the string input with that lenght 
            Assert.AreEqual(edgeSize, 25);

        }

        /* IsCharsInRange Tests: */

        /* This function Tests the IsCharsInRange function in InputValidator class
           by set valid char value in 4 on 4 board */
        [TestMethod()]
        public void IsCharsInRange_validChars4X4_ReturnTrue()
        {
            // Arrange
            // build valid 4 on 4 board
            string board = "1004" +
                           "2300" +
                           "0001" +
                           "0030";

            char maxAsci = '4';

            // Act
            // the function will check if all the chars in the board string
            // are in the range from '0' to '4'            
            bool safety = InputValidator.IsCharsInRange(maxAsci, board);


            // Assert
            Assert.AreEqual(safety, true);
        }


        /* This function Tests the IsCharsInRange function in InputValidator class
           by set unvalid char value in 9 on 9 board */
        [TestMethod()]
        public void IsCharsInRange_UnValidCharinBoard_ReturnFalse()
        {
            // Arrange
            // build 9x9 board with unvalid Chars in 
            string board = "50%080049" +
                            "000500030" +
                            "067300001" +
                            "150!00000" +
                            "000208000" +
                            "000000018" +
                            "7000*4150" +
                            "030002000" +
                            "490050000";

            char maxAsci = '9';

            // Act
            // The function will check if all the chars in the board string
            // are in the range from '0' to '9' 
            bool safety = InputValidator.IsCharsInRange(maxAsci, board);

            // Assert
            // The function should find unvalid chars in the board like '%','!','*' and return false
            Assert.AreEqual(safety, false);
        }


        // Board Tests:

        // IsLegalSudoku tests functions :

        /* This function Tests the isValidSudoku function in Board class
           by build a Ilegal board with duplicates in rows/cols/boxes  */

        [TestMethod()]
        public void IsLegalSudoku_IlegalSudokuBoard_ReturnIlegal()
        {
            // Arrange
            // Create Ilegal sudoku board 
            // Notice: The duplicate of '5' in First box 
            string ilegalBoard = "500080049" +
                                "500500030" +
                                "067300001" +
                                "150000009" +
                                "000208008" +
                                "000000018" +
                                "700004150" +
                                "030002000" +
                                "490050000";
            // build a board
            Board board = new Board(ilegalBoard, 9);

            // Act - Check if the sudoku is Valid by 
            //       Search duplicates numbers in each row,col and box
            LegalSudoku sudokuCheck = board.IsLegalSudoku();

            // Assert
            Assert.AreEqual(sudokuCheck, LegalSudoku.Ilegal);
        }


        /* This function Tests the isValidSudoku function in Board class
           by build a Legal board and check his legalization*/
        [TestMethod()]
        public void IsLegalSudoku_LegalSudokuBoard_ReturnLegal()
        {
            // Create Legal 16 on 16 Legal string Board
            string legalBoard = "6?;>00<:03425000" +
                               "10@:02456000?0900" +
                               "00006000:;0>@0007" +
                               "020>800000<03000>" +
                               "0000?000300000604" +
                               "509@00?;0:2000000" +
                               "000008000008<0301" +
                               "0296=:;7>0005000=" +
                               "0<100900093<0@010" +
                               ">=00080?=02000000" +
                               "0504070007003;0?2" +
                               "0015000000040;000" +
                               "506@720608=0>:000" +
                               "30;005080@0900070" +
                               "1=000=?6000;0902<00";

          
            // build a board
            Board board = new Board(legalBoard, 16);

            // Act - Check if the sudoku is Legal by 
            //       Search duplicates numbers in each row,col and box
            LegalSudoku sudokuCheck = board.IsLegalSudoku();

            // Assert
            // The function should return true, this is legal board
            Assert.AreEqual(sudokuCheck, LegalSudoku.Legal);
        }


        // isSafe test functions :

        /*This function tests isSafe function in board by sending 
          the function values that make duplicate values in of the rows*/

        [TestMethod()]
        public void IsSafe_DuplicateInRow_ReturnFalse()
        {
            //Arrange 
            string boardStr = "1304" +
                              "2403" +
                              "3042" +
                              "0230";
            // build a board
            Board board = new Board(boardStr, 4);

            //Act
            // try to put '4' in index [1,3] will make duplicate values in second row
            bool safety = board.IsSafe(0, 3, '1');

            // Assert

            Assert.AreEqual(safety, false);
        }

        /*This function tests isSafe function in board by sending 
          the function values that make duplicate values in of the cols*/

        [TestMethod()]
        public void IsSafe_DuplicateInCol_ReturnFalse()
        {
            // Arrange
            // Build a 16 on 16 string board
            string boardStr = "0006010<80500730" +
                              "00@00=7:01?<0;20" +
                              "83>?;0@5004000:9" +
                              "51;<000?00>0@8=0" +
                              "094000861@<>=00?" +
                              "0000<00=239;0040" +
                              ";803000207040900" +
                              "<00000:0?=85>000" +
                              ":080?>2009;6007=" +
                              "0@=58:34007?<190" +
                              "04?>659@0000;083" +
                              "07000<0;52=04@>:" +
                              "0=940200;>010058" +
                              "0;0070100530?06@" +
                              "000008>9:0@20=;4" +
                              "0000030060009200";
            // Buld a board from boardStr string
            Board board = new Board(boardStr, 16);
            int rowInsert = 0, colInsert = 0;

            // the char that we want to set in rowInsert,colInsert indexes of the board grid
            char numberInsert = ':';

            // Try to put '"' in index [0,0] will make duplicate values in first Col
            // The important thing is to make sure that IsSafe
            // function will return false only because the duplicate in cols
            // we make sure that this value will not cause by mistake also, dupliactes
            // in one of the boxes or rows

            bool safety = board.IsSafe(rowInsert, colInsert, numberInsert);

            // Assert: check if the function isSafe return false as expected
            Assert.AreEqual(safety, false);
        }


        /*This function tests isSafe function in board by sending 
          the function values that make duplicate values in of the boxes*/
        [TestMethod()]
        public void IsSafe_DuplicateInBox_ReturnFalse()
        {
            // build a 16 on 16 string board
            string boardStr = "0006010<80500730" +
                              "00@00=7:01?<0;20" +
                              "83>?;0@5004000:9" +
                              "51;<000?00>0@8=0" +
                              "094000861@<>=00?" +
                              "0000<00=239;0040" +
                              ";803000207040900" +
                              "<00000:0?=85>000" +
                              ":080?>2009;6007=" +
                              "0@=58:34007?<190" +
                              "04?>659@0000;083" +
                              "07000<0;52=04@>:" +
                              "0=940200;>010058" +
                              "0;0070100530?06@" +
                              "000008>9:0@20=;4" +
                              "0000030060009200";

            Board board = new Board(boardStr, 16);
            int rowInsert = 15, colInsert = 3;
            // the char that we want to set in rowInsert,colInsert indexes of the board grid
            char numberInsert = ';';

            // Try to put ';' in index [15,3] will make duplicate values in left low corner box
            // The important thing is to make sure that IsSafe
            // function will return false only because the duplicate in this box
            // we make sure that this value will not cause by mistake also, dupliactes
            // in one of the row or cols

            bool safety = board.IsSafe(rowInsert, colInsert, numberInsert);

            Assert.AreEqual(safety, false);
        }

        /*This function tests isSafe function in board by sending 
          the function indexs and char value that are safe by the rules of sudoku */
        [TestMethod()]
        public void IsSafe_SafeNumber_ReturnTrue()
        {
            // Arrange
            // Build a string board
            string boardStr =   "750001800" +
                                "000000004" +
                                "002000300" +
                                "045070200" +
                                "800300006" +
                                "000000003" +
                                "200000080" +
                                "000000000" +
                                "067050129";

            // Build Board object from boardStr string
            Board board = new Board(boardStr, 9);

            int rowInsert = 1, colInsert = 0;
            // the char that we want to set in rowInsert,colInsert indexes of the board grid
            char numberInsert = '6';



            // Act
            // Check if can set the value of cell in indexes [1,0] 6 according to the rules of sudoku
            bool safety = board.IsSafe(rowInsert, colInsert, numberInsert);

            // Assert
            // We expect the function to return true
            // '6' is safe char to put in [1,0]
            Assert.AreEqual(safety, true);
        }


        /* Tests to SolveGame function in gameSolver class:
           Important: the boards that we create Must be valid and Legal sudoku borads
           Because in my program I build GameSolver only after checking
           The validation of the board in inputValidator class: GetValidEdge,IsCharsInRange
        */

        /* Try to solve 9 on 9 Legal Sudoku board */
        [TestMethod()]
        public void SolveGame_Legal_9X9_ReturnTrue()
        {
            // Arrange
            // create valid 9 on 9 sudoku Board string
            string testBoard = "500080049" +
                               "000500030" +
                               "067300001" +
                               "150000000" +
                               "000208000" +
                               "000000018" +
                               "700004150" +
                               "030002000" +
                               "490050000";

            // build a Board and gameSolver
            Board board = new Board(testBoard, 9);
            GameSolver solver = new GameSolver(board);

            // Act 
            // try to solve the sudoku board
            bool solved = solver.SolveGame();

            // Assert - we expect the solver to Solve the sudoku and return true
            Assert.AreEqual(solved, true);
        }

        /*Try to solve 9 on 9 Legal unsolvable Sudoku board*/
        [TestMethod()]
        public void SolveGame_Unsolvable_9X9_ReturnFalse()
        {
            // Arrange 
            // Create valid 9 on 9 sudoku Board 
            // this board in unslovable because we have to set '9' in
            // indexes [0,8] and this wiil casue '9' dupliacte in the last col

            string testBoard = "123456780" +
                               "000000000" +
                               "000000000" +
                               "009000000" +
                               "000000000" +
                               "000005000" +
                               "000000000" +
                               "030002009" +
                               "000000000";

            Board board = new Board(testBoard, 9);
            GameSolver solver = new GameSolver(board);

            // Act 
            bool solved = solver.SolveGame();

            // Assert - The solver needs to try to solve the board 
            // without success and return false

            Assert.AreEqual(solved, false);
        }

        /*Try to solve 16 on 16 empty board*/
        [TestMethod()]
        public void SolveGame_EmptyBoard16X16_ReturnTrue()
        {
            // Arrange
            // create empty 16 on 16 sudoku Board  
            string testBoard = new string('0', 16 * 16);
            Board board = new Board(testBoard, 16);
            GameSolver solver = new GameSolver(board);

            // Act
            bool solved = solver.SolveGame();

            // Assert
            // empty board is solvable board with a lot of solvign options 
            // the function should return true
            Assert.AreEqual(solved, true);
        }

        /*Try to solve 4 on 4 full board */
        [TestMethod()]
        public void SolveGame_SolvedBoard_ReturnTrue()
        {
            // Arrange
            // Create 4 on 4 solved board
            string slovedBoard = "1234" +
                                 "3412" +
                                 "2143" +
                                 "4321";


            Board board = new Board(slovedBoard, 4);
            GameSolver solver = new GameSolver(board);

            // Act
            bool solved = solver.SolveGame();

            // Assert
            Assert.AreEqual(solved, true);
        }

        /*Try to solve 16 on 16 Legal Sudoku board*/
        [TestMethod()]
        public void SolveGame_Legal_16X16_ReturnTrue()
        {
            // Arrange
            // create valid 16 on 16 sudoku Board 
            string testBoard = "0080040000000003" +
                               "0000000000000000" +
                               "@00000=000000000" +
                               "<0000>000?000000" +
                               "07@000000;030000" +
                               "000000>005000;00" +
                               "00000000=0400000" +
                               "0000000000009000" +
                               "0000060800>00007" +
                               ":0;00000@<000000" +
                               "0000000000?00200" +
                               "000000@>00000000" +
                               "00700=0;00000060" +
                               "0000000000000@00" +
                               "0>002@000=000000" +
                               "000100000000=500";

            Board board = new Board(testBoard, 16);
            GameSolver solver = new GameSolver(board);

            // Act
            bool solved = solver.SolveGame();

            // Assert
            Assert.AreEqual(solved, true);
        }

        /*Try to solve 4 on 4 Legal Sudoku board*/
        [TestMethod()]
        public void SolveGame_Legal_4X4_ReturnTrue()
        {
            // Arrange
            // create valid 4 on 4 sudoku Board 
            string testBoard = "1234" +
                               "3400" +
                               "2040" +
                               "4002";

            Board Sudokuboard = new Board(testBoard, 4);
            GameSolver solver = new GameSolver(Sudokuboard);

            // Act 
            bool solved = solver.SolveGame();

            // Assert
            Assert.AreEqual(solved, true);
        }

        /*Try to solve 4 on 4 Unsolvable Sudoku board*/
        [TestMethod()]
        public void SolveGame_Unsolvable_4X4_ReturnFalse()
        {
            // Arrange
            // Create Legal 4 on 4 sudoku Board 
            // this board in unslovable because we have to set '4' in
            // indexes [2,1] and this wiil casue '4' dupliacte in the third row

            string testBoard = "4000" +
                               "0000" +
                               "1042" +
                               "2300";

            Board Sudokuboard = new Board(testBoard, 4);
            GameSolver solver = new GameSolver(Sudokuboard);

            // Act 
            bool solved = solver.SolveGame();

            // Assert - The solver needs to try to solve the board 
            // without success and return false

            Assert.AreEqual(solved, false);
        }

      

    }
}

