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
    let rec combat q1 q2 =
        match (dequeue q1, dequeue q2) with
        | (Some c1, Some c2) when c1 > c2 -> q1.Enqueue(c1); q1.Enqueue(c2); combat q1 q2
        | (Some c1, Some c2) when c1 < c2 -> q2.Enqueue(c2); q2.Enqueue(c1); combat q1 q2
        | (Some c, None) -> Some (1, q1.Prepend(c))
        | (None, Some c) -> Some (2, q2.Prepend(c))
        | _ -> None
        
    let rec recCombat decksSeen q1 q2 =
        let currentState = (q1 |> Seq.toList, q2 |> Seq.toList)
        if Set.contains currentState decksSeen then
            Some (1, q1 :> IEnumerable<int>)
        else
            let subQueue (q: Queue<'a>) count = Queue(q.Take(count))
            let inline recurse (q: Queue<'a>) cs =
               List.iter q.Enqueue cs
               recCombat (Set.add currentState decksSeen) q1 q2
               
            match (dequeue q1, dequeue q2) with
            | (Some c1, Some c2) when c1 <= q1.Count && c2 <= q2.Count ->
                match recCombat Set.empty (subQueue q1 c1) (subQueue q2 c2) with
                | Some (p, _) -> if p = 1 then recurse q1 [c1;c2] else recurse q2 [c2;c1]
                | _ -> recurse q1 []
            | (Some c1, Some c2) when c1 > c2 -> recurse q1 [c1;c2]
            | (Some c1, Some c2) when c1 < c2 -> recurse q2 [c2;c1]
            | (Some _, Some _) -> recurse q1 []
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
        let makeQ p = Queue(Seq.ofList p)
        let winner = game (makeQ p1) (makeQ p2)
        Option.map (snd >> score) winner
        |> Option.defaultValue 0
        
    let part1 = play combat
    let part2 = play (recCombat Set.empty)
        
