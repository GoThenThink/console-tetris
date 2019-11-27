using System;
using System.Collections.Generic;

namespace TetrisApp.Objects
{
    // T - фигура.
    sealed class Ttetra : ITetramino
    {
        public Dictionary<int, bool[,]> FigureState { get; set; }
        public string Name { get; set; }
        public int State { get; set; }
        public int CountState { get; set; }
        public int CurrentCol { get; set; }
        public int CurrentRow { get; set; }
        public Boolean[,] Body { get; set; }
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
        public object Clone()
        {
            Ttetra newTetra = (Ttetra)this.MemberwiseClone();
            Dictionary<int, bool[,]> stateOfNewTetra = new Dictionary<int, bool[,]>
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
            newTetra.FigureState = stateOfNewTetra;
            return newTetra;
        }

    }
}
