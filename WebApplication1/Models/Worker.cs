namespace WebApplication1.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class Worker : Person {


		public Nullable<System.Int32> EnterpriseID { get; set; }

		public System.String Company { get; set; }

		public Nullable<System.Double> Wage { get; set; }

		public virtual ICollection<FavouriteNumber> FavouriteNumber { get; set; }

		public virtual ICollection<Worker> Coworkers { get; set; }

		public System.String NamesFirstCharacter { get { throw new NotImplementedException(); } }

		public virtual Enterprise Enterprise { get; set; }
	}
}