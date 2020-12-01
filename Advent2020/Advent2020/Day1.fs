namespace Advent2020

open System
module Day1 =
    let findMatch sum sequence e =
        let sumIs sum s =
            if e + s = sum then Some (e, s) else None
        Seq.choose (sumIs sum) sequence |> Seq.tryHead
        
    let findMatch3 sum sequence e =
        Seq.choose (findMatch (sum - e) sequence) sequence
        |> Seq.tryHead
        |> Option.map (fun (x, y) -> (e, x, y))
        
        
        
    let part1 (numbers: string) =
        let sequence = numbers.Split('\n') |> Array.map (int) |> Array.toSeq
        let found = Seq.choose (findMatch 2020 sequence) sequence |> Seq.tryHead
        match found with
            | Some ( a, b ) -> a * b
            | _ -> 0
            
    let part2 (numbers: string) =
        let sequence = numbers.Split('\n') |> Array.map (int) |> Array.toSeq
        let found = Seq.choose (findMatch3 2020 sequence) sequence |> Seq.tryHead
        match found with
            | Some ( a, b, c ) -> a * b * c
            | _ -> 0
        
