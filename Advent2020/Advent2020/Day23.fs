namespace Advent2020

open System
open FParsec

module Day23 =
    let parsec parser input =
        match run parser input with
        | Success(v, _, _) -> v
        | Failure(e, _, _) -> raise (Exception(e))
        
    let rec pickDestination length h pickup =
        if h = 1 then pickDestination length length pickup
        else if List.contains (h - 1) pickup then
            pickDestination length (h - 1) pickup
        else
            h - 1
    let round c =
        match c with
        | h :: a :: b :: c :: tail ->
            let picked = [a; b; c]
            let dest = pickDestination 10 h picked
            let circle = h :: tail
            let splitIndex = List.findIndex ((=) dest) circle
            let (before, d :: after) = List.splitAt splitIndex circle
            let (h :: t) = List.concat [before; d :: picked; after]
            List.append t [h]
            
    let rec pickDest2 len picked x =
        let value = x - 1 
        let v = if value = 0 then len else value
        if List.contains v picked then pickDest2 len picked (v)
    let fastRound (array: int[]) index =
        let len = 1000000
        let value = index
        let n1 = array.[value]
        let n2 = array.[n1]
        let n3 = array.[n2]
        let dest = (pickDestination len value [n1;n2;n3])
        array.[value] <- array.[n3]
        array.[n3] <- array.[dest]
        array.[dest] <- n1
        array.[value]
    
    let rec play r rounds state =
        match rounds with
        | 0 -> state
        | _ -> play r (rounds - 1) (r state)
        
    let answer result =
        let index = List.findIndex ((=) 1) result
        let (before, i :: after) = List.splitAt index result
        List.concat [after; before]
        |> List.map (fun i -> i.ToString())
        |> List.fold (+) ""
        
    let part1 rounds (input: string) =
        let starting = Seq.toList input |> List.map (fun c -> c.ToString()) |> List.map Int32.Parse
        let result = play round rounds starting
        answer result
        
    let answer2 (array: int[]) =
        let v = array.[1]
        let v2 = array.[v]
        (int64 v) * (int64 v2)
        
    let part2 rounds (input: string) =
        let len = 1000000
        let start = Seq.toList input |> List.map (fun c -> c.ToString()) |> List.map Int32.Parse
        let array =
            Array.zeroCreate (len + 1)
            |> Array.mapi (fun i _ -> i + 1)
        "389125467" |> ignore
        array.[0] <- 3
        array.[3] <- 8
        array.[8] <- 9
        array.[9] <- 1
        array.[1] <- 2
        array.[2] <- 5
        array.[5] <- 4
        array.[4] <- 6
        array.[6] <- 7
        array.[7] <- 10
        array.[len] <- 1
        let result = play (fastRound array) rounds array.[0]
        answer2 array
        
