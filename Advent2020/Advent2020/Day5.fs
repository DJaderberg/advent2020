namespace Advent2020

open System
open System.Linq
open System.Collections.Generic
open FParsec

module Day5 =
    let cToB c = match c with | 'B' -> 1 | 'F' -> 0 | _ -> -1
    let rToB c = match c with | 'R' -> 1 | 'L' -> 0 | _ -> -1
    let rec arrToId exp sum arr =
        let h = List.head arr
        match exp with
        | 1 -> sum + h*exp
        | _ -> arrToId (exp/2) (sum+h*exp) (List.tail arr)
    let row = manyMinMaxSatisfy 7 7 (fun c -> c = 'F' || c = 'B') |>> (Seq.toList >> List.map cToB >> (arrToId 64 0))
    let col = manyMinMaxSatisfy 3 3 (fun c -> c = 'R' || c = 'L') |>> (Seq.toList >> List.map rToB >> (arrToId 4 0))
    let id = row .>>. col |>> (fun (r, c) -> r * 8 + c)

    let idList = sepEndBy id spaces1
    let parsec input =
        match run idList input with
        | Success(v, _, _) -> v
        | Failure(e, _, _) -> raise (Exception("Parsing error: " + e))
        
    let part1 input =
        parsec input
        |> List.max
        
    let part2 input =
        let set =
            parsec input
            |> Set.ofList
        Seq.find (fun seat -> not (Set.contains seat set)) (seq { 96 .. 897 })

