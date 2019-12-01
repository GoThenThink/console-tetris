using System;

namespace TetrisDAL.Models
{
    public class TetrisResults
    {
        public int RecordID { get; set; }
        public string Name { get; set; }
        public int Points { get; set; }
        public int Lines { get; set; }
        public string Time { get; set; }
    }
}
