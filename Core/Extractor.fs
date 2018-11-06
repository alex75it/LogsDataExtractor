module LogDataExtractor.Core.Extractor

open System

type Record = {
    Date: DateTime
    Message: string
} 


let extract line =

    let date = DateTime.Now
    
    { 
        Date = date
        Message = line
    }


     
