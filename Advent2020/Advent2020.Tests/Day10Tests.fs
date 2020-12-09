module Advent2020.Tests.Day10Tests

open System.IO
open Xunit
open Advent2020.Day10

[<Fact>]
let ``Part 1 Example`` () =
    let input = ""
    let result = part1 input
    Assert.Equal(127, result)
    
[<Fact>]
let ``Part 1`` () =
    let numbers = File.ReadAllText("Data/day9.txt")
    let result = part1 numbers
    Assert.Equal(1212510616, result)
    
[<Fact>]
let ``Part 2 Example`` () =
    let input = ""
    let result = part2 input
    Assert.Equal(62, result)
    
[<Fact>]
let ``Part 2`` () =
    let numbers = File.ReadAllText("Data/day9.txt")
    let result = part2 numbers
    Assert.Equal(171265123, result)

