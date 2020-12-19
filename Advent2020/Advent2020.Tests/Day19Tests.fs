module Advent2020.Tests.Day19Tests

open System.IO
open Xunit
open Advent2020.Day19

[<Fact>]
let ``Part 1 Example`` () =
    let input = "0: 1 2
1: \"a\"
2: 1 3 | 3 1
3: \"b\"

aab
a
ab
ba"
    let result = part1 input
    Assert.Equal(1, result)
    
[<Fact>]
let ``Part 1`` () =
    let numbers = File.ReadAllText("Data/day19.txt")
    let result = part1 numbers
    Assert.Equal(279, result)
    
[<Fact>]
let ``Part 2 Example`` () =
    let input = ""
    let result = part2 input
    Assert.Equal(51, result)
    
[<Fact>]
let ``Part 2`` () =
    let numbers = File.ReadAllText("Data/day19.txt")
    let result = part2 numbers
    Assert.Equal(1892, result)
