namespace WebApplication2.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class CertyfikowaneKwalifikacjeInstruktor {

		public int ID { get; set; }

		public Nullable<System.Int32> CertyfikowaneKwalifikacjeID { get; set; }

		[ForeignKey("CertyfikowaneKwalifikacjeID")]
		public virtual DyscyplinaZPoziomem CertyfikowaneKwalifikacje { get; set; }
	}
}