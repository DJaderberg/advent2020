module Advent2020.Tests.Day8Tests

open System.IO
open Xunit
open Advent2020.Day8

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
    let numbers = File.ReadAllText("Data/day8.txt")
    let result = part1 numbers
    Assert.Equal(2058, result)
[<Fact>]
let ``Part 2 Example`` () =
    let input = "nop +0
acc +1
jmp +4
acc +3
jmp -3
acc -99
acc +1
jmp -4
acc +6"
    let result = part2 input
    Assert.Equal(8, result)
    
[<Fact>]
let ``Part 2`` () =
    let numbers = File.ReadAllText("Data/day8.txt")
    let result = part2 numbers
    Assert.Equal(1000, result)

