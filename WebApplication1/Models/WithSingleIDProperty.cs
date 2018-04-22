namespace WebApplication1.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;

	public class WithSingleIDProperty {

		[Key]
		public String MyIdentifier { get; set; }

		public Nullable<Int32> Another { get; set; }
	}
}