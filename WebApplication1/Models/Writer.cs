namespace WebApplication1.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class Writer {


		public int ID {get; set;}
		public Nullable<System.Int32> BookID { get; set; }

		[ForeignKey("BookID")]
		public virtual ICollection<BookWriter> Book { get; set; }
	}
}