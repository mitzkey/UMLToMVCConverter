namespace WebApplication4.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class Widok {

		[DatabaseGenerated(DatabaseGeneratedOption.None)]		public int ID { get; set; }


		public System.String Name { get; set; }
	}
}