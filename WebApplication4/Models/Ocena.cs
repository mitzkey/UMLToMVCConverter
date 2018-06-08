namespace WebApplication4.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class Ocena {

		public int ID { get; set; }


		[ForeignKey("WybranaWartoscWartosc")]
		public virtual Pozycja WybranaWartosc { get; set; }

		[Required]
		public System.Int32 WybranaWartoscWartosc { get; set; }

		[InverseProperty("PrzedmiotBadaniaOcena")]
		[ForeignKey("OdpowiedzID")]
		public virtual Odpowiedz Odpowiedz { get; set; }

		[Required]
		public Nullable<System.Int32> OdpowiedzID { get; set; }

		[InverseProperty("OdpowiedzOcena")]
		[ForeignKey("PrzedmiotBadaniaID")]
		public virtual PrzedmiotBadania PrzedmiotBadania { get; set; }

		[Required]
		public Nullable<System.Int32> PrzedmiotBadaniaID { get; set; }
	}
}