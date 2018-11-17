module Tests.ExtractorTest

open System
open NUnit.Framework
open LogsDataExtractor.Core
open LogsDataExtractor.Core.Entities



[<Test>][<Category("Extractor")>]
let ``extract one line should return a Record`` () =
    
    let line = "this is a line"
    let dateFormat = "yyyy-MM-dd HH:mm:ss"

    let result = Extractor.extract(line, dateFormat, None)

    Assert.NotNull(result)
    Assert.True(typeof<Record> = result.GetType() )
    Assert.AreEqual(line, result.Message)


[<Test>]
let ``extract the date from the line`` () =
    
    let line = "9999-12-31 23:59:59 aaaaaaaaaaaaaaaaaa"
    let dateFormat = "yyyy-MM-dd HH:mm:ss"
    let expectedResult = new DateTime(9999, 12, 31, 23, 59, 59)
    
    let record = Extractor.extract( line, dateFormat, None)

    Assert.AreEqual(expectedResult, record.Date)

    
[<TestCase("DEBUG", LogLevel.Debug)>]
[<TestCase("INFO", LogLevel.Info)>]
[<TestCase("WARN", LogLevel.Warn)>]
[<TestCase("ERROR", LogLevel.Error)>]
[<TestCase("FATAL", LogLevel.Fatal)>]
let ``extract the log level from the line`` (level, logLevel:LogLevel) =
    
    let line = sprintf "9999-12-31 00:00:00 %s aaaaaaaaaaaaaaaaaa" level
    let dateFormat = "yyyy-MM-dd HH:mm:ss"

    let record = Extractor.extract(line, dateFormat, None)

    Assert.AreEqual( logLevel, record.Level)



[<TestCase(1)>]
[<TestCase(234)>]
let ``extract the thread from the line`` (thread:int) =
    
    let line = sprintf "9999-12-31 00:00:00 INFO [%i] aaaaaaaaaaaaaaaaaa" thread
    let dateFormat = "yyyy-MM-dd HH:mm:ss"

    let record = Extractor.extract(line, dateFormat, Some(2))

    Assert.AreEqual( thread, record.Thread)


[<Test>]
let ``extract 0 as thread from the line when there is no thread`` () =
    
    let line = "9999-12-31 00:00:00 INFO aaaaaaaaaaaaaaaaaa"
    let dateFormat = "yyyy-MM-dd HH:mm:ss"

    let record = Extractor.extract(line, dateFormat, None)

    Assert.AreEqual( 0, record.Thread)

