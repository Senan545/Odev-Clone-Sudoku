using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SudokuSolver.Tests.Unit
{
    [TestClass]
    public class SudokuTableValidatorTest
    {
        [TestMethod]
        public void VerifyIsValueUsedInBox()
        {
            int[,] array = new int[9, 9]
            {
                { 0,2,3,1,0,0,0,0,0 },
                { 4,5,6,0,0,0,0,0,0 },
                { 7,8,9,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,9,8,7 },
                { 0,0,0,0,0,0,6,5,4 },
                { 0,0,0,0,0,0,3,2,0 },
            };

            SudokuTable st = new SudokuTable(array);
            SudokuTableValidator stv = new SudokuTableValidator(st);

            // box 1 - numbers 2-9 are used, 1 is available to set
            Assert.IsFalse(stv.IsUsedInBox(1, 1));
            Assert.IsTrue(stv.IsUsedInBox(1, 5));
            Assert.IsTrue(stv.IsUsedInBox(1, 9));

            // box 2 - number 1 is used, 2-9 are available to set
            Assert.IsTrue(stv.IsUsedInBox(2, 1));
            Assert.IsFalse(stv.IsUsedInBox(2, 2));
            Assert.IsFalse(stv.IsUsedInBox(2, 3));
            Assert.IsFalse(stv.IsUsedInBox(2, 4));
            Assert.IsFalse(stv.IsUsedInBox(2, 5));
            Assert.IsFalse(stv.IsUsedInBox(2, 6));
            Assert.IsFalse(stv.IsUsedInBox(2, 7));
            Assert.IsFalse(stv.IsUsedInBox(2, 8));
            Assert.IsFalse(stv.IsUsedInBox(2, 9));

            // box 9 - numbers 2-9 are used, 1 is available
            Assert.IsFalse(stv.IsUsedInBox(9, 1));
            Assert.IsTrue(stv.IsUsedInBox(9, 2));
            Assert.IsTrue(stv.IsUsedInBox(9, 3));
            Assert.IsTrue(stv.IsUsedInBox(9, 4));
            Assert.IsTrue(stv.IsUsedInBox(9, 5));
            Assert.IsTrue(stv.IsUsedInBox(9, 6));
            Assert.IsTrue(stv.IsUsedInBox(9, 7));
            Assert.IsTrue(stv.IsUsedInBox(9, 8));
            Assert.IsTrue(stv.IsUsedInBox(9, 9));
        }

        [TestMethod]
        public void VerifyIsValueUsedInRow()
        {
            int[,] array = new int[9, 9]
            {
                { 1,2,3,4,5,0,7,8,9 },
                { 0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0 },
                { 0,8,7,6,5,4,3,2,1 },
                { 0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0 },
                { 9,8,7,6,5,4,3,2,1 },
            };

            SudokuTable st = new SudokuTable(array);
            SudokuTableValidator stv = new SudokuTableValidator(st);

            // row 0 - 2-9 are used, 1 is available
            Assert.IsFalse(stv.IsUsedInRow(0, 6));
            // Assert.IsTrue(stv.IsUsedInRow(0, 1));
            Assert.IsTrue(stv.IsUsedInRow(0, 9));

            // row 1 - 1-9 are not used
            Assert.IsFalse(stv.IsUsedInRow(1, 1));
            Assert.IsFalse(stv.IsUsedInRow(1, 2));
            Assert.IsFalse(stv.IsUsedInRow(1, 3));
            Assert.IsFalse(stv.IsUsedInRow(1, 4));
            Assert.IsFalse(stv.IsUsedInRow(1, 5));
            Assert.IsFalse(stv.IsUsedInRow(1, 6));
            Assert.IsFalse(stv.IsUsedInRow(1, 7));
            Assert.IsFalse(stv.IsUsedInRow(1, 8));
            Assert.IsFalse(stv.IsUsedInRow(1, 9));

            // row 4 - 1-8 are used
            Assert.IsFalse(stv.IsUsedInRow(4, 9));
            Assert.IsTrue(stv.IsUsedInRow(4, 8));
            Assert.IsTrue(stv.IsUsedInRow(4, 4));
            Assert.IsTrue(stv.IsUsedInRow(4, 1));

            // row 9 - all 1-9 are used
            Assert.IsTrue(stv.IsUsedInRow(8, 1));
            Assert.IsTrue(stv.IsUsedInRow(8, 4));
            Assert.IsTrue(stv.IsUsedInRow(8, 9));
        }

        [TestMethod]
        public void VerifyIsValueUsedInColumn()
        {
            int[,] array = new int[9, 9]
            {
                { 1,1,0,9,0,3,0,0,9 },
                { 2,0,0,0,0,0,0,8,8 },
                { 3,0,0,0,0,0,0,7,7 },
                { 4,0,0,0,0,0,0,6,6 },
                { 5,0,0,0,0,0,0,5,5 },
                { 6,0,0,0,0,0,0,4,4 },
                { 7,0,0,0,0,0,0,3,3 },
                { 8,0,0,0,0,0,0,2,2 },
                { 0,0,1,0,9,0,3,1,0 },
            };

            SudokuTable st = new SudokuTable(array);
            SudokuTableValidator stv = new SudokuTableValidator(st);

            // 1st col - 1-8 are used, 9 is available
            Assert.IsTrue(stv.IsUsedInColumn(0, 1));
            Assert.IsFalse(stv.IsUsedInColumn(0, 9));

            // 2nd col - only 1 is used
            Assert.IsTrue(stv.IsUsedInColumn(1, 1));
            Assert.IsFalse(stv.IsUsedInColumn(1, 5));
            Assert.IsFalse(stv.IsUsedInColumn(1, 9));

            // 3rd col - only 1 is used, different position
            Assert.IsTrue(stv.IsUsedInColumn(2, 1));
            Assert.IsFalse(stv.IsUsedInColumn(2, 5));
            Assert.IsFalse(stv.IsUsedInColumn(2, 9));

            // 4th col - only 9 is used
            Assert.IsTrue(stv.IsUsedInColumn(3, 9));
            Assert.IsFalse(stv.IsUsedInColumn(3, 6));
            Assert.IsFalse(stv.IsUsedInColumn(3, 2));

            // 5th col - only 9 is used, different position
            Assert.IsTrue(stv.IsUsedInColumn(4, 9));
            Assert.IsFalse(stv.IsUsedInColumn(4, 6));
            Assert.IsFalse(stv.IsUsedInColumn(4, 2));

            // 6th col - only 3 is used
            Assert.IsTrue(stv.IsUsedInColumn(5, 3));
            Assert.IsFalse(stv.IsUsedInColumn(5, 2));
            Assert.IsFalse(stv.IsUsedInColumn(5, 8));

            // 7th col - only 3 is used, different position
            Assert.IsTrue(stv.IsUsedInColumn(6, 3));
            Assert.IsFalse(stv.IsUsedInColumn(6, 2));
            Assert.IsFalse(stv.IsUsedInColumn(6, 8));

            // 8th col - 1-8 are used, 9 avail
            Assert.IsFalse(stv.IsUsedInColumn(7, 9));
            Assert.IsTrue(stv.IsUsedInColumn(7, 1));
            Assert.IsTrue(stv.IsUsedInColumn(7, 8));

            // 8th col - 2-9 are used, 1 avail
            Assert.IsTrue(stv.IsUsedInColumn(8, 2));
            Assert.IsTrue(stv.IsUsedInColumn(8, 9));
            Assert.IsFalse(stv.IsUsedInColumn(8, 1));
        }

        [TestMethod]
        public void VerifyIsSafe()
        {
            // fully solved sudoku table, but position [4,4], where 9 is missing
            int[,] array = new int[9, 9]
{
                { 2,6,7,1,3,8,5,4,9 },
                { 1,9,4,6,7,5,8,3,2 },
                { 5,3,8,9,2,4,1,6,7 },
                { 9,4,3,8,1,7,2,5,6 },
                { 6,2,1,5,0,3,7,8,4 },
                { 7,8,5,2,4,6,9,1,3 },
                { 3,5,9,4,8,2,6,7,1 },
                { 4,1,6,7,5,9,3,2,8 },
                { 8,7,2,3,6,1,4,9,5 },
};

            SudokuTable st = new SudokuTable(array);
            SudokuTableValidator stv = new SudokuTableValidator(st);

            Assert.IsTrue(stv.IsSafe(4, 4, 9));
            Assert.IsFalse(stv.IsSafe(4, 4, 1));
            Assert.IsFalse(stv.IsSafe(4, 4, 8));
            Assert.IsFalse(stv.IsSafe(4, 4, 3));
        }
    }
}
