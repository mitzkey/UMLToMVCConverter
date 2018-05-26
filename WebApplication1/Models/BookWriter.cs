namespace WebApplication1.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class BookWriter {


		public int ID {get; set;}
		public Nullable<System.Int32> WriterID { get; set; }
		public Nullable<System.Int32> BookID { get; set; }

		[InverseProperty("Book")]
		[ForeignKey("WriterID")]
		public virtual Writer Writer { get; set; }

		[InverseProperty("Writer")]
		[ForeignKey("BookID")]
		public virtual Book Book { get; set; }
	}
}