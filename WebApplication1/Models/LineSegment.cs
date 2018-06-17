namespace WebApplication1.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	public class LineSegment {

		public int ID { get; set; }


		[Required]
		public Point A { get; set; }

		[Required]
		public Point B { get; set; }
	}
}