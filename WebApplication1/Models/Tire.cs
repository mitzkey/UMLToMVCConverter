namespace WebApplication1.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class Tire {

		public int ID { get; set; }


		[Required]
		public System.String Brand { get; set; }

		[InverseProperty("Tire")]
		[ForeignKey("CarBrand,CarModel,CarVersion")]
		public virtual Car Car { get; set; }

		[Required]
		public System.String CarBrand { get; set; }

		[Required]
		public System.String CarModel { get; set; }

		[Required]
		public System.String CarVersion { get; set; }
	}
}