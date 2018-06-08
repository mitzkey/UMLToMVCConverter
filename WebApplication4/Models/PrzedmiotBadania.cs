namespace WebApplication4.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class PrzedmiotBadania {

		public int ID { get; set; }


		[Required]
		public System.String Nazwa { get; set; }

		public System.String Opis { get; set; }

		public Obraz Ikona { get; set; }

		[InverseProperty("PrzedmiotBadania")]
		public virtual Ocena OdpowiedzOcena { get; set; }
	}
}