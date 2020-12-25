module Advent2020.Tests.Day24Tests


open System.IO
open Xunit
open Advent2020.Day24

[<Fact>]
let ``Part 1 Example`` () =
    let input = "sesenwnenenewseeswwswswwnenewsewsw
neeenesenwnwwswnenewnwwsewnenwseswesw
seswneswswsenwwnwse
nwnwneseeswswnenewneswwnewseswneseene
swweswneswnenwsewnwneneseenw
eesenwseswswnenwswnwnwsewwnwsene
sewnenenenesenwsewnenwwwse
wenwwweseeeweswwwnwwe
wsweesenenewnwwnwsenewsenwwsesesenwne
neeswseenwwswnwswswnw
nenwswwsewswnenenewsenwsenwnesesenew
enewnwewneswsewnwswenweswnenwsenwsw
sweneswneswneneenwnewenewwneswswnese
swwesenesewenwneswnwwneseswwne
enesenwswwswneneswsenwnewswseenwsese
wnwnesenesenenwwnenwsewesewsesesew
nenewswnwewswnenesenwnesewesw
eneswnwswnwsenenwnwnwwseeswneewsenese
neswnwewnwnwseenwseesewsenwsweewe
wseweeenwnesenwwwswnew"
    let result = part1 input
    Assert.Equal(10, result)
    
[<Fact>]
let ``Part 1 example flip origo`` () =
    let input = "nwwswee"
    let result = part1Map input |> Map.toSeq
    Assert.Equal([(0,0), Black], result)
    
[<Fact>]
let ``Part 1`` () =
    let input = File.ReadAllText("Data/day24.txt")
    let result = part1 input
    Assert.Equal(549, result)
    
[<Fact>]
let ``Part 2 Example`` () =
    let input = "sesenwnenenewseeswwswswwnenewsewsw
neeenesenwnwwswnenewnwwsewnenwseswesw
seswneswswsenwwnwse
nwnwneseeswswnenewneswwnewseswneseene
swweswneswnenwsewnwneneseenw
eesenwseswswnenwswnwnwsewwnwsene
sewnenenenesenwsewnenwwwse
wenwwweseeeweswwwnwwe
wsweesenenewnwwnwsenewsenwwsesesenwne
neeswseenwwswnwswswnw
nenwswwsewswnenenewsenwsenwnesesenew
enewnwewneswsewnwswenweswnenwsenwsw
sweneswneswneneenwnewenewwneswswnese
swwesenesewenwneswnwwneseswwne
enesenwswwswneneswsenwnewswseenwsese
wnwnesenesenenwwnenwsewesewsesesew
nenewswnwewswnenesenwnesewesw
eneswnwswnwsenenwnwnwwseeswneewsenese
neswnwewnwnwseenwseesewsenwsweewe
wseweeenwnesenwwwswnew"
    let result = part2 100 input
    Assert.Equal(2208, result)
    
[<Fact>]
let ``Part 2`` () =
    let input = File.ReadAllText("Data/day24.txt")
    let result = part2 100 input
    Assert.Equal(4147, result)
