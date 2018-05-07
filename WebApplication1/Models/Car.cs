namespace WebApplication1.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class Car {



		public String Brand { get; set; }

		public String Model { get; set; }

		public String Version { get; set; }

		public virtual SteeringWheel SteeringWheel { get; set; }

		public virtual CarRadio CarRadio { get; set; }
	}
}