module Advent2020.Tests.Day10Tests

open System.IO
open Xunit
open Advent2020.Day10

[<Fact>]
let ``Part 1 Example`` () =
    let input = "16
10
15
5
1
11
7
19
6
12
4"
    let result = part1 input
    Assert.Equal(7L * 5L, result)
    
[<Fact>]
let ``Part 1 Example 2`` () =
    let input = "28
33
18
42
31
14
46
20
48
47
24
23
49
45
19
38
39
11
1
32
25
35
8
17
7
9
4
2
34
10
3"
    let result = part1 input
    Assert.Equal(22L * 10L, result)
    
[<Fact>]
let ``Part 1`` () =
    let numbers = File.ReadAllText("Data/day10.txt")
    let result = part1 numbers
    Assert.Equal(2080L, result)
    
[<Fact>]
let ``Part 2 Example`` () =
    let input = "16
10
15
5
1
11
7
19
6
12
4"
    let result = part2 input
    Assert.Equal(8L, result)
    
[<Fact>]
let ``Part 2 Example 2`` () =
    let input = "28
33
18
42
31
14
46
20
48
47
24
23
49
45
19
38
39
11
1
32
25
35
8
17
7
9
4
2
34
10
3"
    let result = part2 input
    Assert.Equal(19208L, result)
    
[<Fact>]
let ``Part 2`` () =
    let numbers = File.ReadAllText("Data/day10.txt")
    let result = part2 numbers
    Assert.Equal(6908379398144L, result)

