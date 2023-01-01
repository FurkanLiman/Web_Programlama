using NuGet.Protocol.Core.Types;
using System.Linq;
using System.Xml.Linq;

namespace g201210007_WebOdev.Models
{
    public class LinqCoffees
    {
      
        public List<Coffee> askComm(List<Coffee> coffees)
        {
            return coffees.Where(x => x.Comments.Count() > 1).ToList();
        }
        public List<Coffee> askS (List<Coffee> coffees, string sthing)
        {
            return coffees.Where(x => x.Name.Contains(sthing) || x.Brand.Contains(sthing) || x.Taste.Contains(sthing)).ToList();
        }
    }
}
