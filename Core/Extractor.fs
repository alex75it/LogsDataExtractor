module LogsDataExtractor.Core.Extractor

open System
open System.Globalization
open Entities


// Active Patterns: https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/active-patterns 

// create a partial active pattern with 2 arguments for the Date
let (|Date|_|) (input:string, format:string) =
    match DateTime.TryParseExact(input, format, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal) with
    | true, date -> Some(date)
    | _ -> None

//let (|Date|_|) (input:string) =
//    match DateTime.TryParseExact(input, "", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal) with
//    | true, date -> Some(date)
//    | _ -> None

let (|LogLevel|_|) (input:string) =

    match input with
        | "DEBUG" -> Some(LogLevel.Debug)
        | "INFO" -> Some(LogLevel.Info)
        | "WARN" -> Some(LogLevel.Warn)
        | "ERROR" -> Some(LogLevel.Error)
        | "FATAL" -> Some(LogLevel.Fatal)
        | _ -> None


// extract the Record from the single line
let extract (line:string, dateFormat:string) =

    let pieces = line.Split(' ', 5) // separators // date, time, thread, level, message



    let date = 
        match (sprintf "%s %s" pieces.[0] pieces.[1]), dateFormat with
        | Date date -> date
        | _ -> DateTime.Now 


    let logLevel = match pieces.[2] with 
    | LogLevel level -> level
    | _ -> LogLevel.Info

    { 
        Date = date
        Level = logLevel
        Message = line
    }


     
