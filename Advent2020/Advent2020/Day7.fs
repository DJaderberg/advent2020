namespace Advent2020

open System
open FParsec

module Day7 =
    let parsec parser input =
        match run parser input with
        | Success(v, _, _) -> v
        | Failure(e, _, _) -> raise (Exception("Parsing error: " + e))
    let part1 input = parsec (letter |>> int) input
    let part2 input = raise (NotImplementedException())
