using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace CSVParser
{
    class FileParser
    {
        //String used to temporarily store each line that is read in from the source file
        public string line { get; set; }
        
        //String used to store the entire JSON output
        public string output { get; set; }

        //String used to manage the path of the CSV file being parsed
        public string FilePath { get; set; }

        //List that contains all of the CSV contents separated by line
        public List<string> fileContents { get; set; }

        //List that contains all of the CSV contents separated by line, where each array element is a unique value in the line
        public List<string[]> rowContents { get; set; }

        /***
         * Default FileParser constructor
         ***/
        public FileParser() { }

        /***
         * FileParser constructor that determines if a file exists and then sets it as the FilePath value
         ***/
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

        /***
         * LoadFileText takes the file referenced by FilePath and reads in the content line by line, adding it to fileContents
         ***/
        public int LoadFileText()
        {
            try
            {
                string ext = Path.GetExtension(FilePath);
                if(ext.ToLower() != "csv")
                {
                    Console.WriteLine("Incorrect file type specified. Please provide a .csv file");
                    return 0;
                }

                StreamReader reader = File.OpenText(FilePath);
                fileContents = new List<string>();

                while ((line = reader.ReadLine()) != null)
                {
                    string lineContent = line;
                    fileContents.Add(lineContent);
                }
                return 1;
            }
            catch(Exception e)
            {
                Console.WriteLine("An error occurred while reading the file contents. Prcoess Failes: {0}", e.ToString());
                return 0;
            }
        }

        /***
         * ParseRowData loops through fileContents and breaks each line into an array, adding it into rowContents
         ***/
        public int ParseRowData(int delimeterType)
        {
            Regex delimeterString = new Regex("");
            List<string[]> parsedRow = new List<string[]>();

            if (delimeterType == 0)
            {
                delimeterString = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
            }
            else if (delimeterType == 1)
            {
                delimeterString = new Regex("[|](?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");

            }

            foreach ( var row in fileContents)
            {
                try
                {
                    string[] rowContent = delimeterString.Split(row);
                    string[] sanitizedContent = rowContent;

                    for (int i = 0; i < rowContent.Count(); i++)
                    {
                        if (rowContent[i].Contains("\"\"\""))
                        {
                            sanitizedContent[i] = rowContent[i].Replace("\"\"\"", @"\""");

                        }
                        else if (rowContent[i].Contains("\""))
                        {
                            sanitizedContent[i] = rowContent[i].Replace("\"", "");

                        }
                    }
                    parsedRow.Add(sanitizedContent);
                    rowContents = parsedRow;
                }
                catch (Exception e)
                {
                    Console.WriteLine("An error occurred while separating row content. Process Failed: {0}", e.ToString());
                    return 0;
                }
            }

            return 1;
        }

        /***
         * BuildJSON loops through rowContents and builds each JSON element, 
         ***/
        public int BuildJSON()
        {
            string[] headerRow = rowContents[0];
            string rowValue = @"{ ""Result"" : [";

            try
            {
                foreach (var row in rowContents)
                {
                    if (rowContents.IndexOf(row) != 0)
                    {
                        if (rowContents.IndexOf(row) == 1)
                        {
                            rowValue += "{";
                        }
                        else
                        {
                            rowValue += ",{";
                        }
                        for (var i = 0; i < headerRow.Count(); i++)
                        {
                            if (i != headerRow.Count() - 1)
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
            }
            catch(Exception e)
            {
                Console.WriteLine("An error occurred when building the JSON data. Process Failed: {0}", e.ToString());
                return 0;
            }
            
            rowValue += " ]}";
            output = rowValue;

            return 1;
        }

        /***
         * OutputFile takes the JSON output and pushes it into a file named 'parsedData.json' in the same directory as the console application
         ***/
        public int OutputFile()
        {
            try
            {
                System.IO.File.WriteAllText(@".\parsedData.json", output);
                return 1;
            }
            catch(Exception e)
            {
                Console.WriteLine("An error occurred while writing the output file. Process Failed: {0}", e.ToString());
                return 0;
            }
        }

    }
}
