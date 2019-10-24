using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestFramework;

namespace UITest
{
    [TestClass]
    public class Table_Test
    {
        [TestInitialize]
        public void Init()
        {
            Driver.Inicialize();            
        }

        [TestMethod]
        public void TableTest()
        {
            Table.WaitForTable();

            var info = Table.GetColumnNames();

            foreach (var i in info)
            {
                Console.WriteLine(i);
            }

            Table.GetResults(3);

            Console.WriteLine(Table.GetNumberOfResults());

            Table.Click("1", "uwi");
        }

        [TestCleanup]
        public void Creanup()
        {
            Driver.Close();
            Driver.Quit();
        }
    }
}
