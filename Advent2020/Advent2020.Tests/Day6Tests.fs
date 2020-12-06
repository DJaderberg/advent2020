module Advent2020.Tests.Day6Tests

open System.IO
open Xunit
open Advent2020.Day6

[<Fact>]
let ``Part 1 Example`` () =
    let input = "abc

a
b
c

ab
ac

a
a
a
a

b"
    let result = part1 input
    Assert.Equal(11, result)
    
[<Fact>]
let ``Part 1`` () =
    let numbers = File.ReadAllText("Data/day6.txt")
    let result = part1 numbers
    Assert.Equal(6630, result)
    
[<Fact>]
let ``Part 2 Example`` () =
    let input = "abc

a
b
c

ab
ac

a
a
a
a

b"
    let result = part2 input
    Assert.Equal(6, result)
    
[<Fact>]
let ``Part 2`` () =
    let numbers = File.ReadAllText("Data/day6.txt")
    let result = part2 numbers
    Assert.Equal(3437, result)

