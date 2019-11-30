using System;
using System.Collections.Generic;

namespace TetrisApp.Objects
{
    interface ITetramino : ICloneable
    {
        Dictionary<int, bool[,]> FigureState { get; set; }
        string Name { get; set; }            //Название фигуры.
        int State { get; set; }              //Положение фигуры.
        int CountState { get; set; }         //Максимально возможное количество положений фигуры.
        int CurrentCol { get; set; }         //Координата x.
        int CurrentRow { get; set; }         //Координата y.
        Boolean[,] Body { get; set; }        //Фигура.
    }
}
