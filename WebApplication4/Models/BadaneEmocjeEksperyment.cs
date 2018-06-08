namespace WebApplication4.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class BadaneEmocjeEksperyment {

		public int ID { get; set; }


		[InverseProperty("BadaneEmocje")]
		[ForeignKey("EksperymentIdentyfikator")]
		public virtual Eksperyment Eksperyment { get; set; }

		[Required]
		public System.String EksperymentIdentyfikator { get; set; }

		[InverseProperty("Eksperyment")]
		[ForeignKey("BadaneEmocjeID")]
		public virtual Emocja BadaneEmocje { get; set; }

		public Nullable<System.Int32> BadaneEmocjeID { get; set; }
	}
}