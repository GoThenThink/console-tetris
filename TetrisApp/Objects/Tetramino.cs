using System;
using System.Collections.Generic;

namespace TetrisApp.Objects
{
    // Базовый класс для фигур.
    abstract class Tetramino : ICloneable
    {
        public Dictionary<int, bool[,]> FigureState { get; set; }
        public string Name { get; set; }            //Название фигуры.
        public int State { get; set; }              //Положение фигуры.
        public int CountState { get; set; }         //Максимально возможное количество положений фигуры.
        public int CurrentCol { get; set; } = 0;    //Координата x.
        public int CurrentRow { get; set; }         //Координата y.
        public Boolean[,] Body { get; set; }        //Фигура.
        public object Clone()
        {
            Tetramino newTetra = (Tetramino)this.MemberwiseClone();
            Dictionary<int, bool[,]> stateOfNewTetra = new Dictionary<int, bool[,]>();
            newTetra.FigureState = stateOfNewTetra;
            return newTetra;
        }

    }
}
