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


		public System.String Nazwa { get; set; }

		[Required]
		[InverseProperty("Przeznaczenie")]
		public virtual ICollection<PrzystosowaneSalePrzeznaczenie> PrzystosowaneSale { get; set; }

		public virtual ICollection<WymaganeWyposazenieDyscyplina> WymaganeWyposazenie { get; set; }

		[InverseProperty("Dyscypliny")]
		public virtual ICollection<PoziomyDyscypliny> Poziomy { get; set; }

		[InverseProperty("Dyscyplina")]
		public virtual ICollection<PrzystosowanieSali> PrzystosowanieSali { get; set; }

		[Required]
		[InverseProperty("Dyscyplina")]
		public virtual ICollection<DyscyplinaZPoziomem> DyscyplinaZPoziomem { get; set; }

		public Sala Sale(Termin t) {
			throw new NotImplementedException();
		}
	}
}