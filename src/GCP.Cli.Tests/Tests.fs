module Tests

open System
open Program
open Xunit

type Assert with
    static member Some(value: 'a option) = value |> Option.isSome |> Assert.True

    static member Some(value: 'a option, msg: string) =
        Assert.True(value |> Option.isSome, msg)

    static member None(value: 'a option) = value |> Option.isNone |> Assert.True

    static member None(value: 'a option, msg: string) =
        Assert.True(value |> Option.isNone, msg)


[<Fact>]
let ``should parse top level command tags`` () =
    // Arrange
    let args = [| "GCP"; "-a"; "123" |]
    let cmd =
        command "GCP" {
            ignoreCasing
            tags [ "-a"; "--add" ]
            description "represents the help description for the command."

            handler
                (fun (m: Map<string, string []>) ->
                    // Assert
                    Assert.Some(m.TryFind "-a"))
        }


    // Act
    runner [ cmd ] args
