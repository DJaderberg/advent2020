module Advent2020.Tests.Day3Tests

open System.IO
open Xunit
open Advent2020.Day3

[<Fact>]
let ``Part 1 Example`` () =
    let input = "..##.......
#...#...#..
.#....#..#.
..#.#...#.#
.#...##..#.
..#.##.....
.#.#.#....#
.#........#
#.##...#...
#...##....#
.#..#...#.#"
    let result = part1 input
    Assert.Equal(7, result)
    
[<Fact>]
let ``Part 1`` () =
    let numbers = File.ReadAllText("Data/day3.txt")
    let result = part1 numbers
    Assert.Equal(247, result)
    
[<Fact>]
let ``Part 2 Example`` () =
    let input = "..##.......
#...#...#..
.#....#..#.
..#.#...#.#
.#...##..#.
..#.##.....
.#.#.#....#
.#........#
#.##...#...
#...##....#
.#..#...#.#"
    let result = part2 input
    Assert.Equal(336L, result)
    
[<Fact>]
let ``Part 2 Tree on last line`` () =
    let input = "..##.......
#...#...#..
.#....#..#.
..#.#...#.#
.#...##..#.
..#.##.....
.#.#.#....#
.#........#
#.##...#...
#...##....#
###########"
    let result = part2 input
    Assert.Equal(672L, result)
    
[<Fact>]
let ``Part 2`` () =
    let input = File.ReadAllText("Data/day3.txt")
    let result = part2 input
    Assert.Equal(2983070376L, result)
