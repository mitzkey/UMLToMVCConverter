namespace WebApplication1.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class Car {



		public System.String Brand { get; set; }

		public System.String Model { get; set; }

		public System.String Version { get; set; }

		[InverseProperty("Car")]
		public SteeringWheel SteeringWheel { get; set; }

		[InverseProperty("Car")]
		public CarRadio CarRadio { get; set; }

		[InverseProperty("Car")]
		public virtual ICollection<Tire> Tire { get; set; }

		[InverseProperty("Car")]
		public virtual ICollection<Seat> Seat { get; set; }
	}
}