namespace Railway.Game

open Railway.Types

open System.Collections.Generic

module ManyPaths =
    type ManyPaths() =
        inherit Railway.Game.Game()

        override _this.lines = ["s11";"s13";"s15";"s21";"s23";"s25";"s31";"s33";"s35";"s41";"s43";"s45";"s51"]
        override _this.points = ["s12";"s14";"s22";"s24";"s32";"s34";"s42";"s44"]
        (*
        override _this.consUpList = dict [("s11",Stem("s12","s21","s13"));
                                          ("s21",Stem("s22","s31","s23"));
                                          ("s31",Stem("s32","s41","s33"));
                                          ("s41",Stem("s42","s51","s43"));
                                          ("s13",Minus("s14","s15"));
                                          ("s23",Minus("s24","s25"));
                                          ("s33",Minus("s34","s35"));
                                          ("s43",Minus("s44","s45"));
                                          ("s23",Plus("s14","s15"));
                                          ("s33",Plus("s24","s25"));
                                          ("s43",Plus("s34","s35"));
                                          ("s51",Plus("s44","s45"));]
                                          *)
        override _this.consUpList = dict [("s11",Stem("s12","s13","s21"));
                                          ("s21",Stem("s22","s23","s31"));
                                          ("s31",Stem("s32","s33","s41"));
                                          ("s41",Stem("s42","s43","s51"));
                                          ("s13",Plus("s14","s15"));
                                          ("s23",Plus("s24","s25"));
                                          ("s33",Plus("s34","s35"));
                                          ("s43",Plus("s44","s45"));
                                          ("s25",Minus("s14","s15"));
                                          ("s35",Minus("s24","s25"));
                                          ("s45",Minus("s34","s35"));
                                          ("s51",Minus("s44","s45"));]
        override _this.consDownList = dict []
        
        override _this.signalsUp = []
        override _this.signalsDown = []

        override _this.endPointsUp = ["s15"]
        override _this.endPointsDown = ["s11"]
        
        override _this.S = ControlState(["s11"],[])
        override _this.F = ControlState(["s15"],[])

        override _this.consUp x =
                    if _this.consUpList.ContainsKey x
                    then _this.consUpList.[x]
                    else Crash

        override _this.consDown x =
                    if _this.consDownList.ContainsKey x
                    then _this.consDownList.[x]
                    else Crash

