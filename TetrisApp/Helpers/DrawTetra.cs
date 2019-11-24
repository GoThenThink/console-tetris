using System;
using TetrisApp.Objects;

namespace TetrisApp.Helpers
{
    //Вспомогательный статичный класс для отрисовки фигуры.
    static class DrawTetra
    {
        public static void Draw(int xAxis, int yAxis, Tetramino figure)
        {
            for (int row = 0; row < figure.Body.GetLength(0); row++)
            {
                for (int col = 0; col < figure.Body.GetLength(1); col++)
                {
                    if (figure.Body[row, col] == true)
                    {
                        Console.SetCursorPosition(xAxis + col, yAxis + row);
                        Console.Write("*");
                    }
                }
            }
        }
    }
}
