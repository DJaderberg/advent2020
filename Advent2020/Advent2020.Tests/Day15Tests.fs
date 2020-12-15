module Advent2020.Tests.Day15Tests

open System.IO
open Xunit
open Advent2020.Day15

[<Fact>]
let ``Part 1 Example 2020`` () =
    let input = "0,3,6"
    let result = part1 2020 input
    Assert.Equal(436, result)
    
[<Fact>]
let ``Part 1 Example 10`` () =
    let input = "0,3,6"
    let result = part1 10 input
    Assert.Equal(0, result)
    
[<Fact>]
let ``Part 1 Example 2`` () =
    let input = "1,3,2"
    let result = part1 2020 input
    Assert.Equal(1, result)
    
[<Fact>]
let ``Part 1 Example 3`` () =
    let input = "1,2,3"
    let result = part1 2020 input
    Assert.Equal(27, result)
    
[<Fact>]
let ``Part 1 Example 4`` () =
    let input = "2,3,1"
    let result = part1 2020 input
    Assert.Equal(78, result)
    
[<Fact>]
let ``Part 1 Example 5`` () =
    let input = "3,2,1"
    let result = part1 2020 input
    Assert.Equal(438, result)
    
[<Fact>]
let ``Part 1 Example 6`` () =
    let input = "3,1,2"
    let result = part1 2020 input
    Assert.Equal(1836, result)
    
[<Fact>]
let ``Part 1`` () =
    let numbers = "16,11,15,0,1,7"
    let result = part1 2020 numbers
    Assert.Equal(42, result)
    
[<Fact>]
let ``Part 2`` () =
    let numbers = "16,11,15,0,1,7"
    let result = part1 30000000 numbers
    Assert.Equal(37312, result)
