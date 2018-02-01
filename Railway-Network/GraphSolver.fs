namespace Railway

open System.Collections.Generic
open Railway.Types
open Railway.Game
open FSharpx.Collections

module GraphSolver =
    type GraphSolver(H : int list list list) =
        let H = H //List of hyperedge destinations
        let A = ResizeArray<sbyte>()
        let D = ResizeArray<(int * int list) list>()

        let rec load k l W =
            match l with
            | [] -> W
            | x::xs -> load k xs ((k,x)::W)

        let rec while1 (W : (int * int list) list) =
            match W with
            | [] -> A
            | (k,_)::xs when A.[k] = 1y -> while1 xs
            | (k,l)::xs  -> match List.skipWhile (fun x -> A.[x] = 1y) l with
                            | []                     -> A.[k] <- 1y;             while1 (D.[k]@xs)
                            | y::ys when A.[y] = 0y  -> D.[y] <- (k,ys)::D.[y];  while1 xs
                            | y::ys                  -> A.[y] <- 0y;             D.[y] <- [(k,ys)];  while1 (load y H.[y] W)
        
        member this.start = 

            for i in 0 .. H.Length - 1 do A.Add -1y; D.Add []
            A.[0] <- 0y;
            A.[1] <- 1y;
            
            let W = load 0 H.[0] []
            let res1 = while1 W

            res1
           