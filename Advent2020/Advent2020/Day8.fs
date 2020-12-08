namespace Advent2020

open System
open System.Collections
open System.Collections.Generic
open FParsec

module Day8 =
    type Instruction = Jmp of int | Nop of int | Acc of int
    let jmp = pstring "jmp " >>. pint32 |>> Jmp
    let nop = pstring "nop " >>. pint32 |>> Nop
    let acc = pstring "acc " >>. pint32 |>> Acc
    
    let instr = acc <|> jmp <|> nop
    let program = sepEndBy1 instr newline |>> Array.ofList
    let parsec parser input =
        match run parser input with
        | Success(v, _, _) -> v
        | Failure(e, _, _) -> raise (Exception("Parsing error: " + e))
        
    let rec exec (program: Instruction[]) (alreadyExeced: Set<int>) acc pc =
        if Set.contains pc alreadyExeced then
            FSharp.Core.Error acc
        else if Array.length program <= pc then
            FSharp.Core.Ok acc
        else
            match Array.get program pc with
            | Nop _ -> exec program (Set.add pc alreadyExeced) acc (pc+ 1)
            | Jmp i -> exec program (Set.add pc alreadyExeced) acc (pc + i)
            | Acc i -> exec program (Set.add pc alreadyExeced) (acc + i) (pc+ 1)
    let part1 input =
        let prog = parsec program input
        match exec prog Set.empty 0 0 with
        | FSharp.Core.Error i -> i
        | FSharp.Core.Ok i -> i
        
        
    let part2 input = 
        let prog = parsec program input
        let update (i: int, instr: Instruction) program =
            let newArray = Array.copy program
            Array.set newArray i instr
            newArray
        let success = function |FSharp.Core.Ok v -> Some v |FSharp.Core.Error _ -> None
        let perturbed =
            seq { 0 .. Array.length prog - 1 }
            |> Seq.map (fun i -> (i, Array.get prog i))
            |> Seq.choose (function | (i, Jmp v) -> Some (i, Nop v) | (i, Nop v) -> Some (i, Jmp v) | _ -> None)
            |> Seq.choose (fun tup -> exec (update tup prog) Set.empty 0 0 |> success)
            |> Seq.head
        perturbed
        

