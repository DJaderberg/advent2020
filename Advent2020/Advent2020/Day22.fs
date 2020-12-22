namespace Advent2020

open System
open System.Linq
open System.Collections.Generic
open FParsec

module Day22 =
    let card = many1Chars digit |>> int
    let cards = sepEndBy1 card newline
    let playerId = pstring "Player " >>. anyChar .>> pchar ':' .>> newline
    let player = playerId >>. cards
    let players = player .>> newline .>>. player
    let parsec parser input =
        match run parser input with
        | Success(v, _, _) -> v
        | Failure(e, _, _) -> raise (Exception(e))
    
    let dequeue (q: Queue<'a>) =
        let found, value = q.TryDequeue()
        if found then Some value else None
    let rec combat (q1: Queue<int>) (q2: Queue<int>) =
        match (dequeue q1, dequeue q2) with
        | (Some c1, Some c2) when c1 > c2 -> q1.Enqueue(c1); q1.Enqueue(c2); combat q1 q2
        | (Some c1, Some c2) when c1 < c2 -> q2.Enqueue(c2); q2.Enqueue(c1); combat q1 q2
        | (Some c, None) -> Some (1, q1.Prepend(c))
        | (None, Some c) -> Some (2, q2.Prepend(c))
        | _ -> None
        
    let rec recCombat (decksSeen: Set<int list * int list>) (q1: Queue<int>) (q2: Queue<int>) =
        let currentState = q1 |> Seq.toList, q2 |> Seq.toList
        if Set.contains currentState decksSeen then
            Some (1, q1 :> IEnumerable<int>)
        else
            match (dequeue q1, dequeue q2) with
            | (Some c1, Some c2) ->
                let newDecks = Set.add currentState decksSeen
                let inline recurse player =
                   let (q, a, b) = if player = 1 then (q1, c1, c2) else (q2, c2, c1)
                   q.Enqueue(a); q.Enqueue(b); recCombat newDecks q1 q2 
                if c1 <= q1.Count && c2 <= q2.Count then
                    let subQ1 = Queue(q1.Take(c1))
                    let subQ2 = Queue(q2.Take(c2))
                    match recCombat Set.empty subQ1 subQ2 with
                    | Some (p, _) -> recurse p
                    | _ -> recCombat newDecks q1 q2
                else if c1 > c2 then recurse 1
                else if c2 > c1 then recurse 2
                else recCombat newDecks q1 q2
            | (Some c, None) -> Some (1, q1.Prepend(c))
            | (None, Some c) -> Some (2, q2.Prepend(c))
            | (None, None) -> None
    
    let score cards =
        cards
        |> Seq.rev
        |> Seq.mapi (fun i e -> (i + 1) * e)
        |> Seq.sum
        
    let play game input =
        let (p1, p2) = parsec players input
        let makeQ p = Queue<int>(Seq.ofList p)
        let winner = game (makeQ p1) (makeQ p2)
        Option.map (snd >> score) winner
        |> Option.defaultValue 0
        
    let part1 = play combat
    let part2 = play (recCombat Set.empty)
        
