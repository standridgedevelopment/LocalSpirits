using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalSpirits.Data
{
    public class City
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        [ForeignKey(nameof(State))]
        public int? StateID { get; set; }
        public virtual State State { get; set; }
        
    }
}
