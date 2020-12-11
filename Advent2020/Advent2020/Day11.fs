namespace Advent2020

open System
open System.Collections
open System.Collections.Generic
open FParsec

module Day11 =
    type Spot = Floor | Empty | Occupied
    let floor = pchar '.' |>> fun _ -> Floor
    let empty = pchar 'L' |>> fun _ -> Empty
    let occupied = pchar '#' |>> fun _ -> Occupied
    let row = many1 (floor <|> empty <|> occupied)
    let layout = sepEndBy1 row newline |>> array2D
    let parsec parser input =
        match run parser input with
        | Success(v, _, _) -> v
        | Failure(e, _, _) -> raise (Exception("Parsing error: " + e))
    
    let newState limit curr neighbors =
        match (curr, neighbors) with
        | (Empty, 0) -> Occupied
        | (Occupied, n) when n >= limit -> Empty
        | (c, _) -> c
    let eval spot =
        match spot with
        | Occupied -> 1
        | _ -> 0
        
    let tryGet field (i,j) =
        let iMax = (Array2D.length1 field)
        let jMax = (Array2D.length2 field)
        if i >= iMax || j >= jMax || i < 0 || j < 0 then
            None
        else
            Some (Array2D.get field i j)
            
    let update1 field i j e =
        [(i + 1, j); (i - 1, j); (i, j + 1); (i, j - 1); (i - 1, j - 1); (i + 1, j - 1); (i + 1, j + 1); (i - 1, j + 1)]
        |> List.choose (tryGet field)
        |> List.map eval |> List.sum
        |> newState 4 e
    
    let makeStep (is, js) (ci, cj) = (ci + is, cj + js)
    let rec anyOccupied field step curr =
        let n = makeStep step curr
        match tryGet field n with
        | None -> false
        | Some Occupied -> true
        | Some Empty -> false
        | Some _ -> anyOccupied field step n
    let update2 field i j e =
        [(1, 0); (-1, 0); (0, 1); (0, -1); (-1, -1); (1, -1); (1, 1); (-1, 1)]
        |> List.filter (fun s -> anyOccupied field s (i, j))
        |> List.length
        |> newState 5 e
    
    let calculate field =
        let mutable counter = 0
        let _ = Array2D.iter (fun e -> (counter <- counter + eval e)) field
        counter
        
    let step updater field =
        let n = field |> Array2D.mapi (updater field)
        n
    let rec play updater old =
        let newLayout = step updater old
        if newLayout = old then
            calculate newLayout
        else
            play updater newLayout
            
    let part1 input =
        let layout = parsec layout input
        play update1 layout
        
    let part2 input =
        let layout = parsec layout input
        play update2 layout
        
    let iter2 input n =
        let layout = parsec layout input
        seq { 1 .. n }
        |> Seq.fold (fun s _ -> step update2 s) layout 

    let flatten (A:'a[,]) = A |> Seq.cast<'a>
    let toString layout =
        layout
        |> Array2D.map (function |Occupied -> "#" |Empty -> "L" |Floor -> ".")
        |> flatten
        |> Seq.fold (fun s e -> s + e) ""
