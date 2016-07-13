using System;

namespace InColUn.Db.Models
{
    public class UserBoard
    {
        public long userid { get; set; }
        public long boardid { get; set; }
        public string relation { get; set; }
        public DateTime timestamp { get; set; }
    }
}
