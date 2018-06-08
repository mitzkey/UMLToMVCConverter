namespace WebApplication4.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class Slownik {



		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Required]
		public System.String Nazwa { get; set; }

		public System.DateTime DataUtworzenia { get; set; }

		[InverseProperty("Slownik")]
		public virtual ICollection<Eksperyment> Eksperymenty { get; set; }

		[InverseProperty("Slownik")]
		public virtual JednostkaWSlowniku Jednostki { get; set; }

		public virtual ICollection<ZestawTreningowySlownik> ZestawTreningowy { get; set; }
	}
}