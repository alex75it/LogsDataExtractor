open System
open System.IO



let readlines (filePath:string) = seq {
    use reader = new StreamReader(filePath)
    while not reader.EndOfStream do
        yield reader.ReadLine()
}

let printLine line = printfn "a%A"

let sum x y = x + y

type ItemVersion(id, version) =
    member this.Id = id
    member this.Version = version
    
type CustomerName(firstName, middleInitial, lastName) = 
    member this.FirstName = firstName
    member this.MiddleInitial = middleInitial
    member this.LastName = lastName  


[<EntryPoint>]
let main argv =
    printfn """Hello World from F#!"""
    System.Console.WriteLine(sum 1 3)
    
    printfn "%d" (sum 4 5)

    //Seq.iter printLine readlines "XRP Shop-2018-10-13.log" 

    //if isLogError line

    0
