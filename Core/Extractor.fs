module LogsDataExtractor.Core.Extractor

open System
open System.Globalization
open Entities


// create an active pattern for the Date
let (|Date|_|) (input:string) =
    match DateTime.TryParseExact (input.Substring(0, 10), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal) with
    | true, date -> Some(date)
    | _ -> None

let (|LogLevel|_|) (input:string) =
    let pieces = input.Split(" ")
    if pieces.Length > 1 then
        match pieces.[1] with
        | "DEBUG" -> Some(LogLevel.Debug)
        | "INFO" -> Some(LogLevel.Info)
        | "WARN" -> Some(LogLevel.Warn)
        | "ERROR" -> Some(LogLevel.Error)
        | "FATAL" -> Some(LogLevel.Fatal)
        | _ -> None
    else
        None

let extract line =

    let date = 
        match line with
        | Date date -> date
        | _ -> DateTime.Now 

    let logLevel = match line with 
    | LogLevel level -> level
    | _ -> LogLevel.Info

    { 
        Date = date
        Level = logLevel
        Message = line
    }


     
