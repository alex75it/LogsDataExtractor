module Tests.ExtractorTest

open System
open NUnit.Framework
open LogsDataExtractor.Core
open LogsDataExtractor.Core.Entities



[<Test>][<Category("Extractor")>]
let ``extract one line should return a Record`` () =
    
    let line = "this is a line"

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

    
[<TestCase("DEBUG", LogLevel.Debug)>]
[<TestCase("INFO", LogLevel.Info)>]
[<TestCase("WARN", LogLevel.Warn)>]
[<TestCase("ERROR", LogLevel.Error)>]
[<TestCase("FATAL", LogLevel.Fatal)>]
let ``extract the log level from the line`` (level, logLevel:LogLevel) =
    
    let line = sprintf "9999-12-31 %s aaaaaaaaaaaaaaaaaa" level
        
    let record = Extractor.extract line
    Assert.AreEqual( logLevel, record.Level)
