namespace Advent2020

open System
open FParsec

module Day23 =
    let parsec parser input =
        match run parser input with
        | Success(v, _, _) -> v
        | Failure(e, _, _) -> raise (Exception(e))
    let part1 input = 42
    let part2 input = 42
        
