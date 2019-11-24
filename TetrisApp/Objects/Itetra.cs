using System;
using System.Collections.Generic;

namespace TetrisApp.Objects
{
    // I - фигура.
    sealed class Itetra : Tetramino
    {
        public Itetra()
        {
            FigureState = new Dictionary<int, bool[,]>
            {
                [1] = new bool[1, 4] {      { true, true, true, true }},
                [2] = new bool[4, 1] {      { true},
                                            { true},
                                            { true},
                                            { true}},
            };
            State = 1;
            CountState = FigureState.Count;
            Name = "I-тетрамино";
            Body = FigureState[State];
        }
    }
}
