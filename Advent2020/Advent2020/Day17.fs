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
    let initialLine4d (x,y, set) next =
        match next with
        | '#' -> (x+1, y, Set.add (x,y,0,0) set)
        | _ -> (x+1, y, set)
    let initialGrid (y,set) nexts =
        let (_,_,newSet) = List.fold initialLine (0, y, set) nexts
        (y + 1, Set.union set newSet)
        
    let initialGrid4d (y,set) nexts =
        let (_,_,newSet) = List.fold initialLine4d (0, y, set) nexts
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
        |> Seq.ofList
        
    let neighbors4d (x,y,z,w) =
        let xs = seq { x - 1 .. x + 1 }
        let ys = seq { y - 1 .. y + 1 }
        let zs = seq { z - 1 .. z + 1 }
        let ws = seq { w - 1 .. w + 1 }
        Seq.allPairs (Seq.allPairs (Seq.allPairs xs ys) zs) ws
        |> Seq.map (fun (((x,y),z),w) -> (x,y,z,w))
        |> Seq.except (Seq.singleton (x,y,z,w))
    let stepSingle set vec =
        let ns = neighbors vec
        let count = ns |> Seq.filter (isActive set) |> Seq.length
        match count with
        | 3 -> true
        | 2 when isActive set vec -> true
        | _ -> false
        
    let stepSingle4d set vec =
        let ns = neighbors4d vec
        let count = ns |> Seq.filter (isActive set) |> Seq.length
        match count with
        | 3 -> true
        | 2 when isActive set vec -> true
        | _ -> false
    
    let updateSingle oldSet set vec =
        let next = stepSingle oldSet vec
        if next then Set.add vec set
        else set
    let updateSingle4d oldSet set vec =
        let next = stepSingle4d oldSet vec
        if next then Set.add vec set
        else set
    let searchSpace3d (set: Set<int * int * int>) =
        let (minX, minY, minZ) = List.reduce (fun (sx: int, sy: int, sz: int) (x,y,z) ->
            (Math.Min(sx,x), Math.Min(sy,y), Math.Min(sz,z))) (Set.toList set)
        let (maxX, maxY, maxZ) = List.reduce (fun (sx: int, sy: int, sz: int) (x,y,z) ->
            (Math.Max(sx,x), Math.Max(sy,y), Math.Max(sz,z))) (Set.toList set)
        (seq {minX - 1 .. maxX + 1}, seq {minY - 1 .. maxY + 1}, seq {minZ - 1 .. maxZ + 1})
    let searchSpace4d (set: Set<int * int * int * int>) =
        let (minX, minY, minZ, minW) = List.reduce (fun (sx: int, sy: int, sz: int, sw: int) (x,y,z,w) ->
            (Math.Min(sx,x), Math.Min(sy,y), Math.Min(sz,z), Math.Min(sw, w))) (Set.toList set)
        let (maxX, maxY, maxZ, maxW) = List.reduce (fun (sx: int, sy: int, sz: int, sw: int) (x,y,z, w) ->
            (Math.Max(sx,x), Math.Max(sy,y), Math.Max(sz,z), Math.Max(sw,w))) (Set.toList set)
        (seq {minX - 1 .. maxX + 1}, seq {minY - 1 .. maxY + 1}, seq {minZ - 1 .. maxZ + 1}, seq { minW - 1 .. maxW + 1 })
    let step3d set =
        let (spaceX, spaceY, spaceZ) = searchSpace3d set
        let range = Seq.allPairs (Seq.allPairs spaceX spaceY) spaceZ |> Seq.map (fun ((x,y),z) -> (x,y,z))
        Seq.fold (updateSingle set) Set.empty range
        
    let step4d set =
        let (spaceX, spaceY, spaceZ, spaceW) = searchSpace4d set
        let range = Seq.allPairs (Seq.allPairs (Seq.allPairs spaceX spaceY) spaceZ) spaceW |> Seq.map (fun (((x,y),z),w) -> (x,y,z,w))
        Seq.fold (updateSingle4d set) Set.empty range
    let rec steps stepper n set =
        match n with
        | 0 -> set
        | _ -> steps stepper (n - 1) (stepper set)
    let part1 iterations input =
        let parsed = parsec p2dSlice input
        let (_, initial) = List.fold initialGrid (0, Set.empty) parsed
        let result = steps step3d iterations initial
        Set.count result
        
    let part2 iterations input = 
        let parsed = parsec p2dSlice input
        let (_, initial) = List.fold initialGrid4d (0, Set.empty) parsed
        let result = steps step4d iterations initial
        Set.count result
