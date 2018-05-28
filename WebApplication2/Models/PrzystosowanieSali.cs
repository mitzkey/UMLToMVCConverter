namespace WebApplication2.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class PrzystosowanieSali {

		public int ID { get; set; }


		public Nullable<System.Int32> Pojemnosc { get; set; }

		public Nullable<System.Double> StawkaZaZajecia { get; set; }

		[InverseProperty("PrzystosowanieSali")]
		[ForeignKey("PoziomID")]
		public virtual PoziomZaawansowania Poziom { get; set; }

		[Required]
		public Nullable<System.Int32> PoziomID { get; set; }

		[InverseProperty("PrzystosowaneSale")]
		[ForeignKey("DyscyplinaID")]
		public virtual Dyscyplina Dyscyplina { get; set; }

		[Required]
		public Nullable<System.Int32> DyscyplinaID { get; set; }

		[InverseProperty("Przeznaczenie")]
		[ForeignKey("SalaID")]
		public virtual Sala Sala { get; set; }

		[Required]
		public Nullable<System.Int32> SalaID { get; set; }

		public virtual ICollection<BrakujaceWyposazeniePrzystosowanieSali> BrakujaceWyposazenie { get; set; }
	}
}