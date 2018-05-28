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

		[InverseProperty("Kwalifikacje")]
		[ForeignKey("InstruktorID")]
		public virtual Instruktor Instruktor { get; set; }

		[Required]
		public Nullable<System.Int32> InstruktorID { get; set; }

		[InverseProperty("Uprawnieni")]
		[ForeignKey("DyscyplinaZPoziomemID")]
		public virtual DyscyplinaZPoziomem DyscyplinaZPoziomem { get; set; }

		[Required]
		public Nullable<System.Int32> DyscyplinaZPoziomemID { get; set; }
	}
}