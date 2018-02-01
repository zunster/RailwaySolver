namespace Railway.Game

open Railway.Types

module Double =
    type Double(game : Game) =
        inherit Railway.Game.Game()

        override _this.lines = [for label in game.lines do yield (label + "_0")]@[for label in game.lines do yield (label + "_1")]
        override _this.points = [for label in game.points do yield (label + "_0")]@[for label in game.points do yield (label + "_1")]

        override _this.consUpList = [ (dict [for x in game.consUpList do match x.Key, x.Value with
                                                                         | label0, Line(label1) -> yield (label0 + "_0",Line(label1 + "_0"))
                                                                         | label0, Plus(label1,label2) -> yield (label0 + "_0",Plus(label1 + "_0",label2 + "_0"))
                                                                         | label0, Minus(label1,label2) -> yield (label0 + "_0",Minus(label1 + "_0",label2 + "_0"))
                                                                         | label0, Stem(label1,label2,label3) -> yield (label0 + "_0",Stem(label1 + "_0",label2 + "_0",label3 + "_0"))
                                                                         | label0, Crash -> yield (label0 + "_0",Crash)]);
                                                                         (dict [for x in game.consDownList do match x.Key, x.Value with
                                                                                                              | label0, Line(label1) -> yield (label0 + "_1",Line(label1 + "_1"))
                                                                                                              | label0, Plus(label1,label2) -> yield (label0 + "_1",Plus(label1 + "_1",label2 + "_1"))
                                                                                                              | label0, Minus(label1,label2) -> yield (label0 + "_1",Minus(label1 + "_1",label2 + "_1"))
                                                                                                              | label0, Stem(label1,label2,label3) -> yield (label0 + "_1",Stem(label1 + "_1",label2 + "_1",label3 + "_1"))
                                                                                                              | label0, Crash -> yield (label0 + "_1",Crash)]);
                                                                         (dict [for label in game.endPointsUp do yield (label + "_0",Line(label + "_1"))])
                                                                          ]
                                                                          |> Seq.concat
                                                                          |> Seq.map (fun keyvalue -> keyvalue.Key, keyvalue.Value)
                                                                          |> dict

        override _this.consDownList = [ (dict [for x in game.consDownList do match x.Key, x.Value with
                                                                             | label0, Line(label1) -> yield (label0 + "_0",Line(label1 + "_0"))
                                                                             | label0, Plus(label1,label2) -> yield (label0 + "_0",Plus(label1 + "_0",label2 + "_0"))
                                                                             | label0, Minus(label1,label2) -> yield (label0 + "_0",Minus(label1 + "_0",label2 + "_0"))
                                                                             | label0, Stem(label1,label2,label3) -> yield (label0 + "_0",Stem(label1 + "_0",label2 + "_0",label3 + "_0"))
                                                                             | label0, Crash -> yield (label0 + "_0",Crash)]);
                                                                             (dict [for x in game.consUpList do match x.Key, x.Value with
                                                                                                                | label0, Line(label1) -> yield (label0 + "_1",Line(label1 + "_1"))
                                                                                                                | label0, Plus(label1,label2) -> yield (label0 + "_1",Plus(label1 + "_1",label2 + "_1"))
                                                                                                                | label0, Minus(label1,label2) -> yield (label0 + "_1",Minus(label1 + "_1",label2 + "_1"))
                                                                                                                | label0, Stem(label1,label2,label3) -> yield (label0 + "_1",Stem(label1 + "_1",label2 + "_1",label3 + "_1"))
                                                                                                                | label0, Crash -> yield (label0 + "_1",Crash)]);
                                                                             (dict [for label in game.endPointsUp do yield (label + "_1",Line(label + "_0"))])
                                                                              ]
                                                                              |> Seq.concat
                                                                              |> Seq.map (fun keyvalue -> keyvalue.Key, keyvalue.Value)
                                                                              |> dict

        override _this.signalsUp = [for label in game.signalsUp do yield (label + "_0")]@[for label in game.signalsDown do yield (label + "_1")]
        override _this.signalsDown = [for label in game.signalsDown do yield (label + "_0")]@[for label in game.signalsUp do yield (label + "_1")]

        override _this.endPointsUp = [for label in game.endPointsDown do yield (label + "_1")]
        override _this.endPointsDown = [for label in game.endPointsDown do yield (label + "_0")]
        
        //TODO change these to match all instances
        //override _this.F = ControlState(["s64_1";"s65_1";"s76_1";"s77_1"],["s87_0";"s86_0"; "s49_0"; "s82_0"])
        //override _this.S = ControlState(["s64_0";"s65_0";"s76_0";"s77_0"],["s87_1";"s86_1"; "s49_1"; "s82_1"])
        override _this.F = ControlState(["s64_1"],["s87_0"])
        override _this.S = ControlState(["s64_0"],["s87_1"])

        override _this.consUp x =
                    if _this.consUpList.ContainsKey x
                    then _this.consUpList.[x]
                    else Crash

        override _this.consDown x =
                    if _this.consDownList.ContainsKey x
                    then _this.consDownList.[x]
                    else Crash