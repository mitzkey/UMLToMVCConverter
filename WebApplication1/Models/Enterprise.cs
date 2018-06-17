namespace WebApplication1.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class Enterprise {

		public int ID { get; set; }


		[Required]
		public System.String Name { get; set; }

		[Required]
		public CompanyInfo CompanyInfo { get; set; }

		[InverseProperty("Enterprise")]
		public virtual ICollection<Worker> Worker { get; set; }
	}
}