namespace WebApplication2.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class Wniosek {



		public Nullable<System.DateTime> DataZlozenia { get; set; }

		public Nullable<System.DateTime> DataRozpatrzenia { get; set; }

		[Key]
		public System.String Pesel { get; set; }

		public StatusWniosku Status { get; set; }		

		[ForeignKey("StatusWniosku")]
        public int StatusID { get; set; }
		

		[InverseProperty("WniosekPrzyjetyNaPodstawie")]
		public virtual CzlonekKlubu CzlonekKlubuPrzyjetyNaPodstawie { get; set; }

		[ForeignKey("AdresZameldowaniaID")]
		public virtual Adres AdresZameldowania { get; set; }

		public Nullable<System.Int32> AdresZameldowaniaID { get; set; }

		[ForeignKey("AdresDoKorespondencjiID")]
		public virtual Adres AdresDoKorespondencji { get; set; }

		public Nullable<System.Int32> AdresDoKorespondencjiID { get; set; }

		public Nullable<System.Boolean> Weryfikacja(System.String p,Wniosek w) {
			throw new NotImplementedException();
		}
	}
}