// Learn more about F# at http://fsharp.org

open System

let add x y = x + y

let subtract x y = x - y

let multiply x y = x * y

let divide x y = x / y

let calculate op = match op with
    | "+" -> add
    | "-" -> subtract
    | "*" -> multiply
    | "/" -> divide

[<EntryPoint>]
let main argv =
    let a = Console.ReadLine() |> Int32.Parse
    let op = Console.ReadLine()
    let b = Console.ReadLine() |> Int32.Parse
    let t = calculate op a b
    Console.WriteLine(t)
    0