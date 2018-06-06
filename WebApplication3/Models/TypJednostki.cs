namespace WebApplication3.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class TypJednostki : TypJednostkiOrganizacyjnej {

		[DatabaseGenerated(DatabaseGeneratedOption.None)]

		public System.String Name { get; set; }
	}
}