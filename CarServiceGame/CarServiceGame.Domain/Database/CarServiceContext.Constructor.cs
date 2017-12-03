using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace CarServiceGame.Domain.Database
{
    public partial class CarServiceContext
    {
        public virtual DbSet<GarageBalance> GarageBalance { get; set; }

        public CarServiceContext()
        {
            
        }

        public CarServiceContext(DbContextOptions<CarServiceContext> options) : base(options)
        {
            
        }
    }
}
