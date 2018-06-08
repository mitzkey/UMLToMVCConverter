namespace WebApplication4.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class ZestawTreningowySlownik {

		public int ID { get; set; }


		[ForeignKey("ZestawTreningowyIdentyfikatorWordNet")]
		public virtual JednostkaLeksykalna ZestawTreningowy { get; set; }

		public System.Int32 ZestawTreningowyIdentyfikatorWordNet { get; set; }
	}
}