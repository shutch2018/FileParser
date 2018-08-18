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

        public List<string> fileContents { get; set; }

        public List<string[]> rowContents { get; set; }

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
            fileContents = new List<string>();

            while ((line = reader.ReadLine()) != null)
            {
                string lineContent = line;
                fileContents.Add(lineContent);
            }

            return 1;

        }

        //!- Adjust for error handling
        //!- Adjust for cells that have double quotes in them
        public int ParseRowData()
        {

            List<string[]> parsedRow = new List<string[]>();
            Regex delimterString = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
            foreach( var row in fileContents)
            {
                string[] rowContent = delimterString.Split(row);
                parsedRow.Add(rowContent); 
            }

            rowContents = parsedRow;
            return 1;
        }

        //Maybe put each json object as a value in an array?
        public int BuildJSON()
        {
            string[] headerRow = rowContents[0];
            string rowValue = @"{ ""Result"" : [";

            foreach (var row in rowContents)
            {
                if (rowContents.IndexOf(row) != 0)
                {
                    if(rowContents.IndexOf(row) == 1)
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
                            string temp = string.Format(@" ""{0}"" : ""{1}"", ", headerRow[i], row[i]);
                            rowValue += temp;
                        }
                        else
                        {
                            string temp = string.Format(@" ""{0}"" : ""{1}"" ", headerRow[i], row[i]);
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
            System.IO.File.WriteAllText(@".\mockedData.json", output);
        }

    }
}
