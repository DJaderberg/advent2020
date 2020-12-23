namespace Advent2020

open System
open FParsec

module Day23 =
    let parsec parser input =
        match run parser input with
        | Success(v, _, _) -> v
        | Failure(e, _, _) -> raise (Exception(e))
        
    let rec pickDestination h pickup =
        if h = 1 then pickDestination 10 pickup
        else if List.contains (h - 1) pickup then
            pickDestination (h - 1) pickup
        else
            h - 1
    let round c =
        match c with
        | h :: a :: b :: c :: tail ->
            let picked = [a; b; c]
            let dest = pickDestination h picked
            let circle = h :: tail
            let splitIndex = List.findIndex ((=) dest) circle
            let (before, d :: after) = List.splitAt splitIndex circle
            let (h :: t) = List.concat [before; d :: picked; after]
            List.append t [h]
    
    let rec play rounds state =
        match rounds with
        | 0 -> state
        | _ -> play (rounds - 1) (round state)
    let answer result =
        let index = List.findIndex ((=) 1) result
        let (before, i :: after) = List.splitAt index result
        List.concat [after; before]
        |> List.map (fun i -> i.ToString())
        |> List.fold (+) ""
        
    let part1 rounds (input: string) =
        let starting = Seq.toList input |> List.map (fun c -> c.ToString()) |> List.map Int32.Parse
        let result = play rounds starting
        answer result
    let part2 input = 42
        
