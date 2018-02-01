namespace Railway

open Railway.Types
open System.Collections.Generic

module PathFinder =

    type Tree = 
        | Node of Label * int * Tree
        | First
        
    let getH h nh =
        if nh = -1
        then h
        else nh

    let rec getNodes n (labelSet : Dictionary<Label,int>) nh =
        match n with
        | [] -> labelSet
        | Node(s,h,First)::xs -> let gh = getH h nh in if labelSet.ContainsKey s
                                                       then labelSet.[s] <- (min (gh - h) labelSet.[s]); getNodes xs labelSet -1
                                                       else labelSet.Add(s,gh - h); getNodes xs labelSet -1
        | Node(s,h,prev)::xs -> let gh = getH h nh in if labelSet.ContainsKey s
                                                      then labelSet.[s] <- (min (gh - h) labelSet.[s]); getNodes (prev::xs) labelSet gh
                                                      else labelSet.Add(s,gh - h); getNodes (prev::xs) labelSet gh
        | _ -> failwith "Don't iterate on First nodes"

    let rec findPath n f cons =
        match n with
        | Node(s,idx,_) -> if s = f then [n]
                           else match !cons s with
                                | Crash      -> []
                                | Line(line) -> findPath (Node(line,idx+1,n)) f cons
                                | Plus(_,line) -> findPath (Node(line,idx+1,n)) f cons                                                                   
                                | Minus(_,line)  -> findPath (Node(line,idx+1,n)) f cons
                                | Stem(_,linePlus,lineMinus) -> (findPath (Node(linePlus,idx+1,n)) f cons)@(findPath (Node(lineMinus,idx+1,n)) f cons)
        | _             -> failwith "Should never reach cases with First Trees"
    
    let getPaths S F consUp consDown =
        match S, F with
        | ControlState(up1, down1), ControlState(up2, down2) -> let downPath = List.foldBack (fun (sdown,fdown) l -> (findPath (Node(sdown,0,First)) fdown consDown)::l) (List.zip down1 down2) []
                                                                let upPath = List.foldBack (fun (sup,fup) l -> (findPath (Node(sup,0,First)) fup consUp)::l) (List.zip up1 up2) []
                                                                [for n in upPath do yield getNodes n (new Dictionary<Label,int>()) -1],[for n in downPath do yield getNodes n (new Dictionary<Label,int>()) -1]
        | _ -> failwith "Start and finish states must be control states"