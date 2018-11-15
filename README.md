# Logs Data Extractor

This small F# program is intended to be an utility to extract filtered data from application logs.  
It is specifically designed on real log4net log files used in a personal project.  
The core application is a .Net Standard library that can be used in .Net Core or .Net Standard projects (also C# ones).
The extracted data are saved in a CSV file.  

  
The log files have this log4net layout pattern:
%d{yyyy-MM-dd HH:mm:ss} %-5p %message%newline

