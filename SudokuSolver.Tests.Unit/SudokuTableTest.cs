using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SudokuSolver.Tests.Unit
{
    [TestClass]
    public class SudokuTableTest
    {
        [TestMethod]
        public void VerifyIfTableIs9x9()
        {
            SudokuTable sudokuTable = new SudokuTable();

            // this app is valid for 9x9 2D Sudoku boards
            Assert.IsTrue(sudokuTable.GetTableCells().Rank.Equals(2));
            Assert.IsTrue(sudokuTable.GetTableCells().GetLength(0).Equals(9));
            Assert.IsTrue(sudokuTable.GetTableCells().GetLength(1).Equals(9));
        }

        [TestMethod]
        public void VerifyIfTableCreatedFromArrayIs9x9()
        {
            int[,] array = new int[9, 9]
            {
                { 1,1,1,1,1,1,1,1,1 },
                { 1,1,1,1,1,1,1,1,1 },
                { 1,1,1,1,1,1,1,1,1 },
                { 1,1,1,1,1,1,1,1,1 },
                { 1,1,1,1,1,1,1,1,1 },
                { 1,1,1,1,1,1,1,1,1 },
                { 1,1,1,1,1,1,1,1,1 },
                { 1,1,1,1,1,1,1,1,1 },
                { 1,1,1,1,1,1,1,1,1 },
            };

            SudokuTable sudokuTableFromArray = new SudokuTable(array);
            SudokuTable sudokuTableFromEmptyCtor = new SudokuTable();

            for (int row = 0; row < 9; row++)
            {
                for (int column = 0; column < 9; column++)
                {
                    sudokuTableFromEmptyCtor.SetCell(row, column, 1, true);
                }
            }

            // dimensions and size matches
            Assert.AreEqual(sudokuTableFromEmptyCtor.GetTableCells().Rank, sudokuTableFromArray.GetTableCells().Rank);
            Assert.AreEqual(sudokuTableFromEmptyCtor.GetTableCells().GetLength(0), sudokuTableFromArray.GetTableCells().GetLength(0));
            Assert.AreEqual(sudokuTableFromEmptyCtor.GetTableCells().GetLength(1), sudokuTableFromArray.GetTableCells().GetLength(1));

            // values match. No idea why equality comparison between entire SudokuTables as objects fails...
            for (int row = 0; row < 9; row++)
            {
                for (int column = 0; column < 9; column++)
                {
                    Assert.IsTrue(sudokuTableFromEmptyCtor.GetTableCells()[row, column].Value.Equals(sudokuTableFromArray.GetTableCells()[row, column].Value));
                }
            }
        }

        [TestMethod]
        public void VerifyGetBoxNumber()
        {
            // expect numbers 1- 9 to address each of 3x3 boxes of sudoku table
            SudokuTable st = new SudokuTable();

            Assert.IsTrue(st.GetBoxNumber(0, 0).Equals(1));
            Assert.IsTrue(st.GetBoxNumber(2, 2).Equals(1));
            Assert.IsTrue(st.GetBoxNumber(2, 5).Equals(2));
            Assert.IsTrue(st.GetBoxNumber(0, 3).Equals(2));
            Assert.IsTrue(st.GetBoxNumber(0, 6).Equals(3));
            Assert.IsTrue(st.GetBoxNumber(2, 8).Equals(3));
            Assert.IsTrue(st.GetBoxNumber(3, 0).Equals(4));
            Assert.IsTrue(st.GetBoxNumber(5, 2).Equals(4));
            Assert.IsTrue(st.GetBoxNumber(3, 6).Equals(6));
            Assert.IsTrue(st.GetBoxNumber(5, 8).Equals(6));
            Assert.IsTrue(st.GetBoxNumber(6, 0).Equals(7));
            Assert.IsTrue(st.GetBoxNumber(8, 2).Equals(7));
            Assert.IsTrue(st.GetBoxNumber(6, 6).Equals(9));
            Assert.IsTrue(st.GetBoxNumber(8, 8).Equals(9));
        }

        [TestMethod]
        public void VerifyGetNextEmptyCell()
        {
            int[,] array = new int[9, 9]
            {
                { 1,1,1,1,1,1,1,1,1 },
                { 1,1,1,1,1,1,1,1,1 },
                { 1,1,1,1,1,1,1,1,1 },
                { 1,1,1,1,1,1,1,1,1 },
                { 1,1,1,1,1,1,1,1,1 },
                { 1,1,1,1,1,1,1,1,1 },
                { 1,1,1,1,1,1,1,1,1 },
                { 1,1,1,1,1,1,1,1,1 },
                { 1,1,1,1,1,1,1,1,1 },
            };

            SudokuTable st = new SudokuTable(array);
            SudokuTableValidator stv = new SudokuTableValidator(st);

            // no empty cells, so it should return [-1,-1]
            Assert.IsTrue(st.GetNextEmptyCell()[0].Equals(-1));
            Assert.IsTrue(st.GetNextEmptyCell()[1].Equals(-1));


            array = new int[9, 9]
{
                { 0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0 },
};

            st = new SudokuTable(array);
            stv = new SudokuTableValidator(st);

            // all empty cells, so 1st one - [0,0]
            Assert.IsTrue(st.GetNextEmptyCell()[0].Equals(0));
            Assert.IsTrue(st.GetNextEmptyCell()[1].Equals(0));


            array = new int[9, 9]
{
                { 1,2,3,4,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0 },
};

            st = new SudokuTable(array);
            stv = new SudokuTableValidator(st);

            // 1st empty cell - [0,4]
            Assert.IsTrue(st.GetNextEmptyCell()[0].Equals(0));
            Assert.IsTrue(st.GetNextEmptyCell()[1].Equals(4));


            array = new int[9, 9]
{
                { 1,2,3,4,5,6,7,8,9 },
                { 1,2,3,4,5,6,7,8,9 },
                { 1,2,3,4,5,6,7,8,9 },
                { 1,2,3,4,5,6,7,8,9 },
                { 0,2,3,4,5,6,7,8,0 },
                { 1,2,3,4,5,6,7,8,9 },
                { 1,2,3,4,5,6,7,8,9 },
                { 1,2,3,4,5,6,7,8,9 },
                { 1,2,3,4,5,6,7,8,9 },
};

            st = new SudokuTable(array);
            stv = new SudokuTableValidator(st);

            // 1st empty cell - [4,0]
            Assert.IsTrue(st.GetNextEmptyCell()[0].Equals(4));
            Assert.IsTrue(st.GetNextEmptyCell()[1].Equals(0));


            array = new int[9, 9]
{
                { 1,2,3,4,5,6,7,8,9 },
                { 1,2,3,4,5,6,7,8,9 },
                { 1,2,3,4,5,6,7,8,9 },
                { 1,2,3,4,5,6,7,8,9 },
                { 1,2,3,4,5,6,7,8,0 },
                { 1,2,3,4,5,6,7,8,9 },
                { 1,2,3,4,5,6,7,8,9 },
                { 1,2,3,4,5,6,7,8,9 },
                { 1,2,3,4,5,6,7,8,9 },
};

            st = new SudokuTable(array);
            stv = new SudokuTableValidator(st);

            // 1st empty cell - [4,8]
            Assert.IsTrue(st.GetNextEmptyCell()[0].Equals(4));
            Assert.IsTrue(st.GetNextEmptyCell()[1].Equals(8));


            array = new int[9, 9]
{
                { 1,2,3,4,5,6,7,8,9 },
                { 1,2,3,4,5,6,7,8,9 },
                { 1,2,3,4,5,6,7,8,9 },
                { 1,2,3,4,5,6,7,8,9 },
                { 1,2,3,4,5,6,7,8,9 },
                { 1,2,3,4,5,6,7,8,9 },
                { 1,2,3,4,5,6,7,8,9 },
                { 1,2,3,4,5,6,7,8,9 },
                { 0,2,3,4,5,6,7,8,9 },
};

            st = new SudokuTable(array);
            stv = new SudokuTableValidator(st);

            // 1st empty cell - [8,0]
            Assert.IsTrue(st.GetNextEmptyCell()[0].Equals(8));
            Assert.IsTrue(st.GetNextEmptyCell()[1].Equals(0));


            array = new int[9, 9]
{
                { 1,2,3,4,5,6,7,8,9 },
                { 1,2,3,4,5,6,7,8,9 },
                { 1,2,3,4,5,6,7,8,9 },
                { 1,2,3,4,5,6,7,8,9 },
                { 1,2,3,4,5,6,7,8,9 },
                { 1,2,3,4,5,6,7,8,9 },
                { 1,2,3,4,5,6,7,8,9 },
                { 1,2,3,4,5,6,7,8,9 },
                { 1,2,3,4,5,6,7,8,0 },
};

            st = new SudokuTable(array);
            stv = new SudokuTableValidator(st);

            // 1st empty cell - [8,8]
            Assert.IsTrue(st.GetNextEmptyCell()[0].Equals(8));
            Assert.IsTrue(st.GetNextEmptyCell()[1].Equals(8));

        }

        [TestMethod]
        public void VerifySolvingWorksCorrectly()
        {

            int[,] simpleSudoku = new int[9, 9]
            {
                { 2,0,7,0,0,0,0,0,0 },
                { 0,9,4,0,0,5,0,3,0 },
                { 5,3,0,0,2,4,1,0,0 },
                { 0,0,0,0,0,0,2,5,0 },
                { 0,0,1,0,9,0,7,0,0 },
                { 0,8,5,0,0,0,0,0,0 },
                { 0,0,9,4,8,0,0,7,1 },
                { 0,1,0,7,0,0,3,2,0 },
                { 0,0,0,0,0,0,4,0,5 }
            };

            int[,] solvedSimpleSudoku = new int[9, 9]
            {
                { 2, 6, 7, 1, 3, 8, 5, 4, 9},
                { 1, 9, 4, 6, 7, 5, 8, 3, 2},
                { 5, 3, 8, 9, 2, 4, 1, 6, 7},
                { 9, 4, 3, 8, 1, 7, 2, 5, 6},
                { 6, 2, 1, 5, 9, 3, 7, 8, 4},
                { 7, 8, 5, 2, 4, 6, 9, 1, 3},
                { 3, 5, 9, 4, 8, 2, 6, 7, 1},
                { 4, 1, 6, 7, 5, 9, 3, 2, 8},
                { 8, 7, 2, 3, 6, 1, 4, 9, 5}
            };


            SudokuTable st = new SudokuTable(simpleSudoku);
            st.Solve(10);

            SudokuTable st1 = new SudokuTable(solvedSimpleSudoku);

            for (int row = 0; row < st.GetTableCells().GetLength(0); row++)
            {
                for (int col = 0; col < st.GetTableCells().GetLength(1); col++)
                {
                    Assert.IsTrue(st.GetTableCells()[row, col].Value == st1.GetTableCells()[row, col].Value);
                }
            }
        }

        [TestMethod]
        public void VerifyUnsolvableFailsGracefully()
        {

            int[,] simpleSudoku = new int[9, 9]
            {
                // changed 7 at position [0,2] to 8 making it unsolvable
                // replace 8 with 7 there to make it solvable again
                { 2,0,8,0,0,0,0,0,0 },
                { 0,9,4,0,0,5,0,3,0 },
                { 5,3,0,0,2,4,1,0,0 },
                { 0,0,0,0,0,0,2,5,0 },
                { 0,0,1,0,9,0,7,0,0 },
                { 0,8,5,0,0,0,0,0,0 },
                { 0,0,9,4,8,0,0,7,1 },
                { 0,1,0,7,0,0,3,2,0 },
                { 0,0,0,0,0,0,4,0,5 }
            };

            SudokuTable st = new SudokuTable(simpleSudoku);
            Assert.IsFalse(st.Solve(10));
        }

        /*
        [TestMethod]
        public void VerifyUnsolvableInTimeLimitFailsGracefully()
        {

            int[,] simpleSudoku = new int[9, 9]
            {
                {9,0,0,8,0,0,0,0,0},
                {0,0,0,0,0,0,5,0,0},
                {0,0,0,0,0,0,0,0,0},
                {0,2,0,0,1,0,0,0,3},
                {0,1,0,0,0,0,0,6,0},
                {0,0,0,4,0,0,0,7,0},
                {7,0,8,6,0,0,0,0,0},
                {0,0,0,0,3,0,1,0,0},
                {4,0,0,0,0,0,2,0,0}
            };

            SudokuTable st = new SudokuTable(simpleSudoku);
            Assert.IsTrue(st.Solve(10));
        }
        */


        [TestMethod]
        public void VerifyHashCodesForTheSameSudokuTablesEqual()
        {
            int[,] simpleSudoku = new int[9, 9]
            {
                { 2,0,7,0,0,0,0,0,0 },
                { 0,9,4,0,0,5,0,3,0 },
                { 5,3,0,0,2,4,1,0,0 },
                { 0,0,0,0,0,0,2,5,0 },
                { 0,0,1,0,9,0,7,0,0 },
                { 0,8,5,0,0,0,0,0,0 },
                { 0,0,9,4,8,0,0,7,1 },
                { 0,1,0,7,0,0,3,2,0 },
                { 0,0,0,0,0,0,4,0,5 }
            };

            // pre - solving equal
            SudokuTable st = new SudokuTable(simpleSudoku);
            SudokuTable st1 = new SudokuTable(simpleSudoku);
            Assert.IsTrue(st.GetHashCode().Equals(st1.GetHashCode()));

            // post - solving equal
            st.Solve(10);
            st1.Solve(10);
            Assert.IsTrue(st.GetHashCode().Equals(st1.GetHashCode()));
        }
    }
}
