using System;
using TetrisApp.Map;
using TetrisApp.Objects;
using TetrisApp.Helpers;
using TetrisDAL.DataOperations;
using TetrisDAL.Models;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;

namespace TetrisApp
{
    //Класс, отвечающий за процесс игры.
    class Tetris : ITetris
    {
        /// <summary>
        /// Метод старта игры.
        /// </summary>
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
            map.GameData.InsertIntoDB();
        }

        /// <summary>
        /// Метод отображает главное меню.
        /// </summary>
        /// <param name="tetris"></param>
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
                            ShowRecordTable(tetris);
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


        /// <summary>
        /// Метод отображает таблицу рекордов (данные считываются из БД).
        /// </summary>
        /// <param name="tetris"></param>
        public void ShowRecordTable(ITetris tetris)
        {
            Console.Clear();
            Console.WindowWidth = 50;
            Console.BufferWidth = Console.WindowWidth;
            Console.WindowHeight = 17;
            Console.BufferHeight = Console.WindowHeight;

            ResultsDAL results = new ResultsDAL();
            List<TetrisResults> recordList = results.GetTopFive();

            // Если записи в БД отсутствуют, то сообщаем, что запией нет.
            if (recordList == null)
            {
                Console.WriteLine(" Записи отсутствуют.");
                Console.SetCursorPosition(2, 15);
                Console.Write("Нажмите пробел, чтобы вернуться в главное меню");
                while (true)
                {
                    if (Console.KeyAvailable)
                    {
                        var key = Console.ReadKey();
                        if (key.Key == ConsoleKey.Spacebar)
                        {
                            break;
                        }
                        else
                        {
                            Console.SetCursorPosition(0, 14);
                            Console.WriteLine("                                      ");
                        }
                    }
                    Thread.Sleep(100);
                }
                tetris.MainMenuShow(tetris);
            }
            else
            {
                string heading = "Пять лучших результатов";
                int indexOfCentralPosition = (Console.WindowWidth - heading.Length) / 2;
                Console.SetCursorPosition(indexOfCentralPosition, 1);
                Console.Write(heading);

                string table = String.Format("| {0, 1} | {1,-3} | {2,-4} | {3,-3} | {4,-10} |",
                        "№", "Очки", "Время", "Линии", "Имя");
                indexOfCentralPosition = (Console.WindowWidth - table.Length) / 2;
                Console.SetCursorPosition(indexOfCentralPosition, 3);
                Console.Write(table);
                Console.SetCursorPosition(indexOfCentralPosition, 4);
                Console.Write(String.Format("| {0, 1} | {1,-3} | {2,-4} | {3,-3} | {4,-10} |",
                    "-", "----", "-----", "-----", "----------"));

                for (int i = 0; i < recordList.Count; i++)
                {
                    Console.SetCursorPosition(indexOfCentralPosition, i + 5);
                    Console.WriteLine(String.Format("| {0, 1} | {1,-4} | {2:00}:{3:00} | {4,-5} | {5,-10} |",
                        i + 1, recordList[i].Points, TimeSpan.Parse(recordList[i].Time).Minutes, TimeSpan.Parse(recordList[i].Time).Seconds, recordList[i].Lines, recordList[i].Name));
                    Console.SetCursorPosition(indexOfCentralPosition, i + 6);
                }
                Console.WriteLine("| {0, 1} | {1,-3} | {2,-4} | {3,-3} | {4,-10} |",
                   "-", "----", "-----", "-----", "----------");

                Console.WriteLine();
                Console.WriteLine("Нажмите пробел, чтобы вернуться в главное меню");
                while (true)
                {
                    if (Console.KeyAvailable)
                    {
                        var key = Console.ReadKey();
                        if (key.Key == ConsoleKey.Spacebar)
                        {
                            tetris.MainMenuShow(tetris);
                            break;
                        }
                        else
                        {
                            Console.SetCursorPosition(0, 14);
                            Console.WriteLine("                                      ");
                        }
                    }
                    Thread.Sleep(100);
                }
            }
        }

    }
}
