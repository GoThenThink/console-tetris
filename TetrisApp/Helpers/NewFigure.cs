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
        private readonly List<Tetramino> _listOfTetramino;
        public NewFigure(List<Tetramino> listOfTetramino)
        {
            _listOfTetramino = listOfTetramino;
        }

        public Tetramino GenerateFigure()
        {
            Tetramino temp = _listOfTetramino[blessRng.Next(_listOfTetramino.Count)];
            temp.CurrentRow = 0;
            temp.CurrentCol = 4;
            return temp;
        }

    }
}
