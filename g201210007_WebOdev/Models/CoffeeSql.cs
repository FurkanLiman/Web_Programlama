using Newtonsoft.Json;
using Npgsql;
using NuGet.Packaging;
using NuGet.Protocol;
using System.Collections;
using System.Linq;
using static System.Net.WebRequestMethods;

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
                        coffee.Comments = new();
                        coffee.Comments.AddRange((IEnumerable<string>)(coffees[5] as Array));
                        coffeeList.Add(coffee);

                    }
                    
                }
                else
                {
                    
                }
                return coffeeList;

            }

        }
        public void deleteCommentSql(string id, string comId)
        {
            using (var connection = new NpgsqlConnection("Server=localhost;Port=5432;Database=postgres;User Id=postgres;Password=postgres;"))
            using (var command = new NpgsqlDataAdapter())
            using (var insertCommand = new NpgsqlCommand("select * from \"Coffee\" where \"id\" =" + id))
            {
                insertCommand.Connection = connection;
                command.InsertCommand = insertCommand;

                List<string> coms = new List<string>();
                connection.Open();
                NpgsqlDataReader coffeesy = insertCommand.ExecuteReader();
                if (coffeesy.Read() && coffeesy.GetString(1) != null)
                {

                    

                    
                    coms.AddRange((IEnumerable<string>)(coffeesy[5] as Array));
                    string toplam = "";

                    if (coms != null)
                    {

                        int sayac = 0;
                        foreach (var item in coms)
                        {
                            
                            if (sayac != Int32.Parse(comId) && sayac + 1 != coms.Count())
                            {
                                toplam += "\"" + item + "\",";                            
                            }else if(sayac+1 == coms.Count() && sayac != Int32.Parse(comId)) {
                                toplam += "\"" + item + "\"";
                            }
                            sayac++;
                        }
                    }
                    

                    using (var connection1 = new NpgsqlConnection("Server=localhost;Port=5432;Database=postgres;User Id=postgres;Password=postgres;"))
                    using (var command1 = new NpgsqlDataAdapter())
                    using (var insertCommand1 = new NpgsqlCommand("UPDATE \"Coffee\"\r set \"Coffee_Comment\" = '{" + toplam + "}' \r WHERE \"id\" =" + id.ToString()))
                    {
                        insertCommand1.Connection = connection1;
                        command1.InsertCommand = insertCommand1;


                        connection1.Open();
                        NpgsqlDataReader state = insertCommand1.ExecuteReader();

                    }
                   
                }
                else
                {
                }


            }

        }
        public void AddCoffee(string name, string brand, string taste, string image)
        {
            if(image == null)
            {
                image = "https://images-na.ssl-images-amazon.com/images/I/71nuFLPr7JL._AC_UL600_SR600,600_.jpg";
            }
            using (var connection = new NpgsqlConnection("Server=localhost;Port=5432;Database=postgres;User Id=postgres;Password=postgres;"))
            using (var command = new NpgsqlDataAdapter())
            using (var insertCommand = new NpgsqlCommand("insert into \"Coffee\"(\"Coffee_Name\",\"Coffe_Brand\",\"Coffee_Taste\",\"Coffee_Image\",\"Coffee_Comment\") values ('"+name+"', '"+brand+"','"+taste+"','"+image+"', '{\"\"}')"))
            {
                insertCommand.Connection = connection;
                command.InsertCommand = insertCommand;

                connection.Open();
                NpgsqlDataReader coffeeAdd = insertCommand.ExecuteReader();

            }


        }
        public void DeleteCoffee(string id)
        {
            using (var connection = new NpgsqlConnection("Server=localhost;Port=5432;Database=postgres;User Id=postgres;Password=postgres;"))
            using (var command = new NpgsqlDataAdapter())
            using (var insertCommand = new NpgsqlCommand("DELETE FROM \"Coffee\" WHERE id ="+id))
            {
                insertCommand.Connection = connection;
                command.InsertCommand = insertCommand;

                connection.Open();
                NpgsqlDataReader deleteCoffee= insertCommand.ExecuteReader();
                if (deleteCoffee.Read())
                {
                    Console.WriteLine("silindi");
                }
                else
                {

                }
                

            }
        }
        public Coffee CoffeeReadInfo(int id)
        {
            using (var connection = new NpgsqlConnection("Server=localhost;Port=5432;Database=postgres;User Id=postgres;Password=postgres;"))
            using (var command = new NpgsqlDataAdapter())
            using (var insertCommand = new NpgsqlCommand("select * from \"Coffee\" where \"id\" ="+ id.ToString() ))
            {
                insertCommand.Connection = connection;
                command.InsertCommand = insertCommand;

                Coffee coffee = new Coffee();

                connection.Open();
                NpgsqlDataReader coffees = insertCommand.ExecuteReader();
                if (coffees.Read() && coffees.GetString(1)!=null)
                {
                    coffee.Id = coffees.GetInt32(0);
                    coffee.Name = coffees.GetString(1);
                    coffee.Brand = coffees.GetString(2);
                    coffee.Taste = coffees.GetString(3);
                    coffee.Image = coffees.GetString(4);

                    coffee.Comments = new();
                    coffee.Comments.AddRange((IEnumerable<string>)(coffees[5] as Array));
                  

                }
                else
                {

                }
                return coffee;

            }

        }
        public Coffee MakeComment(string comment, int id)
        {
            using (var connection = new NpgsqlConnection("Server=localhost;Port=5432;Database=postgres;User Id=postgres;Password=postgres;"))
            using (var command = new NpgsqlDataAdapter())
            using (var insertCommand = new NpgsqlCommand("select * from \"Coffee\" where \"id\" =" + id.ToString()))
            {
                insertCommand.Connection = connection;
                command.InsertCommand = insertCommand;

                Coffee coffee = new Coffee();

                connection.Open();
                NpgsqlDataReader coffeesy = insertCommand.ExecuteReader();
                if (coffeesy.Read() && coffeesy.GetString(1) != null)
                {

                    coffee.Id = coffeesy.GetInt32(0);
                    coffee.Name = coffeesy.GetString(1);
                    coffee.Brand = coffeesy.GetString(2);
                    coffee.Taste = coffeesy.GetString(3);
                    coffee.Image = coffeesy.GetString(4);

                    coffee.Comments = new();
                    coffee.Comments.AddRange((IEnumerable<string>)(coffeesy[5] as Array));
                    string toplam = "";
                    if (coffee.Comments != null)
                    {
                        foreach (var item in coffee.Comments)
                        {
                            toplam +="\""+item + "\",";
                        }
                    }
                    toplam += "\"" + comment+ "\"";
                    coffee.Comments.Add(comment);
                    
                    using (var connection1 = new NpgsqlConnection("Server=localhost;Port=5432;Database=postgres;User Id=postgres;Password=postgres;"))
                    using (var command1 = new NpgsqlDataAdapter())
                    using (var insertCommand1 = new NpgsqlCommand("UPDATE \"Coffee\"\r set \"Coffee_Comment\" = '{"+toplam+"}' \r WHERE \"id\" ="+id.ToString()))
                    {
                        insertCommand1.Connection = connection1;
                        command1.InsertCommand = insertCommand1;

                        
                        connection1.Open();
                        NpgsqlDataReader state = insertCommand1.ExecuteReader();

                    }
                    return coffee;
                }
                else
                {
                    //hata verebilir aynı coffee ile 
                    return coffee;
                }
                

            }
        }


    }
}
