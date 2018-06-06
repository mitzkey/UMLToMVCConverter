namespace WebApplication3.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class JednostkaOrganizacyjna {



		[Required]
		public System.String Nazwa { get; set; }

		public virtual ICollection<Skrot> Skrot { get; set; }

		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Required]
		public System.String NazwaKwalifikowana { get; set; }

		[NotMapped]
		public Nullable<System.Boolean> AktualnejEdycji { get { throw new NotImplementedException(); } private set {} }

		[Required]
		public TypJednostkiOrganizacyjnej Typ { get; set; }		

		[ForeignKey("TypJednostkiOrganizacyjnej")]
        public int TypID { get; set; }
		

		[ForeignKey("DaneAdresoweID")]
		public virtual DaneAdresowe DaneAdresowe { get; set; }

		[Required]
		public Nullable<System.Int32> DaneAdresoweID { get; set; }

		[InverseProperty("JednostkaOrganizacyjna")]
		public virtual ICollection<StatusZatrudnienia> EkspertStatusZatrudnienia { get; set; }
	}
}