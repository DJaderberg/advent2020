module Advent2020.Tests.Day16Tests

open System.IO
open Xunit
open Advent2020.Day16

[<Fact>]
let ``Part 1 Example`` () =
    let input = ""
    let result = part1 input
    Assert.Equal(436, result)
    
[<Fact>]
let ``Part 1`` () =
    let numbers = ""
    let result = part1 numbers
    Assert.Equal(662, result)
    
[<Fact>]
let ``Part 2 Example`` () =
    let input = ""
    let result = part2 input
    Assert.Equal(436, result)
    
[<Fact>]
let ``Part 2`` () =
    let numbers = ""
    let result = part2 numbers
    Assert.Equal(37312, result)

