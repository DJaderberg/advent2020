module Advent2020.Tests.Day25Tests


open System.IO
open Xunit
open Advent2020.Day25

[<Fact>]
let ``Part 1 Example`` () =
    let input = ""
    let result = part1 input
    Assert.Equal(10L, result)
    
[<Fact>]
let ``Part 1 Example card`` () =
    let pk = 5764801L
    let result = findLoopSize pk 7L 1L 0
    Assert.Equal(8, result)
    
[<Fact>]
let ``Part 1 Example door`` () =
    let pk = 17807724L
    let result = findLoopSize pk 7L 1L 0
    Assert.Equal(11, result)
    
[<Fact>]
let ``Part 1`` () =
    let input = File.ReadAllText("Data/day25.txt")
    let result = part1 input
    Assert.Equal(15467093L, result)
    
[<Fact>]
let ``Part 2 Example`` () =
    let input = ""
    let result = part2 input
    Assert.Equal(2208, result)
    
[<Fact>]
let ``Part 2`` () =
    let input = File.ReadAllText("Data/day25.txt")
    let result = part2 input
    Assert.Equal(0, result)
