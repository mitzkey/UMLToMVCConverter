namespace WebApplication2.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class CzlonekKlubu : Osoba {


		public System.String WniosekPrzyjetyNaPodstawiePesel { get; set; }

		[Required]
		[InverseProperty("CzlonekKlubuPrzyjetyNaPodstawie")]
		[ForeignKey("WniosekPrzyjetyNaPodstawiePesel")]
		public virtual Wniosek WniosekPrzyjetyNaPodstawie { get; set; }
	}
}