using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace SudokuSolver.Tests.Unit
{
    [TestClass]
    public class FileReaderWorkerTest
    {
        [TestMethod]
        public void VerifyInitializeFromFile()
        {
            string Url = "testfile.txt";
            StreamWriter sw = new StreamWriter(Url);

            try
            {
                using (sw)
                {
                    sw.WriteLine(" , , , , , , ,1,2");
                    sw.WriteLine(" , , , ,3,5, , ,");
                    sw.WriteLine(" , , ,6, , , ,7,3");
                    sw.WriteLine("7, , , , , ,3, ,");
                    sw.WriteLine(" , , ,4, , ,8, ,");
                    sw.WriteLine("1, , , , , , , ,");
                    sw.WriteLine(" , , ,1,2, , , ,");
                    sw.WriteLine(" ,8, , , , , , ,");
                    sw.WriteLine(" ,5, , , , ,6, ,");
                }
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine(e.Message);
            }

            SudokuTable st = new SudokuTable(Url);

            Assert.AreEqual(st.GetTableCells()[0, 0].Value, 0);
            Assert.AreEqual(st.GetTableCells()[0, 8].Value, 2);
            Assert.AreEqual(st.GetTableCells()[4, 3].Value, 4);
            Assert.AreEqual(st.GetTableCells()[8, 0].Value, 0);
            Assert.AreEqual(st.GetTableCells()[7, 8].Value, 0);
            Assert.AreEqual(st.GetTableCells()[8, 8].Value, 0);
        }
    }
}
