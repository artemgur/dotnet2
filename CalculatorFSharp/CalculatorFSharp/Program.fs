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

let write (t:float option) = if t=None then Console.WriteLine("None") else Console.WriteLine(t.Value)

[<EntryPoint>]
let main argv =
    let a = Console.ReadLine() |> Double.Parse
    let op = Console.ReadLine()
    let b = Console.ReadLine() |> Double.Parse
    let t = calculate op a b
    write t
    0