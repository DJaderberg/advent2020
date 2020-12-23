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
            
    let rec pickDest2 len n1 n2 n3 x =
        let value = x - 1 
        let v = if value = 0 then len - 1 else value
        if n1 = value || n2 = value || n3 = value then (pickDest2 len n1 n2 n3 v)
        else v
        
    let fastRound len (array: int[]) current =
        let value = current
        let n1 = array.[value] % len
        let n2 = array.[n1] % len
        let n3 = array.[n2] % len
        let dest = (pickDest2 len n1 n2 n3 value) % len
        array.[current] <- array.[n3] % len
        array.[n3] <- array.[dest] % len
        array.[dest] <- n1 % len
        array.[current]
    
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
        
    let part2implForPart1 len rounds (input: string) =
        let start = Seq.toList input |> List.map (fun c -> c.ToString()) |> List.map Int32.Parse
        let initializer = Seq.zip (0 :: start) start |> Seq.toList
        let array =
            Array.zeroCreate (len + 1)
            |> Array.mapi (fun i _ -> i + 1)
        List.iter (fun (i, v) -> array.[i] <- v) initializer
        array.[len] <- 1
        //array.[7] <- 3
        array.[List.last start] <- List.head start
        let result = play (fastRound len array) rounds (List.head start)
        answer (List.ofArray array)
        
    let part2 len rounds (input: string) =
        let start = Seq.toList input |> List.map (fun c -> c.ToString()) |> List.map Int32.Parse
        let initializer = Seq.zip (0 :: start) (List.append start [List.length start + 1]) |> Seq.toList
        let array =
            Array.zeroCreate (len + 1)
            |> Array.mapi (fun i _ -> i + 1)
        "389125467" |> ignore
        List.iter (fun (i, v) -> array.[i] <- v) initializer
        //array.[0] <- 3
        //array.[3] <- 8
        //array.[8] <- 9
        //array.[9] <- 1
        //array.[1] <- 2
        //array.[2] <- 5
        //array.[5] <- 4
        //array.[4] <- 6
        //array.[6] <- 7
        //array.[7] <- 10
        //array.[List.last start] <- List.head start
        array.[List.last start] <- 10
        array.[len] <- List.head start
        let result = play (fastRound len array) rounds (List.head start)
        answer2 array
        
