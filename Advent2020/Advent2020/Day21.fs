namespace Advent2020

open System
open System.Linq.Expressions
open System.Reflection.Emit
open FParsec

module Day21 =
    let line =
        sepEndBy1 (many1Chars letter) spaces
        .>> pstring "(contains "
        .>>.sepEndBy1 (many1Chars letter) (pstring ", ")
        .>> pstring ")"
    let lines = sepEndBy1 line newline
    let parsec parser input =
        match run parser input with
        | Success(v, _, _) -> v
        | Failure(e, _, _) -> raise (Exception(e))
    
    let filterPossibilities ingredients (possibilities: Map<string,Set<string>>) allergen =
        let newOptions = Set.ofList ingredients
        let set = Option.defaultValue newOptions (Map.tryFind allergen possibilities)
        Map.add allergen (Set.intersect newOptions set) possibilities
            
    let filterLine possibilities (ingredients, allergens: string list) =
        List.fold (filterPossibilities ingredients) possibilities allergens
    
    let drawConclusions (conclusions: Map<string, string>) (allergen, ingredients) =
        let known = Set.ofSeq (Map.toSeq conclusions |> Seq.map snd)
        let remaining = Set.difference ingredients known
        if Set.count remaining = 1 then
            Map.add allergen (Set.toList remaining |> List.head) conclusions
        else
            conclusions
    
    let rec drawConclusionsRec allAllergens (previouslyKnown: Map<string,string>) previousPass =
        let sorted = Map.toList previousPass |> List.sortBy (snd >> Set.count)
        let iteration = List.fold drawConclusions Map.empty sorted
        let total = List.fold (fun s (k,v) -> Map.add k v s) previouslyKnown (Map.toList iteration)
        let knownAllergens = Map.toList total |> List.map fst |> Set.ofList
        if knownAllergens = allAllergens then
            total
        else
            let known = Map.toList total |> List.map snd |> Set.ofList
            let updatedMap = Map.map (fun k v -> Set.difference v known) previousPass
            drawConclusionsRec allAllergens total updatedMap
            
    let countSafeOccurences ingredientsWithAllergens ingredients =
        List.filter (fun e -> Set.contains e ingredientsWithAllergens |> not) ingredients
        |> List.length
        
    let part1 (input: string) =
        let parsed = parsec lines input
        let firstPass = List.fold filterLine Map.empty parsed
        let allAllergens = List.map snd parsed |> List.concat |> Set.ofList
        let solution = drawConclusionsRec allAllergens Map.empty firstPass
        let ingredientsWithAllergens = solution |> Map.toList |> List.map snd |> Set.ofList
        let ingredientOccurrences = parsed |> List.map fst |> List.concat
        countSafeOccurences
            ingredientsWithAllergens
            ingredientOccurrences
    let part2 (input: string) =
        let parsed = parsec lines input
        let firstPass = List.fold filterLine Map.empty parsed
        let allAllergens = List.map snd parsed |> List.concat |> Set.ofList
        let solution = drawConclusionsRec allAllergens Map.empty firstPass
        let ingredientsWithAllergens = solution |> Map.toList |> List.sortBy fst |> List.map snd
        let result = (List.fold (fun s e -> s + "," + e) "" ingredientsWithAllergens).Trim(',')
        result
        
