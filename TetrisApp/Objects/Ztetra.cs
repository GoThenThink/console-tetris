using System;
using System.Collections.Generic;

namespace TetrisApp.Objects
{
    // Z - фигура.
    sealed class Ztetra : Tetramino
    {
        public Ztetra()
        {
            FigureState = new Dictionary<int, bool[,]>
            {
                [1] = new bool[2, 3] {      { true,  true, false },
                                            { false, true, true }},
                [2] = new bool[3, 2] {      { false, true       },
                                            { true,  true       },
                                            { true,  false      }},
            };
            State = 1;
            CountState = FigureState.Count;
            Name = "Z-тетрамино";
            Body = FigureState[State];
        }

    }
}
