namespace WebApplication4.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class WymiarWBadaniu {

		public int ID { get; set; }


		[ForeignKey("SkalaID")]
		public virtual Skala Skala { get; set; }

		[Required]
		public Nullable<System.Int32> SkalaID { get; set; }

		[InverseProperty("EksperymentWymiarWBadaniu")]
		[ForeignKey("WymiarID")]
		public virtual Wymiar Wymiar { get; set; }

		[Required]
		public Nullable<System.Int32> WymiarID { get; set; }

		[InverseProperty("BadaneWymiary")]
		[ForeignKey("EksperymentIdentyfikator")]
		public virtual Eksperyment Eksperyment { get; set; }

		[Required]
		public System.String EksperymentIdentyfikator { get; set; }
	}
}