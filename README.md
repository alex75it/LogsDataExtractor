
 # **Abandoned**
 I'm using SumoLogic.
 
 ---
 
 # Logs Data Extractor

This small F# program is intended to be an utility to extract filtered data from application logs.  
The core application is a .Net Standard library that can be used in .Net Core or .Net Standard projects (also C# ones).
The extracted data are saved in a CSV file.  

  
The log files have this [log4net layout pattern](https://svn.apache.org/repos/asf/logging/site/trunk/docs/log4net/release/sdk/log4net.Layout.PatternLayout.html):  
%d{yyyy-MM-dd HH:mm:ss} %-5p %message%newline

## Work in progress. This project is under development and not yet completed ##

## How it works

### On a single file
Run the _FileExtrctor_ `extract` method passing the log file path as argument.

### On a log files folder
Run the _FileExtractor_ `extract` method passing the log file directory as argument.  
FileExtractor will run parse all the files in the directory.

### Initial date
An initial date can be given as argument. The process will ignore logs with a date before that one. (WIP)


## DEV

### Use this exsample to process the lines:  
https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/values/null-values

### Look at this for a custom null-ci=oalescing operator:  
https://troykershaw.com/null-coalescing-operator-in-fsharp-but-for-options/
