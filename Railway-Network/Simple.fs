namespace Railway.Game

open Railway.Types

open System.Collections.Generic

module Simple =
    type Simple() =
        inherit Railway.Game.Game()

        override _this.lines = ["s10";"s12";"s20"]
        override _this.points = ["s11"]

        override _this.consUpList = dict [("s10",Plus("s11","s12"));("s20",Minus("s11","s12"))]
        override _this.consDownList = dict [("s12",Stem("s11","s10","s20"))]
        
        override _this.signalsUp = ["s20"]
        override _this.signalsDown = ["s10"]

        override _this.endPointsUp = ["s20"]
        override _this.endPointsDown = ["s10";"s12"]
        
        override _this.S = ControlState(["s20"],["s12"])
        override _this.F = ControlState(["s12"],["s10"])

        override _this.consUp x =
                    if _this.consUpList.ContainsKey x
                    then _this.consUpList.[x]
                    else Crash

        override _this.consDown x =
                    if _this.consDownList.ContainsKey x
                    then _this.consDownList.[x]
                    else Crash