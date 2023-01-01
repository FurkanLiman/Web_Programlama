using System.Linq;

namespace g201210007_WebOdev.Models
{
    public class deneme
    {
        public string Id { get; set; }
        public string Name { get; set; }  
     


    }
    public class LinqTranslate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public void ssaa()
        {
            List<deneme> denemes = new List<deneme>()
            {
               new deneme{Id= "ses", Name="ver"},
               new deneme{Id= "ses", Name="ver"},
               new deneme{Id= "ses", Name="ver"}
        };
            var result = denemes.Where(test => test.Id == "ses");

        }
    }
}
