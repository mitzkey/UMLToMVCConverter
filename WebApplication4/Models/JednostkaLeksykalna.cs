namespace WebApplication4.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class JednostkaLeksykalna {



		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public System.Int32 IdentyfikatorWordNet { get; set; }

		[Required]
		public System.String Lemat { get; set; }

		public virtual ICollection<PrzykladUzyc> PrzykladUzyc { get; set; }

		[InverseProperty("JednostkaLeksykalna")]
		public virtual ICollection<JednostkaWSlowniku> Slowniki { get; set; }

		[InverseProperty("JednostkaLeksykalna")]
		public virtual ICollection<Odpowiedz> Ankiety { get; set; }
	}
}