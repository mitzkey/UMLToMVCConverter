namespace WebApplication4.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class Odpowiedz {

		public int ID { get; set; }


		public System.Int32 NumerPorzadkowy { get; set; }

		public System.Boolean Kompletna { get; set; }

		public System.Int32 CzasOdpowiedzi { get; set; }

		[InverseProperty("Ankiety")]
		[ForeignKey("JednostkaLeksykalnaIdentyfikatorWordNet")]
		public virtual JednostkaLeksykalna JednostkaLeksykalna { get; set; }

		[Required]
		public System.Int32 JednostkaLeksykalnaIdentyfikatorWordNet { get; set; }

		[InverseProperty("Jednostki")]
		[ForeignKey("AnkietaNumer")]
		public virtual Ankieta Ankieta { get; set; }

		[Required]
		public System.Int64 AnkietaNumer { get; set; }

		[InverseProperty("Odpowiedz")]
		public virtual Ocena PrzedmiotBadaniaOcena { get; set; }
	}
}