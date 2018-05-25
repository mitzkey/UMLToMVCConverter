namespace WebApplication1.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class Enterprise {


		public int ID {get; set;}
		public Nullable<System.Int32> WorkerID { get; set; }

		public System.String Name { get; set; }

		[Required]
		public CompanyInfo CompanyInfo { get; set; }

		[ForeignKey("WorkerID")]
		public virtual ICollection<Worker> Worker { get; set; }
	}
}