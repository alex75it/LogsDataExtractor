module tests.FileExtractor_test

open NUnit.Framework
open LogsDataExtractor.Core
open System.IO



[<Test>][<Category("File extractor")>]
let ``extract when file does not exists raise an error``() =

    let file = "C://file that does not exists"
    
    let action = fun() -> FileExtractor.extract file |> ignore
    //Assert.Throws<System.Exception>(action)  // does not work
    Assert.Throws<FileNotFoundException>(TestDelegate(action)) 
    //Assert.Throws<System.Exception>( fun () -> FileExtractor.extract file |> ignore )
    |> ignore


[<Test>][<Category("File extractor")>]
let ``extract when there is a line return one record``() =

    let file = "data/one line.log"

    let records = FileExtractor.extract file

    Assert.NotNull records
    //Assert.That(records.Count = 1 , "message" + "aa")  // does not work
    Assert.That(records.Count = 1 , sprintf "records count is %i" records.Count)
