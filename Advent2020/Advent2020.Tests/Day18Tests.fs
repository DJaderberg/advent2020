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
    Assert.Equal(8515L, result)
    
[<Fact>]
let ``Part 1`` () =
    let numbers = File.ReadAllText("Data/day18.txt")
    let result = part1 numbers
    Assert.Equal(18213007238947L, result)
    
[<Fact>]
let ``Part 2 Example`` () =
    let input = "1 + (2 * 3) + (4 * (5 + 6))"
    let result = part2 input
    Assert.Equal(51L, result)
    
[<Fact>]
let ``Part 2 Example 2`` () =
    let input = "2 * 3 + (4 * 5)"
    let result = part2 input
    Assert.Equal(46L, result)
    
[<Fact>]
let ``Part 2 Example 3`` () =
    let input = "5 + (8 * 3 + 9 + 3 * 4 * 3)"
    let result = part2 input
    Assert.Equal(1445L, result)
    
[<Fact>]
let ``Part 2 Example 4`` () =
    let input = "5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))"
    let result = part2 input
    Assert.Equal(669060L, result)
    
[<Fact>]
let ``Part 2 Example 4b`` () =
    let input = "5 * 9 * (7 * 3 * 12 * 3 + (8 + 6 * 4))"
    let result = part2 input
    Assert.Equal(669060L, result)
    
[<Fact>]
let ``Part 2 Example 4c`` () =
    let input = "5 * 9 * (7 * 3 * 12 * 3 + (14 * 4))"
    let result = part2 input
    Assert.Equal(669060L, result)
    
[<Fact>]
let ``Part 2 Example 5`` () =
    let input = "((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2"
    let result = part2 input
    Assert.Equal(23340L, result)
    
[<Fact>]
let ``Part 2`` () =
    let numbers = File.ReadAllText("Data/day18.txt")
    let result = part2 numbers
    Assert.Equal(388966573054664L, result)
