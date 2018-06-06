namespace WebApplication2.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class Zajecia {

		public int ID { get; set; }


		[InverseProperty("Zajecia")]
		[ForeignKey("TerminDzien,TerminGodzinaRozpoczecia")]
		public virtual Termin Termin { get; set; }

		[Required]
		public Nullable<System.DateTime> TerminDzien { get; set; }

		[Required]
		public Nullable<System.DateTime> TerminGodzinaRozpoczecia { get; set; }

		[InverseProperty("Zajecia")]
		[ForeignKey("KursKod")]
		public virtual Kurs Kurs { get; set; }

		[Required]
		public System.String KursKod { get; set; }

		[InverseProperty("Zajecia")]
		[ForeignKey("SalaID")]
		public virtual Sala Sala { get; set; }

		[Required]
		public Nullable<System.Int32> SalaID { get; set; }
	}
}