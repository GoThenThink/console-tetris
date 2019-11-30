using System;
using TetrisApp.Objects;

namespace TetrisApp.Map
{
    // Класс, отвечающий за механику карты.
    class MapMechanics
    {
        //Проверка на столкновение текущей фигуры с построенными блоками.
        public bool IntersectionCheck(ITetramino currentFigure, bool[,] buildingArea)
        {
            if (currentFigure.CurrentRow + currentFigure.Body.GetLength(0) >= buildingArea.GetLength(0))
            {
                return true;
            }

            for (int row = 0; row < currentFigure.Body.GetLength(0); row++)
            {
                for (int col = 0; col < currentFigure.Body.GetLength(1); col++)
                {
                    int lowestBuildingRow = currentFigure.CurrentRow + row + 1;

                    if (currentFigure.Body[row, col] == true &
                        buildingArea[lowestBuildingRow, currentFigure.CurrentCol + col] == true)
                        return true;
                }
            }
            return false;
        }

        //Метод, отвечающий за размещение фигуры среди построенных блоков.
        public void PlaceCurrentFigure(ITetramino currentFigure, bool[,] buildingArea)
        {
            for (int row = 0; row < currentFigure.Body.GetLength(0); row++)
            {
                for (int col = 0; col < currentFigure.Body.GetLength(1); col++)
                {
                    if (currentFigure.Body[row, col])
                        buildingArea[currentFigure.CurrentRow + row, currentFigure.CurrentCol + col] = true;
                }
            }
        }

        //Проверяем заполнена ли линия и подсчитываем количество таковых. 
        public int CompletedLines(bool[,] buildingArea)
        {
            int disappearedLines = 0;
            for (int row = 0; row < buildingArea.GetLength(0); row++)
            {
                bool completeLine = true;
                for (int col = 0; col < buildingArea.GetLength(1); col++)
                {
                    if (buildingArea[row, col] == false)
                    {
                        completeLine = false;
                        break;
                    }
                }

                if (completeLine)
                {
                    for (int nextLine = row - 1; nextLine >= 0; nextLine--)
                    {
                        if (row < 0)
                        {
                            continue;
                        }

                        for (int colFromNextLine = 0; colFromNextLine < buildingArea.GetLength(1); colFromNextLine++)
                        {
                            buildingArea[nextLine + 1, colFromNextLine] =
                                buildingArea[nextLine, colFromNextLine];
                        }
                    }
                    for (int firstLine = 0; firstLine < buildingArea.GetLength(1); firstLine++)
                    {
                        buildingArea[0, firstLine] = false;
                    }
                    disappearedLines++;
                }
            }
            return disappearedLines;
        }

        //Метод, отвечающий за отрисовку поля построенных блоков.
        public void DrawBuildingArea(bool[,] buildingArea)
        {
            for (int row = 0; row < buildingArea.GetLength(0); row++)
            {
                for (int col = 0; col < buildingArea.GetLength(1); col++)
                {
                    if (buildingArea[row, col] == true)
                    {
                        Console.SetCursorPosition(col, row);
                        Console.Write("*");
                    }
                    else
                    {
                        Console.SetCursorPosition(col, row);
                        Console.Write(" ");
                    }
                }
            }
        }

        //Метод, описывающий условие конца игры.
        public bool GameOver(ITetramino currentFigure, bool[,] buildingArea)
        {
            bool gameOver = false;
            if (currentFigure.CurrentRow == 0)
                for (int row = 0; row < currentFigure.Body.GetLength(0); row++)
                    for (int col = 0; col < currentFigure.Body.GetLength(1); col++)
                    {
                        if (currentFigure.Body[row, col] == true & buildingArea[currentFigure.CurrentRow + row, currentFigure.CurrentCol + col] == true)
                        {
                            return true;
                        }
                        else gameOver = false;
                    }
            else gameOver = false;
            return gameOver;
        }
    }
}
