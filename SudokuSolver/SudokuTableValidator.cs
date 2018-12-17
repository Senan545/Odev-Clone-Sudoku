using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuSolver
{
    class SudokuTableValidator
    {
        private SudokuTable table;

        public SudokuTableValidator(SudokuTable sudokuTable)
        {
            table = sudokuTable;
        }

        public bool IsSafe(int row, int column, int value)
        {
            int boxNumber = table.GetBoxNumber(row, column);
            return !(IsUsedInRow(row, value) || IsUsedInColumn(column, value) || IsUsedInBox(boxNumber, value));
        }

        public bool IsUsedInColumn(int columnNumber, int value)
        {
            for (int row = 0; row < 9; row++)
            {
                if (table.GetTableCells()[row,columnNumber].Value.Equals(value))
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsUsedInRow(int rowNumber, int value)
        {
            for (int column = 0; column < 9; column++)
            {
                if (table.GetTableCells()[rowNumber,column].Value.Equals(value))
                {
                    return true;
                }
            }
            return false;
        }

        // there are 9 boxes of 3x3 size - 1 - 9
        public bool IsUsedInBox(int boxNumber, int value)
        {
            int startingRow = (int)((boxNumber - 1) / 3) * 3;
            int endingRow = startingRow + 3;

            Math.DivRem(boxNumber-1,3, out int modulo);
            int startingColumn = modulo * 3;
            int endingColumn = startingColumn + 3;

            for (int row = startingRow; row < endingRow; row++)
            {
                for (int column = startingColumn; column < endingColumn; column++)
                {
                    if (table.GetTableCells()[row,column].Value.Equals(value))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

    }
}
