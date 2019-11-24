using System;
using System.Collections.Generic;

namespace TetrisApp.Objects
{
    // О - фигура.
    sealed class Otetra : Tetramino
    {
        public Otetra()
        {
            FigureState = new Dictionary<int, bool[,]>
            {
                [1] = new bool[2, 2] {      { true, true},
                                            { true, true}, }
            };
            State = 1;
            CountState = FigureState.Count;
            Name = "O-тетрамино";
            Body = FigureState[State];
        }

    }
}
