using System;
using System.Collections.Generic;

namespace TetrisApp.Objects
{
    // L - фигура
    sealed class Ltetra : ITetramino
    {
        public Dictionary<int, bool[,]> FigureState { get; set; }
        public string Name { get; set; }
        public int State { get; set; }
        public int CountState { get; set; }
        public int CurrentCol { get; set; }
        public int CurrentRow { get; set; }
        public Boolean[,] Body { get; set; }
        public Ltetra()
        {
            FigureState = new Dictionary<int, bool[,]>
            {
                [1] = new bool[3, 2] {      { true, false },
                                            { true, false },
                                            { true, true  } },
                [2] = new bool[2, 3] {      { true, true, true },
                                            { true, false, false  } },
                [3] = new bool[3, 2] {      { true, true},
                                            { false, true},
                                            { false, true} },
                [4] = new bool[2, 3] {      { false, false, true },
                                            { true, true, true }},
            };
            State = 1;
            CountState = FigureState.Count;
            Name = "L-тетрамино";
            Body = FigureState[State];
        }
        public object Clone()
        {
            Ltetra newTetra = (Ltetra)this.MemberwiseClone();
            Dictionary<int, bool[,]> stateOfNewTetra = new Dictionary<int, bool[,]>
            {
                [1] = new bool[3, 2] {      { true, false },
                                            { true, false },
                                            { true, true  } },
                [2] = new bool[2, 3] {      { true, true, true },
                                            { true, false, false  } },
                [3] = new bool[3, 2] {      { true, true},
                                            { false, true},
                                            { false, true} },
                [4] = new bool[2, 3] {      { false, false, true },
                                            { true, true, true }},
            };
            newTetra.FigureState = stateOfNewTetra;
            return newTetra;
        }

    }
}
