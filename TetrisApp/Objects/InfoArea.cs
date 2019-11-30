using System;
using TetrisApp.Helpers;
using System.Collections.Generic;

namespace TetrisApp.Objects
{
    //Класс, отвечающий за отображение информационной панели и границ.
    class InfoArea
    {
        private readonly int InfoAreaCol;
        private readonly int playAreaWidth;
        private readonly int playAreaHeight;
        public Dictionary<int, string> Message = new Dictionary<int, string>()
        {
            [0]="",
            [1]="",
            [2]="2-ое комбо!",
            [3]="3-ое комбо!",
            [4]="Тетрис!    "
        };

        public InfoArea(int paW, int paH)
        {
            playAreaWidth = paW;
            playAreaHeight = paH;
            InfoAreaCol = paW + 2;
        }

        //Метод, отвечающий за отрисовку информационной панели.
        public void DrawInfoArea(ITetramino nextFigure, GameData gd)
        {
            ClearInfoArea(InfoAreaCol + 4, 3);
            Console.SetCursorPosition(InfoAreaCol, 1);
            Console.Write("Следующая:");
            DrawTetra.Draw(InfoAreaCol + 4, 3, nextFigure);
            Console.SetCursorPosition(InfoAreaCol, 8);
            Console.Write("Очки: {0}", gd.Points);
            Console.SetCursorPosition(InfoAreaCol, 10);
            Console.Write("Линии: {0}", gd.CompletedLinesNumber);
            Console.SetCursorPosition(InfoAreaCol, 12);
            Console.Write("Время: {0:00}:{1:00}", gd.Time.Minutes, gd.Time.Seconds);
            Console.SetCursorPosition(InfoAreaCol, 15);
            Console.Write(Message[gd.LastStreak]);

        }

        //Метод, отвечающий за очистку области отображения поля "Следующая фигура".
        private void ClearInfoArea(int xAxis, int yAxis)
        {
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    Console.SetCursorPosition(xAxis + col, yAxis + row);
                    Console.Write(" ");
                }
            }
        }

        // Метод, отвечающий за отрисовку границ.
        public void DrawBorders()
        {
            Console.SetCursorPosition(0, playAreaHeight + 1);
            for (int i = 0; i <= playAreaWidth; i++)
            {
                Console.Write("-");
            }
            Console.SetCursorPosition(playAreaWidth + 1, 0);
            for (int i = 0; i < Console.WindowHeight; i++)
            {
                Console.SetCursorPosition(playAreaWidth + 1, 0 + i);
                Console.Write("|");
            }

        }
    }
}
