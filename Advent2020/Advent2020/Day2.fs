namespace Advent2020

open System
open System.Text.RegularExpressions
open FParsec

module Day2 =
    type Range = Range of int * int
    
    type Line = Line of Range * char * char list
    
    let line r c s = Line (r, c, s)
    let range l u = Range (l, u)
    let rangeContains r i =
        match r with
            Range (l, u) -> l <= i && i <= u 
    let isMatch l =
        match l with
        | Line (r, c, s) -> 
            let numHits = s |> Seq.toList |> Seq.filter c.Equals |> Seq.length
            rangeContains r numHits
            
    let isMatchPart2 l =
        let exactlyOneIs a b char (cs: char list) =
            char.Equals(cs.[a]) <> char.Equals(cs.[b]) // <> is the same as XOR, so use it
        match l with
        | Line (r, c, s) ->
            match r with
                | Range (a, b) -> exactlyOneIs (a-1) (b-1) c s
        
    let parsec str =
        let r = pipe2 (pint32 .>> pstring "-")  pint32 range
        let c = pchar ' ' >>. letter .>> pstring ": "
        let total = pipe3 r c (many letter) line
        match run total str with
        | Success (result, _, _) -> Some result
        | _ -> raise (Exception("Parsing error: '" + str + "'"))
    let part1 (input: string) =
        input.Split('\n')
        |> Array.choose parsec
        |> Array.filter isMatch
        |>Array.length
        
    let part2 (input: string) =
        input.Split('\n')
        |> Array.choose parsec
        |> Array.filter isMatchPart2
        |>Array.length
