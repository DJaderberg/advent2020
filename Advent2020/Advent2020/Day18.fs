namespace Advent2020

open System
open System.Linq.Expressions
open System.Reflection.Emit
open FParsec

module Day18 =
    let term = many1Chars digit |>> int64
    let operator = (pchar '*' |>> fun _ -> (*)) <|> (pchar '+' |>> fun _ -> (+))
    let expr, exprImpl = createParserForwardedToRef()
    let rec parens =
        pchar '(' >>. spaces >>. expressions .>> spaces .>> pchar ')'
    and rest = many ((spaces >>. operator) .>>. (spaces >>. expr)) |>> List.map (fun (op, v) -> op v)
    and expressions =
        spaces >>. expr .>>. rest
        |>> (fun (i, fs) -> List.fold (fun s f -> f s) i fs)
    
    do exprImpl := choice [parens; term]
    
    let parsec parser input =
        match run parser input with
        | Success(v, _, _) -> v
        | Failure(e, _, _) -> raise (Exception(e))
        
    let part1 (input: string) =
        let lines = input.Split("\r\n") |> Array.toList
        let y = lines |> List.map (parsec expressions)
        y
        |> List.sum
    let part2 input = 42L
