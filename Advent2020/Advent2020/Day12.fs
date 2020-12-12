namespace Advent2020

open System
open System.Collections
open System.Collections.Generic
open FParsec

module Day12 =
    type Instruction =
        | Forward of int
        | North of int
        | West of int
        | East of int
        | South of int
        | Left of int
        | Right of int
    let direction c =
        match c with
        | 'F' -> Forward
        | 'N' -> North
        | 'E' -> East
        | 'W' -> West
        | 'S' -> South
        | 'L' -> Left
        | 'R' -> Right
    
    let instr = (anyChar |>> direction) .>>. (many1 digit |>> String.Concat |>> int) |>> (fun (f, v) -> f v)
    let all = sepEndBy1 instr newline
    let parsec parser input =
        match run parser input with
        | Success(v, _, _) -> v
        | Failure(e, _, _) -> raise (Exception("Parsing error: " + e))
    
    let degRange i =
        let x = i % 360
        if x < 0
        then x + 360
        else x
        
    let add (x, y) (a, b) = (x + a, y + b)
    let rec move (facing, location) step =
        match step with
        | Forward i -> 
            match facing with
                | 0 -> move (facing, location) (North i)
                | 90 -> move (facing, location) (East i)
                | 180 -> move (facing, location) (South i)
                | 270 -> move (facing, location) (West i)
        | North i -> (facing, add location (0, i))
        | East i -> (facing, add location (-i, 0))
        | West i -> (facing, add location (i, 0))
        | South i -> (facing, add location (0, -i))
        | Left i -> (degRange (facing - i), location)
        | Right i -> (degRange (facing + i), location)
        
    let moveForward location (wx, wy) n =
        let newLocation = add location (n * wx, n * wy)
        newLocation
        
    let rec rotate (wx, wy) deg =
        match deg with
        | 0 -> (wx, wy)
        | _ -> rotate (-wy, wx) (deg - 90)
        
        
    let rec move2 (location, waypoint) step =
        match step with
        | Forward i -> (moveForward location waypoint i, waypoint)
        | North i -> (location, add waypoint (0, i))
        | East i -> (location, add waypoint (-i, 0))
        | West i -> (location, add waypoint (i, 0))
        | South i -> (location, add waypoint (0, -i))
        | Left i -> (location, rotate waypoint (degRange -i))
        | Right i -> (location, rotate waypoint (degRange i))
        
    let part1 input =
        let instructions = parsec all input
        let (_, (x, y)) = List.fold move (90, (0,0)) instructions
        abs x + abs y
        
    let part2 input =
        let instructions = parsec all input
        let ((x, y), _) = List.fold move2 ((0,0), (-10, 1)) instructions
        abs x + abs y
        
