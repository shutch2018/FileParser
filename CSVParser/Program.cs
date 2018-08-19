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

            FileParser parser = new FileParser();
            Console.WriteLine("Welcome to the CSVParser!");
            bool parseLoop = true;

            /***
             * Parent loop to keep the user interace open. 
             * This should terminate when a user opts not to parse another file or a process fails during the processing.
             ***/
            while (parseLoop)
            {
                string input = "";
                int delimeterType = 0;
                bool fileLoop = true;
                bool delimeterLoop = true;
                bool repeatLoop = true;

                Console.WriteLine("Please specify the path for the CSV you would like to upload...");
                input = Console.ReadLine();

                /***
                 * Loop to allow a user to specify which file they wish to upload if there is an error with their input
                 * This should terminate when a user specifies a file path that points to a valid file
                ***/
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

                /***
                 * Loop to allow a user to specify how their csv file is delimeted (comma or pipe)
                 * This should termiante when a user specifies a valid delimeter choice
                 ***/
                while (delimeterLoop)
                {
                    Console.WriteLine("Please specify your delimeter by number: [0] = Comma  [1] = Pipe");
                    try
                    {
                        delimeterType = Int32.Parse(Console.ReadLine());
                        if(delimeterType == 0 || delimeterType == 1)
                        {
                            delimeterLoop = false;
                        }
                        else
                        {
                            Console.WriteLine("Invalid delimeter selection. Please try again.");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Delimeter error. Please choose a valid separator. Process Failed: {0}", e.ToString());
                    }
                }

                Console.WriteLine("Parsing file. Please wait...");
                if(parser.LoadFileText() == 0)
                {
                    parseLoop = false;
                }

                if(parser.ParseRowData(delimeterType) == 0)
                {
                    parseLoop = false;
                }
                if(parser.BuildJSON() == 0)
                {
                    parseLoop = false;
                }

                Console.WriteLine("Writing output file. Please wait...");
                if(parser.OutputFile() == 0)
                {
                    parseLoop = false;
                }
                else
                {
                    Console.WriteLine("Congratulations! Your file was converted successfully!");
                }

                /***
                 * Loop to allow a user to specify if there are other files to parse or if they are done with the application
                 * This should terminate if they choose to repeate the whole process, or conclude their work.
                 ***/
                while (repeatLoop)
                {
                    Console.WriteLine("Would you like to parse another file [Y/N]?");
                    input = Console.ReadLine().ToUpper();

                    if (input == "Y")
                    {
                        repeatLoop = false;
                    }
                    else if (input == "N")
                    {
                        parseLoop = false;
                        repeatLoop = false;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please try again.");
                    }
                }
            }
        }
    }
}
