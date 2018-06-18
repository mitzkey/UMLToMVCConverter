namespace WebApplication2.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class Wniosek {



		public System.DateTime DataZlozenia { get; set; }

		public System.DateTime DataRozpatrzenia { get; set; }

		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Required]
		public System.String Pesel { get; set; }

		[Required]
		public StatusWniosku Status { get; set; }		

		[ForeignKey("StatusWniosku")]
        public int StatusID { get; set; }
		

		[InverseProperty("WniosekPrzyjetyNaPodstawie")]
		public virtual CzlonekKlubu CzlonekKlubuPrzyjetyNaPodstawie { get; set; }

		[ForeignKey("AdresZameldowaniaID")]
		public virtual Adres AdresZameldowania { get; set; }

		[Required]
		public Nullable<System.Int32> AdresZameldowaniaID { get; set; }

		[ForeignKey("AdresDoKorespondencjiID")]
		public virtual Adres AdresDoKorespondencji { get; set; }

		[Required]
		public Nullable<System.Int32> AdresDoKorespondencjiID { get; set; }

		public System.Boolean Weryfikacja(System.String p,Wniosek w) {
			throw new NotImplementedException();
		}
	}
}