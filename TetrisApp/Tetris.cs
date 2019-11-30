using System;
using TetrisApp.Map;
using TetrisApp.Objects;
using TetrisApp.Helpers;
using System.Threading.Tasks;
using System.Threading;

namespace TetrisApp
{
    //Класс, отвечающий за процесс игры.
    class Tetris : ITetris
    {
        // Метод старта игры.
        public void Play()
        {
            Console.Clear();
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
                if (map.MapMechanics.IntersectionCheck(map.CurrentFigure, map.BuildingArea))
                {
                    if (map.MapMechanics.GameOver(map.CurrentFigure, map.BuildingArea))
                        break;
                    map.MapMechanics.PlaceCurrentFigure(map.CurrentFigure, map.BuildingArea);
                    map.GameData.PointsCount(map.MapMechanics.CompletedLines(map.BuildingArea));
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
                map.GameData.Time = CountTime.Elapsed;
                CountTime.Start();

                /* Рисуем панель информации, текущую фигуру и границы.
                 * Устанавливаем задержку перед следующим шагом фигуры.
                 */
                map.InfoArea.DrawInfoArea(map.NextFigure, map.GameData);
                DrawTetra.Draw(map.CurrentFigure.CurrentCol, map.CurrentFigure.CurrentRow, map.CurrentFigure);
                map.InfoArea.DrawBorders();
                Thread.Sleep(300);
            }
            //Результаты текущей игры.
            map.GameData.ShowCurrentResults();
        }

        // Метод отображения главного меню.
        public void MainMenuShow(ITetris tetris)
        {
            Console.Clear();
            Console.Title = "Тетрис";
            Console.CursorVisible = false;
            Console.WindowWidth = 50;
            Console.BufferWidth = Console.WindowWidth;
            Console.WindowHeight = 17;
            Console.BufferHeight = Console.WindowHeight;
            string[] menu = new string[3] { "Начать игру", "Рекорды", "Выход" };
            int[] stringLength = new int[3] { 11, 7, 5 };
            int[] rowPos = new int[3]
            {
                ((Console.WindowHeight - 3) / 2),
                ((Console.WindowHeight - 3) / 2) + 1,
                ((Console.WindowHeight - 3) / 2) + 2
            };
            int curPos = 0;

            while (true)
            {

                Console.SetCursorPosition(15, rowPos[curPos]);
                Console.Write("*");

                Console.SetCursorPosition(2, 15);
                Console.Write("Для выбора нажмите Пробел");

                for (int i = 0; i < 3; i++)
                {
                    Console.SetCursorPosition(((Console.WindowWidth - stringLength[i]) / 2) - 1, rowPos[i]);
                    Console.Write(menu[i]);
                }


                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey();
                    if (key.Key == ConsoleKey.DownArrow)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            Console.SetCursorPosition(15, rowPos[i]);
                            Console.Write(" ");
                        }
                        if (curPos + 1 > 2)
                            curPos = 0;
                        else curPos++;
                        Console.SetCursorPosition(15, rowPos[curPos]);
                        Console.Write("*");
                    }
                    else if (key.Key == ConsoleKey.UpArrow)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            Console.SetCursorPosition(15, rowPos[i]);
                            Console.Write(" ");
                        }
                        if (curPos - 1 < 0)
                            curPos = 2;
                        else curPos--;
                        Console.SetCursorPosition(15, rowPos[curPos]);
                        Console.Write("*");
                    }
                    else if (key.Key == ConsoleKey.Spacebar)
                    {
                        if (curPos == 2)
                            Environment.Exit(0);
                        else if (curPos == 1)
                        {
    //                        _playerResults.Show(tetris);
                            break;
                        }
                        else if (curPos == 0)
                        {
                            Play();
                            break;
                        }
                    }
                    else if (key.Key == ConsoleKey.Escape)
                    {
                        Environment.Exit(0);
                    }
                }
                Thread.Sleep(100);
            }
        }

    }
}
