using System;

namespace TetrisApp
{
    //Класс, отвечающий за игровую статистику.
    static class GameData
    {
        public static int Points { get; set; } = 0;
        public static int CompletedLinesNumber { get; set; } = 0;
        public static int LastStreak = 0;
        public static TimeSpan Time { get; set; }

        public static void PointsCount(int disappearedLines)
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

    }
}
