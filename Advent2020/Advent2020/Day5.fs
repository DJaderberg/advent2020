namespace Advent2020

open System
open FParsec

module Day5 =
    let cToDigit = function | 'B' | 'R' -> '1' | 'F' | 'L' -> '0' | _ -> raise (Exception "cToDigit")
    let toInt str = int ("0b" + str)
    let all =
        manyMinMaxSatisfy 10 10 (fun _ -> true) |>>
        (Seq.toList >> List.map cToDigit >> String.Concat >> toInt)

    let idList = sepEndBy all spaces1
    let parsec input =
        match run idList input with
        | Success(v, _, _) -> v
        | Failure(e, _, _) -> raise (Exception("Parsing error: " + e))
        
    let part1 input =
        parsec input
        |> List.max
        
    let part2 input =
        let found =
            parsec input
            |> Set.ofList
        let all = seq { Set.minElement found .. Set.maxElement found } |> Set.ofSeq
        all - found |> Set.toList |> List.head

