using System;
using TetrisApp.Map;


namespace TetrisApp
{
    //Интерфейс, отвечающий за процесс игры.
    interface ITetris
    {
        void MainMenuShow(ITetris tetris);
        void Play();
        void ShowRecordTable(ITetris tetris);
    }
}
