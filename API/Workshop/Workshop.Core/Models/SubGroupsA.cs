using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workshop.Core.Models
{
    public class SubGroupsA
    {
        public int Id { get; set; }
        public string Name { get; set; }


        [ForeignKey(nameof(MainGroups))]
        public int MainGroups_Id { get; set; }
        public MainGroups MainGroups { get; set; }

        public ICollection<Items> Items { get; set; } = new HashSet<Items>();
        public ICollection<SubGroupsB> SubGroupsB { get; set; } = new HashSet<SubGroupsB>();
    }
}
