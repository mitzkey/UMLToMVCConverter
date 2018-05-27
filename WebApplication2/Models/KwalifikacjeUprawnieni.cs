namespace WebApplication2.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class KwalifikacjeUprawnieni {

		public int ID { get; set; }

		public Nullable<System.Int32> UprawnieniID { get; set; }
		public Nullable<System.Int32> KwalifikacjeID { get; set; }

		[Required]
		[InverseProperty("Kwalifikacje")]
		[ForeignKey("UprawnieniID")]
		public virtual Instruktor Uprawnieni { get; set; }

		[InverseProperty("Uprawnieni")]
		[ForeignKey("KwalifikacjeID")]
		public virtual DyscyplinaZPoziomem Kwalifikacje { get; set; }
	}
}