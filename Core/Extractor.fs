module LogsDataExtractor.Core.Extractor

open System
open System.Globalization
open Entities


// create an active pattern for the Date
let (|Date|_|) (input:string) =
    match DateTime.TryParseExact (input.Substring(0, 10), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal) with
    | true, date -> Some(date)
    | _ -> None

  

let extract line =

    let date = 
        match line with
        | Date date -> date
        | _ -> DateTime.Now 

    
    { 
        Date = date
        Message = line
    }


     
