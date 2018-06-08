namespace WebApplication4.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class Wymiar : PrzedmiotBadania {



		[Required]
		public System.String ZakresOd { get; set; }

		[Required]
		public System.String ZakresDo { get; set; }

		[InverseProperty("Wymiar")]
		public virtual ICollection<WymiarWBadaniu> EksperymentWymiarWBadaniu { get; set; }
	}
}