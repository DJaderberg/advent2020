namespace Advent2020

open System
open System.Collections
open System.Collections.Generic
open FParsec

module Day7 =
    type Tree = Node of string * Tree | Leaf of string
    let color =
        ((many letter .>> spaces1 |>> String.Concat) .>>. (many letter .>> spaces1 |>> String.Concat)
        |>> fun (x, y) -> x + y)
        .>> (pstring "bags" <|> pstring "bag")
    let bag = (many1 digit |>> String.Concat |>> int) .>> spaces1 .>>. color
    let flip (x, y) = (y, x)
    let contains = sepEndBy1 bag (pchar ',' >>. spaces1) |>> List.map flip |>> dict
    let none = pstring "no other bags" |>> (fun _ -> dict [])
    let rule = (color .>> (pstring " contain ")) .>>. (none <|> contains) .>> pchar '.'
    let rules = sepEndBy1 rule newline |>> dict
    let parsec parser input =
        match run parser input with
        | Success(v, _, _) -> v
        | Failure(e, _, _) -> raise (Exception("Parsing error: " + e))
        
    let rec traverseGraph (graphElements: IDictionary<string, seq<string>>) matches queue =
        match List.tryHead queue with
        | Some element ->
            if graphElements.ContainsKey element then
                let newMatches = (graphElements.Item element |> Set.ofSeq) - Set.union matches (Set.ofList queue)
                let newQueue = List.append (List.tail queue) (Set.toList newMatches)
                traverseGraph graphElements (Set.union matches newMatches) newQueue
            else
                traverseGraph graphElements (Set.add element matches) (List.tail queue)
        | None -> matches
        
    let matchSet input =
        let parsed = parsec rules input
        let containsMap = parsed |> Seq.map (fun kvp -> (kvp.Key, kvp.Value.Keys |> Seq.cast<string>))
        let inverted =
            containsMap
            |> Seq.map (fun (v, sequence) -> Seq.map (fun e -> (v, e)) sequence)
            |> Seq.concat
            |> Seq.map flip
            |> Seq.groupBy fst
            |> Seq.map (fun (k, sequenceTup) -> (k, Seq.map snd sequenceTup))
            |> dict
        traverseGraph inverted Set.empty (List.singleton "shinygold")
    let part1 input = matchSet input |> Set.count
        
    let part2 input = raise (NotImplementedException())
