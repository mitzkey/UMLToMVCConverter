namespace WebApplication3.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class JednostkaNaukowa : JednostkaOrganizacyjna {



		[Required]
		public TypJednostki Typ { get; set; }		

		[ForeignKey("TypJednostki")]
        public int TypID { get; set; }
		

		[InverseProperty("Uczelnia")]
		public virtual ICollection<ZgloszeniePracy> Praca { get; set; }

		[InverseProperty("JednostkaNaukowaPodjednostki")]
		[ForeignKey("NadrzednaNazwaKwalifikowana")]
		public virtual JednostkaNaukowa Nadrzedna { get; set; }

		[Required]
		public System.String NadrzednaNazwaKwalifikowana { get; set; }

		[InverseProperty("Nadrzedna")]
		public virtual ICollection<JednostkaNaukowa> JednostkaNaukowaPodjednostki { get; set; }
	}
}