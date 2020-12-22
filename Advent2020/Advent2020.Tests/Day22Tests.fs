module Advent2020.Tests.Day22Tests

open System.IO
open Xunit
open Advent2020.Day22

[<Fact>]
let ``Part 1 Example`` () =
    let input = "Player 1:
9
2
6
3
1

Player 2:
5
8
4
7
10"
    let result = part1 input
    Assert.Equal(306, result)
    
[<Fact>]
let ``Part 1`` () =
    let numbers = File.ReadAllText("Data/day22.txt")
    let result = part1 numbers
    Assert.Equal(35013, result)
    
[<Fact>]
let ``Part 2 Example`` () =
    let input = "Player 1:
9
2
6
3
1

Player 2:
5
8
4
7
10"
    let result = part2 input
    Assert.Equal(291, result)
    
[<Fact>]
let ``Part 2 Example Possibly infinite`` () =
    let input = "Player 1:
43
19

Player 2:
2
29
14"
    let result = part2 input
    // Score is not really important here, just that there's no infinite loop
    Assert.Equal(105, result)
    
[<Fact>]
let ``Part 2`` () =
    let numbers = File.ReadAllText("Data/day22.txt")
    let result = part2 numbers
    Assert.Equal(32806, result)
