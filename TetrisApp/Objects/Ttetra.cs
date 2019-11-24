using System;
using System.Collections.Generic;

namespace TetrisApp.Objects
{
    // T - фигура.
    sealed class Ttetra : Tetramino
    {
        public Ttetra()
        {
            FigureState = new Dictionary<int, bool[,]>
            {
                [1] = new bool[2, 3] {      { true, true, true },
                                            { false, true, false }},
                [2] = new bool[3, 2] {      { false, true},
                                            { true, true},
                                            { false, true} },
                [3] = new bool[2, 3] {      { false, true, false },
                                            { true, true, true }},
                [4] = new bool[3, 2] {      { true, false },
                                            { true, true },
                                            { true, false} },
            };
            State = 1;
            CountState = FigureState.Count;
            Name = "T-тетрамино";
            Body = FigureState[State];
        }

    }
}
