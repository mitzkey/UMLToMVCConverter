using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Test.Models
{
	public class Baby : Person {

		public int ID {get; set;}
		
			public class Nested {
		
				public int ID {get; set;}
		
				public String name { get; set; }
		
				public String name { get; set; }
			}

		public ICollection<System.String> knownWords { get; set; }

		public String name { get; set; }

		public static Worker MakeWorker() {
			throw new NotImplementedException();
		}
	}
}