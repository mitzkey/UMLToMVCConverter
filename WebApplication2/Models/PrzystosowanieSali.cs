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

		public Nullable<System.Int32> PoziomID { get; set; }
		public Nullable<System.Int32> DyscyplinaID { get; set; }
		public Nullable<System.Int32> SalaID { get; set; }

		public Nullable<System.Int32> Pojemnosc { get; set; }

		public Nullable<System.Double> StawkaZaZajecia { get; set; }

		[Required]
		[InverseProperty("PrzystosowanieSali")]
		[ForeignKey("PoziomID")]
		public virtual PoziomZaawansowania Poziom { get; set; }

		public virtual ICollection<BrakujaceWyposazeniePrzystosowanieSali> BrakujaceWyposazenie { get; set; }

		[Required]
		[InverseProperty("PrzystosowanieSali")]
		[ForeignKey("DyscyplinaID")]
		public virtual Dyscyplina Dyscyplina { get; set; }

		[Required]
		[InverseProperty("PrzystosowanieSali")]
		[ForeignKey("SalaID")]
		public virtual Sala Sala { get; set; }
	}
}