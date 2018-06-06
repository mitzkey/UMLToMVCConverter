namespace WebApplication3.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class StatusZatrudnienia {

		public int ID { get; set; }


		public System.Boolean Aktualne { get; set; }

		[InverseProperty("Zatrudnienia")]
		[ForeignKey("EkspertID")]
		public virtual Ekspert Ekspert { get; set; }

		[Required]
		public Nullable<System.Int32> EkspertID { get; set; }

		[InverseProperty("EkspertStatusZatrudnienia")]
		[ForeignKey("JednostkaOrganizacyjnaNazwaKwalifikowana")]
		public virtual JednostkaOrganizacyjna JednostkaOrganizacyjna { get; set; }

		[Required]
		public System.String JednostkaOrganizacyjnaNazwaKwalifikowana { get; set; }
	}
}