namespace Test.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;

	public class Baby : Person {
		
			public class Nested {
		
				public int ID {get; set;}
		
				public String Name { get; set; }
			}

		public virtual ICollection<KnownWords> KnownWords { get; set; }

		public String Name { get; set; }

		public static Worker MakeWorker() {
			throw new NotImplementedException();
		}
	}
}