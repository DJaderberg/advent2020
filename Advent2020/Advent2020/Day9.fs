﻿namespace Advent2020

open System
open System.Collections
open System.Collections.Generic
open FParsec

module Day9 =
    let parsec parser input =
        match run parser input with
        | Success(v, _, _) -> v
        | Failure(e, _, _) -> raise (Exception("Parsing error: " + e))
        
    let part1 input = parsec parser input
        
