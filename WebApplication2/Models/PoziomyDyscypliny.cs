namespace WebApplication2.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class PoziomyDyscypliny {

		public int ID { get; set; }

		public Nullable<System.Int32> DyscyplinyID { get; set; }
		public Nullable<System.Int32> PoziomyID { get; set; }

		[Required]
		[InverseProperty("Poziomy")]
		[ForeignKey("DyscyplinyID")]
		public virtual Dyscyplina Dyscypliny { get; set; }

		[InverseProperty("Dyscypliny")]
		[ForeignKey("PoziomyID")]
		public virtual PoziomZaawansowania Poziomy { get; set; }
	}
}