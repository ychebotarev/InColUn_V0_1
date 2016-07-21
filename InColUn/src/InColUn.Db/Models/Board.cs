using System;

namespace InColUn.Db.Models
{
    public class Board
    {
        public long id { get; set; }
        public long parentid { get; set; }
        public long boardid { get; set; }
        public string Title { get; set; }
        public DateTime created { get; set; }
        public DateTime updated { get; set; }
        public string status { get; set; }
    }
}
