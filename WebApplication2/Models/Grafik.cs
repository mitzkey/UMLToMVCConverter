namespace WebApplication2.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class Grafik {

		public int ID { get; set; }


		public System.Int32 Rok { get; set; }

		[Required]
		public System.String Semestr { get; set; }

		[NotMapped]
		public System.Boolean Aktualny { get { throw new NotImplementedException(); } private set {} }

		[InverseProperty("Grafik")]
		public virtual ICollection<Termin> Terminy { get; set; }

		[InverseProperty("Grafik")]
		public virtual ICollection<Kurs> Kursy { get; set; }
	}
}