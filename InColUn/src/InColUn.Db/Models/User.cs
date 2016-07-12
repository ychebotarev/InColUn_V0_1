using System;

namespace InColUn.Db.Models
{
    public class User
    {
        public long Id { get; set; }
        public string login_string { get; set; }
        public string password_hash { get; set; }
        public int? salt { get; set; }
        public string display_name { get; set; }
        public string email { get; set; }
        public string auth_provider { get; set; }
        public DateTime created { get; set; }
        public string status { get; set; }
    }
}