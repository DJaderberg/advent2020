﻿namespace Advent2020

open System
open System.Linq.Expressions
open System.Reflection.Emit
open FParsec

module Day20 =
    type Edge = Above | Below | Left | Right
    type Tile = Tile of int * char[,]
    
    let idLine = pstring "Tile " >>. many1Chars digit |>> int .>> pchar ':' .>> newline
    let arrayLine = many1 (anyOf ['#';'.'])
    let array = sepEndBy1 arrayLine newline |>> array2D
    let tile = idLine .>>. array
    let tiles = sepEndBy1 tile newline |>> Map.ofList
    let parsec parser input =
        match run parser input with
        | Success(v, _, _) -> v
        | Failure(e, _, _) -> raise (Exception(e))
        
        
    let opposite =
        function
            | Above -> Below
            | Below -> Above
            | Left -> Right
            | Right -> Left
        
    let getEdge edge (tile: char[,]) =
        match edge with
        | Above -> tile.[0,*]
        | Below -> tile.[9,*]
        | Left -> tile.[*,0]
        | Right -> tile.[*,9]
        
    let variants tile = [tile] // TODO: Actually implement this...
    let fulfills tile ((e,arr): Edge * char[]) = arr = getEdge e tile
    let findMatchSingle (requirements: (Edge * char[]) list) (tile: char[,]) =
        let vs = variants tile
        let out = List.filter (fun v -> List.forall (fulfills v) requirements) vs
        out
        
    let findOptions (currentSolution: Map<int * int,int * char[,]>) (options: Map<int, char[,]>) (x,y) =
        let requirements =
            [(x-1,y,Left);(x+1,y,Right);(x,y-1,Above);(x,y+1,Below)]
            |> List.choose (fun (x,y,e) -> Map.tryFind (x,y) currentSolution |> Option.map (fun (i,t) -> (e, getEdge (opposite e) t)))
        let matches = List.map (fun (i, t) -> (i, findMatchSingle requirements t)) (Map.toList options)
        matches 
        |> List.map (fun (i, cArrays) -> List.map (fun c -> (i,c)) cArrays)
        |> List.concat
    
    let findPositionOptions solvedPositions =
        let neighbors (x,y) = [(x,y-1);(x+1,y);(x-1,y);(x,y+1)]
        let neighborsSolved (x,y) =
            neighbors (x,y)
            |> List.filter (fun pos -> Map.containsKey pos solvedPositions)
            |> List.length
        if Map.isEmpty solvedPositions then [(0,0)] else
            Map.toList solvedPositions
            |> List.map fst
            |> List.map neighbors |> List.concat
            |> List.filter (fun pos -> None = Map.tryFind pos solvedPositions)
            |> List.sortByDescending neighborsSolved
    
    let rec solve (solved: Map<int * int, int * char[,]>) (remaining: Map<int, char[,]>) =
        if Map.isEmpty remaining then Seq.singleton solved else
            let ps = findPositionOptions solved
            let options = ps |> List.map (fun pos -> findOptions solved remaining pos |> List.map (fun e -> (pos, e))) |> List.concat
            let addToSolved p e = Map.add p e solved
            let removeFromRemaining (i, tile) = Map.remove i remaining
            Seq.map (fun (pos, e) -> solve (addToSolved pos e) (removeFromRemaining e)) options
            |> Seq.concat
    
    let part1 (input: string) =
        let tiles = parsec tiles input
        let solved = solve Map.empty tiles |> Seq.head
        24 |> int64
        
    let part2 (input: string) = 42L
