namespace Advent2020

open System
open System.Linq.Expressions
open System.Reflection.Emit
open FParsec

module Day19 =
    type Rule =
        | SubRules of int list list
        | CharRule of char
    
    let okChar = anyOf ['a'; 'b']
    let space = optional (pchar ' ')
    let id = many1Chars digit |>> int
    let subrule = space >>. sepEndBy1 id space
    let subrules = sepEndBy1 subrule (pchar '|') |>> SubRules
    let charrule = pchar '"' >>. okChar .>> pchar '"' |>> CharRule
    let rule = many1Chars digit |>> int .>> pchar ':' .>> space .>>. (charrule <|> subrules)
    let rules = sepEndBy1 rule newline |>> Map.ofList
    let data = sepEndBy1 (many1 okChar) newline
    
    let all = rules .>> newline .>>. data
    let parsec parser input =
        match run parser input with
        | Success(v, _, _) -> v
        | Failure(e, _, _) -> raise (Exception(e))
        
    let evalCharRule c (data: char list list): char list list =
        let evalSingle (d: char list) =
            match d with
            | ch :: tail when ch = c -> Some tail
            | _ -> None
        List.choose evalSingle data
    let rec eval (rules: Map<int,Rule>) rule (data: char list list): char list list =
        match rule with
        | CharRule c -> evalCharRule c data
        | SubRules sr -> List.map (evalSubRule rules data) sr |> List.concat
    and evalSubRule (rules: Map<int, Rule>) (data: char list list) (remainingRules: int list): char list list =
        match remainingRules with
        | [] -> data
        | i :: tail ->
            if data = [] then [] else
            let r = Map.find i rules
            let value = eval rules r data
            evalSubRule rules value tail
        
        
    let part1 (input: string) =
        let (rules, data) = parsec all input
        let result = List.map (fun d -> eval rules (Map.find 0 rules) [d]) data |> List.filter (List.contains [])
        result
        |> List.length
        
    let part2 (input: string) = 42
