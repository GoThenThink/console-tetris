using System;
using System.Collections.Generic;
using TetrisApp.Map;
using TetrisApp.Objects;


namespace TetrisApp.Helpers
{
    //Класс, отвечающий за генерирование новой фигуры.
    class NewFigure
    {
        private readonly Random blessRng = new Random();
        private List<ITetramino> _listOfTetramino;

        public ITetramino GenerateFigure()
        {
            _listOfTetramino = new List<ITetramino>() {  new Ltetra(), new Jtetra(), new Ztetra(),
                                                        new Stetra(), new Ttetra(), new Itetra(),
                                                        new Otetra() };
            ITetramino temp = _listOfTetramino[blessRng.Next(_listOfTetramino.Count)];
            temp.CurrentRow = 0;
            temp.CurrentCol = 4;
            return temp;
        }

    }
}
