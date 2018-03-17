namespace Test.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;

	public class Worker : Person {

		public String Company { get; set; }

		public Nullable<Double> Wage { get; set; }

		public virtual ICollection<FavouriteNumber> FavouriteNumber { get; set; }

		public virtual ICollection<Worker> Coworkers { get; set; }
	}
}