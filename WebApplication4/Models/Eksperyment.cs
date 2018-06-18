namespace WebApplication4.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class Eksperyment {



		[Required]
		public System.String Nazwa { get; set; } = "slownik.nazwa";

		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Required]
		public System.String Identyfikator { get; set; }

		public System.DateTime DataRozpoczecia { get; set; }

		public System.DateTime DataZakonczenia { get; set; }

		public System.Int64 LiczbaJednostekWZestawie { get; set; }

		public System.Int32 MaksymalnyCzasPojedynczejOdpowiedzi { get; set; }

		public System.Int32 MaksymalnyCzasBadania { get; set; }

		[Required]
		public Widok WidokEmocji { get; set; }		

		[ForeignKey("Widok")]
        public int WidokEmocjiID { get; set; }
		

		[Required]
		public Tryb Tryb { get; set; }		

		[ForeignKey("Tryb")]
        public int TrybID { get; set; }
		

		[NotMapped]
		public System.Boolean Zrealizowany { get { throw new NotImplementedException(); } private set {} }

		public System.Int64 WymaganePokrycie { get; set; }

		[InverseProperty("Eksperymenty")]
		[ForeignKey("SlownikNazwa")]
		public virtual Slownik Slownik { get; set; }

		[Required]
		public System.String SlownikNazwa { get; set; }

		[InverseProperty("Eksperyment")]
		public virtual ICollection<Ankieta> Ankiety { get; set; }

		[ForeignKey("SkalaEmocjiID")]
		public virtual Skala SkalaEmocji { get; set; }

		[Required]
		public Nullable<System.Int32> SkalaEmocjiID { get; set; }

		[InverseProperty("Eksperyment")]
		public virtual ICollection<WymiarWBadaniu> BadaneWymiary { get; set; }

		[InverseProperty("Eksperyment")]
		public virtual ICollection<BadaneEmocjeEksperyment> BadaneEmocje { get; set; }
	}
}