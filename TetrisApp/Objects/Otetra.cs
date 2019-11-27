using System;
using System.Collections.Generic;

namespace TetrisApp.Objects
{
    // О - фигура.
    sealed class Otetra : ITetramino
    {
        public Dictionary<int, bool[,]> FigureState { get; set; }
        public string Name { get; set; }
        public int State { get; set; }
        public int CountState { get; set; }
        public int CurrentCol { get; set; }
        public int CurrentRow { get; set; }
        public Boolean[,] Body { get; set; }
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
        public object Clone()
        {
            Otetra newTetra = (Otetra)this.MemberwiseClone();
            Dictionary<int, bool[,]> stateOfNewTetra = new Dictionary<int, bool[,]>
            {
                [1] = new bool[2, 2] {      { true, true},
                                            { true, true}, }
            };
            newTetra.FigureState = stateOfNewTetra;
            return newTetra;
        }

    }
}
