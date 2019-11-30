using System;
using System.Collections.Generic;

namespace TetrisApp.Objects
{
    // J - фигура
    sealed class Jtetra : ITetramino
    {
        public Dictionary<int, bool[,]> FigureState { get; set; }
        public string Name { get; set; }
        public int State { get; set; }
        public int CountState { get; set; }
        public int CurrentCol { get; set; }
        public int CurrentRow { get; set; }
        public Boolean[,] Body { get; set; }
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
        public object Clone()
        {
            Jtetra newTetra = (Jtetra)this.MemberwiseClone();
            Dictionary<int, bool[,]> stateOfNewTetra = new Dictionary<int, bool[,]>
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
            newTetra.FigureState = stateOfNewTetra;
            return newTetra;
        }
    }
}
