module Tests.ExtractorTest

open System
open NUnit.Framework
open LogsDataExtractor.Core
open LogsDataExtractor.Core.Entities



[<Test>]
let ``extract one line should return a Record`` () =
    
    let line = "this is a line"
    //let expectedResult = line

    let result = Extractor.extract line
    Assert.NotNull(result)
    Assert.True(typeof<Record> = result.GetType() )
    Assert.AreEqual(line, result.Message)


[<Test>]
let ``extract the date from the line`` () =
    
    let line = "9999-12-31 aaaaaaaaaaaaaaaaaa"
    let expectedResult = new DateTime(9999, 12, 31)

    let record = Extractor.extract line
    Assert.AreEqual(expectedResult, record.Date)



