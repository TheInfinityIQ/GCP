module Tests

open System
open Program
open Xunit
open GCP.Cli.Tests.Utilities
open Xunit
open Xunit



module ParseTagsTests =
    let private args = seq { "GCP" }
    let private group = "GCP"
    let private tags = [| "-a" |]
    let private noArgs = Seq.empty :> string seq
    let private noTags = Seq.empty :> string seq

    let private (@) x y =
        (x |> Seq.toList) @ (y |> Seq.toList)
        |> List.toSeq

    module ``argMap should have all tags`` =
        let private data: seq<obj []> =
            seq {
                yield [| args; group; tags |]
                yield [| noArgs; group; noTags |]
                yield [| noArgs; group; [ "-a" ] |]

                yield
                    [| noArgs @ [ "1"; "2"; "3" ]
                       group
                       seq { "-a" } |]

                yield
                    [| args
                       @ [ "--add"; "1" ] @ [ "-a"; "2" ] @ [ "someValue" ]
                       group
                       [ "-a"; "--add" ] |]
            }

        type private TestData() =
            inherit ClassDataBase(data)

        [<Theory>]
        [<ClassData(typeof<TestData>)>]
        let Test (args, group, myTags) =
            // Arrange
            let cmd =
                command group {
                    ignoreCasing
                    tags myTags
                    description "represents the help description for the command."

                    handler
                        (fun (argMap: Map<string, string []>) ->
                            let containsAllTags =
                                argMap
                                |> Map.keys
                                |> Seq.forall (fun tagKey -> myTags |> Seq.contains tagKey)
                            // Assert
                            Assert.True(containsAllTags))
                }

            // Act
            Command.run cmd args

    module ``argMap should have tag and corresponding value`` =
        let private data: seq<obj []> =
            seq {
                yield
                    [| args
                       group
                       tags
                       Map.empty.Add("-a", Array.empty<string>) |]

                yield
                    [| noArgs
                       group
                       noTags
                       Map.empty<string, string []> |]

                yield
                    [| noArgs
                       group
                       [ "-a" ]
                       Map.empty.Add("-a", Array.empty<string>) |]

                yield
                    [| noArgs @ [ "1"; "2"; "3" ]
                       group
                       seq { "-a" }
                       Map.empty.Add("-a", Array.empty<string>) |]

                yield
                    [| args
                       @ [ "--add"; "1" ] @ [ "-a"; "2" ] @ [ "someValue" ]
                       group
                       [ "-a"; "--add" ]
                       Map
                           .empty
                           .Add("-a", [| "2" |])
                           .Add("--add", [| "1" |]) |]
            }

        type private TestData() =
            inherit ClassDataBase(data)

        [<Theory>]
        [<ClassData(typeof<TestData>)>]
        let Test (args, group, myTags, expectedMap: Map<string, string []>) =
            // Arrange
            let cmd =
                command group {
                    ignoreCasing
                    tags myTags
                    description "represents the help description for the command."

                    handler (
                        Map.iter
                            (fun k v ->
                                let expectedValues = expectedMap.[k]
                                //Assert
                                expectedValues
                                |> Array.iter (fun x -> v |> Array.contains x |> Assert.True)

                                Assert.True(expectedValues.Length = v.Length)

                                ())
                    )
                }

            // Act
            Command.run cmd args
