namespace WebApplication1.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class CarRadio {


		public int ID {get; set;}
		public System.String RadiosCarBrand { get; set; }
		public System.String RadiosCarModel { get; set; }
		public System.String RadiosCarVersion { get; set; }

		public System.String Producer { get; set; }

		[InverseProperty("SuperRadio")]
		[ForeignKey("RadiosCarBrand,RadiosCarModel,RadiosCarVersion")]
		public virtual Car RadiosCar { get; set; }
	}
}