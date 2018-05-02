namespace WebApplication1.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;

	public class Worker : Person {
		public Nullable<Int32> EnterpriseID { get; set; }

		public String Company { get; set; }

		public Nullable<Double> Wage { get; set; }

		public virtual ICollection<FavouriteNumber> FavouriteNumber { get; set; }

		public virtual ICollection<Worker> Coworkers { get; set; }

		public String NamesFirstCharacter { get { throw new NotImplementedException(); } }

		public virtual Enterprise Enterprise { get; set; }
	}
}