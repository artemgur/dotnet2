open System
open FSharp.Data

type AsyncMaybeBuilder () =
    member this.Bind(x, f) =
        async {
            let! x' = x
            match x' with
            | Some s -> return! f s
            | None -> return None
            }
        member this.Return x =
            async{return x}
   
let asyncMaybe = AsyncMaybeBuilder()
   
let url = "https://localhost:5001/calculate?expression="

let CreateAnswerAsync(response) =
    async{
        return
            match response.StatusCode with
            | 404 -> None
            | 400 -> Some response.Headers.["calculator_result"] //Will contain error message in that case
            | 500 -> Some "Unknown server error"
            | 200 -> Some response.Headers.["calculator_result"]
            | _ -> None
    }

let private GetRequestAsync(url) =
    async{
        let! res = Http.AsyncRequestStream(url, silentHttpErrors=true)
        let res = Some res
        return res
    }
   
let calculate (s:string) =
    Async.RunSynchronously (asyncMaybe{
        let address = url + s.Replace("+", "%2B").Replace("*", "%2A").Replace("/", "%2F")
        let! a = GetRequestAsync address
        let! b = CreateAnswerAsync a
        return Some b
    })
    
let write (s:string option) =
    match s with
    | Some x -> Console.WriteLine(x)
    | None -> Console.WriteLine "Unknown error"

[<EntryPoint>]
let main argv =
    let expression = calculate (Console.ReadLine())
    write expression
    0
