namespace WebApplication1.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class Car {



		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Required]
		public System.String Brand { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Required]
		public System.String Model { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Required]
		public System.String Version { get; set; }

		[InverseProperty("RadiosCar")]
		public virtual CarRadio SuperRadio { get; set; }

		[InverseProperty("Car")]
		public virtual ICollection<Tire> Tire { get; set; }

		[InverseProperty("Car")]
		public virtual ICollection<Seat> Seat { get; set; }
	}
}