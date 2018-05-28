namespace WebApplication2.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class Termin {



		public Nullable<System.DateTime> Dzien { get; set; }

		public Nullable<System.DateTime> GodzinaRozpoczecia { get; set; }

		[InverseProperty("Terminy")]
		[ForeignKey("GrafikID")]
		public virtual Grafik Grafik { get; set; }

		[Required]
		public Nullable<System.Int32> GrafikID { get; set; }

		[InverseProperty("Termin")]
		public virtual ICollection<Zajecia> Zajecia { get; set; }
	}
}