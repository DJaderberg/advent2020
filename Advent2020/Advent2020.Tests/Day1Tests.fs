module Advent2020.Tests.Day1Tests

open System
open System.IO
open Xunit
open Advent2020.Day1

[<Fact>]
let ``Part 1 Example`` () =
    let numbers = "1721
979
366
299
675
1456"
    let result = part1 numbers
    Assert.Equal(514579, result)
    
[<Fact>]
let ``Part 1`` () =
    let numbers = File.ReadAllText("Data/day1part1.txt")
    let result = part1 numbers
    Assert.Equal(787776, result)
    
[<Fact>]
let ``Part 2 Example`` () =
    let numbers = "1721
979
366
299
675
1456"
    let result = part2 numbers
    Assert.Equal(241861950, result)
    
[<Fact>]
let ``Part 2`` () =
    let numbers = File.ReadAllText("Data/day1part1.txt")
    let result = part2 numbers
    Assert.Equal(262738554, result)
