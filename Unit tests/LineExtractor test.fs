module Tests.ExtractorTest

open System
open NUnit.Framework
open LogsDataExtractor.Core.LineExtractor
open LogsDataExtractor.Core.Entities



//let setup() =
    


[<Test>][<Category("Extractor")>]
let ``extract one line should return a Record`` () =
    
    let line = "this is a line"
    let dateFormat = "yyyy-MM-dd HH:mm:ss"

    let extractor = new LineExtractor(dateFormat, false)
    let result = extractor.extract line

    Assert.NotNull(result)
    Assert.True(typeof<Record> = result.GetType() )


[<Test>]
let ``extract the date from the line`` () =
    
    let line = "9999-12-31 23:59:59 aaaaaaaaaaaaaaaaaa"
    let dateFormat = "yyyy-MM-dd HH:mm:ss"
    let expectedResult = new DateTime(9999, 12, 31, 23, 59, 59)
    
    let extractor = new LineExtractor(dateFormat, false)
    let record = extractor.extract line

    Assert.AreEqual(expectedResult, record.Date)

    
[<TestCase("DEBUG", LogLevel.Debug)>]
[<TestCase("INFO ", LogLevel.Info)>]
[<TestCase("WARN ", LogLevel.Warn)>]
[<TestCase("ERROR", LogLevel.Error)>]
[<TestCase("FATAL", LogLevel.Fatal)>]
let ``extract the log level from the line`` (level, logLevel:LogLevel) =
    
    let line = sprintf "9999-12-31 00:00:00 %s aaaaaaaaaaaaaaaaaa" level
    let dateFormat = "yyyy-MM-dd HH:mm:ss"

    let extractor = new LineExtractor(dateFormat, false)
    let record = extractor.extract line

    Assert.AreEqual( logLevel, record.Level)



[<TestCase(1)>]
[<TestCase(234)>]
let ``extract the thread from the line`` (thread:int) =
    
    let line = sprintf "9999-12-31 00:00:00 INFO  [%i] aaaaaaaaaaaaaaaaaa" thread
    let dateFormat = "yyyy-MM-dd HH:mm:ss"

    let extractor = new LineExtractor(dateFormat, true)
    let record = extractor.extract line

    Assert.AreEqual( thread, record.Thread)


[<Test>]
let ``extract 0 as thread from the line when there is no thread`` () =
    
    let line = "9999-12-31 00:00:00 INFO  aaaaaaaaaaaaaaaaaa"
    let dateFormat = "yyyy-MM-dd HH:mm:ss"

    let extractor = new LineExtractor(dateFormat, false)
    let record = extractor.extract line

    Assert.AreEqual( 0, record.Thread)

[<TestCase("aaaaaa")>]
let ``extract the message, single line`` (message) =
    
    let line = sprintf "9999-12-31 00:00:00 INFO  %s" message
    let dateFormat = "yyyy-MM-dd HH:mm:ss"

    let extractor = new LineExtractor(dateFormat, false)
    let record = extractor.extract line

    Assert.AreEqual(message, record.Message)
