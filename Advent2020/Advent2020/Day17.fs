namespace Advent2020

open System
open System.Collections
open System.Collections.Generic
open FParsec

module Day17 =
    let active = pchar '#'
    let inactive = pchar '.'
    let p1dSlice = many1 (active <|> inactive)
    let p2dSlice = sepEndBy1 p1dSlice newline
    let parsec parser input =
        match run parser input with
        | Success(v, _, _) -> v
        | Failure(e, _, _) -> raise (Exception(e))
    let initialLine (x,y, set) next =
        match next with
        | '#' -> (x+1, y, Set.add (x,y,0) set)
        | _ -> (x+1, y, set)
    let initialGrid (y,set) nexts =
        let (_,_,newSet) = List.fold initialLine (0, y, set) nexts
        (y + 1, Set.union set newSet)
    let isActive set vec = Set.contains vec set
    let neighbors (x,y,z) =
        [
            (x+1,y,z);
            (x+1,y,z+1);
            (x+1,y,z-1);
            (x+1,y+1,z);
            (x+1,y+1,z+1);
            (x+1,y+1,z-1);
            (x+1,y-1,z);
            (x+1,y-1,z+1);
            (x+1,y-1,z-1);
            //(x,y,z);
            (x,y,z+1);
            (x,y,z-1);
            (x,y+1,z);
            (x,y+1,z+1);
            (x,y+1,z-1);
            (x,y-1,z);
            (x,y-1,z+1);
            (x,y-1,z-1);
            (x-1,y,z);
            (x-1,y,z+1);
            (x-1,y,z-1);
            (x-1,y+1,z);
            (x-1,y+1,z+1);
            (x-1,y+1,z-1);
            (x-1,y-1,z);
            (x-1,y-1,z+1);
            (x-1,y-1,z-1);
        ]
        
    let stepSingle set vec =
        let ns = neighbors vec |> Set.ofList |> Set.toList
        let count = ns |> List.filter (isActive set) |> List.length
        match count with
        | 3 -> true
        | 2 when isActive set vec -> true
        | _ -> false
    
    let updateSingle oldSet set vec =
        let currently = isActive oldSet vec
        let next = stepSingle oldSet vec
        if next then Set.add vec set
        else set
    let searchSpace (set: Set<int * int * int>) =
        let (minX, minY, minZ) =List.reduce (fun (sx: int, sy: int, sz: int) (x,y,z) ->
            (Math.Min(sx,x), Math.Min(sy,y), Math.Min(sz,z))) (Set.toList set)
        let (maxX, maxY, maxZ) =List.reduce (fun (sx: int, sy: int, sz: int) (x,y,z) ->
            (Math.Max(sx,x), Math.Max(sy,y), Math.Max(sz,z))) (Set.toList set)
        (seq {minX - 1 .. maxX + 1}, seq {minY - 1 .. maxY + 1}, seq {minZ - 1 .. maxZ + 1})
    let step set =
        let (spaceX, spaceY, spaceZ) = searchSpace set
        let range = Seq.allPairs (Seq.allPairs spaceX spaceY) spaceZ |> Seq.map (fun ((x,y),z) -> (x,y,z))
        Seq.fold (updateSingle set) Set.empty range
    let rec steps n set =
        match n with
        | 0 -> set
        | _ -> steps (n - 1) (step set)
    let part1 iterations input =
        let parsed = parsec p2dSlice input
        let (_, initial) = List.fold initialGrid (0, Set.empty) parsed
        let result = steps iterations initial
        Set.count result
        
    let part2 input = 42
