using Npgsql;
using System.Collections;

namespace g201210007_WebOdev.Models
{
    public class CoffeeSql
    {

        public List<Coffee> CoffeeRead()
        {
            using (var connection = new NpgsqlConnection("Server=localhost;Port=5432;Database=postgres;User Id=postgres;Password=postgres;"))
            using (var command = new NpgsqlDataAdapter())
            using (var insertCommand = new NpgsqlCommand("select * from \"Coffee\""))
            {
                insertCommand.Connection = connection;
                command.InsertCommand = insertCommand;

                List<Coffee> coffeeList = new List<Coffee>();

                connection.Open();
                NpgsqlDataReader coffees = insertCommand.ExecuteReader();
                if (coffees.HasRows == true)
                {
                    while (coffees.Read())
                    {
                        Coffee coffee = new Coffee();
                        coffee.Id = coffees.GetInt32(0);
                        coffee.Name = coffees.GetString(1);
                        coffee.Brand = coffees.GetString(2);
                        coffee.Taste =  coffees.GetString(3);
                        coffee.Image = coffees.GetString(4);
                        coffeeList.Add(coffee);

                    }
                    
                }
                else
                {
                    
                }
                return coffeeList;

            }

        }

    }
}
