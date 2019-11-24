using System;
using TetrisApp.Objects;
using System.Collections.Generic;
using TetrisApp.Helpers;

namespace TetrisApp.Map
{
    // Класс, отвечающий за создание карты игры.
    class MapBuilder : IMapBuilder
    {
        private readonly int playAreaWidth;
        private readonly int infoAreaWidth;
        private readonly int playAreaHeight;
        private readonly List<Tetramino> _listOfTetramino;  //Список типов фигур.
        private readonly bool[,] BuildingArea;              //Область построенных блоков.
        private readonly InfoArea infoArea;
        private readonly NewFigure newFigure;               //Генерирование новой фигуры.
        private readonly MapMechanics mapMechanics;         //Игровые механики.


        public MapBuilder()
        {
            playAreaWidth = 9;
            infoAreaWidth = 15;
            playAreaHeight = 20;
            BuildingArea = new bool[playAreaHeight+1, playAreaWidth+1];
            _listOfTetramino = new List<Tetramino>() {  new Ltetra(), new Jtetra(), new Ztetra(),
                                                        new Stetra(), new Ttetra(), new Itetra(),
                                                        new Otetra() };
            newFigure = new NewFigure(_listOfTetramino);
            infoArea = new InfoArea(playAreaWidth, playAreaHeight);
            mapMechanics = new MapMechanics();
        }

        public IMap Build()
        {
            Console.Title = "Тетрис";
            Console.WindowWidth = playAreaWidth + infoAreaWidth;
            Console.BufferWidth = Console.WindowWidth;
            Console.WindowHeight = playAreaHeight + 2;      // 1 это Границы
            Console.BufferHeight = Console.WindowHeight;
            Console.CursorVisible = false;
            infoArea.DrawBorders();

            return new Map()
            {
                ListOfTetramino = _listOfTetramino,
                BuildingArea = this.BuildingArea,
                InfoArea = this.infoArea,
                NextFigure = newFigure.GenerateFigure(),
                CurrentFigure = newFigure.GenerateFigure(),
                NewFigureRng = newFigure,
                MapMechanics = mapMechanics
            };
        }


    }
}
