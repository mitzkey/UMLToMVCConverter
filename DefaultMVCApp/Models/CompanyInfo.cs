namespace DefaultMVCApp.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;

	public class CompanyInfo {

		public int ID {get; set;}

		public static String CompanyName { get; private set; } = "ACME";

		public static Nullable<Boolean> ExampleStaticMethod(String inputString) {
			throw new NotImplementedException();
		}
	}
}