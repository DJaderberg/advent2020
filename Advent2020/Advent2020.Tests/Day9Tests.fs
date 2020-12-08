module Advent2020.Tests.Day9Tests

open System.IO
open Xunit
open Advent2020.Day9

[<Fact>]
let ``Part 1 Example`` () =
    let input = "nop +0
acc +1
jmp +4
acc +3
jmp -3
acc -99
acc +1
jmp -4
acc +6"
    let result = part1 input
    Assert.Equal(5, result)
    
[<Fact>]
let ``Part 1`` () =
    let numbers = File.ReadAllText("Data/day9.txt")
    let result = part1 numbers
    Assert.Equal(2058, result)
