namespace WebApplication3.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class Propozycja {

		public int ID { get; set; }


		[Required]
		public StatusPropozycji Status { get; set; }		

		[ForeignKey("StatusPropozycji")]
        public int StatusID { get; set; }
		

		public System.DateTime DataPrzeslaniaProsby { get; set; }

		[InverseProperty("ProponowanePrace")]
		[ForeignKey("EkspertID")]
		public virtual Ekspert Ekspert { get; set; }

		[Required]
		public Nullable<System.Int32> EkspertID { get; set; }

		[InverseProperty("ProponowaniRecenzenci")]
		[ForeignKey("ZgloszeniePracyNumer")]
		public virtual ZgloszeniePracy ZgloszeniePracy { get; set; }

		[Required]
		public System.Int32 ZgloszeniePracyNumer { get; set; }
	}
}