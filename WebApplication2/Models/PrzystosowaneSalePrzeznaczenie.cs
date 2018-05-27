namespace WebApplication2.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class PrzystosowaneSalePrzeznaczenie {

		public int ID { get; set; }

		public Nullable<System.Int32> PrzeznaczenieID { get; set; }
		public Nullable<System.Int32> PrzystosowaneSaleID { get; set; }

		[InverseProperty("PrzystosowaneSale")]
		[ForeignKey("PrzeznaczenieID")]
		public virtual Dyscyplina Przeznaczenie { get; set; }

		[Required]
		[InverseProperty("Przeznaczenie")]
		[ForeignKey("PrzystosowaneSaleID")]
		public virtual Sala PrzystosowaneSale { get; set; }
	}
}