namespace WebApplication4.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class Skala {

		public int ID { get; set; }


		[Required]
		public System.String Nazwa { get; set; }

		public System.Int64 Wielkosc { get; set; }

		public System.Int32 WartoscOd { get; set; } = 0;

		[InverseProperty("Skala")]
		public virtual ICollection<Pozycja> Pozycje { get; set; }
	}
}