module Advent2020.Tests.Day13Tests

open System.IO
open Xunit
open Advent2020.Day13

[<Fact>]
let ``Part 1 Example`` () =
    let input = "939
7,13,x,x,59,x,31,19"
    let result = part1 input
    Assert.Equal(295, result)
    
[<Fact>]
let ``Part 1`` () =
    let numbers = File.ReadAllText("Data/day13.txt")
    let result = part1 numbers
    Assert.Equal(3882, result)
    
[<Fact>]
let ``Part 2`` () =
    let result = part2
    Assert.Equal(867295486378319L, result)

