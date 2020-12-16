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
        
    let isOk (rulesArray : bool array) e = rulesArray.[e]
    let part1 input =
        let (rules, _, nearby) = parsec total input
        let rulesArray = List.fold setupRules (Array.zeroCreate 1000) rules
        List.concat nearby |> List.filter (isOk rulesArray >> not) |> List.sum
        
    let isOkForRule value (Rule (_, (a,b), (c,d))) = (a <= value  &&  value <= b) || (c <= value && value <= d)
    let filterRuleList (value, rs) = List.filter (isOkForRule value) rs
    let x rulesDict names ticket = List.map (fun n -> Map.find n rulesDict) names
    
    let processTicket (ruless: Rule list list) (ticket: int list) =
        let zipped = List.zip ticket ruless
        List.map filterRuleList zipped
        
    let drawConclusions (known: (int * Rule) list) (next: (int * Rule list)) =
        let addToKnown = snd next |> List.except (known |> List.map snd) |> List.head
        (fst next, addToKnown) :: known
    let part2 input =
        let (rules, ticket, nearby) = parsec total input
        let myTicket = Array.ofList ticket
        let rulesArray = List.fold setupRules (Array.zeroCreate 1000) rules
        let validTickets = nearby |> List.filter (List.forall (isOk rulesArray))
        let positionPossibilities = Array.zeroCreate (List.length rules) |> Array.map (fun _ -> rules) |> Array.toList
        let calculated = List.fold processTicket positionPossibilities validTickets
        let zip = List.indexed calculated |> List.sortBy (snd >> List.length)
        let conclusions = List.fold drawConclusions [] zip
        let interestingIndices =
            conclusions
            |> List.map (fun (index, (Rule (name, _, _))) -> (index, name))
            |> List.filter (fun (_, name) -> name.Contains "departure")
            |> List.map fst
        interestingIndices
        |> List.map (fun e -> myTicket.[e] |> int64)
        |> List.fold (*) 1L
