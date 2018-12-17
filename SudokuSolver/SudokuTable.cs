using System;
using System.Text;
using System.Threading;

namespace SudokuSolver
{
    class SudokuTable : IEquatable<SudokuTable>
    {
        // not set cells will have 0 in them 
        private readonly SudokuCell[,] initialTable;
        private SudokuCell[,] workingTable;
        private SudokuTableValidator sudokuTableValidator;

        // ctors
        public SudokuTable()
        {
            this.initialTable = new SudokuCell[9, 9]
            {
                { new SudokuCell(), new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell() },
                { new SudokuCell(), new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell() },
                { new SudokuCell(), new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell() },
                { new SudokuCell(), new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell() },
                { new SudokuCell(), new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell() },
                { new SudokuCell(), new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell() },
                { new SudokuCell(), new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell() },
                { new SudokuCell(), new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell() },
                { new SudokuCell(), new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell() }
            };
            this.workingTable = new SudokuCell[9, 9]
            {
                { new SudokuCell(), new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell() },
                { new SudokuCell(), new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell() },
                { new SudokuCell(), new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell() },
                { new SudokuCell(), new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell() },
                { new SudokuCell(), new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell() },
                { new SudokuCell(), new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell() },
                { new SudokuCell(), new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell() },
                { new SudokuCell(), new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell() },
                { new SudokuCell(), new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell(),new SudokuCell() }
            };
            sudokuTableValidator = new SudokuTableValidator(this);
        }

        public SudokuTable(string Url) : this()
        {
            if (Url != string.Empty)
            {
                FileReaderWorker reader = new FileReaderWorker();
                reader.InitializeFromFile(this, Url);
            }
        }

        public SudokuTable(int[,] array) : this()
        {
            if (workingTable.GetLength(0).Equals(9) && workingTable.GetLength(1).Equals(9) && workingTable.Rank.Equals(2))
            {
                for (int row = 0; row < 9; row++)
                {
                    for (int column = 0; column < 9; column++)
                    {
                        bool isChangeable = true;
                        if (!array[row, column].Equals(0))
                        {
                            isChangeable = false;
                        }
                        workingTable[row, column].TrySet(array[row, column], isChangeable);
                    }
                }
            }
            else
            {
                throw new Exception($"Exception. Trying to initialize sudoku table with wrong array." +
                    $"Expected dimensions: 2. Found: {workingTable.Rank}." +
                    $"Expected 1st dimension size: 9. Found: {workingTable.GetLength(0)}." +
                    $"Expected 2nd dimension size: 9. Found: {workingTable.GetLength(1)}."
                    );
            }
        }

        // methods
        public void SetCell(int row, int column, int newValue, bool isChangeable)
        {
            this.workingTable[row, column].TrySet(newValue, isChangeable);

            // in initial table we set things once, and leave them readonly
            this.initialTable[row, column].TrySet(newValue, false);
        }

        public int[] GetNextEmptyCell()
        {
            int[] arrayAddress = new int[2] { -1,-1};

            for (int row = 0; row < 9; row++)
            {
                for (int column = 0; column < 9; column++)
                {
                    if (this.GetTableCells()[row,column].Value.Equals(0))
                    {
                        arrayAddress.SetValue(row, 0);
                        arrayAddress.SetValue(column, 1);
                        // we need first occurance
                        return arrayAddress;
                    }
                }
            }

            return arrayAddress;
        }

        public SudokuCell GetCell(int row, int column)
        {
            return this.workingTable[row, column];
        }

        internal SudokuCell[,] GetTableCells()
        {
            return workingTable;
        }

        public int GetBoxNumber(int row, int column)
        {
            // 1 - 9 for boxes numbers
            int boxNumber = ((int)(row / 3)) * 3 + column / 3 + 1;
            return boxNumber;
        }

        // Sudoku depth first search algorithm - simplest, no optimizations 
        public bool Solve(int secondsToTimeout)
        {
            Timer timer = new Timer(SolvingTimeout, null, secondsToTimeout * 1000, Timeout.Infinite);

            int[] nextEmptyCellCoordinates = GetNextEmptyCell();
            int row = nextEmptyCellCoordinates[0];
            int col = nextEmptyCellCoordinates[1];

            if (row.Equals(-1) && col.Equals(-1))
            {
                return true;
            }
            for (int val = 1; val<10; val++)
            {
                if(sudokuTableValidator.IsSafe(row,col,val))
                {
                    SetCell(row, col, val,true);
                    if(Solve(secondsToTimeout))
                    {
                        return true;
                    }
                    else
                    {
                        SetCell(row, col, 0, true);
                    }
                }
            }
            return false;
        }

        private void SolvingTimeout(object state)
        {
            Console.WriteLine($"Could not solve sudoku in time limit.");
            Console.WriteLine("Closing...");
            Thread.Sleep(5000);
            Environment.Exit(0);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < workingTable.GetLength(0); i++)
            {
                if (i == 0)
                {
                    sb.AppendLine("-------------------------------------");
                }
                for (int j = 0; j < workingTable.GetLength(1); j++)
                {
                    if (j == 0)
                    {
                        sb.Append("| ");
                    } else
                    {
                        sb.Append(" | ");
                    }
                    if (workingTable[i, j].Value.ToString() == "0")
                    {
                        sb.Append(" ");
                    }
                    else
                    {
                        sb.Append(workingTable[i, j].Value.ToString());
                    }
                    if (j == workingTable.GetLength(1) - 1)
                    {
                        sb.Append(" | \n");
                    }
                }
                sb.AppendLine("-------------------------------------");
            }
            return sb.ToString();
        }

        public bool Equals(SudokuTable other)
        {
            return other != null && Array.Equals(initialTable, other.initialTable) && Array.Equals(workingTable, other.workingTable);
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();

            // initial table holds sudoku initial layout
            for (int row = 0; row < initialTable.GetLength(0); row++)
            {
                for (int column = 0; column < initialTable.GetLength(1); column++)
                {
                    hash.Add(initialTable[row, column]);
                }
            }

            // working table holds current sudoku table state
            for (int row = 0; row < workingTable.GetLength(0); row++)
            {
                for (int column = 0; column < workingTable.GetLength(1); column++)
                {
                    hash.Add(workingTable[row, column]);
                }
            }

            return hash.ToHashCode();
        }
    }
}
