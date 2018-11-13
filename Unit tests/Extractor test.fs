module Tests.ExtractorTest

open System
open NUnit.Framework
open LogsDataExtractor.Core
open LogsDataExtractor.Core.Entities



[<Test>][<Category("Extractor")>]
let ``extract one line should return a Record`` () =
    
    let line = "this is a line"
    let dateFormat = "yyyy-MM-dd HH:mm:ss"

    let result = Extractor.extract(line, dateFormat)

    Assert.NotNull(result)
    Assert.True(typeof<Record> = result.GetType() )
    Assert.AreEqual(line, result.Message)


[<Test>]
let ``extract the date from the line`` () =
    
    let line = "9999-12-31 23:59:59 aaaaaaaaaaaaaaaaaa"
    let dateFormat = "yyyy-MM-dd HH:mm:ss"
    let expectedResult = new DateTime(9999, 12, 31, 23, 59, 59)
    
    let record = Extractor.extract( line, dateFormat)

    Assert.AreEqual(expectedResult, record.Date)

    
[<TestCase("DEBUG", LogLevel.Debug)>]
[<TestCase("INFO", LogLevel.Info)>]
[<TestCase("WARN", LogLevel.Warn)>]
[<TestCase("ERROR", LogLevel.Error)>]
[<TestCase("FATAL", LogLevel.Fatal)>]
let ``extract the log level from the line`` (level, logLevel:LogLevel) =
    
    let line = sprintf "9999-12-31 00:00:00 %s aaaaaaaaaaaaaaaaaa" level
    let dateFormat = "yyyy-MM-dd HH:mm:ss"

    let record = Extractor.extract(line, dateFormat)

    Assert.AreEqual( logLevel, record.Level)
