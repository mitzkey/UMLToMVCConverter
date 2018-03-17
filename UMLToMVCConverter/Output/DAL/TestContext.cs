using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Test.DAL
{
    public class TestContext : DbContext
    {
                public DbSet<Worker> WorkerSet { get; set; }
                public DbSet<Baby> BabySet { get; set; }
                public DbSet<Nested> NestedSet { get; set; }
                public DbSet<Point> PointSet { get; set; }
                public DbSet<CompanyInfo> CompanyInfoSet { get; set; }
            }
}