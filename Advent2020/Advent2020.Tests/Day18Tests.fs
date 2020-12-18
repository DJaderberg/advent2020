module Advent2020.Tests.Day18Tests

open System.IO
open Xunit
open Advent2020.Day18

[<Fact>]
let ``Part 1 Example`` () =
    let input = "3 + 8"
    let result = part1 input
    Assert.Equal(11L, result)
    
[<Fact>]
let ``Part 1 Example 2`` () =
    let input = "3 * 8"
    let result = part1 input
    Assert.Equal(24L, result)
    
[<Fact>]
let ``Part 1 Example 3`` () =
    let input = "3 * (8)"
    let result = part1 input
    Assert.Equal(24L, result)
    
[<Fact>]
let ``Part 1 Example 4`` () =
    let input = "3 * 8 + 1"
    let result = part1 input
    Assert.Equal(25L, result)
    
[<Fact>]
let ``Part 1 Example 5`` () =
    let input = "3 * (8 + 1)"
    let result = part1 input
    Assert.Equal(27L, result)
    
[<Fact>]
let ``Part 1 Example 6`` () =
    let input = "(6 + 9 + 2 * 7) + ((6 + 3 + 9) + 5) + (6 * 7 * 7 + (2 * 4 + 2 * 8 * 5 + 3) * (3 + 3 + 6) + 9)"
    let result = part1 input
    Assert.Equal(42L, result)
    
[<Fact>]
let ``Part 1`` () =
    let numbers = File.ReadAllText("Data/day18.txt")
    let result = part1 numbers
    Assert.Equal(426L, result)
    
[<Fact>]
let ``Part 2 Example`` () =
    let input = ""
    let result = part2 input
    Assert.Equal(848L, result)
    
[<Fact>]
let ``Part 2`` () =
    let numbers = File.ReadAllText("Data/day18.txt")
    let result = part2 numbers
    Assert.Equal(1892L, result)
