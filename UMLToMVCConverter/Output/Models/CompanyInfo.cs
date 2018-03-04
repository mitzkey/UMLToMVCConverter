using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Test.Models
{
	public class CompanyInfo {

		public int ID {get; set;}

		public static String CompanyName { get; private set; } = "ACME";

		public static Nullable<System.Boolean> ExampleStaticMethod(String inputString) {
			throw new NotImplementedException();
		}
	}
}