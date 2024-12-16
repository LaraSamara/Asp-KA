using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workshop.Core.Models;

namespace Workshop.Core.Dto_s.Items
{
    public class ItemsDto
    {
        public int Id { get; set; } 
        public string Name {  get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public IEnumerable<string> ItemsUnits { get; set; }
        public IEnumerable<string> Stores { get; set; }
    }
}
