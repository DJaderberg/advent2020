namespace Advent2020

open System
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
        let set =
            match Map.tryFind allergen possibilities with
            | Some existing -> Set.intersect existing newOptions
            | None -> newOptions
        Map.add allergen set possibilities
            
    let filterLine possibilities (ingredients, allergens: string list) =
        List.fold (filterPossibilities ingredients) possibilities allergens
    
    let drawConclusions (conclusions: Map<string, string>) (allergen, ingredients) =
        let known = Set.ofSeq (Map.toSeq conclusions |> Seq.map snd)
        let remaining = Set.difference ingredients known |> Set.toList
        match remaining with
        | [ r ] -> Map.add allergen r conclusions
        | _ -> conclusions
    
    let rec drawConclusionsRec allAllergens (previouslyKnown: Map<string,string>) previousPass =
        let sorted = Map.toList previousPass |> List.sortBy (snd >> Set.count)
        let iteration = List.fold drawConclusions Map.empty sorted
        let total = List.fold (fun s (k,v) -> Map.add k v s) previouslyKnown (Map.toList iteration)
        let knownAllergens = Map.toList total |> List.map fst |> Set.ofList
        if knownAllergens = allAllergens then
            total
        else
            let known = Map.toList total |> List.map snd |> Set.ofList
            let updatedMap = Map.map (fun _ v -> Set.difference v known) previousPass
            drawConclusionsRec allAllergens total updatedMap
    let flip f x y = f y x 
    let countSafe withAllergens all =
        List.filter ((flip Set.contains) withAllergens >> not) all
        |> List.length
        
    let solve parsed =
        let firstPass = List.fold filterLine Map.empty parsed
        let allAllergens = List.map snd parsed |> List.concat |> Set.ofList
        drawConclusionsRec allAllergens Map.empty firstPass
        
    let part1 (input: string) =
        let parsed = parsec lines input
        let solution = solve parsed
        let withAllergens = solution |> Map.toList |> List.map snd |> Set.ofList
        let ingredientOccurrences = parsed |> List.map fst |> List.concat
        countSafe withAllergens ingredientOccurrences
    let part2 (input: string) =
        let parsed = parsec lines input
        let solution = solve parsed
        let ingredientsWithAllergens = solution |> Map.toList |> List.sortBy fst |> List.map snd
        (List.fold (fun s e -> s + "," + e) "" ingredientsWithAllergens).Trim(',')
        
