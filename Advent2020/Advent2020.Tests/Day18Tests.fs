module Advent2020.Tests.Day18Tests

open System.IO
open Xunit
open Advent2020.Day18

[<Fact>]
let ``Part 1 Example`` () =
    let input = ""
    let result = part1 input
    Assert.Equal(112, result)
    
[<Fact>]
let ``Part 1`` () =
    let numbers = File.ReadAllText("Data/day18.txt")
    let result = part1 numbers
    Assert.Equal(426, result)
    
[<Fact>]
let ``Part 2 Example`` () =
    let input = ""
    let result = part2 input
    Assert.Equal(848, result)
    
[<Fact>]
let ``Part 2`` () =
    let numbers = File.ReadAllText("Data/day18.txt")
    let result = part2 numbers
    Assert.Equal(1892, result)
