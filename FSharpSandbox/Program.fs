let rec mapStackOverflow f = function
    | [] -> []
    | x::xs -> f x::mapStackOverflow f xs

let mapTailRecAcc fn lst =
    let rec loop acc = function
        | []    -> acc |> List.rev
        | x::xs -> loop (fn x :: acc) xs
    loop lst []

let mapCont f l =
    let rec loop cont = function
        | [] -> cont []
        | x::xs -> loop ( fun acc -> cont (f x::acc) ) xs
    loop id l

[<EntryPoint>]
let main argv =
    FSharpSandbox.TypeFiddling.printPerson
    let meh = [1..10_000_000] |> mapTailRecAcc ((+) 1)
    printfn "Lenght is %i" meh.Length
    0