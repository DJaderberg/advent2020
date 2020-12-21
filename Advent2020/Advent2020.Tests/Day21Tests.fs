module Advent2020.Tests.Day21Tests

open System.IO
open Xunit
open Advent2020.Day21

[<Fact>]
let ``Part 1 Example`` () =
    let input = "mxmxvkd kfcds sqjhc nhms (contains dairy, fish)
trh fvjkl sbzzf mxmxvkd (contains dairy)
sqjhc fvjkl (contains soy)
sqjhc mxmxvkd sbzzf (contains fish)"
    let result = part1 input
    Assert.Equal(5, result)
    
[<Fact>]
let ``Part 1`` () =
    let numbers = File.ReadAllText("Data/day21.txt")
    let result = part1 numbers
    Assert.Equal(2170, result)
    
[<Fact>]
let ``Part 2 Example`` () =
    let input = "mxmxvkd kfcds sqjhc nhms (contains dairy, fish)
trh fvjkl sbzzf mxmxvkd (contains dairy)
sqjhc fvjkl (contains soy)
sqjhc mxmxvkd sbzzf (contains fish)"
    let result = part2 input
    Assert.Equal("mxmxvkd,sqjhc,fvjkl", result)
    
[<Fact>]
let ``Part 2`` () =
    let numbers = File.ReadAllText("Data/day21.txt")
    let result = part2 numbers
    Assert.Equal("nfnfk,nbgklf,clvr,fttbhdr,qjxxpr,hdsm,sjhds,xchzh", result)
