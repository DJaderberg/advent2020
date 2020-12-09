namespace Advent2020

open System
open System.Collections
open System.Collections.Generic
open System.Diagnostics.CodeAnalysis
open FParsec

module Day9 =
    let number = many1 digit |>> String.Concat |>> int64
    let numbers = sepEndBy1 number newline
    let parsec parser input =
        match run parser input with
        | Success(v, _, _) -> v
        | Failure(e, _, _) -> raise (Exception("Parsing error: " + e))
        
    let hasMatch (numbers: seq<int64 * int64>) (sum: int64) =
        let elem e e2 = fst e + fst e2 = sum && snd e <> snd e2
        let subSearch e = (Seq.filter (elem e) numbers) |> Seq.tryHead
        let m =
            numbers
            |> Seq.choose subSearch
            |> Seq.tryHead
        m |> Option.map (fun _ -> sum)
        
    let rec findNonMatch (preambleSize: int64) (ql: int64) (queue: Queue<int64 * int64>) (numbers: seq<int64 * int64>) =
        if ql = preambleSize then
            let next = Seq.head numbers
            match hasMatch queue (fst next) with
            | Some _ -> 
                let _ = queue.Dequeue()
                let _ = queue.Enqueue(next)
                findNonMatch preambleSize ql queue (Seq.tail numbers)
            | None -> fst next
        else
            let next = Seq.head numbers
            let _ = queue.Enqueue(next)
            findNonMatch preambleSize (ql + 1L) queue (Seq.tail numbers)
    let part1 (size: int64) input =
        let ns = parsec numbers input
        findNonMatch size 0L (Queue<int64 * int64>()) (Seq.zip (Seq.ofList ns) (seq { 0L..1000000L }))
        
    let rec contiguousSum sum (options: (int64 * int64) list) numbers =
        let h = List.head numbers
        let opts = List.map (fun e -> (fst h + fst e, snd e)) options |> List.filter (fun e -> fst e <= sum)
        let m = opts |> List.filter (fun e -> fst e = sum) |> List.tryHead
        match m with
        | Some (v, start) -> (start , snd h)
        | None -> contiguousSum sum (List.head numbers :: opts) (List.tail numbers)
    let part2 (size: int64) input =
        let ns = parsec numbers input
        let sum = findNonMatch size 0L (Queue<int64 * int64>()) (Seq.zip (Seq.ofList ns) (seq { 0L..1000000L }))
        let (l, u) = contiguousSum sum List.empty (Seq.zip ns (seq { 0L .. 1000000L }) |> List.ofSeq)
        let range = ns |> List.skip (int l) |> List.take (int (u - l + 1L))
        let min = List.min range
        let max = List.max range
        min + max
        
