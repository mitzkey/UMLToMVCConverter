namespace WebApplication1.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class Tire {


		public int ID {get; set;}
		public System.String CarBrand { get; set; }
		public System.String CarModel { get; set; }
		public System.String CarVersion { get; set; }

		public System.String Brand { get; set; }

		public virtual Car Car { get; set; }
	}
}