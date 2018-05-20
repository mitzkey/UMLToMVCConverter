namespace WebApplication1.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class SteeringWheel {


		public int ID {get; set;}

		public Nullable<System.Double> Perimeter { get; set; }

		[Required]
		[InverseProperty("SteeringWheel")]
		public Car Car { get; set; }
	}
}