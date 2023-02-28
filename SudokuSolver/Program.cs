using System;

namespace SudokuSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            string Url = "../../../hardToDFS_sudoku.txt";
            SudokuTable newSudoku = new SudokuTable();

            FileReaderWorker fileWorker = new FileReaderWorker();
            fileWorker.InitializeFromFile(newSudoku, Url);

            Console.WriteLine("Sudoku table from file:");
            Console.WriteLine(newSudoku.ToString());
            
//asjfakjfhskdfjh            {
                Console.WriteLine("Solved sudoku:");
                Console.WriteLine(newSudoku.ToString());
            }
            else
            {
                Console.WriteLine("Could not solve sudoku");
            }
            Console.ReadLine();

            // Acaba diger cs file olursa komitler ayni yerde gozukur mu?
        }
    }
}
