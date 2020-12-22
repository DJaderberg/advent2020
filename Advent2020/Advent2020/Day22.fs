namespace Advent2020

open System
open System.Linq
open System.Collections.Generic
open FParsec

module Day22 =
    let card = many1Chars digit |>> int
    let cards = sepEndBy1 card newline
    let playerId = pstring "Player " >>.anyChar .>> pchar ':' .>> newline
    let player = playerId .>>. cards
    let players = player .>> newline .>>. player
    let parsec parser input =
        match run parser input with
        | Success(v, _, _) -> v
        | Failure(e, _, _) -> raise (Exception(e))
    
    let dequeue (q: Queue<'a>) =
        let found, value = q.TryDequeue()
        if found then Some value else None
    let rec play (q1: Queue<int>) (q2: Queue<int>) =
        match (dequeue q1, dequeue q2) with
        | (Some c1, Some c2) ->
            if c1 > c2 then q1.Enqueue(c1); q1.Enqueue(c2); play q1 q2
            else if c2 > c1 then q2.Enqueue(c2); q2.Enqueue(c1); play q1 q2
            else play q1 q2
        | (Some c, None) -> q1.Prepend(c)
        | (None, Some c) -> q2.Prepend(c)
        | (None, None) -> Seq.empty
    
    let score cards =
        cards
        |> Seq.rev
        |> Seq.mapi (fun i e -> (i + 1) * e)
        |> Seq.sum
    let part1 (input: string) =
        let ((_, p1), (_,p2)) = parsec players input
        let q1 = Queue<int>(Seq.ofList p1)
        let q2 = Queue<int>(Seq.ofList p2)
        let winningDeck = play q1 q2
        score winningDeck
    let part2 (input: string) = 42
        
