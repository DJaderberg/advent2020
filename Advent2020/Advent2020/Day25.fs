namespace Advent2020

open System
open FParsec

module Day25 =
    let parsec parser input =
        match run parser input with
        | Success(v, _, _) -> v
        | Failure(e, _, _) -> raise (Exception(e))
        
    let pks = sepEndBy1 (many1Chars digit |>> int64) newline

    let singleTransform subject i = (i * subject) % 20201227L
    let rec transform subject numberSoFar iters =
        match iters with
        | 0 -> numberSoFar
        | _ -> transform subject (singleTransform subject numberSoFar) (iters - 1)
    let rec findLoopSize (pk: int64) subjectNumber numberSoFar state =
        let number = singleTransform subjectNumber numberSoFar
        let loops = state + 1
        if number = pk then
            loops
        else
            findLoopSize pk subjectNumber number loops
            
    let getEncryptionKey pk loopSize = transform pk 1L loopSize
    let part1 input =
        let card :: door :: [] = parsec pks input
        let cardLoopSize = findLoopSize card 7L 1L 0
        getEncryptionKey door cardLoopSize
    let part2 input = 42
