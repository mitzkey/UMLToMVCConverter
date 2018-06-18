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

		[Required]
		public System.String Opis { get; set; }

		[Required]
		public Obraz Ikona { get; set; }

		[InverseProperty("PrzedmiotBadania")]
		[ForeignKey("OdpowiedzOcenaID")]
		public virtual Ocena OdpowiedzOcena { get; set; }

		[Required]
		public Nullable<System.Int32> OdpowiedzOcenaID { get; set; }
	}
}