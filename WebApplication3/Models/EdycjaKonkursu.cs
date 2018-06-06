namespace WebApplication3.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class EdycjaKonkursu {



		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public System.Int32 Numer { get; set; }

		public System.Int32 Rok { get; set; }

		[Required]
		public StatusEdycji Status { get; set; }		

		[ForeignKey("StatusEdycji")]
        public int StatusID { get; set; }
		

		public Nullable<System.DateTime> PlanowanaDataOpracowaniaRecenzji { get; set; }

		public Nullable<System.DateTime> PlanowanaDataRozstrzygnieciaKonkursu { get; set; }

		public System.Int32 WymaganeMinimum { get; set; } = 2;

		[InverseProperty("Edycja")]
		public virtual ICollection<ZgloszeniePracy> ZgloszonePrace { get; set; }

		[InverseProperty("EdycjaKonkursuPrzydzielanaWRamach")]
		public virtual ICollection<Nagroda> Nagrody { get; set; }
	}
}