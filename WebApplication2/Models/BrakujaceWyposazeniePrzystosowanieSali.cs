namespace WebApplication2.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class BrakujaceWyposazeniePrzystosowanieSali {


		public int ID {get; set;}
		public Nullable<System.Int32> BrakujaceWyposazenieID { get; set; }

		[ForeignKey("BrakujaceWyposazenieID")]
		public virtual Wyposażenie BrakujaceWyposazenie { get; set; }
	}
}