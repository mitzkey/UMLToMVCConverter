namespace WebApplication4.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class EmocjaPodstawowa : Emocja {



		public Nullable<System.Boolean> ZModeluPK { get; set; } = true;

		[InverseProperty("EmocjaPodstawowa")]
		[ForeignKey("EksperymentLokalnyIdentyfikator")]
		public virtual EksperymentLokalny EksperymentLokalny { get; set; }

		public System.String EksperymentLokalnyIdentyfikator { get; set; }
	}
}