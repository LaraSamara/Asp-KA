using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workshop.Core.Models
{
    public class MainGroups
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Items> Items { get; set; }
        public ICollection<SubGroupsA> SubGroupsA { get; set; } = new HashSet<SubGroupsA>();
        public ICollection<SubGroupsB> SubGroupsB { get; set; } = new HashSet<SubGroupsB>();
    }
}
