namespace WebApplication4.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class EksperymentLokalny : Eksperyment {



		[InverseProperty("EksperymentLokalny")]
		public virtual ICollection<EmocjaPodstawowa> EmocjaPodstawowa { get; set; }
	}
}