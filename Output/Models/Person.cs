using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Test.Models
{
        
public abstract class Person     {
                    public int PersonID {get; set;}
        
                     public  Nullable<System.DateTime> dateOfBirth { get; set; }
                    public  String name { get; set; }
                    public void DoSomething(
            Nullable<System.Int32> x,Nullable<System.Int32> y) {
throw new NotImplementedException();
}
            }
}