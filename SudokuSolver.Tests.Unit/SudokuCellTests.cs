using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SudokuSolver.Tests.Unit
{
    [TestClass]
    public class SudokuCellTests
    {
        [TestMethod]
        public void TrySetInvalidValue()
        {
            SudokuCell cell = new SudokuCell();

            Assert.IsFalse(cell.TrySet(-1,false));
            Assert.IsFalse(cell.TrySet(10,false));
        }

        [TestMethod]
        public void TrySetGoodValue()
        {
            SudokuCell cell = new SudokuCell();
            Assert.IsTrue(cell.TrySet(1,false));

            SudokuCell cell1 = new SudokuCell();
            Assert.IsTrue(cell1.TrySet(4,false));

            SudokuCell cell2 = new SudokuCell();
            Assert.IsTrue(cell2.TrySet(9,false));

            SudokuCell cell3 = new SudokuCell();
            Assert.IsTrue(cell3.TrySet(0, false));
        }

        [TestMethod]
        public void TrySetValueToCellThatAlreadyHasValue()
        {
            // overwriting existing cell is allowed, providing it is not set as initialized from array or file
            SudokuCell cell = new SudokuCell();

            Assert.IsTrue(cell.TrySet(1,false));
            Assert.IsFalse(cell.TrySet(1,false));
        }

        [TestMethod]
        public void TrySetValueToInitiallySetCell()
        {
            SudokuCell cell = new SudokuCell();

            // overwriting cell value SET initially from file or array is NOT allowed
            Assert.IsTrue(cell.TrySet(1, false));
            Assert.IsFalse(cell.TrySet(0, false));

        }

        [TestMethod]
        public void TrySetValueToNotInitiallySetCell()
        {
            SudokuCell cell = new SudokuCell();

            // overwriting cell value is allowed, provided the flag is set to changeable
            Assert.IsTrue(cell.TrySet(1, true));
            Assert.IsTrue(cell.TrySet(9, true));
        }

        [TestMethod]
        public void DisallowSetIsChangeableFromFalseToTrue()
        {
            SudokuCell cell = new SudokuCell();

            // once we set up cell from file or array and say its NOT changeable, we cannot set it back to be changeable
            Assert.IsTrue(cell.TrySet(1, false));
            Assert.IsFalse(cell.TrySet(0, true));
        }

        [TestMethod]
        public void VerifyCellEqualityWorksCorrectly()
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

            SudokuTable st = new SudokuTable(simpleSudoku);
            SudokuTable st1 = new SudokuTable(simpleSudoku);

            for (int row = 0; row < st.GetTableCells().GetLength(0); row++)
            {
                for (int col = 0; col < st.GetTableCells().GetLength(1); col++)
                {
                    Assert.IsTrue(st.GetTableCells()[row, col].Equals(st1.GetTableCells()[row, col]));
                }
            }
        }
    }
}
