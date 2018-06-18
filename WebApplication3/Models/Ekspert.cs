namespace WebApplication3.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class Ekspert {

		public int ID { get; set; }


		[Required]
		public System.String Imie { get; set; }

		[Required]
		public System.String Nazwisko { get; set; }

		[Required]
		public System.String Email { get; set; }

		public virtual ICollection<ObszarBadan> ObszarBadan { get; set; }

		public virtual ICollection<Telefon> Telefon { get; set; }

		[Required]
		public System.String Plec { get; set; }

		[NotMapped]
		public System.Boolean AktualnejEdycji { get { throw new NotImplementedException(); } private set {} }

		[NotMapped]
		public System.Boolean PracownikNaukowy { get { throw new NotImplementedException(); } private set {} }

		[InverseProperty("Promotor")]
		public virtual ICollection<ZgloszeniePracy> NadzorowanePrace { get; set; }

		[InverseProperty("Ekspert")]
		public virtual Recenzent Konto { get; set; }

		[InverseProperty("Ekspert")]
		public virtual ICollection<Propozycja> ProponowanePrace { get; set; }

		[InverseProperty("Ekspert")]
		public virtual ICollection<StatusZatrudnienia> Zatrudnienia { get; set; }
	}
}