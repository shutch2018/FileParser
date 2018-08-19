# Using the CSVParser Console Application:

## From the Executable:
If you have the CSVParser.exe, run the executable. 

Following the in-console prompts, you will have to specify a CSV file for the application to read, parse, and convert into a json file.
	:warning: **Please note that depending on the security on your machine, you may need to run the executable as an administrator to read in your CSV file**
 - Your CSV file can only use one type of delimeter, a comma (,) or a pipe (|) and cannot use a combination of the two.
 - The Parser will separate all of the values by your delimeter value and parse data on any instances of these delimeters that are contained within double quotes ("")

Your output json file will be written in the same directory as the executable under the name 'parsedData.json'.


## From the Solution:
If you are editing or reviewing the CSVParser solution, please clean and then build the solution in Visual Studio. Once built, you will find the CSVParser.exe under the bin\Debug directory.

Following the in-console prompts, you will have to specify a CSV file for the application to read, parse, and convert into a json file.
	- Please note that depending on the security on your machine, you may need to run the executable as an administrator to read in your CSV file
	- Your CSV file can only use one type of delimeter, a comma (,) or a pipe (|) and cannot use a combination of the two.
	- The Parser will separate all of the values by your delimeter value and parse data on any instances of these delimeters that are contained within double quotes ("")

For testing cases, there are several files inside of the Lib directory that can be referenced to execute the application.

Your output json file will be written in the same directory as the executable under the name 'parsedData.json'.

Any questions can be sent to seth.hutcheson@gmail.com
