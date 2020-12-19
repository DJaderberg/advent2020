namespace Advent2020

open System
open System.Linq.Expressions
open System.Reflection.Emit
open FParsec

module Day18 =
    type Operator = Add | Multiply
    type Expression =
        | Term of int64
        | Parenthesis of Expression
        | Operation of Expression * (Operator * Expression) list
    let term = many1Chars digit |>> int64 |>> Term
    let operator = (pchar '*' |>> fun _ -> Multiply) <|> (pchar '+' |>> fun _ -> Add)
    let expr, exprImpl = createParserForwardedToRef()
    let rec parens =
        pchar '(' >>. spaces >>. expressions .>> spaces .>> pchar ')'
    and rest = many ((spaces >>. operator) .>>. (spaces >>. expr))
    and expressions =
        spaces >>. expr .>>. rest |>> Operation
    
    do exprImpl := choice [parens; term]
    
    let parsec parser input =
        match run parser input with
        | Success(v, _, _) -> v
        | Failure(e, _, _) -> raise (Exception(e))
        
    let rec eval1 (expr: Expression) =
        match expr with
        | Term i -> i
        | Parenthesis e -> eval1 e
        | Operation (first, list) ->
            let help state (op, v) =
                match op with
                | Add -> state + (eval1 v)
                | Multiply -> state * (eval1 v)
            List.fold help (eval1 first) list
            
    type Tree =
        | Node of Expression * Operator * Expression
        | Leaf of Expression
    let rec eval2 (expr: Expression) =
        let (opEval: Operator -> int64 -> int64 -> int64) = function | Add -> (+) | Multiply -> (*)
        match expr with
        | Term i -> i
        | Parenthesis e -> eval1 e
        | Operation (f, l) ->
            let rec listEval first list =
                match list with
                | [] -> eval2 first
                | (op1, e1) :: (op2, e2) :: tail ->
                    match op1, op2 with
                    | op, Add -> (listEval first ((op1, (Term (eval2 e1 + eval2 e2))) :: tail))
                    | _ -> listEval (Term (opEval op1 (eval2 first) (eval2 e1))) (((op2, e2) :: tail))
                | (op1, e1) :: [] -> (opEval op1) (eval2 first) (eval2 e1)
            listEval f l
            
                
        
    let part1 (input: string) =
        let lines = input.Split("\r\n") |> Array.toList
        let y = lines |> List.map (parsec expressions)
        y
        |> List.map eval1
        |> List.sum
        
    let part2 (input: string) =
        let lines = input.Split("\r\n") |> Array.toList
        let y = lines |> List.map (parsec expressions)
        y
        |> List.map eval2
        |> List.sum
