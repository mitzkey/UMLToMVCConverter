namespace WebApplication2.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class Osoba {



		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Required]
		public System.String Pesel { get; set; }
	}
}