# Logs Data Extractor

This small F# program is intended to be an utility to extract filtered data from application logs.  
It is specifically designed on log4net log files examples.  
The core application is a .Net Standard library that can be used in .Net Core or .Net Standard projects (also C# ones).
The extracted data are saved in a CSV file.


The input log files have this log4net layout pattern:
%d{yyyy-MM-dd HH:mm:ss} %-5p %message%newline



## DEV

### Use this exsample to process the lines:  
https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/values/null-values

### Look at this for a custom null-ci=oalescing operator:  
https://troykershaw.com/null-coalescing-operator-in-fsharp-but-for-options/
