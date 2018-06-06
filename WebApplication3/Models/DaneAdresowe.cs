namespace WebApplication3.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class DaneAdresowe {

		public int ID { get; set; }


		[Required]
		public System.String Email { get; set; }

		[Required]
		public System.String Telefon { get; set; }

		public System.String Ulica { get; set; }

		[Required]
		public System.String Numer { get; set; }

		[Required]
		public System.String KodPocztowy { get; set; }

		[Required]
		public System.String Miejscowosc { get; set; }
	}
}