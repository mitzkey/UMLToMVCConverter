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

		[InverseProperty("Writer")]
		public virtual ICollection<BookWriter> Book { get; set; }
	}
}