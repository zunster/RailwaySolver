namespace Railway

module Types =

    type PointStatus =
        | Minus
        | Plus
    type Direction =
        | Up
        | Down

    //Network representation
    type Label = string
    type Port =
        | Line of Label
        | Plus of Label * Label
        | Minus of Label * Label
        | Stem of Label * Label * Label
        | Crash

    //State types
    type ControlState = (Label list * Label list ) //trains up * trans down    
    type EnvironState = (Label list * Label list * (Label * Direction) option * PointStatus list) //trains up * trains down * signal states * point states

    type State =
        | ControlState of ControlState
        | EnvironState of EnvironState
        | CrashState
