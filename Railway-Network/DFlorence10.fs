namespace Railway.Game

open Railway.Types
open Railway.Game.Double
open Railway.Game.Florence8

open System.Collections.Generic

module DFlorence10 =
    type DFlorence10() =
        inherit Railway.Game.Game()

        let game0 = new Florence8()
        let game1 = new Double(game0)

        override _this.lines = game1.lines
        override _this.points = game1.points

        override _this.consUpList = game1.consUpList
        override _this.consDownList = game1.consDownList
        
        override _this.signalsUp = game1.signalsUp@["s66_0";"s72_1";"s67_1";"s76_1";"s63_1";"s70_1";"s65_1"]
        override _this.signalsDown = game1.signalsDown@["s68_0";"s69_0";"s63_0";"s67_0";"s70_0";"s75_0";"s76_0";"s88_1";"s74_0"]

        override _this.endPointsUp = game1.endPointsUp
        override _this.endPointsDown = game1.endPointsDown
        
        override _this.F = ControlState(["s66_0";"s72_1";"s67_1";"s76_1";"s63_1"],["s68_0";"s69_0";"s63_0";"s67_0";"s70_0"])//;"s75_0";"s76_0"])//;"s88_1";"s74_0"]) ;"s70_1";"s65_1"]
        override _this.S = ControlState(["s64_0";"s65_0";"s68_0";"s76_0";"s63_0"],["s63_1";"s65_1";"s67_1";"s68_1";"s70_1"])//;"s73_1";"s74_1"])//;"s75_1";"s76_1"]) ;"s69_0";"s66_1"

        override _this.consUp x =
                    if _this.consUpList.ContainsKey x
                    then _this.consUpList.[x]
                    else Crash

        override _this.consDown x =
                    if _this.consDownList.ContainsKey x
                    then _this.consDownList.[x]
                    else Crash