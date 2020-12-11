module Advent2020.Tests.Day11Tests

open System.IO
open Xunit
open Advent2020.Day11

[<Fact>]
let ``Part 1 Example`` () =
    let input = "L.LL.LL.LL
LLLLLLL.LL
L.L.L..L..
LLLL.LL.LL
L.LL.LL.LL
L.LLLLL.LL
..L.L.....
LLLLLLLLLL
L.LLLLLL.L
L.LLLLL.LL"
    let result = part1 input
    Assert.Equal(37, result)
    
[<Fact>]
let ``Part 1`` () =
    let numbers = File.ReadAllText("Data/day11.txt")
    let result = part1 numbers
    Assert.Equal(2368, result)
    
[<Fact>]
let ``Part 2 Example`` () =
    let input = "L.LL.LL.LL
LLLLLLL.LL
L.L.L..L..
LLLL.LL.LL
L.LL.LL.LL
L.LLLLL.LL
..L.L.....
LLLLLLLLLL
L.LLLLLL.L
L.LLLLL.LL"
    let result = part2 input
    Assert.Equal(26, result)
    
[<Fact>]
let ``Part 2`` () =
    let numbers = File.ReadAllText("Data/day11.txt")
    let result = part2 numbers
    Assert.Equal(2124, result)

