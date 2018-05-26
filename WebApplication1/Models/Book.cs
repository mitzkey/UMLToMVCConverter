namespace WebApplication1.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class Book {


		public int ID {get; set;}
		public Nullable<System.Int32> AuthorID { get; set; }

		[InverseProperty("TextBook")]
		[ForeignKey("AuthorID")]
		public virtual Professor Author { get; set; }

		[InverseProperty("Book")]
		public virtual ICollection<BookWriter> Writer { get; set; }
	}
}