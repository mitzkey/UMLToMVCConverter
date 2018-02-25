using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Test.Models
{
	public class CompanyInfo {

		public int CompanyInfoID {get; set;}

		public static  String CompanyName { get; private set; }

		public static Nullable<System.Boolean> ExampleStaticMethod(String inputString) {
			throw new NotImplementedException();
		}
	}
}