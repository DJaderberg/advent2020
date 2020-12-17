module Advent2020.Tests.Day17Tests

open System.IO
open Xunit
open Advent2020.Day17

[<Fact>]
let ``Part 1 Example 1`` () =
    let input = ".#.
..#
###"
    let result = part1 1 input
    Assert.Equal(11, result)
[<Fact>]
let ``Part 1 Example`` () =
    let input = ".#.
..#
###"
    let result = part1 6 input
    Assert.Equal(112, result)
    
[<Fact>]
let ``Part 1`` () =
    let numbers = File.ReadAllText("Data/day17.txt")
    let result = part1 6 numbers
    Assert.Equal(426, result)
    
[<Fact>]
let ``Part 2 Example`` () =
    let input = ".#.
..#
###"
    let result = part2 6 input
    Assert.Equal(848, result)
    
[<Fact>]
let ``Part 2`` () =
    let numbers = File.ReadAllText("Data/day17.txt")
    let result = part2 6 numbers
    Assert.Equal(1892, result)
