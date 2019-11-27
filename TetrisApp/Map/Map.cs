using System;
using System.Collections.Generic;
using TetrisApp.Objects;
using TetrisApp.Helpers;

namespace TetrisApp.Map
{
    //Класс, представляющий объекты на карте.
    class Map : IMap
    {
        public List<ITetramino> ListOfTetramino { get; set; }    //Список фигур.
        public bool[,] BuildingArea { get; set; }               //Область поставленных блоков.
        public InfoArea InfoArea { get; set; }                  //Панель информации.
        public ITetramino CurrentFigure { get; set; }            //Текущая фигура.
        public ITetramino NextFigure { get; set; }               //Следующая фигура.
        public NewFigure NewFigureRng { get; set; }             //Генератор следующей фигуры.
        public MapMechanics MapMechanics { get; set; }          //Игровые механики.
        public GameData GameData { get; set; }                  //Статистика.

    }
}
