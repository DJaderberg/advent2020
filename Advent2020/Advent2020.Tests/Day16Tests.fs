﻿module Advent2020.Tests.Day16Tests

open System.IO
open Xunit
open Advent2020.Day16

[<Fact>]
let ``Part 1 Example`` () =
    let input = "class: 1-3 or 5-7
row: 6-11 or 33-44
seat: 13-40 or 45-50

your ticket:
7,1,14

nearby tickets:
7,3,47
40,4,50
55,2,20
38,6,12"
    let result = part1 input
    Assert.Equal(71, result)
    
[<Fact>]
let ``Part 1`` () =
    let numbers = File.ReadAllText("Data/day16.txt")
    let result = part1 numbers
    Assert.Equal(19060, result)
    
[<Fact>]
let ``Part 2 Example`` () =
    let input = "class: 0-1 or 4-19
row: 0-5 or 8-19
seat: 0-13 or 16-19

your ticket:
11,12,13

nearby tickets:
3,9,18
15,1,5
5,14,9"
    let result = part2 input
    Assert.Equal(1L, result)
    
[<Fact>]
let ``Part 2`` () =
    let numbers = File.ReadAllText("Data/day16.txt")
    let result = part2 numbers
    Assert.Equal(953713095011L, result)

