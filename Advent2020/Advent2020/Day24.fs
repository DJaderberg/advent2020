namespace Advent2020

open System
open FParsec

module Day24 =
    let parsec parser input =
        match run parser input with
        | Success(v, _, _) -> v
        | Failure(e, _, _) -> raise (Exception(e))

    type Color = Black | White
    type Tile = Tile of string
    let tile = (pstring "ne" <|> pstring "nw" <|> pstring "sw" <|> pstring "se" <|> pstring "e" <|> pstring "w") |>> Tile
    let line = many1 tile
    let total = sepEndBy1 line newline
    
    let flipTile d t =
        match Map.tryFind t d with
        | None -> Map.add t Black d
        | Some color -> Map.add t (if color = Black then White else Black) d
    
    let step (x,y) tile =
        match tile with
        | Tile t ->
            match t with
            | "ne" -> (x + 1, y + 1)
            | "nw" -> (x - 1, y + 1)
            | "se" -> (x + 1, y - 1)
            | "sw" -> (x - 1, y - 1)
            | "e" -> (x + 2, y)
            | "w" -> (x - 2, y)
            | _ -> raise (Exception("Unknown direction in step"))
    let folder map tiles =
        let position = List.fold step (0,0) tiles
        flipTile map position
        
    let part1Map input =
        let parsed = parsec total input
        List.fold folder Map.empty parsed
    let countOfMap map =
        map
        |> Map.toList
        |> List.map snd
        |> List.filter ((=) Black)
        |> List.length
        
    let part1 input =
        countOfMap (part1Map input)
    
    let findSearchSpace (map: Map<int * int, 'a>) =
        let arr = Map.toSeq map |> Seq.map fst |> Seq.toArray
        let minX = Array.fold (fun s (x,_) -> if x < s then x else s) Int32.MaxValue arr
        let minY = Array.fold (fun s (_,y) -> if y < s then y else s) Int32.MaxValue arr
        let maxX = Array.fold (fun s (x,_) -> if x > s then x else s) Int32.MinValue arr
        let maxY = Array.fold (fun s (_,y) -> if y > s then y else s) Int32.MinValue arr
        Seq.allPairs (seq { minX - 2 .. maxX + 2 }) (seq { minY - 2 .. maxY + 2 })
        |> Seq.filter (fun (x,y) -> (x + y) % 2 = 0)
        
    let neighbors (x,y) =
            [
            (x + 1, y + 1)
            (x - 1, y + 1)
            (x + 1, y - 1)
            (x - 1, y - 1)
            (x + 2, y)
            (x - 2, y)
            ]
        
    let get map position =
        Map.tryFind position map
        |> Option.defaultValue White
    let conwaySingle map position =
        let count = neighbors position |> List.map (get map) |> List.filter ((=) Black) |> List.length
        match get map position with
        | Black when count = 0 || count > 2 -> White
        | White when count = 2 -> Black
        | c -> c
    let conwayStep map =
        let searchSpace = findSearchSpace map
        Seq.map (fun p -> (p, conwaySingle map p)) searchSpace
        |> Seq.filter (snd >> ((=) Black))
        |> Map.ofSeq
        
    let rec play player state rounds =
        match rounds with
        | 0 -> state
        | _ -> play player (player state) (rounds - 1)
    
    let part2 rounds input =
        let initial = part1Map input
        let result = play conwayStep initial rounds
        countOfMap result