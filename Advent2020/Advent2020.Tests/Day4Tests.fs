module Advent2020.Tests.Day4Tests

open System.IO
open Xunit
open Advent2020.Day4

[<Fact>]
let ``Part 1 Example`` () =
    let input = "ecl:gry pid:860033327 eyr:2020 hcl:#fffffd
byr:1937 iyr:2017 cid:147 hgt:183cm

iyr:2013 ecl:amb cid:350 eyr:2023 pid:028048884
hcl:#cfa07d byr:1929

hcl:#ae17e1 iyr:2013
eyr:2024
ecl:brn pid:760753108 byr:1931
hgt:179cm

hcl:#cfa07d eyr:2025 pid:166559648
iyr:2011 ecl:brn hgt:59in"
    let result = part1 input
    Assert.Equal(2, result)
    
[<Fact>]
let ``Part 1`` () =
    let numbers = File.ReadAllText("Data/day4.txt")
    let result = part1 numbers
    Assert.Equal(226, result)
