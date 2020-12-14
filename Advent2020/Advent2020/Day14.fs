

namespace Advent2020

open System
open System.Collections
open System.Collections.Generic
open FParsec

module Day14 =
    type Instruction = | Mask of uint64 * uint64 | Mem of int * uint64
    let rec makeMask defV state bits =
        match bits with
        | [] -> state
        | b :: bs -> makeMask defV ((state <<< 1) + Option.defaultValue defV b) bs
    let makeOr = makeMask 0UL 0UL
    let makeAnd = makeMask 1UL 0UL
        
    let mask =
        pstring "mask = "
        >>. many1 (anyOf ['X'; '1'; '0'])
        |>> (fun l ->
            (List.map (function | '0' -> Some 0UL | _ -> None) l,
             List.map (function | '1' -> Some 1UL | _ -> None) l))
        |>> (fun (a, b) ->
            (makeAnd a, makeOr b))
    let mem = pstring "mem[" >>. (many1 digit |>> String.Concat |>> int)  .>> pstring "] = " .>>. (many1 digit |>> String.Concat |>> uint64)
    let instruction = (mask |>> Mask) <|> (mem |>> Mem)
    let list = sepEndBy1 instruction newline
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
    let part1 input =
        let instructions = parsec list input
        let e = execInstr (0UL, UInt64.MaxValue) Map.empty instructions
        Map.toSeq e
        |> Seq.map snd
        |> Seq.sum
        
    let part2 input = 86729
