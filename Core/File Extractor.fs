module LogsDataExtractor.Core.FileExtractor

open System.IO
open System.Collections.Generic
open LineExtractor
open LogsDataExtractor.Core.Entities

type fileResult = {
    totalLines: int
    readedLines: int
    totalRecords: int
    }

//let readlines (filePath:string) = seq {
//    use reader = new StreamReader(filePath)
//    while not reader.EndOfStream do
//        yield reader.ReadLine()
//}

// https://stackoverflow.com/questions/2365527/how-read-a-file-into-a-seq-of-lines-in-f
let readlines filePath = 
    if File.Exists filePath then System.IO.File.ReadLines(filePath)
    else Seq.empty<string>


// read a file and returns the log Records and stats
type FileExtractor (filePath:string) =

    
    member this.extract = 

        // how to add this check without increasing the indentation ?
        //if File.Exists filePath then

        let dateFormat = "yyyy-MM-dd HH:mm:ss"
        let lineExtracor = new LineExtractor(dateFormat, false)

        let records = new List<Record>()

        let mutable currentRecord = Some(Record)
        let addRecord record = 
            records.Add record
            currentRecord <- None // how to pass record instead ??
        let concatenateMessage record partialMessage = () // record.addLine partialMessage

        
      
        let manageLine line currentRecord option = 
            match lineExtracor.extract line with
            | Record record ->  addRecord record
            | String message -> concatenateMessage currentRecord message


        for line in readlines filePath do
            match lineExtracor.extract line with
            | Record record ->  addRecord record
            | String message -> concatenateMessage None message

        records

        //File.ReadLines filePath
        //|> Seq.iter (fun line -> manageLine)
        //|> lineExtracor.extract 
        //|> match result with
        //    | Record -> ignore
        //    | _ -> ignore
        //readlines filePath |> 



    

    

