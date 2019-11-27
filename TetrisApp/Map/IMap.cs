using System;
using System.Collections.Generic;
using TetrisApp.Objects;
using TetrisApp.Helpers;

namespace TetrisApp.Map
{
    //Интерфейс, представляющий объекты на карте.
    interface IMap
    {
        List<ITetramino> ListOfTetramino { get; }        //Список фигур.
        bool[,] BuildingArea { get; set; }              //Область поставленных блоков.
        InfoArea InfoArea { get; set; }                 //Панель информации.
        ITetramino CurrentFigure { get; set; }           //Текущая фигура.
        ITetramino NextFigure { get; set; }              //Следующая фигура.
        NewFigure NewFigureRng { get; set; }            //Генератор следующей фигуры.
        MapMechanics MapMechanics { get; set; }         //Игровые механики.
        GameData GameData { get; set; }                 //Статистика.
    }
}
