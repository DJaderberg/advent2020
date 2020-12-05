module Advent2020.Tests.Day5Tests

open System.IO
open Xunit
open Advent2020.Day5

[<Fact>]
let ``Part 1 Example`` () =
    let input = "BFFFBBFRRR"
    let result = part1 input
    Assert.Equal(567, result)
    
[<Fact>]
let ``Part 1`` () =
    let numbers = File.ReadAllText("Data/day5.txt")
    let result = part1 numbers
    Assert.Equal(987, result)
    
[<Fact>]
let ``Part 2`` () =
    let numbers = File.ReadAllText("Data/day5.txt")
    let result = part2 numbers
    Assert.Equal(603, result)
