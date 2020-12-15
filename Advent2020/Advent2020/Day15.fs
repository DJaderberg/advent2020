namespace Advent2020

open System
open System.Collections
open System.Collections.Generic
open FParsec

module Day15 =
    let number = many1 digit |>> String.Concat |>> int
    let startingNumbers = sepEndBy1 number (pchar ',')
    let parsec parser input =
        match run parser input with
        | Success(v, _, _) -> v
        | Failure(e, _, _) -> raise (Exception("Parsing error: " + e))
                
    let rec step (whenSaid, curTurn, prevSaid) remaining =
        match remaining with
        | 0 -> prevSaid
        | _ ->
            match Map.tryFind prevSaid whenSaid with
            | None -> step ((Map.add prevSaid curTurn whenSaid), (curTurn + 1), 0) (remaining - 1)
            | Some turn ->
                let say = curTurn - turn
                step ((Map.add prevSaid curTurn whenSaid), (curTurn + 1), say) (remaining - 1)
    
    let setup (map, prev, index) current =
        match prev with
        | None -> (map, Some current, index + 1)
        | Some p -> ((Map.add p index map), Some current, index + 1)
    let part1 turns input =
        let start = parsec startingNumbers input
        let (startingMap, prev, setupTurns) = List.fold setup (Map.empty, None, 0) start
        let p = Option.defaultValue 0 prev
        step (startingMap, setupTurns, p) (turns - setupTurns)
        
    let part2 input = 42

