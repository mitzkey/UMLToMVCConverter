namespace WebApplication1.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class Professor {


		public int ID {get; set;}

		[InverseProperty("Author")]
		public virtual ICollection<Book> TextBook { get; set; }

		[InverseProperty("Fans")]
		public Book FavouriteBook { get; set; }
	}
}