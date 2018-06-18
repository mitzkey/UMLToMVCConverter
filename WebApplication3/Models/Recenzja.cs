namespace WebApplication3.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class Recenzja {

		public int ID { get; set; }


		public System.DateTime PlanowanaDataOpracowania { get; set; }

		[NotMapped]
		[Required]
		public StatusRecenzji Status { get { throw new NotImplementedException(); } private set {} }		

		[ForeignKey("StatusRecenzji")]
        public int StatusID { get; set; }
		

		public System.Int32 Ocena { get; set; }

		[Required]
		public System.String DodatkoweUwagi { get; set; }

		public System.DateTime DataPrzeslaniaPonaglenia { get; set; }

		public System.DateTime DataZatwierdzenia { get; set; }

		[NotMapped]
		public System.Boolean Opozniona { get { throw new NotImplementedException(); } private set {} }

		[InverseProperty("OpiniowanePrace")]
		[ForeignKey("RecenzentID")]
		public virtual Recenzent Recenzent { get; set; }

		[Required]
		public Nullable<System.Int32> RecenzentID { get; set; }

		[InverseProperty("Recenzenci")]
		[ForeignKey("ZgloszeniePracyNumer")]
		public virtual ZgloszeniePracy ZgloszeniePracy { get; set; }

		[Required]
		public System.Int32 ZgloszeniePracyNumer { get; set; }
	}
}