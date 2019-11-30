using System;
using System.Collections.Generic;

namespace TetrisApp.Objects
{
    // S - фигура.
    sealed class Stetra : ITetramino
    {
        public Dictionary<int, bool[,]> FigureState { get; set; }
        public string Name { get; set; }
        public int State { get; set; }
        public int CountState { get; set; }
        public int CurrentCol { get; set; }
        public int CurrentRow { get; set; }
        public Boolean[,] Body { get; set; }
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
        public object Clone()
        {
            Stetra newTetra = (Stetra)this.MemberwiseClone();
            Dictionary<int, bool[,]> stateOfNewTetra = new Dictionary<int, bool[,]>
            {
                [1] = new bool[2, 3] {      { false, true, true },
                                            { true, true, false}},
                [2] = new bool[3, 2] {      { true, false},
                                            { true, true},
                                            { false, true} },
            };
            newTetra.FigureState = stateOfNewTetra;
            return newTetra;
        }

    }
}
