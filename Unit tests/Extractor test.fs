module Tests.ExtractorTest

open NUnit.Framework
open LogDataExtractor.Core


[<Test>]
let ``extract one line`` () =
    
    let line = "this is a line"
    let expectedResult = line

    let result = Extractor.extract line
    Assert.AreEqual(expectedResult, result)

