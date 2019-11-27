using System;
using TetrisApp.Helpers;

namespace TetrisApp
{
    class Program
    {
        static void Main(string[] args)
        {
            PlaySound.Playsound();
            ITetris tetris = new Tetris();
            tetris.InitResults();
            while (true)
            {
                tetris.MainMenuShow(tetris);           
            }

        }
    }
}
