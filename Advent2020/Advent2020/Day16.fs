namespace Advent2020

open System
open System.Collections
open System.Collections.Generic
open FParsec

module Day16 =
    type Rule = Rule of string * (int * int) * (int * int)
    let range = manyChars digit |>> int .>> pchar '-' .>>. (manyChars digit |>> int)
    let curry3 f a b c = f (a,b,c)
    let rule =  
        pipe3
            (sepEndBy1 (manyChars letter) spaces1 |>> List.fold ((+)) "" .>> pstring ": ")
            (range .>> pstring " or ")
            range
            (curry3 Rule)
    
    let values = sepEndBy1 (manyChars digit |>> int) (pchar ',')
    let yours = (many1 (anyOf (Seq.toList "your ticket:")) >>. newline) >>. values
    let nearby = (many1 (anyOf (Seq.toList "nearby tickets:")) >>. newline) >>. sepEndBy1 values newline
    
    let total =
        pipe3
            (sepEndBy1 (attempt rule) newline)
            (newline >>. yours .>> newline)
            (newline >>. nearby .>> optional newline)
            (curry3 id)
            
            
    let parsec parser input =
        match run parser input with
        | Success(v, _, _) -> v
        | Failure(e, _, _) -> raise (Exception(e))
    
    let setupRules (array: bool array) rule =
        match rule with
        | Rule (_, (a,b), (c,d)) ->
            for i in Seq.append (seq { a .. b }) (seq {c .. d}) do
                array.[i] <- true
            array
        
    let isOk (rulesArray : bool array) e = not rulesArray.[e]
    let part1 input =
        let (rules, _, nearby) = parsec total input
        let rulesArray = List.fold setupRules (Array.zeroCreate 1000) rules
        List.concat nearby |> List.filter (isOk rulesArray) |> List.sum
    let part2 input =
        42
