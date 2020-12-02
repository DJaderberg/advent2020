module Advent2020.Tests.Day2Tests

open System
open System.IO
open Xunit
open Advent2020.Day2

[<Fact>]
let ``Part 1 Example`` () =
    let numbers = "1-3 a: abcde
1-3 b: cdefg
2-9 c: ccccccccc"
    let result = part1 numbers
    Assert.Equal(2, result)
    
[<Fact>]
let ``Part 1`` () =
    let numbers = File.ReadAllText("Data/day2.txt")
    let result = part1 numbers
    Assert.Equal(582, result)
    
[<Fact>]
let ``Part 2 Example`` () =
    let numbers = "1-3 a: abcde
1-3 b: cdefg
2-9 c: ccccccccc"
    let result = part2 numbers
    Assert.Equal(1, result)
    
[<Fact>]
let ``Part 2`` () =
    let numbers = File.ReadAllText("Data/day2.txt")
    let result = part2 numbers
    Assert.Equal(729, result)
