namespace Railway.Game

open Railway.Types

module Fork =
    type Fork() =
        inherit Railway.Game.Game()

        override _this.lines = ["s08";"s09";"s10";"s12";"s1121";"s21";"s30"]
        override _this.points = ["s11";"s20"]
        override _this.consUpList = dict [("s08",Line("s09"));
                                          ("s09",Line("s10"));
                                          ("s10",Stem("s11","s12","s1121"));
                                          ("s1121",Stem("s20","s30","s21"))]
        override _this.consDownList = dict []

        override _this.signalsUp = ["s08" ; "s09" ; "s10" ; "s12"; "s21" ; "s30"]
        override _this.signalsDown = []

        override _this.endPointsUp = ["s12";"s21";"s30"]
        override _this.endPointsDown = []
        
        override _this.S = ControlState(["s08";"s09";"s10"],[])
        override _this.F = ControlState(["s12";"s21";"s30"],[])

        override _this.consUp x =
                    if _this.consUpList.ContainsKey x
                    then _this.consUpList.[x]
                    else Crash

        override _this.consDown x =
                    if _this.consDownList.ContainsKey x
                    then _this.consDownList.[x]
                    else Crash