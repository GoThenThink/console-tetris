using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace TetrisApp.GameStatistics
{
    //Класс, отвечающий за запись и чтение статистики игры.
    class PlayerResults
    {
        private FileInfo HighScores { get; set; } = new FileInfo("Highscores.txt");
        private List<GameData> HighScoresRecords { get; set; } = new List<GameData>();

        // Записываем результаты в текстовый файл.
        public void SaveResults(GameData gamedata)
        {
            HighScoresRecords.Clear();
            if (HighScores.Exists)
            {
                using (StreamReader sr = HighScores.OpenText())
                {
                    //Извлекаем строки из файла и записываем в массив.
                    string line=null;
                    string[] lines;
                    while((line = sr.ReadLine()) !=null)
                    {
                        lines = line.Split(',');
                        HighScoresRecords.Add(new GameData() 
                        { Name=lines[0], Points=int.Parse(lines[1]), CompletedLinesNumber=int.Parse(lines[2]), Time=TimeSpan.Parse(lines[3])});
                    }                  
                }

                //Записываем результат текущей игры в полученный массив.
                HighScoresRecords.Add(gamedata);
                //Сортируем массив по количеству очков.
                HighScoresRecords.Sort(new GameDataComparer());
                //Удаляем файл.
                HighScores.Delete();
                //Создаем новый файл и записываем туда обновленную коллекцию.
                using (StreamWriter sw = HighScores.CreateText())
                {
                    foreach(GameData gd in HighScoresRecords)
                    {
                        sw.WriteLine(gd.Name + "," + gd.Points + "," + gd.CompletedLinesNumber + "," + gd.Time);
                    }
                }
            }
            else using (StreamWriter sw = HighScores.CreateText())
                {
                    sw.Write(gamedata.Name + "," + gamedata.Points + "," + gamedata.CompletedLinesNumber + "," + gamedata.Time);
                }
        }

        // Метод, рисующий таблицу рекордов, вызываемую из главного меню.
        public void Show(ITetris tetris)
        {
            HighScoresRecords.Clear();
            if (HighScores.Exists)
            {
                using (StreamReader sr = HighScores.OpenText())
                {
                    //Извлекаем строки из файла и записываем в массив.
                    string line = null;
                    string[] lines;
                    while ((line = sr.ReadLine()) != null)
                    {
                        lines = line.Split(',');
                        HighScoresRecords.Add(new GameData()
                        { Name = lines[0], Points = int.Parse(lines[1]), CompletedLinesNumber = int.Parse(lines[2]), Time = TimeSpan.Parse(lines[3]) });
                    }
                }

                // Рисуем таблицу рекордов.
                Console.Clear();
                Console.WindowWidth = 50;
                Console.BufferWidth = Console.WindowWidth;
                Console.WindowHeight = 17;
                Console.BufferHeight = Console.WindowHeight;

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

                if (HighScoresRecords.Count >= 5)
                {

                    for (int i = 0; i < 5; i++)
                    {
                        Console.SetCursorPosition(indexOfCentralPosition, i + 5);
                        Console.WriteLine(String.Format("| {0, 1} | {1,-4} | {2:00}:{3:00} | {4,-5} | {5,-10} |",
                            i + 1, HighScoresRecords[i].Points, HighScoresRecords[i].Time.Minutes, HighScoresRecords[i].Time.Seconds, HighScoresRecords[i].CompletedLinesNumber, HighScoresRecords[i].Name));
                        Console.SetCursorPosition(indexOfCentralPosition, i + 6);
                    }
                }
                else
                {
                    for (int i = 0; i < HighScoresRecords.Count; i++)
                    {
                        Console.SetCursorPosition(indexOfCentralPosition, i + 5);
                        Console.WriteLine(String.Format("| {0, 1} | {1,-4} | {2:00}:{3:00} | {4,-5} | {5,-10} |",
                            i + 1, HighScoresRecords[i].Points, HighScoresRecords[i].Time.Minutes, HighScoresRecords[i].Time.Seconds, HighScoresRecords[i].CompletedLinesNumber, HighScoresRecords[i].Name));
                        Console.SetCursorPosition(indexOfCentralPosition, i + 6);
                    }
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
                    }
                    else
                    {
                        Console.SetCursorPosition(0, 14);
                        Console.WriteLine("                                      ");
                    }
                    Thread.Sleep(100);
                }
            }
            else
            {
                // Рисуем таблицу рекордов.
                Console.Clear();
                Console.WindowWidth = 50;
                Console.BufferWidth = Console.WindowWidth;
                Console.WindowHeight = 17;
                Console.BufferHeight = Console.WindowHeight;
                Console.WriteLine(" Записи отсутствуют.");
                Console.SetCursorPosition(2,15);
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
                    }
                    else
                    {
                        Console.SetCursorPosition(0, 14);
                        Console.WriteLine("                                      ");
                    }
                    Thread.Sleep(100);
                }
                tetris.MainMenuShow(tetris);
            }
            ;
        }        
    }
}
