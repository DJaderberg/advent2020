namespace Advent2020

open System
open System.Collections.Generic
open FParsec

module Day4 =
    let key = many1 letter |>> String.Concat
    let value = many1 (letter <|> digit <|> pchar '#') |>> String.Concat
    let kvp = (key .>> pchar ':' .>>. value) .>> optional (pchar ' ' <|> pchar '\n')
    let passport = many kvp |>> dict
    let passportList = sepEndBy passport spaces1
    let parsec input =
        match run passportList input with
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

