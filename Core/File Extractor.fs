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


// https://stackoverflow.com/questions/2365527/how-read-a-file-into-a-seq-of-lines-in-f
let readlines filePath = 
    if File.Exists filePath then System.IO.File.ReadLines(filePath)
    else Seq.empty<string>


// read a file and returns the log Records and stats
let extract (filePath:string) =

    let dateFormat = "yyyy-MM-dd HH:mm:ss"
    let lineExtracor = new LineExtractor(dateFormat, false)

    let records = new List<Record>()

    //let mutable currentRecord = Some(Record)
    let addRecord record = 
        records.Add record
        record
        //currentRecord <- None // how to pass record instead ??
    let concatenateMessage record partialMessage = 
        //record.addLine partialMessage
        record

      
    let manageLine line currentRecord option = 
        match lineExtracor.extract line with
        | Record record ->  addRecord record
        | String message -> concatenateMessage currentRecord message
        

    //for line in readlines filePath do
    //    match lineExtracor.extract line with
    //    | Record record ->  addRecord record
    //    | String message -> concatenateMessage None message

    //for line in readlines filePath do

    //    manageLine line None        

    //    match lineExtracor.extract line with
    //    | Record record ->  addRecord record |> ignore
    //    | String message -> concatenateMessage None message

    records

    //File.ReadLines filePath
    //|> Seq.iter (fun line -> manageLine)
    //|> lineExtracor.extract 
    //|> match result with
    //    | Record -> ignore
    //    | _ -> ignore
    //readlines filePath |> 
