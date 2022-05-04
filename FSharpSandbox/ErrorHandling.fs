module ErrorHandling
open System
open FsToolkit.ErrorHandling

type AuthenticationError =
  { ResourceName: string
    UserId: int option }

type MyError =
  | AuthenticationError of AuthenticationError
  | EntityNotFound of string
  | InternalError of Exception
  
type Doc =
  { Id: int
    Content: string }
  
type User =
  { Id: int
    Email: string }
  
type ActionResult =
  | Success of string
  | Failure of string

let mapResult =
  function
  | Ok ok -> Success $"{ok}"
  | Error error ->
    match error with
    | AuthenticationError ae ->
      match ae.UserId with
      | None -> Failure $"An anonymous user tried to acccess the resource %s{ae.ResourceName}"
      | Some uid -> Failure $"User %i{uid} tried to access the resource %s{ae.ResourceName}"
    | EntityNotFound enf -> Failure $"Could not find entity of type %s{enf}"
    | InternalError ex -> Failure $"Failed with %s{ex.Message}"
    

let getUser userId =
  match userId % 2 with
  | 0 -> Some { Id = userId; Email = "some@email.com" }
  | _ -> None

let getDoc docId =
  match docId % 2 with
  | 0 -> Some { Id = docId ; Content = "Some content" }
  | _ -> None

let endpoint userId resourceId : ActionResult =
  result {
    do!
      match getUser userId with
      | Some _ -> Ok ()
      | None -> Error <| AuthenticationError { UserId = None; ResourceName = "Document" }
    
    return!
      match getDoc resourceId with
      | Some doc -> Ok doc
      | None -> Error <| EntityNotFound "Document"
  } |> mapResult
