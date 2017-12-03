using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace CarServiceGame.Domain.Database
{
    public class GarageBalance
    {
        [Key]
        public Guid GarageId { get; set; }
        public decimal Balance { get; set; }
    }
}
