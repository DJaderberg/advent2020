namespace Advent2020

open System
open System.Linq
open FParsec


module Day3 =
    type Location = Tree | Open
    
        
    let locations slope array =
        let (x, y) = (0, 0)
        let (right, down) = slope
        let height = Array2D.length1 array
        seq { for i in 0 .. (height) do (y + down * i, x + right * i) }
        |> Seq.tail
        |> Seq.takeWhile (fun (h, _) -> h < height)
        
    let value (array: Location[,]) location =
        match location with
        | (a, b) -> array.[a, b % Array2D.length2 array]
    
    let tree = pchar '#' |>> (fun _ -> Tree)
    let popen = pchar '.' |>> (fun _ -> Open)
    let location = tree <|> popen
    let parsec str =
        match run (many location) str with
        | Success (result,_,_) -> Some result
        | _ -> raise (Exception("Parsing error on: '" + str + "'"))
        
    let getArray (input: String) =
        input.Split('\n')
        |> Array.choose parsec
        |> Array.toList
        |> array2D
        
    let hits input slope =
        let array = getArray input
        let locs = locations slope array
        let trees =
            locs
            |> Seq.map (value array)
            |> Seq.filter ((=) Tree)
        trees.Count()
        
    let part1 (input: String) = hits input (3, 1)
        
    let p2list (input: String) =
            [(1, 1); (3, 1); (5, 1); (7,1); (1, 2)]
            |> List.map (hits input)
    let part2 (input : String) =
        input |> p2list
        |> List.map int64
        |> List.fold (*) 1L
        
        
