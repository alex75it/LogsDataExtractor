module tests.FileExtractor_test

open NUnit.Framework
open LogsDataExtractor.Core



[<Test>][<Category("File extractor")>]
let ``extract when file does not exists raise an error``() =

    let file = "C://file that does not exists"
    
    let action = fun() -> FileExtractor.extract file |> ignore
    //Assert.Throws<System.Exception>(action)  // does not work
    Assert.Throws<System.Exception>(TestDelegate(action)) 
    //Assert.Throws<System.Exception>( fun () -> FileExtractor.extract file |> ignore )
    |> ignore

