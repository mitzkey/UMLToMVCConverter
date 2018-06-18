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
		public virtual Odpowiedz Odpowiedz { get; set; }

		[InverseProperty("OdpowiedzOcena")]
		public virtual PrzedmiotBadania PrzedmiotBadania { get; set; }
	}
}