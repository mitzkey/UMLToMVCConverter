namespace WebApplication3.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class AutorzyPraca {

		public int ID { get; set; }


		[InverseProperty("Autorzy")]
		[ForeignKey("PracaNumer")]
		public virtual ZgloszeniePracy Praca { get; set; }

		[Required]
		public System.Int32 PracaNumer { get; set; }

		[InverseProperty("Praca")]
		[ForeignKey("AutorzyID")]
		public virtual Autor Autorzy { get; set; }

		public Nullable<System.Int32> AutorzyID { get; set; }
	}
}