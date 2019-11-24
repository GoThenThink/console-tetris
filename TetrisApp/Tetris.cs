using System;
using TetrisApp.Map;
using TetrisApp.Objects;
using TetrisApp.Helpers;
using System.Threading.Tasks;
using System.Threading;

namespace TetrisApp
{
    //Класс, отвечающий за процесс игры.
    class Tetris :ITetris
    {
        // Метод старта игры.
        public void Play()
        {
            //Строим игровую карту.
            var map = new MapBuilder().Build();

            //Рисуем первую фигуру.
            DrawTetra.Draw(map.CurrentFigure.CurrentCol, map.CurrentFigure.CurrentRow, map.CurrentFigure);

            //Подсчет времени игры.
            System.Diagnostics.Stopwatch CountTime = new System.Diagnostics.Stopwatch();

            while (true)
            {
                CountTime.Start();
                if (Console.KeyAvailable)
                {
                    FigureMoving.Moving(map);
                }

                /* Сначала проверяем на предмет столкновения фигуры с построенными блоками.
                 * Если было столкновение, то проверяем на предмет выполнения условий конца игры.
                 * Если игра не окончена, то размещаем фигуру среди блоков.
                 * Далее проверяем не заполнилась ли линия и подсчитываем очки.
                 * Генерируем следующую фигуру.
                 * 
                 * Если столкновения не было, то смещаем фигуру на 1 клетку вниз.
                 */
                if(map.MapMechanics.IntersectionCheck(map.CurrentFigure, map.BuildingArea))
                {
                    if (GameOver(map.CurrentFigure, map.BuildingArea))
                        break;
                    map.MapMechanics.PlaceCurrentFigure(map.CurrentFigure, map.BuildingArea);
                    GameData.PointsCount(map.MapMechanics.CompletedLines(map.BuildingArea));
                    map.CurrentFigure = map.NextFigure;
                    map.NextFigure = map.NewFigureRng.GenerateFigure();
                }
                else
                {
                    map.CurrentFigure.CurrentRow++;
                }

                //Рисуем поле построенных блоков (двумерный массив из булевых переменных).
                map.MapMechanics.DrawBuildingArea(map.BuildingArea);

                CountTime.Stop();
                GameData.Time = CountTime.Elapsed;
                CountTime.Start();

                /* Рисуем панель информации, текущую фигуру и границы.
                 * Устанавливаем задержку перед следующим шагом фигуры.
                 */
                map.InfoArea.DrawInfoArea(map.NextFigure);
                DrawTetra.Draw(map.CurrentFigure.CurrentCol, map.CurrentFigure.CurrentRow, map.CurrentFigure);
                map.InfoArea.DrawBorders();
                Thread.Sleep(300);
            }
        }

        //Метод, описывающий условие конца игры.
        public bool GameOver(Tetramino currentFigure, bool[,] buildingArea)
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

        //Метод, показывающий результаты.
        public void ShowResult()
        {
            Console.Clear();
            Console.WindowWidth = 30;
            Console.BufferWidth = Console.WindowWidth;
            Console.WindowHeight = 15; 
            Console.BufferHeight = Console.WindowHeight;
            Console.WriteLine();
            Console.WriteLine(" Игра окончена.");
            Console.WriteLine();
            Console.WriteLine(" Набранные очки: {0}", GameData.Points);
            Console.WriteLine();
            Console.WriteLine(" Проведенное время: {0:00}:{1:00}\n", GameData.Time.Minutes, GameData.Time.Seconds);
        }
    }
}
