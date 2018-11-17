module LogsDataExtractor.Core.LineExtractor

open System
open Entities
open Patterns


type LineExtractor (dateFormat:string, hasThread:bool) =

    // consider date, time, level, thread, message

    let dateHasTwoParts = dateFormat.Contains(" ")
    let partsCount = 
        if dateHasTwoParts then 
            if hasThread then 5 else 4
        else
            if hasThread then 4 else 3
    
    let levelPosition = if dateHasTwoParts then 2 else 1
    let threadPosition = levelPosition + 1
    
   
    // extract the Record from the single line
    member self.extract (line:string) =    

        let pieces = line.Split(' ', partsCount)
    
        let date = 
            match (sprintf "%s %s" pieces.[0] pieces.[1]), dateFormat with
            | Date date -> date
            | _ -> DateTime.Now 


        let logLevel = match pieces.[levelPosition] with 
                       | LogLevel level -> level
                       | _ -> LogLevel.Info

        //let mutable thread = 0
        //if hasThread then thread <- match pieces.[threadPosition] with
        //                            | Thread thread -> thread
        //                            | _ -> 0


        let thread = if not hasThread then 0
                     else match pieces.[threadPosition] with 
                          | Thread thread -> thread
                          | _ -> 0
                

        let message = Array.last(pieces)

        { 
            Date = date
            Level = logLevel
            Thread = thread
            Message = message
        }


    //new (dateFormat) = LineExtractor(dateFormat, None)

     