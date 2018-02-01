namespace Railway

open Railway.Types

module Game =
    open System.Collections.Generic

    [<AbstractClass>]
    type Game() =

        //Station properties
        abstract member lines: Label list
        abstract member points: Label list
        abstract member consUpList: IDictionary<Label,Port>
        abstract member consDownList: IDictionary<Label,Port>
        abstract member signalsUp: Label list
        abstract member signalsDown: Label list

        abstract member endPointsUp: Label list
        abstract member endPointsDown: Label list

        abstract member S: State
        abstract member F: State

        abstract member consUp: Label -> Port
        abstract member consDown: Label -> Port