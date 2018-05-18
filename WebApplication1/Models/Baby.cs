namespace WebApplication1.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class Baby : Person {


		
			public class Nested {
		
		
				public int ID {get; set;}
			}

		public virtual ICollection<KnownWords> KnownWords { get; set; }

		public String Name { get; set; }

		public static Worker MakeWorker() {
			throw new NotImplementedException();
		}
	}
}