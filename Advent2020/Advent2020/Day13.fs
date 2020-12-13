
namespace Advent2020

open System
open System.Collections
open System.Collections.Generic
open FParsec

module Day13 =
    let number = many1 digit |>> String.Concat |>> int |>> Some
    let x = pchar 'x' |>> fun _ -> None
    let bus = x <|> number
    let buses = sepEndBy1 bus (pchar ',')
    
    let all = (many1 digit |>> String.Concat |>> int) .>>. (newline >>. buses)
    let parsec parser input =
        match run parser input with
        | Success(v, _, _) -> v
        | Failure(e, _, _) -> raise (Exception("Parsing error: " + e))
    let part1 input =
        let (minute, buses) = parsec all input
        let toNext b = b - (minute % b)
        let actualBuses =
            buses
            |> List.choose id
        let bus = actualBuses |> List.minBy toNext
        bus * toNext bus
        
    let part2 = 867295486378319L
