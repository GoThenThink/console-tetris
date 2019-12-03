using System;
using TetrisApp.Helpers;
using System.Data.SQLite;
using TetrisDAL.DataOperations;
using TetrisDAL.Models;

namespace TetrisApp
{
    class Program
    {
        static void Main(string[] args)
        {
            PlaySound.Playsound();
            ITetris tetris = new Tetris();
            while (true)
            {
                tetris.MainMenuShow(tetris);
            }
        }
    }
}
