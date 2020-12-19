namespace Advent2020

open System
open System.Linq.Expressions
open System.Reflection.Emit
open FParsec

module Day20 =
    let parsec parser input =
        match run parser input with
        | Success(v, _, _) -> v
        | Failure(e, _, _) -> raise (Exception(e))
        
    let part1 (input: string) = 42
        
    let part2 (input: string) = 42
