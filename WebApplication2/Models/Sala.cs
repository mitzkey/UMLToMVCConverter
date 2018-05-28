namespace WebApplication2.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class Sala {

		public int ID { get; set; }


		public System.String Adres { get; set; }

		public System.String Nazwa { get; set; }

		[InverseProperty("Sala")]
		public virtual ICollection<Zajecia> Zajecia { get; set; }

		[InverseProperty("Sala")]
		public virtual ICollection<PrzystosowanieSali> Przeznaczenie { get; set; }
	}
}