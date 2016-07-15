using System;

namespace InColUn.Db.Models
{
    public class Board
    {
        public ulong id { get; set; }
        public ulong parentid { get; set; }
        public ulong boardid { get; set; }
        public string Title { get; set; }
        public DateTime created { get; set; }
        public DateTime updated { get; set; }
        public string status { get; set; }
    }
}
