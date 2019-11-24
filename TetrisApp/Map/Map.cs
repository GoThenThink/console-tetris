using System;
using System.Collections.Generic;
using TetrisApp.Objects;
using TetrisApp.Helpers;

namespace TetrisApp.Map
{
    //Класс, представляющий объекты на карте.
    class Map : IMap
    {
        public List<Tetramino> ListOfTetramino { get; set; }    //Список фигур.
        public bool[,] BuildingArea { get; set; }               //Область поставленных блоков.
        public InfoArea InfoArea { get; set; }                  //Панель информации.
        public Tetramino CurrentFigure { get; set; }            //Текущая фигура.
        public Tetramino NextFigure { get; set; }               //Следующая фигура.
        public NewFigure NewFigureRng { get; set; }             //Генератор следующей фигуры.
        public MapMechanics MapMechanics { get; set; }          //Игровые механики.

    }
}
