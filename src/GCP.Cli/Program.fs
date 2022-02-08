(* // AN ATTEMPT TO PARSING ARGS // *)
// type ArgType =
//     | FloatingValue of value: string
//     | Tag of tag: string * value: string
//     | Group of group: string * tags: ArgType seq
//     | Nothing

// module ArgType =
//     let isNothing =
//         function
//         | Nothing -> true
//         | _ -> false

//     let isNotNothing = isNothing >> not


// let rec r (g: string) (l: string list) : ArgType seq =
//     seq {
//         match l with
//         | [] -> yield Nothing
//         | [ x ] -> yield FloatingValue x
//         | (x: string) :: xs ->
//             let a = r x xs |> Seq.toArray

//             if x.StartsWith('-') then
//                 let nextHead = xs.Tail.Head

//                 let aaa = (r nextHead xs.Tail)

//                 yield! aaa
//                 yield Tag(x, xs.Head)
//             else
//                 yield Group(x, a)
//         | _ -> yield Nothing
//     }
//     |> Seq.filter ArgType.isNotNothing





// let result =
//     [| "migration"
//        "add"
//        "-o"
//        "C:/code"
//        "-n"
//        "name"
//        "--someVal"
//        "aaa" |]
//     |> Array.toList
//     |> r "ROOT"

// result
// |> logTitle "ALL: "
// |> Seq.filter (function
//     | Group _ -> true
//     | _ -> false)
// |> Seq.collect (function
//     | Group (_, r) -> r
//     | _ -> failwith "death")
// |> Seq.collect (function
//     | Group (_, r) -> r
//     | x -> [ x ])
// |> Seq.toList
// |> logTitle "Last val: "
// |> ignore


// let rec getFloatingValues l =
//     let r =
//         l
//         |> Seq.collect (function
//             | Group (_, l) -> l
//             | FloatingValue x -> [ FloatingValue x ]
//             | _ -> [])

//     let baseCase =
//         r
//         |> Seq.forall (function
//             | FloatingValue _ -> true
//             | _ -> false)

//     if baseCase then
//         r
//     else
//         getFloatingValues r

// seq {
//     //get last most inner group
//     yield! result |> getFloatingValues
// }
// |> logTitle "AAAA: "
// |> ignore

































open System

let log x =
    printfn "%A" x
    x

let logTitle s x =
    printfn "%s%A" s x
    x

type Command =
    { CaseHandling: StringComparison
      Group: string
      Tags: string seq
      Description: string
      Handler: string [] -> unit
      Children: Command seq }
    static member defaultCommand =
        { CaseHandling = StringComparison.InvariantCultureIgnoreCase
          Group = ""
          Tags = []
          Description = ""
          Handler = ignore
          Children = [] }

type CommandBuilder(group: string) =
    new() = CommandBuilder("ROOT")

    member __.Zero() =
        { Command.defaultCommand with Group = group }

    member __.Yield(_) =
        { Command.defaultCommand with Group = group }

    [<CustomOperation("casing")>]
    member __.CaseHandling(cmd, c) = { cmd with CaseHandling = c }

    [<CustomOperation("ignoreCasing")>]
    member __.IgnoreCasing(cmd) =
        { cmd with CaseHandling = StringComparison.InvariantCultureIgnoreCase }

    [<CustomOperation("ignoreCasing")>]
    member __.IgnoreCasing(cmd, b) =
        { cmd with
            CaseHandling =
                if b then
                    StringComparison.InvariantCultureIgnoreCase
                else
                    StringComparison.InvariantCulture }

    [<CustomOperation("group")>]
    member __.Group(cmd, g) = { cmd with Group = g }

    [<CustomOperation("tags")>]
    member __.Tags(cmd, t: string seq) =
        { cmd with Tags = (cmd.Tags |> Seq.toList) @ (t |> Seq.toList) }

    [<CustomOperation("tags")>]
    member __.Tags(cmd, t: string) =
        { cmd with Tags = (cmd.Tags |> Seq.toList) @ [ t ] }

    [<CustomOperation("description")>]
    member __.Description(cmd, d) = { cmd with Description = d }

    [<CustomOperation("handler")>]
    member __.Handler(cmd, h) = { cmd with Handler = h }

    [<CustomOperation("handler")>]
    member __.Handler(cmd, p: string [] -> 'a, h: 'a -> unit) = { cmd with Handler = p >> h }

    [<CustomOperation("handler")>]
    member __.Handler(cmd, p1: string [] -> 'a, p2, h: 'a -> 'b -> unit) =
        { cmd with Handler = (fun args -> h (p1 args) (p2 args)) }

    [<CustomOperation("handler")>]
    member __.Handler(cmd, p1: string [] -> 'a, p2, p3, h: 'a -> 'b -> 'c -> unit) =
        { cmd with Handler = (fun args -> h (p1 args) (p2 args) (p3 args)) }

    [<CustomOperation("children")>]
    member __.Children(cmd, c: Command) =
        { cmd with Children = (cmd.Children |> Seq.toList) @ [ c ] }

    [<CustomOperation("children")>]
    member __.Children(cmd, c: Command seq) =
        { cmd with Children = (cmd.Children |> Seq.toList) @ (c |> Seq.toList) }


let args = [| "myRoot"; "gcp"; "-a"; "123" |]



let command g = CommandBuilder(g)
let rootCommand = command "ROOT"

let gcp =
    command "gcp" {
        ignoreCasing
        tags [ "-a"; "--add" ]
        description "represents the help description for the command."

        handler
            (fun x -> x.[3] |> log |> Int32.tryParse)
            (fun x -> x.[2] |> log |> Int32.tryParse)
            (fun x -> x.[1] |> log |> Int32.tryParse)
            (fun x y z -> printfn "Hello from hell. My args are -> '%A' '%A' '%A'" x y z)
    }



let runner commands args =
    commands
    |> Seq.iter (fun x -> x.Handler(args |> Seq.toArray))


runner [ gcp ] args
