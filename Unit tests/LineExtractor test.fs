module Tests.ExtractorTest

open System
open NUnit.Framework
open LogsDataExtractor.Core.LineExtractor
open LogsDataExtractor.Core.Entities



//let setup() =
    


[<Test>][<Category("Extractor")>]
let ``extract one line should return a MessageLine`` () =
    
    let line = "this is a line"
    let dateFormat = "yyyy-MM-dd HH:mm:ss"

    let extractor = new LineExtractor(dateFormat, false)
    let result = extractor.extract line

    match result with
    | String text -> Assert.AreEqual(line, text)
    | _ -> Assert.Fail "result is not a string"

[<Test>]
let ``extract the date from the line`` () =
    
    let line = "9999-12-31 23:59:59 aaaaaaaaaaaaaaaaaa"
    let dateFormat = "yyyy-MM-dd HH:mm:ss"
    let expectedResult = new DateTime(9999, 12, 31, 23, 59, 59)

    let extractor = new LineExtractor(dateFormat, false)
    let result = extractor.extract line
    
    // Assert.AreEqual(expectedResult, result.Record .Date)
    match result with 
    | Record record -> Assert.AreEqual(expectedResult, record.Date)
    | _ -> Assert.Fail "Record is not returned"

    
let assertRecord result assertFunction =
    match result with 
    | Record record -> assertFunction record
    | _ -> Assert.Fail "Result is not  Record"

[<TestCase("DEBUG", LogLevel.Debug)>]
[<TestCase("INFO ", LogLevel.Info)>]
[<TestCase("WARN ", LogLevel.Warn)>]
[<TestCase("ERROR", LogLevel.Error)>]
[<TestCase("FATAL", LogLevel.Fatal)>]
let ``extract the log level from the line`` (level, logLevel:LogLevel) =
    
    let line = sprintf "9999-12-31 00:00:00 %s aaaaaaaaaaaaaaaaaa" level
    let dateFormat = "yyyy-MM-dd HH:mm:ss"

    let extractor = new LineExtractor(dateFormat, false)
    let result = extractor.extract line

    //Assert.AreEqual( logLevel, record.Level)
    //assertRecord result function Assert.AreEqual(logLevel, record.)
    match result with 
    | Record record -> Assert.AreEqual(logLevel, record.Level)
    | _ -> Assert.Fail "Record is not returned"



[<TestCase(1)>]
[<TestCase(234)>]
let ``extract the thread from the line`` (thread:int) =
    
    let line = sprintf "9999-12-31 00:00:00 INFO  [%i] aaaaaaaaaaaaaaaaaa" thread
    let dateFormat = "yyyy-MM-dd HH:mm:ss"

    let extractor = new LineExtractor(dateFormat, true)
    let result = extractor.extract line

    //Assert.AreEqual( thread, record.Thread)
    match result with 
    | Record record -> Assert.AreEqual(thread, record.Thread)
    | _ -> Assert.Fail "Record is not returned"


[<Test>]
let ``extract 0 as thread from the line when there is no thread`` () =
    
    let line = "9999-12-31 00:00:00 INFO  aaaaaaaaaaaaaaaaaa"
    let dateFormat = "yyyy-MM-dd HH:mm:ss"

    let extractor = new LineExtractor(dateFormat, false)
    let result = extractor.extract line

    //Assert.AreEqual(0, record.Thread)
    match result with 
    | Record record -> Assert.AreEqual(0, record.Thread)
    | _ -> Assert.Fail "Record is not returned"


[<TestCase("aaaaaa")>]
let ``extract the message, single line`` (message) =
    
    let line = sprintf "9999-12-31 00:00:00 INFO  %s" message
    let dateFormat = "yyyy-MM-dd HH:mm:ss"

    let extractor = new LineExtractor(dateFormat, false)
    let result = extractor.extract line

    //Assert.AreEqual(message, record.Message)
    match result with 
    | Record record -> Assert.AreEqual(message, record.Message)
    | _ -> Assert.Fail "Record is not returned"
