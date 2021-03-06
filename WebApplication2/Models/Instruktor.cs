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


		[NotMapped]
		public virtual ICollection<DzienTygodnia> DniZajec { get { throw new NotImplementedException(); } private set {} }

		[InverseProperty("Instruktor")]
		public virtual ICollection<SzczegolyKwalifikacji> Kwalifikacje { get; set; }

		public virtual ICollection<CertyfikowaneKwalifikacjeInstruktor> CertyfikowaneKwalifikacje { get; set; }
	}
}