module Advent2020.Tests.Day12Tests

open System.IO
open Xunit
open Advent2020.Day12

[<Fact>]
let ``Part 1 Example`` () =
    let input = "F10
N3
F7
R90
F11"
    let result = part1 input
    Assert.Equal(25, result)
    
[<Fact>]
let ``Part 1 Example South`` () =
    let input = "F10
N3
F7
R90
F11
S20"
    let result = part1 input
    Assert.Equal(8 + 37, result)
    
[<Fact>]
let ``Part 1 Example East`` () =
    let input = "F10
N3
F7
R90
F11
E20"
    let result = part1 input
    Assert.Equal(8 + 20 + 17, result)
    
[<Fact>]
let ``Part 1 Example West`` () =
    let input = "F10
N3
F7
R90
F11
W20"
    let result = part1 input
    Assert.Equal(11, result)
    
[<Fact>]
let ``Part 1`` () =
    let numbers = File.ReadAllText("Data/day12.txt")
    let result = part1 numbers
    Assert.Equal(759, result)
    
[<Fact>]
let ``Part 2 Example`` () =
    let input = "F10
N3
F7
R90
F11
W20"
    let result = part2 input
    Assert.Equal(286, result)
    
[<Fact>]
let ``Part 2`` () =
    let numbers = File.ReadAllText("Data/day12.txt")
    let result = part2 numbers
    Assert.Equal(45763, result)

