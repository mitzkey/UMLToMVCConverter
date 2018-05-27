namespace WebApplication2.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class Instruktor {

		public int ID { get; set; }


		public virtual ICollection<DzienTygodnia> DniZajec { get { throw new NotImplementedException(); } private set {} }

		public virtual ICollection<CertyfikowaneKwalifikacjeInstruktor> CertyfikowaneKwalifikacje { get; set; }

		[InverseProperty("Uprawnieni")]
		public virtual ICollection<KwalifikacjeUprawnieni> Kwalifikacje { get; set; }

		[Required]
		[InverseProperty("Instruktor")]
		public virtual ICollection<SzczegolyKwalifikacji> SzczegolyKwalifikacji { get; set; }
	}
}