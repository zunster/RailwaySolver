#r @"C:\Users\Sune\Desktop\speciale\source\Railway-Network\packages\FSharpx.Collections.1.17.0\lib\net40\FSharpx.Collections.dll"
#load "RailWayTypes.fs"
#load "PathFinder.fs"
#load "Game.fs"
#load "GraphGenerator.fs"
#load "GraphSolver.fs"
#load "OnTheFlySolver.fs"
#load "PathSolver.fs"
#load "SortedSolver.fs"
#load "SortedSolver2.fs"
#load "Double.fs"

open Railway.GraphGenerator
open Railway.GraphSolver
open Railway.OnTheFlySolver
open Railway.PathSolver
open Railway.SortedSolver
open Railway.SortedSolver2
open Railway.PathFinder

printfn "Creating game"
(*
#load "Simple.fs"
open Railway.Game.Simple
let game = new Simple()

#load "Fork.fs"
open Railway.Game.Fork
let game = new Fork()

#load "ManyPaths.fs"
open Railway.Game.ManyPaths
let game = new ManyPaths()

#load "Florence4.fs"
open Railway.Game.Florence4
let game = new Florence4()

#load "Florence6.fs"
open Railway.Game.Florence6
let game = new Florence6()
*)
#load "Florence7.fs"
open Railway.Game.Florence7
let game = new Florence7()
(*
#load "Florence8.fs"
open Railway.Game.Florence8
let game = new Florence8()

#load "Florence10.fs"
open Railway.Game.Florence10
let game = new Florence10()

#load "Florence8.fs"
#load "DFlorence8.fs"
open Railway.Game.DFlorence8
let game = new DFlorence8()

#load "Florence8.fs"
#load "DFlorence10.fs"
open Railway.Game.DFlorence10
let game = new DFlorence10()

#load "Florence8.fs"
#load "DFlorence9.fs"
open Railway.Game.DFlorence9
let game = new DFlorence9()


*)
printfn "Generating paths or graph"
#time
//let generator = GraphGenerator(game)
//let graph = generator.getGraph
let upPaths,downPaths = getPaths game.S game.F (ref game.consUp) (ref game.consDown)
#time

printfn "Solving game"
#time
//let solver = GraphSolver(graph)
//let solver = OnTheFlySolver(game)
//let solver = PathSolver(game,upPaths,downPaths)
let solver = SortedSolver2(game,upPaths,downPaths)
let res1 = solver.start
#time

printfn "Results:"
printfn "Initial state A: %i" res1.[0]
printfn "Number of correct states: %i" (Seq.length (Seq.filter ((=) 1y) res1))  
printfn "Number of states: %i" res1.Count
printfn "A: %A" res1