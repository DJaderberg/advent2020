module Advent2020.Tests.Day9Tests

open System.IO
open Xunit
open Advent2020.Day9

[<Fact>]
let ``Part 1 Example`` () =
    let input = "35
20
15
25
47
40
62
55
65
95
102
117
150
182
127
219
299
277
309
576"
    let result = part1 5L input
    Assert.Equal(127L, result)
    
[<Fact>]
let ``Part 1`` () =
    let numbers = File.ReadAllText("Data/day9.txt")
    let result = part1 25L numbers
    Assert.Equal(1212510616L, result)
    
[<Fact>]
let ``Part 2 Example`` () =
    let input = "35
20
15
25
47
40
62
55
65
95
102
117
150
182
127
219
299
277
309
576"
    let result = part2 5L input
    Assert.Equal(62L, result)
    
[<Fact>]
let ``Part 2`` () =
    let numbers = File.ReadAllText("Data/day9.txt")
    let result = part2 25L numbers
    Assert.Equal(171265123L, result)
