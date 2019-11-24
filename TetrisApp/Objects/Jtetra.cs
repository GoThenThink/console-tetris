using System;
using System.Collections.Generic;

namespace TetrisApp.Objects
{
    // J - фигура
    sealed class Jtetra:Tetramino
    {
        public Jtetra()
        {
            FigureState = new Dictionary<int, bool[,]>
            {
                [1] = new bool[3, 2] {      { false, true},
                                            { false, true},
                                            { true, true} },
                [2] = new bool[2, 3] {      { true, false, false },
                                            { true, true, true }},
                [3] = new bool[3, 2] {      { true, true},
                                            { true, false},
                                            { true, false}},
                [4] = new bool[2, 3] {      { true, true, true },
                                            { false, false, true  } },
            };
            State = 1;
            CountState = FigureState.Count;
            Name = "J-тетрамино";
            Body = FigureState[State];
        }
    }
}
