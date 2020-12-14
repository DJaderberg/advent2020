namespace Advent2020

open System
open System.Collections
open System.Collections.Generic
open FParsec

module Day14 =
    type BitValue = X | One | Zero
    type Instruction =
        | Mask of uint64 * uint64
        | Mem of uint64 * uint64
    let rec makeMask defV state bits =
        match bits with
        | [] -> state
        | b :: bs -> makeMask defV ((state <<< 1) + Option.defaultValue defV b) bs
    let makeOr = makeMask 0UL 0UL
    let makeAnd = makeMask 1UL 0UL
    let bitMap l =
        (List.map (function | Zero -> Some 0UL | _ -> None) l |> makeAnd,
         List.map (function | One -> Some 1UL | _ -> None) l |> makeOr)
    let bitMap2 l =
        (List.map (function | X -> Some 1UL | _ -> None) l |> makeOr,
         List.map (function | One -> Some 1UL | _ -> None) l |> makeOr)
    let mask =
        pstring "mask = "
        >>. many1 (anyOf ['X'; '1'; '0'])
        |>> List.map (fun v -> (dict ['X',X;'1',One;'0',Zero]).[v])
    let mem = pstring "mem[" >>. (many1 digit |>> String.Concat |>> uint64)  .>> pstring "] = " .>>. (many1 digit |>> String.Concat |>> uint64)
    let instruction mapper = (mask |>> mapper |>> Mask) <|> (mem |>> Mem)
    let list mapper = sepEndBy1 (instruction mapper) newline
    let parsec parser input =
        match run parser input with
        | Success(v, _, _) -> v
        | Failure(e, _, _) -> raise (Exception("Parsing error: " + e))
        
    let apply (a, o) value =
        (value &&& a) ||| o
        
    let rec execInstr mask mem instructions =
        match instructions with
        | [] -> mem
        | i :: is ->
            match i with
            | Mask (a, o) -> execInstr (a, o) mem is
            | Mem (loc, value) ->
                execInstr mask (Map.add loc (apply mask value) mem) is
                
    let rec variants (value: uint64) (floats: uint64) =
        match floats with
        | 0UL -> [value]
        | _ -> 
            let index = 63 - Seq.findIndex (fun i -> floats &&& (1UL <<< i) <> 0UL) (seq {63 .. -1 .. 0})
            let indexHot = 1UL <<< index
            let newValue1 = (value ||| indexHot)
            let newValue2 = (value &&& ~~~ indexHot)
            let newFloats = (floats - indexHot)
            value :: List.append (variants newValue1 newFloats) (variants newValue2 newFloats)
            
    let rec execInstr2 mask mem instructions =
        match instructions with
        | [] -> mem
        | i :: is ->
            match i with
            | Mask (a, o) -> execInstr2 (a, o) mem is
            | Mem (loc, value) ->
                let (floats, things) = mask
                let vs = variants (loc ||| things) floats |> Set.ofList |> Set.toList
                let map = List.fold (fun state v -> Map.add v value state) mem vs
                let keys = Map.toArray map |> Array.map fst
                let binKeys = keys |> Array.map (fun s -> Convert.ToString(s |> int64, 2))
                execInstr2 mask map is
    let part1 input =
        let instructions = parsec (list bitMap) input
        let e = execInstr (0UL, UInt64.MaxValue) Map.empty instructions
        Map.toSeq e
        |> Seq.map snd
        |> Seq.sum
        
    let part2 input =
        let instructions = parsec (list bitMap2) input
        let e = execInstr2 (0UL, UInt64.MaxValue) Map.empty instructions
        Map.toSeq e
        |> Seq.map snd
        |> Seq.sum
