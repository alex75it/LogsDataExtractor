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
let readlines filePath = System.IO.File.ReadLines(filePath)


// read a file and returns the log Records and stats
type FileExtractor (filePath:string) =

    //let dateFormat = "yyyy-MM-dd HH:mm:ss"
    //let lineExtracor = new LineExtractor(dateFormat, false)
    //let result = lineExtracor.extract line

    
    member this.extract = 

        let dateFormat = "yyyy-MM-dd HH:mm:ss"
        let lineExtracor = new LineExtractor(dateFormat, false)

        let records = new List<Record>()

        let addRecord record = records.Add record

        
        //let manageResult result currentRecord =

        //    //let currentRecord <- result

        //    match result with
        //    | Record record -> addRecord |> Some(record)
        //    | _ -> currentRecord.
       
        //let manageLine line:string = 
        //    match lineExtracor.extract line with
        //    | Record -> addRecord
        //    | _ -> ignore
        //    printf "%s" line

        for line in File.ReadLines filePath do
            //printf "%s" line
            let result = lineExtracor.extract line
            match result with
            | Record record -> addRecord record
            | _ -> ()
            //manageLine line


            

        //File.ReadLines filePath
        //|> Seq.iter (fun line -> manageLine)
        //|> lineExtracor.extract 
        //|> match result with
        //    | Record -> ignore
        //    | _ -> ignore
        //readlines filePath |> 



    

    

