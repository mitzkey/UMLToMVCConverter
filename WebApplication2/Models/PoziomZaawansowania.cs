namespace WebApplication2.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class PoziomZaawansowania {


		public int ID {get; set;}

		public System.String Nazwa { get; set; }

		[InverseProperty("Poziom")]
		public virtual ICollection<PrzystosowanieSali> PrzystosowanieSali { get; set; }
	}
}