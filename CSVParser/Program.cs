using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVParser
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = "";
            int delimeterType = 0;
            bool fileLoop = true;
            bool parseLoop = true;
            FileParser parser = new FileParser();


            while (parseLoop)
            {
                Console.WriteLine("Welcome to the CSVParser!");
                Console.WriteLine("Please specify the path for the CSV you would like to upload...");
                input = Console.ReadLine();

                while (fileLoop)
                {
                    if (!File.Exists(input))
                    {
                        Console.WriteLine("The file you specified could not be loaded. Please try again...");
                        input = Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("Your file was found successfully.");
                        fileLoop = false;
                    }
                }

                Console.WriteLine("Loading your file. Please wait...");
                parser = new FileParser(input);

                Console.WriteLine("Please specify your delimeter by number: [0] = Comma  [1] = Pipe");
                delimeterType = Int32.Parse(Console.ReadLine());

                Console.WriteLine("Parsing file. Please wait...");
                parser.LoadFileText();
                parser.ParseRowData(delimeterType);
                parser.BuildJSON();

                Console.WriteLine("Writing output file. Please wait...");
                parser.OutputFile();

                Console.WriteLine("Would you like to parse another file [Y/N]?");
                input = Console.ReadLine().ToUpper();

                if(input == "Y")
                {
                    parseLoop = true;
                }
                else if (input == "N")
                {
                    parseLoop = false;
                }

            }
            //string filePath = @"C: \Users\sethh\source\repos\CSVParser\CSVParser\Lib\MOCK_DATA_QuoteTest.csv"     
        }
    }
}
