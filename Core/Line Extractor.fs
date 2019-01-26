module LogsDataExtractor.Core.LineExtractor

open System
open System.Text.RegularExpressions
open Entities
open Patterns


let threadRegex = Regex("\.*\[(\d+)\]\.*")

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
    member self.extract (line:string): Result =

        let date = 
            if line.Length < dateFormat.Length then
                None
            else
                match line.Substring(0, dateFormat.Length), dateFormat with
                | Date date -> Some(date)
                | _ -> None

        if date.IsSome then

            //let pieces = line.Split(' ', partsCount)
    
            // this is ok for non fixed length level patterns
            //let logLevel = match pieces.[levelPosition] with 
            //               | LogLevel level -> level
            //               | _ -> LogLevel.Info



            // consider a 5 character long level
            let logLevel = match line.Substring(dateFormat.Length + 1, 5).TrimEnd() with 
                           | LogLevel level -> level
                           | _ -> LogLevel.Info


            let thread = 
                if not hasThread then 0
                else                     
                    let matches = threadRegex.Matches(line)
                    if matches.Count > 0 
                    //then Int32.Parse(matches.[0].Value) 
                    //else 0
                    then match Int32.TryParse(matches.[0].Groups.[1].Value) with
                            | true, num -> num
                            | _ -> failwithf "\"%s\" is not an integer" matches.[0].Groups.[1].Value
                    else failwithf "Cannot find a pattern fo the threadv in \"%s\"" line
                

            let threadLength = if not hasThread then 0 else thread.ToString().Length + 3 // add brackets and space 
            
            let message = line.Substring(dateFormat.Length + 7 + threadLength) // add the brackets and the space

            Result.Record { 
                Date = date.Value
                Level = logLevel
                Thread = thread
                Message = message
            }

        else
            Result.String line
            

    //new (dateFormat) = LineExtractor(dateFormat, None)

     