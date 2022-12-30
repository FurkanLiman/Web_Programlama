using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Npgsql;
using static System.Net.Mime.MediaTypeNames;


namespace g201210007_WebOdev.Models
{
    public class Account
    {
        
        public string Email { get; set; } 
        public string Password { get; set; }    
        
        public Account(string email, string password)
        {
            this.Email = email;
            this.Password = password;
        }
        public bool UserControl(Account User)
        {
            UserSql sqlQuestioning = new UserSql();
            return sqlQuestioning.UserCheck(User);
        }
        public bool UserWriteSql(Account User) {
            UserSql sqlkWrite= new UserSql();
            return sqlkWrite.UserWrite(User);

        }
    }
}
