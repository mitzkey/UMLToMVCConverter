namespace WebApplication1.Models
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web;
	using System.ComponentModel.DataAnnotations;

	public class LineSegment {

		public int ID {get; set;}

		public Point X { get; set; }

		public Point Y { get; set; }
	}
}