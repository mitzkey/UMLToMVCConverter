namespace WebApplication2.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class Adres {


		public int ID { get; set; }
		public Nullable<System.Int32> MiejscowoscID { get; set; }

		[Required]
		[ForeignKey("MiejscowoscID")]
		public virtual Miejscowosc Miejscowosc { get; set; }
	}
}