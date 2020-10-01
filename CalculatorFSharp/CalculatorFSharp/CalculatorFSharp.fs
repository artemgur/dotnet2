module CalculatorFSharp

open System

type Maybe() =

    member this.Bind(x, f) = 
        match x with
        | None -> None
        | Some a -> f a

    member this.Return(x) = 
        Some x
        
let argsToTuple a b = if a=None || b=None then None else Some(a.Value,b.Value)
        
let maybe = Maybe()

let add (x:float*float) = fst x + snd x

let subtract (x:float*float) = fst x - snd x

let multiply (x:float*float) = fst x * snd x

let divide (x:float*float) = if snd x = 0.0 then None else Some(fst x / snd x)

let parse (str:string) =
    let isNumber, num = Double.TryParse str
    if isNumber then Some(num) else None

let calculate op a b =
    maybe{
        let! tuple = argsToTuple a b
        let! x = match op with
                    | "+" -> Some(add tuple)
                    | "-" -> Some(subtract tuple)
                    | "*" -> Some(multiply tuple)
                    | "/" -> divide tuple
                    | _ -> None
        return x
    }

let write (t:float option) = if t=None then Console.WriteLine("Computational error. Probably you tried to divide by 0 or inputted invalid number or operator") else Console.WriteLine(t.Value)

[<EntryPoint>]
let main _ =
    let a = parse (Console.ReadLine())
    let op = Console.ReadLine()
    let b = parse (Console.ReadLine())
    let t = calculate op a b
    write t
    0