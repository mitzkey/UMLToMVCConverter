namespace WebApplication2.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class Dyscyplina {

		public int ID { get; set; }


		[Required]
		public System.String Nazwa { get; set; }

		[InverseProperty("Dyscyplina")]
		public virtual ICollection<PrzystosowanieSali> PrzystosowaneSale { get; set; }

		[InverseProperty("Dyscyplina")]
		public virtual ICollection<DyscyplinaZPoziomem> Poziomy { get; set; }

		public virtual ICollection<WymaganeWyposazenieDyscyplina> WymaganeWyposazenie { get; set; }

		public Sala Sale(Termin t) {
			throw new NotImplementedException();
		}
	}
}