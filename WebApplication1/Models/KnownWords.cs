namespace WebApplication1.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class KnownWords {


		public int ID {get; set;}

		public System.String Value { get; set; }
	}
}