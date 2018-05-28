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


		public Nullable<System.Int32> Rok { get; set; }

		public System.String Semestr { get; set; }

		[NotMapped]
		public Nullable<System.Boolean> Aktualny { get { return true; } private set {} }

		[InverseProperty("Grafik")]
		public virtual ICollection<Termin> Terminy { get; set; }

		[InverseProperty("Grafik")]
		public virtual ICollection<Kurs> Kursy { get; set; }
	}
}