namespace WebApplication2.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class SzczegolyKwalifikacji {

		public int ID { get; set; }


		public Nullable<System.Int32> Priorytet { get; set; }

		public Nullable<System.Boolean> Certyfikat { get; set; }

		public Nullable<System.Double> StawkaZaZajecia { get; set; }
	}
}