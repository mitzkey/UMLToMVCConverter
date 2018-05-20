namespace WebApplication1.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class Worker : Person {



		public System.String Company { get; set; }

		public Nullable<System.Double> Wage { get; set; }

		public virtual ICollection<FavouriteNumber> FavouriteNumber { get; set; }

		public virtual ICollection<Worker> Coworkers { get; set; }

		public System.String NamesFirstCharacter { get { throw new NotImplementedException(); } }

		[InverseProperty("Worker")]
		public Enterprise Enterprise { get; set; }
	}
}