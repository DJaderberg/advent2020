namespace Advent2020

open System
open System.Linq.Expressions
open System.Reflection.Emit
open FParsec

module Day20 =
    type Edge = Above | Below | Left | Right
    
    let idLine = pstring "Tile " >>. many1Chars digit |>> int .>> pchar ':' .>> newline
    let arrayLine = many1 (anyOf ['#';'.'])
    let array = sepEndBy1 arrayLine newline |>> array2D
    let tile = idLine .>>. array
    let tiles = sepEndBy1 tile newline |>> Map.ofList
    let parsec parser input =
        match run parser input with
        | Success(v, _, _) -> v
        | Failure(e, _, _) -> raise (Exception(e))
        
        
    let opposite =
        function
            | Above -> Below
            | Below -> Above
            | Left -> Right
            | Right -> Left
        
    let getEdge edge (tile: char[,]) =
        match edge with
        | Above -> tile.[0,*]
        | Below -> tile.[9,*]
        | Left -> tile.[*,0]
        | Right -> tile.[*,9]
        
    let rotations size (tile: char[,]) =
        let maxIndex = size - 1
        let a = Array2D.zeroCreate size size |> Array2D.mapi (fun x y _ -> tile.[y,maxIndex - x])
        let b = Array2D.zeroCreate size size |> Array2D.mapi (fun x y _ -> tile.[maxIndex - x,maxIndex - y])
        let c = Array2D.zeroCreate size size |> Array2D.mapi (fun x y _ -> tile.[maxIndex - y,x])
        [tile;a;b;c]
    let variants size (tile: char[,]) =
        let transposed = Array2D.zeroCreate size size |> Array2D.mapi (fun x y _ -> tile.[y,x])
        [rotations size tile; rotations size transposed] |> List.concat
    let fulfills tile ((e,arr): Edge * char[]) = arr = getEdge e tile
    let findMatchSingle (requirements: (Edge * char[]) list) (tile: char[,]) =
        let vs = variants 10 tile
        let out = List.filter (fun v -> List.forall (fulfills v) requirements) vs
        out
        
    let findOptions (currentSolution: Map<int * int,int * char[,]>) (options: Map<int, char[,]>) (x,y) =
        let requirements =
            [(x-1,y,Left);(x+1,y,Right);(x,y-1,Above);(x,y+1,Below)]
            |> List.choose (fun (x,y,e) -> Map.tryFind (x,y) currentSolution |> Option.map (fun (_,t) -> (e, getEdge (opposite e) t)))
        let matches = List.map (fun (i, t) -> (i, findMatchSingle requirements t)) (Map.toList options)
        matches 
        |> List.map (fun (i, cArrays) -> List.map (fun c -> (i,c)) cArrays)
        |> List.concat
    
    let findPositionOptions solvedPositions =
        let neighbors (x,y) = [(x,y-1);(x+1,y);(x-1,y);(x,y+1)]
        let neighborsSolved (x,y) =
            neighbors (x,y)
            |> List.filter (fun pos -> Map.containsKey pos solvedPositions)
            |> List.length
        if Map.isEmpty solvedPositions then [(0,0)] else
            Map.toList solvedPositions
            |> List.map fst
            |> List.map neighbors |> List.concat
            |> List.filter (fun pos -> None = Map.tryFind pos solvedPositions)
            |> List.sortByDescending neighborsSolved
    
    let rec solve (solved: Map<int * int, int * char[,]>) (remaining: Map<int, char[,]>) =
        if Map.isEmpty remaining then Seq.singleton solved else
            let ps = findPositionOptions solved
            let options = ps |> List.map (fun pos -> findOptions solved remaining pos |> List.map (fun e -> (pos, e))) |> List.concat
            let addToSolved p e = Map.add p e solved
            let removeFromRemaining (i, _) = Map.remove i remaining
            Seq.map (fun (pos, e) -> solve (addToSolved pos e) (removeFromRemaining e)) options
            |> Seq.concat
    
    let corners (locations: (int * int) list) =
        let xMin = locations |> List.map fst |> List.min
        let xMax = locations |> List.map fst |> List.max
        let yMin = locations |> List.map snd |> List.min
        let yMax = locations |> List.map snd |> List.max
        [(xMin,yMin);(xMin,yMax);(xMax,yMin);(xMax,yMax)]
    let part1 (input: string) =
        let tiles = parsec tiles input
        let solved = solve Map.empty tiles |> Seq.head
        corners (Map.toList solved |> List.map fst)
        |> List.map (fun c -> Map.find c solved)
        |> List.map (fst >> int64)
        |> List.fold (*) 1L
        
    let lineFromTile yIndex (tile: char[,]) = tile.[yIndex, 1..8]
    let linesFromTiles (list: char[,] list) =
        let line i =
            let stuff = List.map (lineFromTile i) list
            stuff |> Array.concat
        [1..8] |> List.map line
        
    let constructLargeArray (solved: Map<(int * int), int * char[,]>) =
        let groupByYSortByX =
            Map.toList solved
            |> List.groupBy (fst >> snd)
            |> List.sortBy fst
            |> List.map snd
            |> List.map (List.sortBy (fst >> fst))
        let preLines =
            groupByYSortByX
            |> List.map (List.map snd)
        let lines =
            preLines
            |> List.map (List.map snd)
            |> List.map linesFromTiles
        lines
        |> List.concat
        |> array2D
        
    let findSeaMonsters (array: char[,]) =
        let top = [18]
        let center = [0;5;6;11;12;17;18;19]
        let bottom = [1;4;7;10;13;16]
        let monsterAtIndex (x,y) =
            let resultC = List.forall (fun i -> array.[x + i, y] = '#') center
            let resultT = List.forall (fun i -> array.[x + i, y - 1] = '#') top
            let resultB = List.forall (fun i -> array.[x + i, y + 1] = '#') bottom
            resultC && resultT && resultB
        let xs = [ 0 .. Array2D.length1 array - 20 ]
        let ys = [ 1 .. Array2D.length2 array - 2 ]
        Seq.allPairs xs ys
        |> Seq.filter monsterAtIndex
        |> List.ofSeq
        
    let part2 (input: string) =
        let tiles = parsec tiles input
        let solved = solve Map.empty tiles |> Seq.head
        let big = constructLargeArray solved
        let variants = variants (Array2D.length1 big) big
        let zipped = List.zip (List.map findSeaMonsters variants) variants
        let correctVariant = List.maxBy (fst >> List.length) zipped
        let numSeaMonsters = fst correctVariant |> List.length
        let mutable hashes = 0
        Array2D.iter (fun v -> if v = '#' then hashes <- hashes + 1 else ()) big
        hashes - 15 * numSeaMonsters
