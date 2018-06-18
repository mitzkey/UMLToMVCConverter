namespace WebApplication3.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class Autor {

		public int ID { get; set; }


		[Required]
		public System.String Imie { get; set; }

		[Required]
		public System.String Nazwisko { get; set; }

		public System.DateTime DataUrodzenia { get; set; }

		public System.Boolean Korespondent { get; set; } = true;

		[Required]
		public System.String Pesel { get; set; }

		[ForeignKey("DaneAdresoweID")]
		public virtual DaneAdresowe DaneAdresowe { get; set; }

		[Required]
		public Nullable<System.Int32> DaneAdresoweID { get; set; }

		[InverseProperty("Autorzy")]
		public virtual ICollection<AutorzyPraca> Praca { get; set; }
	}
}