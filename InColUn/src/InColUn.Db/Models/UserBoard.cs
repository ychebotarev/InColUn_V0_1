using System;

namespace InColUn.Db.Models
{
    public class UserBoard
    {
        public ulong userid { get; set; }
        public ulong boardid { get; set; }
        public string relation { get; set; }
        public DateTime timestamp { get; set; }
    }
}
