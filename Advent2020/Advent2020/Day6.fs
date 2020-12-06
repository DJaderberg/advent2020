namespace Advent2020

open System
open FParsec

module Day6 =
    let person = many1 letter |>> set
    let group op = sepEndBy1 person newline |>> op
    let groupCount op = group op |>> Set.count
    let totalCount op = sepEndBy1 (groupCount op) newline |>> List.sum
    let parsec parser input =
        match run parser input with
        | Success(v, _, _) -> v
        | Failure(e, _, _) -> raise (Exception("Parsing error: " + e))
    let part1 input = parsec (totalCount Set.unionMany) input
        
    let part2 input = parsec (totalCount Set.intersectMany) input


