[<AutoOpen>]
module GCP.Cli.Tests.Utilities

open Xunit
open System.Collections

type Assert with
    static member Some(value: 'a option) = value |> Option.isSome |> Assert.True

    static member Some(value: 'a option, msg: string) =
        Assert.True(value |> Option.isSome, msg)

    static member None(value: 'a option) = value |> Option.isNone |> Assert.True

    static member None(value: 'a option, msg: string) =
        Assert.True(value |> Option.isNone, msg)

type ClassDataBase(generator: seq<obj []>) =
    interface seq<obj []> with
        member __.GetEnumerator() = generator.GetEnumerator()

        member __.GetEnumerator() =
            generator.GetEnumerator() :> IEnumerator
