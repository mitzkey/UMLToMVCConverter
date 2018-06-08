namespace WebApplication4.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class Pozycja {



		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public System.Int32 Wartosc { get; set; }

		[InverseProperty("Pozycje")]
		[ForeignKey("SkalaID")]
		public virtual Skala Skala { get; set; }

		[Required]
		public Nullable<System.Int32> SkalaID { get; set; }
	}
}