module LogsDataExtractor.Core.Patterns


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