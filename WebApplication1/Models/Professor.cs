namespace WebApplication1.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class Professor {

		public int ID { get; set; }


		[InverseProperty("Author")]
		public virtual ICollection<Book> TextBook { get; set; }

		[ForeignKey("FavouriteBookID")]
		public virtual Book FavouriteBook { get; set; }

		[Required]
		public Nullable<System.Int32> FavouriteBookID { get; set; }
	}
}