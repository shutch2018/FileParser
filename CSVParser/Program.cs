using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVParser
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = @"C: \Users\sethh\source\repos\CSVParser\CSVParser\Lib\MOCK_DATA_QuoteTest.csv";
            FileParser temp = new FileParser(filePath);
            temp.LoadFileText();
            temp.ParseRowData();
            temp.BuildJSON();
            temp.OutputFile();

        }
    }
}
