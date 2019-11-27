using System;
using TetrisApp.Objects;
using TetrisApp.Map;
using TetrisApp.GameStatistics;


namespace TetrisApp
{
    //Интерфейс, отвечающий за процесс игры.
    interface ITetris
    {
        void MainMenuShow(ITetris tetris);
        ITetris InitResults();
        void Play();
    }
}
