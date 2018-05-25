namespace WebApplication1.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class Car {


		public Nullable<System.Int32> SteeringWheelID { get; set; }
		public Nullable<System.Int32> SuperRadioID { get; set; }
		public Nullable<System.Int32> TireID { get; set; }
		public Nullable<System.Int32> SeatID { get; set; }

		public System.String Brand { get; set; }

		public System.String Model { get; set; }

		public System.String Version { get; set; }

		[ForeignKey("SteeringWheelID")]
		public virtual SteeringWheel SteeringWheel { get; set; }

		[ForeignKey("SuperRadioID")]
		public virtual CarRadio SuperRadio { get; set; }

		[ForeignKey("TireID")]
		public virtual ICollection<Tire> Tire { get; set; }

		[ForeignKey("SeatID")]
		public virtual ICollection<Seat> Seat { get; set; }
	}
}