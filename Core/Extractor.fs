module LogsDataExtractor.Core.Extractor

open System
open System.Globalization
open System.Text.RegularExpressions
open Entities


// Active Patterns: https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/active-patterns 

// create a partial active pattern with 2 arguments for the Date
let (|Date|_|) (input:string, format:string) =
    match DateTime.TryParseExact(input, format, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal) with
    | true, date -> Some(date)
    | _ -> None


let (|LogLevel|_|) (input:string) =
    match input with
    | "DEBUG" -> Some(LogLevel.Debug)
    | "INFO" -> Some(LogLevel.Info)
    | "WARN" -> Some(LogLevel.Warn)
    | "ERROR" -> Some(LogLevel.Error)
    | "FATAL" -> Some(LogLevel.Fatal)
    | _ -> None

let (|Thread|_|) input =
    let match_ = Regex.Match(input, "\[(\d+)\]") in
    if match_.Success then        
        Some(Int32.Parse( match_.Groups.Item(1).Value))
    else
        None



// extract the Record from the single line
let extract (line:string, dateFormat:string, threadPosition:int option) =

    let partsCount = 
        match dateFormat.Contains(" ") with
        | true -> 5
        | _ -> 4

    let pieces = line.Split(' ', partsCount) // separators // date, time, thread, level, message

    
    let date = 
        match (sprintf "%s %s" pieces.[0] pieces.[1]), dateFormat with
        | Date date -> date
        | _ -> DateTime.Now 


    let logLevel = match pieces.[2] with 
                   | LogLevel level -> level
                   | _ -> LogLevel.Info

    let mutable thread = 0

    if threadPosition.IsSome then 
        // move thread position of one because date-time take 2 positions
        thread <- match pieces.[threadPosition.Value+1] with
                 | Thread thread -> thread
                 | _ -> 0

    { 
        Date = date
        Level = logLevel
        Thread = thread
        Message = line
    }


     
