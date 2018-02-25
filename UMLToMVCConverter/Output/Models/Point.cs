using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Test.Models
{
	[ComplexType]
	public class Point {

		public  Nullable<System.Int32> x { get; set; }

		public  Nullable<System.Int32> y { get; set; }
	}
}