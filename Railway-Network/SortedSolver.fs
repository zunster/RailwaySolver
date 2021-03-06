﻿namespace Railway

open System.Collections.Generic
open Railway.Types
open Railway.Game
open FSharpx.Collections

module SortedSolver =

    type SortedSolver(game : Game, upPaths : Dictionary<Label,int> list, downPaths : Dictionary<Label,int> list) =
        let lines = game.lines
        let points = game.points
        let consUp = game.consUp
        let consDown = game.consDown
        let signalsUp = game.signalsUp
        let signalsDown = game.signalsDown
        let S = game.S
        let F = game.F

        let upPaths = upPaths
        let downPaths = downPaths

        let toStates = new Dictionary<int,State>()
        let toIndex = new Dictionary<State,int>()
        let edges = new Dictionary<int,(int*int) list>()
        let A = ResizeArray<sbyte>()
        let D = ResizeArray<(int * (int * int) list) list>()

        let controlled (up : Label list) (down : Label list) = List.forall (fun s -> List.exists ( (=) s ) signalsUp) up &&  
                                                               List.forall (fun s -> List.exists ( (=) s ) signalsDown) down

        let getHeuristic s =
            match s with
            | ControlState(up,down)     -> let mutable h = 0
                                           for n in 0 .. up.Length - 1 do h <- h + upPaths.[n].[up.[n]]
                                           for n in 0 .. down.Length - 1 do h <- h + downPaths.[n].[down.[n]]
                                           h
            | EnvironState(up,down,_,_) -> let mutable h = 0
                                           for n in 0 .. up.Length - 1 do h <- h + upPaths.[n].[up.[n]]
                                           for n in 0 .. down.Length - 1 do h <- h + downPaths.[n].[down.[n]]
                                           h
            | _                         -> 2147483647

        let getIdx pos dir up down =
            if dir.Equals Up
            then List.findIndex ((=) pos) up
            else List.findIndex ((=) pos) down

        let addState s =
            let h = getHeuristic s
            if toIndex.ContainsKey s then (toIndex.[s],h)
            else A.Add -1y;
                 D.Add [];
                 let v = A.Count - 1
                 toIndex.Add(s,v)
                 toStates.Add(v,s)
                 (v,h)

        let addEdge k ys (dependlist : (int * (int * int) list) list) =
            let rec addEdgeRec e (h : int) lst = 
                match lst with
                | [] -> [(k,ys)]
                | (kd,states)::xs -> match states with
                                     | []        -> (kd,states)::(addEdgeRec e h xs)
                                     | (_,xh)::_ -> if xh < h
                                                      then (kd,states)::(addEdgeRec e h xs)
                                                      else e::lst
            match ys with
            | []        -> (k,ys)::dependlist
            | (_,h)::_  -> addEdgeRec (k,ys) h dependlist


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
            if dir.Equals Up
            then (not (List.exists ((=) label) up) && not (List.exists ((=) label) down)) && upPaths.[idx].ContainsKey label
            else (not (List.exists ((=) label) up) && not (List.exists ((=) label) down)) && downPaths.[idx].ContainsKey label
            
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

        let predictNextControlState (k : int) (s : State) =
            match s with
            | EnvironState(up,down,Some (signal,dir),points)      -> let ls = addNextControlState signal dir up down points
                                                                     edges.Add(k,[ls])
                                                                     snd ls
            | EnvironState(up,down,None,points)                   -> let ls = (List.fold (fun l x -> if noSignal x Up then (addNextControlState x Up up down points)::l else l) [] up)
                                                                     let lls = List.sortBy (fun (_,x) -> x) (List.fold (fun l x -> if noSignal x Down then (addNextControlState x Down up down points)::l else l) ls down)
                                                                     if lls.IsEmpty then failwith "no next states"
                                                                     else edges.Add(k,lls); snd lls.[0]
            | _ -> failwith "Only EnvironmentState in this function"


        let addNextEnvironsStates pos up down dir signal =
            let consRef = getConnections dir
            let points = match (!consRef pos) with
                         | Crash -> []
                         | Line(_) -> [setPoints -1  points.Length PointStatus.Minus]
                         | Plus(point,_) -> [setPoints (List.findIndex ((=) point ) points)  points.Length PointStatus.Plus]
                         | Minus(point,_) -> [setPoints (List.findIndex ((=) point ) points)  points.Length PointStatus.Minus]
                         | Stem(point,_,_) -> (setPoints (List.findIndex ((=) point ) points)  points.Length PointStatus.Plus)::
                                              (setPoints (List.findIndex ((=) point ) points)  points.Length PointStatus.Minus)::[]
            let test = (List.fold (fun l x -> (addState (EnvironState(up, down, signal, x))) :: l) [] points)
            List.map (fun (t,_) -> if edges.ContainsKey t then (t,snd edges.[t].[0]) else (t,predictNextControlState t toStates.[t])) test


        let load (k : int) (s : State) (W : (int * (int * int) list) list) =
            match s with
            | ControlState(up,down) when not (controlled up down) -> let ls = (List.fold (fun l x -> (addNextEnvironsStates x up down Up None)::l) [] up)
                                                                     let lls = List.sortBy (fun (_,x) -> x) (List.concat (List.fold (fun l x -> (addNextEnvironsStates x up down Down None)::l) ls down))
                                                                     [for l in lls do yield (k,[l])]@W
            | ControlState(up,down)                               -> let ls = (List.fold (fun l x -> (addNextEnvironsStates x up down Up (Some (x,Up)))::l) [] up)
                                                                     let lls = List.sortBy (fun (_,x) -> x) (List.concat (List.fold (fun l x -> (addNextEnvironsStates x up down Down (Some (x,Down)))::l) ls down))
                                                                     [for l in lls do yield (k,[l])]@W
            | EnvironState(up,down,signal,points)                 -> if toIndex.ContainsKey (EnvironState(up,down,signal,points))
                                                                     then let s = toIndex.[EnvironState(up,down,signal,points)]
                                                                          (k,edges.[s])::W
                                                                     else W
            | CrashState                                          -> W

        let rec while1 (W : (int * (int * int) list) list) =
            if A.[0] = 1y then A
            else
            match W with
            | [] -> A
            | (k,_)::xs when A.[k] = 1y -> while1 xs
            | (k,l)::xs  -> match List.skipWhile (fun (x,_) -> A.[x] = 1y) l with
                            | []                            -> A.[k] <- 1y;                 while1 (D.[k]@xs)
                            | (y,_)::ys when A.[y] = 0y     -> D.[y] <- addEdge k ys D.[y]; while1 xs
                            | (y,_)::ys                     -> A.[y] <- 0y;                 D.[y] <- [(k,ys)];  while1 (load y toStates.[y] W)

        
        member this.start = 

            ignore(addState S)
            ignore(addState F)

            A.[0] <- 0y
            A.[1] <- 1y
            
            let W = load 0 S []
            let res1 = while1 W

            res1
           