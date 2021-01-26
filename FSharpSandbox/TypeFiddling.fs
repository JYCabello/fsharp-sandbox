module FSharpSandbox.TypeFiddling

// simple types in one line
type Person = {First:string; Last:string}
type Animal = {First:string; NotLast:string}

// complex types in a few lines
type Employee = 
  | Worker of Person
  | Manager of Employee list

// type inference
let jdoe = {First="John"; Last="Doe"}
let pepe = {First="Pepe"; Last="Ramos"}
let worker: Employee = Worker jdoe

let manager = Manager [Worker jdoe; Worker pepe]

let rec getName = function
  | Worker w -> w.First
  | Manager m -> if m.IsEmpty then "A manager with no underlings" else m.Head |> getName
let result = manager |> getName

let printPerson = printfn "%s" result