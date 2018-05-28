namespace WebApplication2.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class Miejscowosc {

		public int ID { get; set; }


		public System.String Nazwa { get; set; }

		public Nullable<System.Boolean> Aktualna { get; set; }

		[InverseProperty("MiejscowoscMiejscowosci")]
		[ForeignKey("WojewodztwoMiejscowosciID")]
		public virtual Wojewodztwo WojewodztwoMiejscowosci { get; set; }

		[Required]
		public Nullable<System.Int32> WojewodztwoMiejscowosciID { get; set; }
	}
}