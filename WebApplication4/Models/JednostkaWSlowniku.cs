namespace WebApplication4.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class JednostkaWSlowniku {

		public int ID { get; set; }


		public System.Int64 NrPorzadkowy { get; set; }

		[InverseProperty("Slowniki")]
		[ForeignKey("JednostkaLeksykalnaIdentyfikatorWordNet")]
		public virtual JednostkaLeksykalna JednostkaLeksykalna { get; set; }

		[Required]
		public System.Int32 JednostkaLeksykalnaIdentyfikatorWordNet { get; set; }

		[InverseProperty("Jednostki")]
		[ForeignKey("SlownikNazwa")]
		public virtual Slownik Slownik { get; set; }

		[Required]
		public System.String SlownikNazwa { get; set; }
	}
}