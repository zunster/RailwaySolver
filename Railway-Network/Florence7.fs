namespace Railway.Game

open Railway.Types

module Florence7 =
    type Florence7() =
        inherit Railway.Game.Game()

        override _this.lines = ["s63"; "s4"; "s50"; "s78"; "s64"; "s5";
                                "s65"; "s6"; "s2223"; "s24"; "s66"; "s2325"; 
                                "s67"; "s7"; "s36"; "s3537"; "s51"; "s82"; "s3738";
                                "s68";"s8"; "s3334"; "s52"; "s83"; "s3435"; "s69"; "s9"; 
                                "s3132";"s53"; "s84"; "s3233"; "s70"; "s10"; "s2930";
                                "s59";  "s85"; "s3031"; "s71"; "s11"; "s2728"; "s49";
                                "s54"; "s86"; "s2829"; "s72"; "s12";"s43";"s4244";
                                "s4445";"s4547"; "s55"; "s87";"s2627"; "s4748"; 
                                "s46"; "s88"; "s73"; "s13"; "s4142"; "s74"; "s16";
                                "s3940"; "s4041"; "s75"; "s17"; "s76"; "s14";
                                "s77"; "s15"]
        override _this.points = ["s22"; "s23"; "s25";"s26"; "s27"; "s28";"s29"; "s30"; "s31"; "s32";"s33";"s34";
                                 "s35"; "s37";"s38"; "s39"; "s40"; "s41"; "s44";"s42"; "s45";"s47"; "s48"]
        override _this.consUpList = dict [
                                     ("s63",Line("s4") );
                                     ("s65",Line("s6") );
                                     ("s24",Line("s66"));
                                     ("s50",Line("s78"));
                                     ("s64",Line("s5") );
                                     ("s67",Line("s7") );
                                     ("s68",Line("s8") );
                                     ("s52",Line("s83"));
                                     ("s69",Line("s9") );
                                     ("s53",Line("s84"));
                                     ("s70",Line("s10"));
                                     ("s59",Line("s85"));
                                     ("s75",Line("s17"));
                                     ("s51",Line("s82"));
                                     ("s71",Line("s11"));
                                     ("s54",Line("s86"));
                                     ("s72",Line("s12"));
                                     ("s55",Line("s87"));
                                     ("s46",Line("s88"));
                                     ("s73",Line("s13"));
                                     ("s74",Line("s16"));
                                     ("s76",Line("s14"));
                                     ("s77",Line("s15"));
                                     ("s5",Minus("s22", "s2223")          );
                                     ("s6",Plus("s22",  "s2223")          );
                                     ("s2223",Stem("s23", "s24", "s2325") );
                                     ("s4",Plus("s38", "s50")             );
                                     ("s3738",Minus("s38", "s50")         );
                                     ("s2325",Minus("s25", "s36")         );
                                     ("s7",Plus("s25", "s36")             );
                                     ("s3435",Minus("s35", "s3537")       );
                                     ("s36",Plus("s35", "s3537")          );
                                     ("s3537",Stem("s37", "s51", "s3738") );
                                     ("s8",Plus("s33", "s3334")           );
                                     ("s3233",Minus("s33", "s3334")       );
                                     ("s3334",Stem("s34", "s52", "s3435") );
                                     ("s9",Plus("s31", "s3132")           );
                                     ("s3031",Minus("s31", "s3132")       );
                                     ("s2728",Stem("s28", "s49", "s2829") );
                                     ("s3132",Stem("s32", "s53", "s3233") );
                                     ("s10",Plus("s29", "s2930")          );
                                     ("s2829",Minus("s29", "s2930")       );
                                     ("s2930",Stem("s30", "s59", "s3031") );
                                     ("s11",Plus("s27", "s2728")          );
                                     ("s2627",Minus("s27", "2728")        );
                                     ("s12",Stem("s26", "s43", "s2627")   );
                                     ("s49",Plus("s48", "s54")            );
                                     ("s4748",Minus("s48", "s54")         );
                                     ("s43",Plus("s42", "s4244")          );
                                     ("s4142",Minus("s42", "s4244")       );
                                     ("s4244",Plus("s44", "s4445")        );
                                     ("s15",Minus("s44", "s4445")         );
                                     ("s4445",Stem("s45", "s4547", "s46") );
                                     ("s4547",Stem("s47", "s55", "s4748") );
                                     ("s13",Plus("s41", "s4142")          );
                                     ("s4041",Minus("s41", "s4142")       );
                                     ("s16",Plus("s39", "s3940")          );
                                     ("s17",Minus("s39", "s3940")         );
                                     ("s3940",Plus("s40", "s4041")        );
                                     ("s14",Minus("s40", "s4041")         )
                                     ]

        override _this.consDownList = dict [
                                       ("s4",Line( "s63" ) );
                                       ("s6",Line( "s65" ) );
                                       ("s66",Line( "s24" ));
                                       ("s78",Line( "s50" ));
                                       ("s5",Line( "s64" ) );
                                       ("s7",Line( "s67" ) );
                                       ("s8",Line( "s68" ) );
                                       ("s83",Line( "s52" ));
                                       ("s9",Line("s69" )  );
                                       ("s84",Line( "s53" ));
                                       ("s10",Line( "s70" ));
                                       ("s85",Line( "s59" ));
                                       ("s17",Line( "s75" ));
                                       ("s82",Line( "s51" ));
                                       ("s11",Line( "s71" ));
                                       ("s86",Line( "s54" ));
                                       ("s12",Line( "s72" ));
                                       ("s87",Line( "s55" ));
                                       ("s88",Line( "s46" ));
                                       ("s13",Line( "s73" ));
                                       ("s16",Line( "s74" ));
                                       ("s14",Line( "s76" ));
                                       ("s15",Line( "s77" ));
                                       ("s2223",Stem("s22", "s6", "s5")    );
                                       ("s2325",Minus("s23", "s2223")      );
                                       ("s24",Plus("s23", "s2223")         );
                                       ("s50",Stem("s38", "s4", "s3738")   );
                                       ("s36",Stem("s25", "s7", "s2325")   );
                                       ("s3537",Stem("s35", "s36", "s3435"));
                                       ("s3738",Plus("s37", "s3537")       );
                                       ("s51",Minus("s37", "s3537")        );
                                       ("s3334",Stem("s33", "s8", "s3233") );
                                       ("s3435",Minus("s34", "s3334")      );
                                       ("s52",Plus("s34", "s3334")         );
                                       ("s3132",Stem("s31","s9", "s3031")  );
                                       ("s2829",Minus("s28", "s2728")      );
                                       ("s49",Plus("s28", "s2728")         );
                                       ("s3233",Minus("s32", "s3132")      );
                                       ("s53",Plus("s32", "s3132")         );
                                       ("s2930",Stem("s29", "s10", "s2829"));
                                       ("s3031",Minus("s30", "s2930")      );
                                       ("s59",Plus("s30", "s2930")         );
                                       ("s2728",Stem("s27", "s11", "s2627")); 
                                       ("s2627",Minus("s26", "s12")        );
                                       ("s43",Plus("s26", "s12")           );
                                       ("s54",Stem("s48", "s49", "s4748")  );
                                       ("s4244",Stem("s42", "s43", "s4142"));
                                       ("s4445",Stem("s44", "s4244", "s15"));
                                       ("s46",Minus("s45", "s4445")        );
                                       ("s4547",Plus("s45", "s4445")       );
                                       ("s4748",Minus("s47", "s4547")      );
                                       ("s55",Plus("s47", "s4547")         );
                                       ("s4142",Stem("s41", "s13", "s4041"));
                                       ("s3940",Stem("s39", "s16", "s17")  );
                                       ("s4041",Stem("s40", "s3940", "s14"))]

        override _this.signalsUp = ["s63"; "s50"; "s64"; "s65"; "s24"; "s67"; "s51"; "s68"; "s52"; "s69"; "s53"; "s70"; "s59";
                       "s71"; "s54"; "s72"; "s55"; "s46"; "s73"; "s74"; "s75"; "s76"; "s77"]
        override _this.signalsDown = ["s4"; "s78"; "s5"; "s6"; "s66"; "s7"; "s82"; "s8"; "s83"; "s9"; "s84"; "s10"; "s85"; 
                       "s11"; "s86"; "s12"; "s87"; "s88"; "s13"; "s16"; "s17"; "s14"; "s15"; "s71"; "s72"]

        override _this.endPointsUp = ["s78";"s82";"s83";"s84";"s85";"s86";"s87"]
        override _this.endPointsDown = ["s63";"s64";"s65";"s67";"s68";"s69";"s70";"s71";"s72";"s73";"s74";"s75";"s76";"s77"]
        
        override _this.F = ControlState(["s50";"s24";"s46";"s54"],["s43";"s71"; "s72"])
        override _this.S = ControlState(["s64";"s65";"s76";"s77"],["s87";"s86"; "s49"])

        override _this.consUp x =
                    if _this.consUpList.ContainsKey x
                    then _this.consUpList.[x]
                    else Crash

        override _this.consDown x =
                    if _this.consDownList.ContainsKey x
                    then _this.consDownList.[x]
                    else Crash