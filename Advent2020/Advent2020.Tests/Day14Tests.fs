module Advent2020.Tests.Day14Tests

open System.IO
open Xunit
open Advent2020.Day14

[<Fact>]
let ``Part 1 Example`` () =
    let input = "mask = XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X
mem[8] = 11
mem[7] = 101
mem[8] = 0"
    let result = part1 input
    Assert.Equal(165UL, result)
    
[<Fact>]
let ``Part 1`` () =
    let numbers = File.ReadAllText("Data/day14.txt")
    let result = part1 numbers
    Assert.Equal(6317049172545UL, result)
    
[<Fact>]
let ``Part 2 Example`` () =
    let input = "mask = 000000000000000000000000000000X1001X
mem[42] = 100
mask = 00000000000000000000000000000000X0XX
mem[26] = 1"
    let result = part2 input
    Assert.Equal(208UL, result)
    
[<Fact>]
let ``Part 2 Example 2`` () =
    let input = "mask = 10100000101101100110X001X0X001100X10
mem[40461] = 1
mem[6217] = 0"
    let result = part2 input
    Assert.Equal(16UL, result)
    
[<Fact>]
let ``Part 2`` () =
    let numbers = File.ReadAllText("Data/day14.txt")
    let result = part2 numbers
    Assert.Equal(3434009980379UL, result)
