namespace WebApplication2.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class Termin {



		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public System.DateTime Dzien { get; set; }

		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public System.DateTime GodzinaRozpoczecia { get; set; }

		[InverseProperty("Terminy")]
		[ForeignKey("GrafikID")]
		public virtual Grafik Grafik { get; set; }

		[Required]
		public Nullable<System.Int32> GrafikID { get; set; }

		[InverseProperty("Termin")]
		public virtual ICollection<Zajecia> Zajecia { get; set; }
	}
}