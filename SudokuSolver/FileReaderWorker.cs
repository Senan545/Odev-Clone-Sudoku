using System;
using System.IO;

namespace SudokuSolver
{
    class FileReaderWorker
    {
        public FileReaderWorker ()
        {
            // nothing to do here commit
        }

        // this reads file, iterates over it, and calls a method to process each line as row
        public void InitializeFromFile(SudokuTable sudokuTable, string Url)
        {
            try
            {
                StreamReader sr = new StreamReader(Url);
                string line = string.Empty;
                int row = 0;

                using (sr)
                {
                    while (sr.Peek() != -1)
                    {
                        line = sr.ReadLine();
                        ConvertLineFromFileToRow(sudokuTable, row, line);
                        row++;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An exception occured during reading file {0}", e.Message);
            }
        }

        // this converts a line from sudoku file, into a row in a storage
        private void ConvertLineFromFileToRow(SudokuTable sudokuTable, int currentRow, string line)
        {
            // get fields with no number set as empty string
            string[] row = line.Split(',', StringSplitOptions.None);

            int columnNumber = 0;

            foreach (string item in row)
            {
                if (int.TryParse(item.Trim(), out int value))
                {
                    bool isChangeable = true;
                    if (!value.Equals(0))
                    {
                        // value different than 0, set field to be NOT changeable
                        isChangeable = false;
                    }
                    sudokuTable.SetCell(currentRow, columnNumber, value, isChangeable);
                }
                else if (item.Trim().Equals(string.Empty))
                {
                    // if empty, we simply leave default value inside this cell     
                }
                else
                {
                    // not empty, and not covertible to int
                    Console.WriteLine($"Can't convert value at line {currentRow + 1}");
                }
                columnNumber++;
            }
        }
    }
}