namespace Advent2020

open System
open FParsec

module Day6 =
    let person = many1 letter
    let group = sepEndBy1 person newline 
    let groupCountAnyone = group |>> List.concat |>> set |>> Set.count
    
    let allYes list =
        let allToFind = List.concat list |> set |> Set.toList
        List.filter (fun c -> List.forall (List.contains c) list) allToFind
    let groupCountEveryone = group |>> allYes |>> List.length
    
    let totalCountAnyone = sepEndBy1 groupCountAnyone newline |>> List.sum
    let totalCountEveryone = sepEndBy1 groupCountEveryone newline |>> List.sum
    
    let parsec parser input =
        match run parser input with
        | Success(v, _, _) -> v
        | Failure(e, _, _) -> raise (Exception("Parsing error: " + e))
    let part1 input = parsec totalCountAnyone input
        
    let part2 input = parsec totalCountEveryone input


