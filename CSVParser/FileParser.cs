using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace CSVParser
{
    class FileParser
    {
        public string line { get; set; }
        public string output { get; set; }

        public string FilePath { get; set; }
        public List<KeyValuePair<int, string>> fileContent { get; set; }
        public List<KeyValuePair<int, string[]>> rowContent { get; set; }
        
        public FileParser(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    FilePath = filePath;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("The specified file could not be loaded. Processed Failed: {0}", e.ToString());
            }

        }

        //!- Adjust for error handling
        //!- Adjust for rows that may have a newline in them
        public int LoadFileText()
        {

            //Regex for newline parsing?: Regex r = new Regex(@"(?m)^[^""\r\n]*(?:(?:""[^""]*"")+[^""\r\n]*)*");
            StreamReader reader = File.OpenText(FilePath);
            int rowNumber = -1;
            fileContent = new List<KeyValuePair<int, string>>();

            while ((line = reader.ReadLine()) != null)
            {
                KeyValuePair<int, string> rowContent = new KeyValuePair<int, string>(++rowNumber, line);
                fileContent.Add(rowContent);
            }

            return 1;

        }

        //!- Adjust for error handling
        //!- Adjust for cells that have double quotes in them
        public int ParseRowData()
        {

            List<KeyValuePair<int, string[]>> parsedRow = new List<KeyValuePair<int, string[]>>();
            Regex delimterString = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
            foreach( var row in fileContent)
            {
                string[] rowContent = delimterString.Split(row.Value);
                KeyValuePair<int, string[]> temp = new KeyValuePair<int, string[]>(row.Key, rowContent);
                parsedRow.Add(temp); 
            }

            rowContent = parsedRow;
            return 1;
        }

        //Maybe put each json object as a value in an array?
        public int BuildJSON()
        {
            string[] headerRow = rowContent[0].Value;
            string rowValue = @"{ ""Result"" : [";

            foreach (var row in rowContent)
            {
                if (row.Key != 0)
                {
                    if(row.Key == 1)
                    {
                        rowValue += "{";
                    }
                    else
                    {
                        rowValue += ",{";
                    }
                    for (var i = 0; i < headerRow.Count(); i++)
                    {
                        if( i != headerRow.Count()-1)
                        {
                            string temp = string.Format(@" ""{0}"" : ""{1}"", ", headerRow[i], row.Value[i]);
                            rowValue += temp;
                        }
                        else
                        {
                            string temp = string.Format(@" ""{0}"" : ""{1}"" ", headerRow[i], row.Value[i]);
                            rowValue += temp;
                        }
                    }
                    rowValue += "}";
                }
            }
            rowValue += " ]}";
            output = rowValue;

            return 1;
        }

        public void OutputFile()
        {
            System.IO.File.WriteAllText(@"C: \Users\sethh\source\repos\ConsoleApp1\ConsoleApp1\Lib\mockedData.json", output);
        }

    }
}
