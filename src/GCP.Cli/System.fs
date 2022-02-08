[<AutoOpen>]
module System

open System

let private toOption =
    function
    | (error, value) when error -> Some value
    | _ -> None


[<RequireQualifiedAccess>]
module Boolean =
    let tryParse (str: string) = Boolean.TryParse str |> toOption


[<RequireQualifiedAccess>]
module Byte =
    let tryParse (str: string) = Byte.TryParse str |> toOption


[<RequireQualifiedAccess>]
module SByte =
    let tryParse (str: string) = SByte.TryParse str |> toOption


[<RequireQualifiedAccess>]
module Int16 =
    let tryParse (str: string) = Int16.TryParse str |> toOption


[<RequireQualifiedAccess>]
module Int32 =
    let tryParse (str: string) = Int32.TryParse str |> toOption


[<RequireQualifiedAccess>]
module Int64 =
    let tryParse (str: string) = Int64.TryParse str |> toOption


[<RequireQualifiedAccess>]
module UInt16 =
    let tryParse (str: string) = UInt16.TryParse str |> toOption


[<RequireQualifiedAccess>]
module UInt32 =
    let tryParse (str: string) = UInt32.TryParse str |> toOption


[<RequireQualifiedAccess>]
module UInt64 =
    let tryParse (str: string) = UInt64.TryParse str |> toOption


[<RequireQualifiedAccess>]
module Decimal =
    let tryParse (str: string) = Decimal.TryParse str |> toOption


[<RequireQualifiedAccess>]
module Double =
    let tryParse (str: string) = Double.TryParse str |> toOption


[<RequireQualifiedAccess>]
module Single =
    let tryParse (str: string) = Single.TryParse str |> toOption


[<RequireQualifiedAccess>]
module Char =
    let tryParse (str: string) = Char.TryParse str |> toOption


[<RequireQualifiedAccess>]
module DateOnly =
    let tryParse (str: string) = DateOnly.TryParse str |> toOption


[<RequireQualifiedAccess>]
module TimeOnly =
    let tryParse (str: string) = TimeOnly.TryParse str |> toOption


[<RequireQualifiedAccess>]
module DateTime =
    let tryParse (str: string) = DateTime.TryParse str |> toOption


[<RequireQualifiedAccess>]
module DateTimeOffset =
    let tryParse (str: string) = DateTimeOffset.TryParse str |> toOption


[<RequireQualifiedAccess>]
module TimeSpan =
    let tryParse (str: string) = TimeSpan.TryParse str |> toOption


[<RequireQualifiedAccess>]
module Guid =
    let tryParse (str: string) = Guid.TryParse str |> toOption



type OptionBuilder() =
    let (>>=) m f = Option.bind f m

    member this.Bind(x, f) = x >>= f
    member this.Return(x) = x |> Some
    member this.ReturnFrom(x) = x
    member this.Zero() = None
    member this.Delay(f) = f ()

    member this.Combine(a, b) =
        match a with
        | Some _ -> a
        | None -> b

    member this.Yield(x) =
        Some x

    member this.YieldFrom(x) =
        x


let option = OptionBuilder()
