namespace WebApplication3.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class Recenzent {

		public int ID { get; set; }


		[InverseProperty("Konto")]
		[ForeignKey("EkspertID")]
		public virtual Ekspert Ekspert { get; set; }

		[Required]
		public Nullable<System.Int32> EkspertID { get; set; }

		[InverseProperty("Recenzent")]
		public virtual ICollection<Recenzja> OpiniowanePrace { get; set; }
	}
}