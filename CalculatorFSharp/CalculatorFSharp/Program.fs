open System

type Maybe() =

    member this.Bind(x, f) = 
        match x with
        | None -> None
        | Some a -> f a

    member this.Return(x) = 
        Some x
        
let maybe = new Maybe()

let add x y = Some(x + y)

let subtract x y = Some(x - y)

let multiply x y = Some(x * y)

let divide x y = if y = 0.0 then None else Some(x / y)

let parse (str:string) =
    let isNumber, num = Double.TryParse str
    if isNumber then Some(num) else None

let calculate op a b =
    maybe{
        let! x = match op with
            | "+" -> add a b
            | "-" -> subtract a b
            | "*" -> multiply a b
            | "/" -> divide a b
            | _ -> None
        return x
    }

let write (t:float option) = if t=None then Console.WriteLine("Computational error. Probably you tried to divide by 0 or used invalid operator") else Console.WriteLine(t.Value)

let calculateAndWrite op a b =
    let t = calculate op a b
    write t

[<EntryPoint>]
let main argv =
    let a = parse (Console.ReadLine())
    let op = Console.ReadLine()
    let b = parse (Console.ReadLine())
    if a = None || b = None then Console.WriteLine("One of parameters is not a number") else calculateAndWrite op a.Value b.Value
    0
