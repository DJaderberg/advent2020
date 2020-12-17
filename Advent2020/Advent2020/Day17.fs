namespace Advent2020

open System
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
        | '#' -> (x+1, y, Set.add (x,y) set)
        | _ -> (x+1, y, set)
    let initialGrid (y,set) nexts =
        let (_,_,newSet) = List.fold initialLine (0, y, set) nexts
        (y + 1, Set.union set newSet)
    let isActive set vec = Set.contains vec set
    let neighbors (x,y,z) =
        let xs = seq { x - 1 .. x + 1 }
        let ys = seq { y - 1 .. y + 1 }
        let zs = seq { z - 1 .. z + 1 }
        Seq.allPairs (Seq.allPairs xs ys) zs
        |> Seq.map (fun ((x,y),z) -> (x,y,z))
        |> Seq.except (Seq.singleton (x,y,z))
    let neighbors4d (x,y,z,w) =
        let xs = seq { x - 1 .. x + 1 }
        let ys = seq { y - 1 .. y + 1 }
        let zs = seq { z - 1 .. z + 1 }
        let ws = seq { w - 1 .. w + 1 }
        Seq.allPairs (Seq.allPairs (Seq.allPairs xs ys) zs) ws
        |> Seq.map (fun (((x,y),z),w) -> (x,y,z,w))
        |> Seq.except (Seq.singleton (x,y,z,w))
        
    let stepSingle neighbors set vec =
        let ns = neighbors vec
        let count = ns |> Seq.filter (isActive set) |> Seq.length
        match count with
        | 3 -> true
        | 2 when isActive set vec -> true
        | _ -> false
        
    let updateSingle neighbors oldSet set vec =
        let next = stepSingle neighbors oldSet vec
        if next then Set.add vec set
        else set
    let space3d (set: Set<int * int * int>) =
        let (minX, minY, minZ) = List.reduce (fun (sx: int, sy: int, sz: int) (x,y,z) ->
            (Math.Min(sx,x), Math.Min(sy,y), Math.Min(sz,z))) (Set.toList set)
        let (maxX, maxY, maxZ) = List.reduce (fun (sx: int, sy: int, sz: int) (x,y,z) ->
            (Math.Max(sx,x), Math.Max(sy,y), Math.Max(sz,z))) (Set.toList set)
        let (spaceX, spaceY, spaceZ) = (seq {minX - 1 .. maxX + 1}, seq {minY - 1 .. maxY + 1}, seq {minZ - 1 .. maxZ + 1})
        Seq.allPairs (Seq.allPairs spaceX spaceY) spaceZ |> Seq.map (fun ((x,y),z) -> (x,y,z))
    let space4d (set: Set<int * int * int * int>) =
        let (minX, minY, minZ, minW) = List.reduce (fun (sx: int, sy: int, sz: int, sw: int) (x,y,z,w) ->
            (Math.Min(sx,x), Math.Min(sy,y), Math.Min(sz,z), Math.Min(sw, w))) (Set.toList set)
        let (maxX, maxY, maxZ, maxW) = List.reduce (fun (sx: int, sy: int, sz: int, sw: int) (x,y,z, w) ->
            (Math.Max(sx,x), Math.Max(sy,y), Math.Max(sz,z), Math.Max(sw,w))) (Set.toList set)
        let (spaceX, spaceY, spaceZ, spaceW) = (seq {minX - 1 .. maxX + 1}, seq {minY - 1 .. maxY + 1}, seq {minZ - 1 .. maxZ + 1}, seq { minW - 1 .. maxW + 1 })
        Seq.allPairs (Seq.allPairs (Seq.allPairs spaceX spaceY) spaceZ) spaceW |> Seq.map (fun (((x,y),z),w) -> (x,y,z,w))
        
    let step space neighbors set =
        let range = space set
        Seq.fold (updateSingle neighbors set) Set.empty range
    let rec steps stepper n set =
        match n with
        | 0 -> set
        | _ -> steps stepper (n - 1) (stepper set)
        
    let play stepper gridToSpace iterations input =
        let parsed = parsec p2dSlice input
        let (_, grid) = List.fold initialGrid (0, Set.empty) parsed
        let initial = grid |> Set.map gridToSpace
        let result = steps stepper iterations initial
        Set.count result
        
    let part1 =
        play (step space3d neighbors) (fun (x,y) -> (x,y,0))
        
    let part2 = 
        play (step space4d neighbors4d) (fun (x,y) -> (x,y,0,0))
