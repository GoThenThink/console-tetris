using System;
using System.Collections.Generic;

namespace TetrisApp.Objects
{
    // Z - фигура.
    sealed class Ztetra : ITetramino
    {
        public Dictionary<int, bool[,]> FigureState { get; set; }
        public string Name { get; set; }
        public int State { get; set; }
        public int CountState { get; set; }
        public int CurrentCol { get; set; }
        public int CurrentRow { get; set; }
        public Boolean[,] Body { get; set; }
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
        public object Clone()
        {
            Ztetra newTetra = (Ztetra)this.MemberwiseClone();
            Dictionary<int, bool[,]> stateOfNewTetra = new Dictionary<int, bool[,]>
            {
                [1] = new bool[2, 3] {      { true,  true, false },
                                            { false, true, true }},
                [2] = new bool[3, 2] {      { false, true       },
                                            { true,  true       },
                                            { true,  false      }},
            };
            newTetra.FigureState = stateOfNewTetra;
            return newTetra;
        }

    }
}
