using System;
using TetrisApp.Objects;


namespace TetrisApp
{
    //Интерфейс, отвечающий за процесс игры.
    interface ITetris
    {
        void Play();
        bool GameOver(Tetramino currentFigure, bool[,] buildingArea);
        void ShowResult();
    }
}
