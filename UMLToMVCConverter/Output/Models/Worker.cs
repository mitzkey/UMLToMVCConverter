using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Test.Models
{
	public class Worker : Person {

		public String company { get; set; }

		public Nullable<Double> wage { get; set; }

		public virtual ICollection<FavouriteNumber> favouriteNumber { get; set; }

		public virtual ICollection<Worker> Coworkers { get; set; }
	}
}