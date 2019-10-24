using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFramework
{
    public class Table
    {
        public static int GetNumberOfResults()
        {
            IList<IWebElement> Number = Driver.Instance.FindElements(By.XPath("//div[@class = 'table-body']/div[contains(@class, 'table-row')]"));
            return Number.Count();
        }

        public static List<string> GetColumnNames()
        {
            List<string> Column_info = new List<string>();

            //List number of columns (XPath for column header)
            IList<IWebElement> Columns = Driver.Instance.FindElements(By.XPath("//header/div[contains(@class, 'table-header-column')]"));
                        
            //List name of columns
            foreach (var Column in Columns) {
                Column_info.Add(Column.Text);
            }

            return Column_info;

        }

        public static void Click(string column, string value)
        {
            var path = String.Format("//div[contains(@class, 'table-row-column')][{0}]//*[text() = '{1}']", column, value);
            Driver.Instance.FindElement(By.XPath(path)).Click();
        }

        public static Dictionary<int, Dictionary<string, string>> GetResults(int Index)
        {
            var columns = GetColumnNames(); // Get column names
            string name = String.Format("//div[@class = 'table-body']/div[contains(@class, table-row)][{0}]", Index); // instead [{0}] use ""
            var elements = Driver.Instance.FindElements(By.XPath(name)); // Get row to collect data from
            var data = new Dictionary<int , Dictionary<string, string>>(); // Final data container

            foreach(var element in elements)
            {
                var parsed_data = new Dictionary<string, string>();

                foreach (var column in columns)
                {
                    string column_path = String.Format("//div[@class = 'table-body']/div[contains(@class, 'table-row')][{0}]//div[contains(@class, 'table-row-column')][{1}]", Index, columns.IndexOf(column) + 1);
                    string value = Driver.Instance.FindElement(By.XPath(column_path)).Text;
                    parsed_data.Add(column, value);                    
                }
                data.Add(Index, parsed_data);
            }
            return data;
        }

        public static void WaitForTable()
        {
            Driver.Instance.Navigate().GoToUrl(Driver.BaseAddress);
            var wait = new WebDriverWait(Driver.Instance, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(By.TagName("header")));
        }
    }
}
