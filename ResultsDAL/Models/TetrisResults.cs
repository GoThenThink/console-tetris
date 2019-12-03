using System;

namespace TetrisDAL.Models
{
    // Аналог GameData класса для приема статистики с БД.
    public class TetrisResults
    {
        public int RecordID { get; set; }
        public string Name { get; set; }
        public int Points { get; set; }
        public int Lines { get; set; }
        public string Time { get; set; }
    }
}
