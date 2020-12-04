namespace Advent2020

open System
open System.Collections.Generic
open FParsec

module Day4 =
    let key = many letter |>> String.Concat
    let value = many (letter <|> digit <|> pchar '#') |>> String.Concat
    let kvp = (key .>> pchar ':' .>>. value) .>> optional (pchar ' ' <|> pchar '\n')
    let passport = (many1 (kvp)) .>> spaces |>> dict
    let parsec input =
        match run (many passport) input with
        | Success(v, _, _) -> v
        | Failure(e, _, _) -> raise (Exception("Parsing error: " + e))
        
    let valid (d: IDictionary<string, string>) =
        ["byr"; "iyr"; "eyr"; "hgt"; "hcl"; "ecl"; "pid"]
        |> List.map d.ContainsKey
        |> List.fold (&&) true
    let part1 input =
        parsec input
        |> List.filter valid
        |> List.length

