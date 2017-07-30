using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Test.Models
{
        
public  class Worker  : Person     {
                    public int WorkerID {get; set;}
        
                     public String company { get; set; }
                    public Nullable<System.Double> wage { get; set; }
                    public ICollection<System.Int32> favouriteNumber { get; set; }
            }
}