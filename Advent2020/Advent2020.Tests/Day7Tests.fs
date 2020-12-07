module Advent2020.Tests.Day7Tests

open System.IO
open Xunit
open Advent2020.Day7

[<Fact>]
let ``Part 1 Example single`` () =
    let input = "light red bags contain 1 bright white bag, 2 muted yellow bags."
    let result = parsec rule input
    result
    
[<Fact>]
let ``Part 1 Example`` () =
    let input = "light red bags contain 1 bright white bag, 2 muted yellow bags.
dark orange bags contain 3 bright white bags, 4 muted yellow bags.
bright white bags contain 1 shiny gold bag.
muted yellow bags contain 2 shiny gold bags, 9 faded blue bags.
shiny gold bags contain 1 dark olive bag, 2 vibrant plum bags.
dark olive bags contain 3 faded blue bags, 4 dotted black bags.
vibrant plum bags contain 5 faded blue bags, 6 dotted black bags.
faded blue bags contain no other bags.
dotted black bags contain no other bags."
    let result = part1 input
    Assert.Equal(4, result)
    
[<Fact>]
let ``Part 1`` () =
    let numbers = File.ReadAllText("Data/day7.txt")
    let result = part1 numbers
    Assert.Equal(370, result)
    
[<Fact>]
let ``Part 2 Example`` () =
    let input = "light red bags contain 1 bright white bag, 2 muted yellow bags.
dark orange bags contain 3 bright white bags, 4 muted yellow bags.
bright white bags contain 1 shiny gold bag.
muted yellow bags contain 2 shiny gold bags, 9 faded blue bags.
shiny gold bags contain 1 dark olive bag, 2 vibrant plum bags.
dark olive bags contain 3 faded blue bags, 4 dotted black bags.
vibrant plum bags contain 5 faded blue bags, 6 dotted black bags.
faded blue bags contain no other bags.
dotted black bags contain no other bags."
    let result = part2 input
    Assert.Equal(32, result)
    
[<Fact>]
let ``Part 2 Example 2`` () =
    let input = "shiny gold bags contain 2 dark red bags.
dark red bags contain 2 dark orange bags.
dark orange bags contain 2 dark yellow bags.
dark yellow bags contain 2 dark green bags.
dark green bags contain 2 dark blue bags.
dark blue bags contain 2 dark violet bags.
dark violet bags contain no other bags."
    let result = part2 input
    Assert.Equal(126, result)
    
[<Fact>]
let ``Part 2`` () =
    let numbers = File.ReadAllText("Data/day7.txt")
    let result = part2 numbers
    Assert.Equal(29547, result)

