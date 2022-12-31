using Npgsql;
using System.Data;

namespace g201210007_WebOdev.Models
{
    public class UserSql
    {
        public Account UserInfo(string usernmail)
        {
            Account account = new Account(usernmail,"*");
            using (var connection = new NpgsqlConnection("Server=localhost;Port=5432;Database=postgres;User Id=postgres;Password=postgres;"))
            using (var command = new NpgsqlDataAdapter())
            using (var insertCommand = new NpgsqlCommand("(SELECT * FROM \"Users\" WHERE \"User_Email\" = '" + usernmail + "')"))
            {
                insertCommand.Connection = connection;
                command.InsertCommand = insertCommand;


                connection.Open();
                NpgsqlDataReader userDetail = insertCommand.ExecuteReader();
                if (userDetail.Read())
                {
                    account.Email = userDetail.GetString(1);
                    account.Name = userDetail.GetString(3); 
                    account.Authority = userDetail.GetString(4);
                }
                else
                {

                }


            }
            return account;

        }
        public bool UserCheck(Account User)
        {
            //"SELECT CASE WHEN EXISTS (SELECT * FROM \"Users\" WHERE \"User_Name\" = '" + User.Email + "' and \"User_Password\" = '" + User.Password + "') THEN CAST(true AS BOOL) ELSE CAST(false AS BOOL) END"))"
            using (var connection = new NpgsqlConnection("Server=localhost;Port=5432;Database=postgres;User Id=postgres;Password=postgres;"))
            using (var command = new NpgsqlDataAdapter())
            using (var insertCommand = new NpgsqlCommand("(SELECT \"User_Name\" FROM \"Users\" WHERE \"User_Email\" = '" + User.Email + "' and \"User_Password\" = '"+User.Password+"')"))
            {
                insertCommand.Connection = connection;
                command.InsertCommand = insertCommand;


                connection.Open();
                NpgsqlDataReader state = insertCommand.ExecuteReader();
                if (state.Read() && state.GetString(0)!= null)
                {
                    User.Name= state.GetString(0);
                    return true;
                }
                else
                {
                    return false;
                }


            }

        }
        public bool UserWrite(Account User)
        {

            using (var connection = new NpgsqlConnection("Server=localhost;Port=5432;Database=postgres;User Id=postgres;Password=postgres;"))
            using (var command = new NpgsqlDataAdapter())
            using (var insertCommand = new NpgsqlCommand("SELECT CASE WHEN EXISTS (SELECT * FROM \"Users\" WHERE \"User_Name\" = '" + User.Email + "') THEN CAST(true AS BOOL) ELSE CAST(false AS BOOL) END"))
            {
                insertCommand.Connection = connection;
                command.InsertCommand = insertCommand;

                connection.Open();
                NpgsqlDataReader state = insertCommand.ExecuteReader();
                if (state.Read() && state.GetBoolean(0))
                {
                    return false;
                }
                else
                { // sistemde kayıtlı kişi yoksa
                    using (var connectionWrite = new NpgsqlConnection("Server=localhost;Port=5432;Database=postgres;User Id=postgres;Password=postgres;"))
                    using (var commandWrite = new NpgsqlDataAdapter())
                    using (var insertCommandWrite = new NpgsqlCommand("INSERT INTO \"Users\" (\"User_Email\", \"User_Password\",\"User_Name\" )\rVALUES ('" + User.Email + "','" + User.Password + "','"+User.Name+"');"))
                    {
                        insertCommandWrite.Connection = connectionWrite;
                        commandWrite.InsertCommand = insertCommandWrite;

                        connectionWrite.Open();
                        NpgsqlDataReader write = insertCommandWrite.ExecuteReader();
                    }
                    return true;
                }

            }
           
        }
    }
}
