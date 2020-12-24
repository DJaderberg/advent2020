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
    let part1 input =
        part1Map input
        |> Map.toList
        |> List.map snd
        |> List.filter ((=) Black)
        |> List.length
    let part2 input = 42