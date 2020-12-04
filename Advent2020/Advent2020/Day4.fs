namespace Advent2020

open System
open System.Linq
open System.Collections.Generic
open FParsec

module Day4 =
    type MaybeBuilder() =
        member this.Bind(x, f) = 
            match x with
            | None -> None
            | Some a -> f a
        member this.Return(x) = 
            Some x
    let maybe = MaybeBuilder()


    type BirthYear = BirthYear of int
    type IssueYear = IssueYear of int
    type ExpirationYear = ExpirationYear of int
    type LengthUnit = Cm | Inch
    type Height = Height of int * LengthUnit
    type HairColor = HairColor of string
    type EyeColor = Amber | Blue | Brown | Gray | Green | Hazel | Other
    type PassportId = PassportId of int
    type CountryId = CountryId of int
    type Info =
        | BirthYearInfo of BirthYear
        | IssueYearInfo of IssueYear
        | ExpirationYearInfo of ExpirationYear
        | HeightInfo of Height
        | HairColorInfo of HairColor
        | EyeColorInfo of EyeColor
        | PassportIdInfo of PassportId
        | CountryIdInfo of CountryId
        
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
    
    let birthYear i = if i >= 1920 && i <= 2002 then Some (BirthYearInfo (BirthYear i)) else None
    let issueYear i = if i >= 2010 && i <= 2020 then Some (IssueYearInfo (IssueYear i)) else None
    let expirationYear i = if i >= 2020 && i <= 2030 then Some (ExpirationYearInfo (ExpirationYear i)) else None
    let height (str: String): Info option =
        if str.EndsWith("cm")
        then
            let v = (str.TrimEnd('c', 'm')) |> tryInt
            v |> Option.bind (fun v -> if v >= 150 && v <= 193 then Some (HeightInfo (Height (v, Cm))) else None)
        else if str.EndsWith("in")
        then
            let v = (str.TrimEnd('i', 'n')) |> tryInt
            v |> Option.bind (fun v -> if v >= 59 && v <= 76 then Some (HeightInfo (Height (v, Inch))) else None)
        else
            None
            
    let hairColor (s: String) =
        let hex c =
            ['0'; '1'; '2'; '3'; '4'; '5'; '6'; '7'; '8'; '9'; 'a'; 'b'; 'c'; 'd'; 'e'; 'f']
            |> Set.ofList
            |> Set.contains c
        if s.StartsWith('#') && s.Length = 7 && (s |> Seq.toList |> Seq.filter hex).Count() = 6
        then
            HairColor s.[1..] |> HairColorInfo |> Some
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
        |> Option.map EyeColorInfo
        
    let passportId (s: String) =
        if s.Length = 9
        then
            tryInt s
        else
            None
        |> Option.map (PassportId >> PassportIdInfo)
        
    let countryId (s: String) =
        None
    let info (kvp: string * string) =
        match kvp with
        | ("byr", v) -> tryInt v |> Option.bind birthYear
        | ("iyr", v) -> tryInt v |> Option.bind issueYear
        | ("eyr", v) -> tryInt v |> Option.bind expirationYear
        | ("hgt", v) -> height v
        | ("hcl", v) -> hairColor v
        | ("ecl", v) -> eyeColor v
        | ("pid", v) -> passportId v
        | ("cid", v) -> countryId v
        | _ -> None
    let key = many1 letter |>> String.Concat
    let value = many1 (letter <|> digit <|> pchar '#') |>> String.Concat
    let kvp = (key .>> pchar ':' .>>. value) .>> optional (pchar ' ' <|> pchar '\n')
    let pinfo = kvp |>> info
    let infos = many pinfo |>> List.choose id
    
    let basicPassport = many kvp |>> dict
    let passport (infos: Info list) =
        maybe
            {
            let! birthYear = List.choose (fun i -> match i with BirthYearInfo e -> Some e | _ -> None) infos |> List.tryHead
            let! issueYear = List.choose (fun i -> match i with IssueYearInfo e -> Some e | _ -> None) infos |> List.tryHead
            let! expirationYear = List.choose (fun i -> match i with ExpirationYearInfo e -> Some e | _ -> None) infos |> List.tryHead
            let! height = List.choose (fun i -> match i with HeightInfo e -> Some e | _ -> None) infos |> List.tryHead
            let! hairColor = List.choose (fun i -> match i with HairColorInfo e -> Some e | _ -> None) infos |> List.tryHead
            let! eyeColor = List.choose (fun i -> match i with EyeColorInfo e -> Some e | _ -> None) infos |> List.tryHead
            let! passportId = List.choose (fun i -> match i with PassportIdInfo e -> Some e | _ -> None) infos |> List.tryHead
            let countryId = List.choose (fun i -> match i with CountryIdInfo e -> Some e | _ -> None) infos |> List.tryHead
            return
                {
                birthYear = birthYear;
                issueYear = issueYear;
                expirationYear = expirationYear;
                height = height;
                hairColor = hairColor;
                eyeColor = eyeColor;
                passportId = passportId;
                countryId = countryId;
                }
            }
    let ppassport = infos |>> passport
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

