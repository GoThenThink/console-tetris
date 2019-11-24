using System;
using System.Collections.Generic;

namespace TetrisApp.Objects
{
    // S - фигура.
    sealed class Stetra : Tetramino
    {
        public Stetra()
        {
            FigureState = new Dictionary<int, bool[,]>
            {
                [1] = new bool[2, 3] {      { false, true, true },
                                            { true, true, false}},
                [2] = new bool[3, 2] {      { true, false},
                                            { true, true},
                                            { false, true} },
            };
            State = 1;
            CountState = FigureState.Count;
            Name = "S-тетрамино";
            Body = FigureState[State];
        }

    }
}
