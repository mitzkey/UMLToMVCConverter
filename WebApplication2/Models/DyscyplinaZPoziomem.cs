namespace WebApplication2.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class DyscyplinaZPoziomem {

		public int ID { get; set; }


		public System.String Nazwa { get { throw new NotImplementedException(); } private set {} }

		[InverseProperty("Poziomy")]
		[ForeignKey("DyscyplinaID")]
		public virtual Dyscyplina Dyscyplina { get; set; }

		[Required]
		public Nullable<System.Int32> DyscyplinaID { get; set; }

		[InverseProperty("Dyscypliny")]
		[ForeignKey("PoziomZaawansowaniaID")]
		public virtual PoziomZaawansowania PoziomZaawansowania { get; set; }

		[Required]
		public Nullable<System.Int32> PoziomZaawansowaniaID { get; set; }

		[InverseProperty("DyscyplinaZPoziomem")]
		public virtual ICollection<SzczegolyKwalifikacji> Uprawnieni { get; set; }
	}
}