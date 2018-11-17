module LogsDataExtractor.Core.Extensions

// NO MORE USED

open System

// based on this: 
// https://stackoverflow.com/questions/4949941/convert-string-to-system-datetime-in-f

/// Extend DateTime with a TryPArse method that ruturn null instead of a default value

type DateTime with
    static member TryParseOption str = 
        match DateTime.TryParse (str:string) with 
        | true, date -> Some(date)
        | _ -> None
        
