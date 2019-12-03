using System;
using System.Collections.Generic;
using System.Threading;
using TetrisDAL.DataOperations;
using TetrisDAL.Models;

namespace TetrisApp
{
    //Класс, отвечающий за игровую статистику.
    class GameData
    {
        public string Name { get; set; }
        public int Points { get; set; } = 0;
        public int CompletedLinesNumber { get; set; } = 0;
        public int LastStreak = 0;
        public TimeSpan Time { get; set; }

        /// <summary>
        /// Метод подсчета статистики текущей игры.
        /// </summary>
        /// <param name="disappearedLines"></param>
        public void PointsCount(int disappearedLines)
        {
            LastStreak = disappearedLines;
            CompletedLinesNumber += disappearedLines;
            switch(disappearedLines)
            {
                case 1:
                    Points += 100;
                    break;
                case 2:
                    Points += 200;
                    break;
                case 3:
                    Points += 400;
                    break;
                case 4:
                    Points += 800;
                    break;
            }
        }

        /// <summary>
        /// Результаты последней игры.
        /// </summary>
        public void ShowCurrentResults()
        {
            Console.Clear();
            Console.WindowWidth = 50;
            Console.BufferWidth = Console.WindowWidth;
            Console.WindowHeight = 17;
            Console.BufferHeight = Console.WindowHeight;
            string playerName;
            bool check = false;

            while (check == false)
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine(" Игра окончена.");
                Console.WriteLine();
                Console.WriteLine(" Набранные очки: {0}", Points);
                Console.WriteLine();
                Console.WriteLine(" Проведенное время: {0:00}:{1:00}\n", Time.Minutes, Time.Seconds);
                Console.WriteLine();
                Console.WriteLine(" Введите ваше имя (не более 9 симоволов)");
                Console.WriteLine();
                playerName = Console.ReadLine();
                Console.WriteLine();
                if (playerName.Length > 9)
                {
                    Console.WriteLine("Вы ввели больше 9 символов :(");
                    check = false;
                    Thread.Sleep(3000);
                }
                else if (playerName == "")
                {
                    playerName = "Без имени";
                    Name = playerName;
                    check = true;
                }
                else
                {
                    Name = playerName;
                    check = true;
                }
            }
        }

        /// <summary>
        /// Запись в БД.
        /// </summary>
        public void InsertIntoDB()
        {
            ResultsDAL results = new ResultsDAL();
            results.InsertNewRecord(new TetrisDAL.Models.TetrisResults
            {
                Name = this.Name,
                Points = this.Points,
                Lines = this.CompletedLinesNumber,
                Time = this.Time.ToString()
            });
        }
    }

    
}
