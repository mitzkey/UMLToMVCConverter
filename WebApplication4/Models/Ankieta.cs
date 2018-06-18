namespace WebApplication4.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class Ankieta {



		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public System.Int64 Numer { get; set; }

		public System.DateTime TerminRozpoczecia { get; set; }

		public System.DateTime TerminZakonczenia { get; set; }

		public System.Boolean PrzekroczonyCzasBadania { get; set; } = false;

		[NotMapped]
		public System.Int64 CzasWypelnianiaAnkiety { get { throw new NotImplementedException(); } private set {} }

		[NotMapped]
		public System.Boolean Kompletna { get { throw new NotImplementedException(); } private set {} }

		[InverseProperty("Ankiety")]
		[ForeignKey("EksperymentIdentyfikator")]
		public virtual Eksperyment Eksperyment { get; set; }

		[Required]
		public System.String EksperymentIdentyfikator { get; set; }

		[InverseProperty("Ankieta")]
		public virtual ICollection<Odpowiedz> Jednostki { get; set; }
	}
}