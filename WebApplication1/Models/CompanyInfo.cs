namespace WebApplication1.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;

	public class CompanyInfo {

		public int ID {get; set;}

		public static String CompanyName { get; } = "ACME";

		public static Nullable<Boolean> ExampleStaticMethod(String inputString) {
			throw new NotImplementedException();
		}
	}
}