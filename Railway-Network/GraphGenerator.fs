namespace Railway

open System.Collections.Generic
open Railway.Types
open Railway.Game
open FSharpx.Collections

module GraphGenerator =
    type GraphGenerator(game : Game) =
        let lines = game.lines
        let points = game.points
        let consUp = game.consUp
        let consDown = game.consDown
        let signalsUp = game.signalsUp
        let signalsDown = game.signalsDown
        let S = game.S
        let F = game.F

        let toStates = new Dictionary<int,State>()
        let toIndex = new Dictionary<State,int>()
        let mutable v = -1
        
        let controlled (up : Label list) (down : Label list) = List.forall (fun s -> List.exists ( (=) s ) signalsUp) up &&  
                                                               List.forall (fun s -> List.exists ( (=) s ) signalsDown) down

        let getIdx pos dir up down =
            if dir.Equals Up
            then List.findIndex ((=) pos) up
            else List.findIndex ((=) pos) down

        let addState s =
            if toIndex.ContainsKey s then toIndex.[s]
            else v <- v + 1
                 toIndex.Add(s,v)
                 toStates.Add(v,s)
                 v

        let setPoints idx len (status : PointStatus) =
            [ for i in 0 .. len - 1 -> if i.Equals idx then status else PointStatus.Minus]

        let rec setPos idx lst pos =
            match idx, lst with
            | 0, _::xs -> pos::xs
            | _, x::xs -> x::setPos (idx - 1) xs pos
            | _, []    -> failwith "index out of range"

        let setPos2 (orgLine : Label) (newLine : Label) (dir : Direction) (up : Label list) (down : Label list) =
            if dir.Equals Direction.Up
            then let idx = List.findIndex ((=) orgLine) up
                 (setPos idx up newLine),down
            else let idx = List.findIndex ((=) orgLine) down
                 up,(setPos idx down newLine)
        
        let free label up down idx dir =
            not (List.exists ((=) label) up) && not (List.exists ((=) label) down)
            
        let getConnections = function
            | Up   -> ref consUp
            | Down -> ref consDown

        let noSignal pos dir =
            let signals = match dir with
                          | Up   -> ref signalsUp
                          | Down -> ref signalsDown
            not (List.exists ((=) pos) !signals)
        
        let addNextControlState pos dir up down (pointStatus : PointStatus list) =
            let consRef = getConnections dir
            match (!consRef pos) with
            | Crash      -> (addState CrashState)
            | Line(line) -> if free line up down (getIdx pos dir up down) dir
                            then let s = ControlState(setPos2 pos line dir up down)
                                 (addState s)
                            else (addState CrashState)
            | Plus(point,line) when pointStatus.[List.findIndex ((=) point) points].Equals PointStatus.Plus->if free line up down (getIdx pos dir up down) dir
                                                                                                             then let s = ControlState(setPos2 pos line dir up down)
                                                                                                                  (addState s)
                                                                                                             else (addState CrashState)
                                                                                                            
            | Minus(point,line) when pointStatus.[List.findIndex ((=) point) points].Equals PointStatus.Minus -> if free line up down (getIdx pos dir up down) dir
                                                                                                                 then let s = ControlState(setPos2 pos line dir up down)
                                                                                                                      (addState s)
                                                                                                                 else (addState CrashState)
            | Stem(point,linePlus,lineMinus) -> let plusBool = pointStatus.[List.findIndex ((=) point) points].Equals PointStatus.Plus
                                                if plusBool
                                                then if free linePlus up down (getIdx pos dir up down) dir
                                                     then let s = ControlState(setPos2 pos linePlus dir up down)
                                                          (addState s)
                                                     else (addState CrashState)
                                                else if free lineMinus up down (getIdx pos dir up down) dir
                                                     then let s = ControlState(setPos2 pos lineMinus dir up down)
                                                          (addState s)
                                                     else (addState CrashState)
            | _                              -> (addState CrashState)


        let addNextEnvironsStates pos up down dir signal =
            let consRef = getConnections dir
            let points = match (!consRef pos) with
                         | Crash -> []
                         | Line(_) -> [setPoints -1  points.Length PointStatus.Minus]
                         | Plus(point,_) -> [setPoints (List.findIndex ((=) point ) points)  points.Length PointStatus.Plus]
                         | Minus(point,_) -> [setPoints (List.findIndex ((=) point ) points)  points.Length PointStatus.Minus]
                         | Stem(point,_,_) -> (setPoints (List.findIndex ((=) point ) points)  points.Length PointStatus.Plus)::
                                              (setPoints (List.findIndex ((=) point ) points)  points.Length PointStatus.Minus)::[]
            (List.fold (fun l x -> (addState (EnvironState(up, down, signal, x))) :: l) [] points)


        let load1 (k : int) =
            match toStates.[k] with
            | ControlState(up,down) when not (controlled up down) -> let ls = (List.fold (fun l x -> (addNextEnvironsStates x up down Up None)::l) [] up)
                                                                     let lls = List.concat (List.fold (fun l x -> (addNextEnvironsStates x up down Down None)::l) ls down)
                                                                     [for l in lls do yield [l]]
            | ControlState(up,down)                               -> let ls = (List.fold (fun l x -> (addNextEnvironsStates x up down Up (Some (x,Up)))::l) [] up)
                                                                     let lls = List.concat (List.fold (fun l x -> (addNextEnvironsStates x up down Down (Some (x,Down)))::l) ls down)
                                                                     [for l in lls do yield [l]]
            | EnvironState(up,down,Some (signal,dir),points) -> let ls = addNextControlState signal dir up down points
                                                                [[ls]]
            | EnvironState(up,down,None,points) -> let ls = (List.fold (fun l x -> if noSignal x Up then (addNextControlState x Up up down points)::l else l) [] up)
                                                   let lls = (List.fold (fun l x -> if noSignal x Down then (addNextControlState x Down up down points)::l else l) ls down)
                                                   if lls.IsEmpty then []
                                                   else [lls]
            | CrashState -> []
    

        let rec getHyperedges i H =
            if i < toStates.Count
            then getHyperedges (i + 1) ((load1 i)::H)
            else List.rev H

        member this.getGraph = 

            ignore(addState S)
            ignore(addState F)
            
            let res1 = getHyperedges 0 []
            printfn "Number of states %i" res1.Length
            res1