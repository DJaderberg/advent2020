namespace Advent2020

open System
open System.Linq
open System.Collections.Generic
open FParsec

module Passport =
    type BirthYear = BirthYear of int
    type IssueYear = IssueYear of int
    type ExpirationYear = ExpirationYear of int
    type LengthUnit = Cm | Inch
    type Height = Height of int * LengthUnit
    type HairColor = HairColor of string
    type EyeColor = Amber | Blue | Brown | Gray | Green | Hazel | Other
    type PassportId = PassportId of string
    type CountryId = CountryId of string
    type Passport = {
        birthYear: BirthYear
        issueYear: IssueYear
        expirationYear: ExpirationYear
        height: Height
        hairColor: HairColor
        eyeColor: EyeColor
        passportId: PassportId
        countryId: CountryId option
    }
        
    let tryInt str =
        try
            int str |> Some
        with :? FormatException -> None
    
    let birthYear i = if i >= 1920 && i <= 2002 then Some (BirthYear i) else None
    let issueYear i = if i >= 2010 && i <= 2020 then Some (IssueYear i) else None
    let expirationYear i = if i >= 2020 && i <= 2030 then Some (ExpirationYear i) else None
    let height (str: String) =
        if str.EndsWith("cm")
        then
            let v = (str.TrimEnd('c', 'm')) |> tryInt
            v |> Option.bind (fun v -> if v >= 150 && v <= 193 then Some (Height (v, Cm)) else None)
        else if str.EndsWith("in")
        then
            let v = (str.TrimEnd('i', 'n')) |> tryInt
            v |> Option.bind (fun v -> if v >= 59 && v <= 76 then Some (Height (v, Inch)) else None)
        else
            None
            
    let hairColor (s: String) =
        let hex c =
            Seq.append (seq { '0' .. '9' }) (seq {'a' .. 'f' })
            |> Set.ofSeq
            |> Set.contains c
        if s.StartsWith('#') && s.Length = 7 && (s |> Seq.toList |> Seq.filter hex).Count() = 6
        then
            HairColor s.[1..] |> Some
        else
            None
            
    let eyeColor (s: String) =
        match s with
        | "amb" -> Some Amber
        | "blu" -> Some Blue
        | "brn" -> Some Brown
        | "gry" -> Some Gray
        | "grn" -> Some Green
        | "hzl" -> Some Hazel
        | "oth" -> Some Other
        | _ -> None
        
    let passportId (s: String) =
        if s.Length = 9
        then
            tryInt s |> Option.map (fun _ -> s)
        else
            None
        |> Option.map PassportId
        
    let countryId _ =
        None

module Day4 =
    open Passport
    type MaybeBuilder() =
        member this.Bind(x, f) = 
            match x with
            | None -> None
            | Some a -> f a
        member this.Return(x) = 
            Some x
    let maybe = MaybeBuilder()

    let key = many1 letter |>> String.Concat
    let value = many1 (letter <|> digit <|> pchar '#') |>> String.Concat
    let kvp = (key .>> pchar ':' .>>. value) .>> optional (pchar ' ' <|> pchar '\n')
    let basicPassport = many kvp |>> dict
    let get (dict: IDictionary<'a,'a>) key =
        let found, value = dict.TryGetValue key
        if found then Some value else None
    let passport (dict: IDictionary<string,string>) =
        maybe
            {
            let! birthYear = get dict "byr" |> Option.bind tryInt |> Option.bind birthYear
            let! issueYear = get dict "iyr" |> Option.bind tryInt |> Option.bind issueYear
            let! expirationYear = get dict "eyr" |> Option.bind tryInt |> Option.bind expirationYear
            let! height = get dict "hgt" |> Option.bind height
            let! hairColor = get dict "hcl" |> Option.bind hairColor
            let! eyeColor = get dict "ecl" |> Option.bind eyeColor
            let! passportId = get dict "pid" |> Option.bind passportId
            return
                {
                 birthYear = birthYear
                 issueYear = issueYear
                 expirationYear = expirationYear
                 height = height
                 hairColor = hairColor
                 eyeColor = eyeColor
                 passportId = passportId
                 countryId = get dict "cid" |> Option.bind countryId
                }
            }
    let ppassport = basicPassport |>> passport
    let passportList = sepEndBy ppassport spaces1
    let passportInfoList = sepEndBy basicPassport spaces1
    let parsec1 input =
        match run passportInfoList input with
        | Success(v, _, _) -> v
        | Failure(e, _, _) -> raise (Exception("Parsing error: " + e))
        
    let parsec2 input =
        match run passportList input with
        | Success(v, _, _) -> v
        | Failure(e, _, _) -> raise (Exception("Parsing error: " + e))
        
    let single input = run ppassport input
        
    let valid (d: IDictionary<string, string>) =
        ["byr"; "iyr"; "eyr"; "hgt"; "hcl"; "ecl"; "pid"]
        |> List.map d.ContainsKey
        |> List.fold (&&) true
    let part1 input =
        parsec1 input
        |> List.filter valid
        |> List.length
    
    let part2 input =
        parsec2 input
        |> List.choose id
        |> List.length

