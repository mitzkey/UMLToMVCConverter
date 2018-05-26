namespace WebApplication2.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class Kurs {


		public Nullable<System.Int32> GrafikID { get; set; }

		[Key]
		public System.String Kod { get; }

		public Nullable<System.Double> KosztTygodniowy { get { throw new NotImplementedException(); } }

		[Required]
		[InverseProperty("Kursy")]
		[ForeignKey("GrafikID")]
		public virtual Grafik Grafik { get; set; }

		[InverseProperty("Kurs")]
		public virtual ICollection<Zajecia> Zajecia { get; set; }
	}
}