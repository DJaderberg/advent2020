module Advent2020.Tests.Day23Tests


open System.IO
open Xunit
open Advent2020.Day23

[<Fact>]
let ``Part 1 Example`` () =
    let input = "389125467"
    let result = part1 10 input
    Assert.Equal("92658374", result)
    
[<Fact>]
let ``Part 1`` () =
    let input = "792845136"
    let result = part1 100 input
    Assert.Equal("98742365", result)
    
[<Fact>]
let ``Part 2 Example`` () =
    let input = "389125467"
    let result = part2 10000000 input
    Assert.Equal(149245887792L, result)
    
[<Fact>]
let ``Part 2`` () =
    let input = "792845136"
    let result = part2 10000000 input
    Assert.Equal(0L, result)
