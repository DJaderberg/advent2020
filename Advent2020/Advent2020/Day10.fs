namespace Advent2020

open System
open System.Collections
open System.Collections.Generic
open System.Diagnostics.CodeAnalysis
open FParsec

module Day10 =
    let adapterJoltage = many1 digit |>> String.Concat |>> int64
    let adapters = sepEndBy1 adapterJoltage newline
    let parsec parser input =
        match run parser input with
        | Success(v, _, _) -> v
        | Failure(e, _, _) -> raise (Exception("Parsing error: " + e))
    
    let rec differences prev jolts =
        match jolts with
        | [] -> []
        | j :: js -> (j - prev) :: differences j js
        
    let part1 input =
        let orig = parsec adapters input
        let sorted = List.sort orig
        let max = List.max sorted
        let full = List.append sorted [max + 3L]
        let diffs =
            differences 0L full
            |> List.groupBy id |> dict
        (diffs.[1L].Length |> int64) * (diffs.[3L].Length |> int64)
        
    let rec paths curr diffs =
        match diffs with
        | [] -> curr
        | [ _ ] -> curr
        | 1L :: 1L :: 1L :: 1L :: ds -> paths (7L * curr) ds
        | 1L :: 1L :: 1L :: ds -> paths (4L * curr) ds
        | 1L :: 1L :: ds -> paths (2L * curr) ds
        | d :: ds -> paths curr ds
        
    let part2 input =
        let orig = parsec adapters input
        let sorted = List.sort orig
        let max = List.max sorted
        let full = List.append sorted [max + 3L]
        let diffs =
            differences 0L full
        paths 1L diffs
