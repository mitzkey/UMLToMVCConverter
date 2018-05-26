namespace WebApplication2.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class WymaganeWyposazenieDyscyplina {


		public int ID {get; set;}
		public Nullable<System.Int32> WymaganeWyposazenieID { get; set; }

		[ForeignKey("WymaganeWyposazenieID")]
		public virtual Wyposażenie WymaganeWyposazenie { get; set; }
	}
}