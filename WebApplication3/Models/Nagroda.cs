namespace WebApplication3.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class Nagroda {

		public int ID { get; set; }


		[Required]
		public System.String Rodzaj { get; set; }

		public System.Int32 Wartosc { get; set; }

		[InverseProperty("Nagrody")]
		[ForeignKey("EdycjaKonkursuPrzydzielanaWRamachNumer")]
		public virtual EdycjaKonkursu EdycjaKonkursuPrzydzielanaWRamach { get; set; }

		[Required]
		public System.Int32 EdycjaKonkursuPrzydzielanaWRamachNumer { get; set; }

		[InverseProperty("Nagroda")]
		public virtual ICollection<ZgloszeniePracy> Prace { get; set; }
	}
}