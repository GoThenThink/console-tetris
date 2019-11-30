using System;
using System.Collections.Generic;

namespace TetrisApp.Objects
{
    // I - фигура.
    sealed class Itetra : ITetramino
    {
        public Dictionary<int, bool[,]> FigureState { get; set; }
        public string Name { get; set; }
        public int State { get; set; }
        public int CountState { get; set; }
        public int CurrentCol { get; set; }
        public int CurrentRow { get; set; }
        public Boolean[,] Body { get; set; }
        public Itetra()
        {
            FigureState = new Dictionary<int, bool[,]>
            {
                [1] = new bool[1, 4] { { true, true, true, true } },
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

        public object Clone()
        {
            Itetra newTetra = (Itetra)this.MemberwiseClone();
            Dictionary<int, bool[,]> stateOfNewTetra = new Dictionary<int, bool[,]>
            {
                [1] = new bool[1, 4] { { true, true, true, true } },
                [2] = new bool[4, 1] {      { true},
                                            { true},
                                            { true},
                                            { true}},
            };
            newTetra.FigureState = stateOfNewTetra;
            return newTetra;
        }
    }
}
