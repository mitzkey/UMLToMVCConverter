namespace WebApplication1.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class CompanyInfo {

		public int ID { get; set; }


		[Required]
		public static System.String CompanyName { get; set; } = "ACME";

		public static System.Boolean ExampleStaticMethod(System.String inputString) {
			throw new NotImplementedException();
		}
	}
}