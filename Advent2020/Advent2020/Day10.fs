namespace Advent2020

open System
open System.Collections
open System.Collections.Generic
open System.Diagnostics.CodeAnalysis
open FParsec

module Day10 =
    let parsec parser input =
        match run parser input with
        | Success(v, _, _) -> v
        | Failure(e, _, _) -> raise (Exception("Parsing error: " + e))
    let part1 input = parsec (pstring "42" |>> int) input
        
    let part2 input = parsec (pstring "42" |>> int) input
