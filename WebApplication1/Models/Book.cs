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

		[InverseProperty("TextBook")]
		public Professor Author { get; set; }

		[InverseProperty("FavouriteBook")]
		public virtual ICollection<Professor> Fans { get; set; }

		[InverseProperty("Book")]
		public virtual ICollection<Writer> Writer { get; set; }
	}
}