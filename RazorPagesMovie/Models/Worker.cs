using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPagesMovie.Models
{
    public class Worker : Person
    {
        public int ID { get; set; }

        public String company { get; set; }

        public Nullable<System.Double> wage { get; set; }

        public virtual ICollection<System.Int32> favouriteNumber { get; set; }
    }
}
