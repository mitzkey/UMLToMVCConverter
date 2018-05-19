namespace WebApplication1.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class WithSingleIDProperty {



		[Key]
		public System.String MyIdentifier { get; set; }

		public Nullable<System.Int32> Another { get; set; }

		public StatusWniosku Status { get; set; }		

		[ForeignKey("StatusWniosku")]
        public int StatusID { get; set; }
		
	}
}