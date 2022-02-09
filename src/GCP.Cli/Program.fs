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

module Command =
    let defaultCommand =
        { CaseHandling = StringComparison.InvariantCultureIgnoreCase
          Group = ""
          Tags = []
          Description = ""
          Handler = ignore
          Children = [] }

    let getMapOfTagValues (cmd) (args: string []) =
        let getArgsForTag (t: string) =
            args
            |> Array.map (fun a -> a.Trim())
            |> Array.mapi (fun i a -> t.Equals(a, cmd.CaseHandling), i + 1)
            |> Array.filter (fun (b, i) -> b && args |> Array.tryItem i |> Option.isSome)
            |> Array.map (fun (_, i) -> i)
            |> Array.map (fun i -> args.[i])

        cmd.Tags
        |> Seq.map (fun t -> t.Trim())
        |> Seq.map (fun t -> t, getArgsForTag t)
        |> Map.ofSeq


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
    member __.Handler(cmd, h: Map<string, string []> -> unit) =
        let getMap args = Command.getMapOfTagValues cmd args
        { cmd with Handler = (fun args -> h (getMap args)) }

    [<CustomOperation("children")>]
    member __.Children(cmd, c: Command) =
        { cmd with Children = (cmd.Children |> Seq.toList) @ [ c ] }

    [<CustomOperation("children")>]
    member __.Children(cmd, c: Command seq) =
        { cmd with Children = (cmd.Children |> Seq.toList) @ (c |> Seq.toList) }




let command g = CommandBuilder(g)
let rootCommand = CommandBuilder()

let gcp =
    command "gcp" {
        ignoreCasing
        tags [ "-a"; "--add" ]
        description "represents the help description for the command."
        // handler (fun (x: string []) -> printfn "Hello from hell. My args are -> '%A'" x)
        handler (fun (x: Map<string, string []>) -> printfn "Hello from hell. My arg map is -> '%A'" x)

        children [ command "test" { tags "-as" } ]

    }



let runner commands args =
    let args =
        args |> logTitle "runnerArgs: " |> Seq.toArray

    commands |> Seq.iter (fun x -> x.Handler args)


let args =
    [| "myRoot"
       "gcp"
       "-a"
       "123"
       "--option"
       "myOptVal"
       "myGroupVal"
       "nextGroupHere"
       "--nextGtag"
       "nextGTagVal"
       "nextGroupHereVal"
       "lastGroup"
       "lastGroupValue"
       "--lastGroupTag"
       "lastGroupTagValue" |]

args
|> Array.skipWhile (fun a -> a <> "gcp")
|> Array.takeWhile (fun a ->
    not (
        [ "nextGroupHere"; "lastGroup" ]
        |> List.contains a
    ))
// we should now have the cmd group's relevant args
|> ignore

let cmds = [ gcp ]

cmds
|> Seq.collect (fun x -> x.Children)
|> Seq.map (fun x -> x.Group)
|> logTitle "child groups:"
|> ignore

runner cmds args

(*
    NOTE
    - we aren't parsing the tag values properly yet. (we look through each arg without checking if said arg is still part group's args)
    - we only support top level commands (ROOT.exe <myTopLevelCmd> <Tag1> <Tag1Value> ...)
        - we need to try looking into parsing something like:
            - ROOT.exe <myTopLevelCmd> <myTopLevelCmdValue> <Tag1> <Tag1Value> <Tag2> <Tag2Value>
            - ROOT.exe <myTopLevelCmd> <Tag1> <Tag1Value> <Tag2> <Tag2Value> <myTopLevelCmdValue>
            - ROOT.exe <myTopLevelCmd> <Tag1> <Tag1Value> <Tag2> <Tag2Value> <myChildCmd> <myChildCmdValue> <ChildTag1> <ChildTag1Value> ...
        - HINT: I think we can find the start of the cmd tags/values by using
            - args |> skipWhile (fun a -> a <> cmd.Group)
        - HINT: then maybe we can find the end by
            - args |> skipWhile (fun a -> a <> cmd.Group) |> Array.takeWhile (fun a -> not (cmd.Children |> Seq.map (fun x -> x.) |> List.contains a))
*)


let ``should parse top level command tags`` =
    // Arrange
    let group = "GCP"
    let args = [| group; "-a"; "123" |]

    let cmd =
        command group {
            tags [ "-a" ]

            handler (fun (m: Map<string, string []>) ->
                // Assert
                match m.TryFind "-a" with
                | Some x when x |> Array.contains "123" -> ()
                | _ -> failwith $"failed to parse one tag under a single top level group cmd.")
        }

    // Act
    runner [ cmd ] args
