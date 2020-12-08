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
            acc
        else
            match Array.get program pc with
            | Nop _ -> exec program (Set.add pc alreadyExeced) acc (pc+ 1)
            | Jmp i -> exec program (Set.add pc alreadyExeced) acc (pc + i)
            | Acc i -> exec program (Set.add pc alreadyExeced) (acc + i) (pc+ 1)
    let part1 input =
        let prog = parsec program input
        exec prog Set.empty 0 0
        
    let part2 input = raise (NotImplementedException())

