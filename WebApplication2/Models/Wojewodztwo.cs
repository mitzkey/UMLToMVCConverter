namespace WebApplication2.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class Wojewodztwo {

		public int ID { get; set; }


		public System.String Nazwa { get; set; }

		public Nullable<System.Boolean> Aktualna { get; set; }

		[InverseProperty("WojewodztwoMiejscowosci")]
		public virtual ICollection<Miejscowosc> MiejscowoscMiejscowosci { get; set; }

		public ICollection<Miejscowosc> ListaMiejscowosci(Wniosek w) {
			throw new NotImplementedException();
		}
	}
}