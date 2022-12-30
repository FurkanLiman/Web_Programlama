using Npgsql;

namespace g201210007_WebOdev.Models
{
    public class UserSql
    {
        public bool UserCheck(Account User)
        {
            using (var connection = new NpgsqlConnection("Server=localhost;Port=5432;Database=postgres;User Id=postgres;Password=postgres;"))
            using (var command = new NpgsqlDataAdapter())
            using (var insertCommand = new NpgsqlCommand("SELECT CASE WHEN EXISTS (SELECT * FROM \"Users\" WHERE \"User_Name\" = '" + User.Email + "' and \"User_Password\" = '" + User.Password + "') THEN CAST(true AS BOOL) ELSE CAST(false AS BOOL) END"))
            {
                insertCommand.Connection = connection;
                command.InsertCommand = insertCommand;


                connection.Open();
                NpgsqlDataReader state = insertCommand.ExecuteReader();
                if (state.Read() && state.GetBoolean(0))
                {
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
                    using (var insertCommandWrite = new NpgsqlCommand("INSERT INTO \"Users\" (\"User_Name\", \"User_Password\")\rVALUES ('" + User.Email + "','" + User.Password + "');"))
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
