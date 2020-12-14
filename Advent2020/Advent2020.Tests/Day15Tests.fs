module Advent2020.Tests.Day15Tests

open System.IO
open Xunit
open Advent2020.Day15

[<Fact>]
let ``Part 1 Example`` () =
    let input = ""
    let result = part1 input
    Assert.Equal(165, result)
    
[<Fact>]
let ``Part 1`` () =
    let numbers = File.ReadAllText("Data/day14.txt")
    let result = part1 numbers
    Assert.Equal(42, result)
    
[<Fact>]
let ``Part 2 Example`` () =
    let input = ""
    let result = part2 input
    Assert.Equal(42, result)
    
[<Fact>]
let ``Part 2`` () =
    let numbers = File.ReadAllText("Data/day14.txt")
    let result = part2 numbers
    Assert.Equal(42, result)
