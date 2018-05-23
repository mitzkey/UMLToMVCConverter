namespace WebApplication1.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class Seat {


		public int ID {get; set;}

		public Nullable<System.Boolean> LeatherMade { get; set; }

		[InverseProperty("Seat")]
		[Required]
		public Car Car { get; set; }
	}
}