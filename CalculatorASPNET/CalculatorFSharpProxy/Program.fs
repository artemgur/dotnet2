module CalculatorFSharpProxy.Program

open System
open CalculatorProxy
    
let write (s:string option) =
    match s with
    | Some x -> Console.WriteLine(x)
    | None -> Console.WriteLine "Unknown error"
    
let calculate calculateFunction expression =
    calculateFunction expression

[<EntryPoint>]
let main argv =
    let proxyCalc = calculate calculateProxy
    let expression = proxyCalc (Console.ReadLine())
    write expression
    0
