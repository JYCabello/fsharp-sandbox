module FSharpSandbox.TypeFiddling

type Person = {First:string; Last:string}
type Animal = {First:string; NotLast:string}

type Employee = 
  | Worker of Person
  | Manager of Employee list

let jdoe = {First="John"; Last="Doe"}
let pepe = {First="Pepe"; Last="Ramos"}
let worker: Employee = Worker jdoe

let manager = Manager [Worker jdoe; Worker pepe]
  
let banana (m: Employee list) =
    if m.IsEmpty then Worker {First="An employee with no underlings"; Last=""} else m.Head

let rec getName =
  function
    | Worker w -> w.First
    | Manager m -> m |> banana |> getName

let printPerson = printfn "%s" (getName manager)