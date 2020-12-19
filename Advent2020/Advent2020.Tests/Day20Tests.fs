module Advent2020.Tests.Day20Tests

open System.IO
open Xunit
open Advent2020.Day20

[<Fact>]
let ``Part 1 Example`` () =
    let input = ""
    let result = part1 input
    Assert.Equal(1, result)
    
[<Fact>]
let ``Part 1`` () =
    let numbers = File.ReadAllText("Data/day20.txt")
    let result = part1 numbers
    Assert.Equal(279, result)
    
[<Fact>]
let ``Part 2 Example`` () =
    let input = ""
    let result = part2 input
    Assert.Equal(12, result)
    
[<Fact>]
let ``Part 2`` () =
    let numbers = File.ReadAllText("Data/day20.txt")
    let result = part2 numbers
    Assert.Equal(384, result)

