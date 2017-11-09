﻿using System;
using System.Collections.Generic;

namespace CarServiceGame.Domain.Database
{
    public partial class Garage
    {
        public Garage()
        {
            RepairProcess = new HashSet<RepairProcess>();
            Worker = new HashSet<Worker>();
        }

        public Guid GarageId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        public ICollection<RepairProcess> RepairProcess { get; set; }
        public ICollection<Worker> Worker { get; set; }
    }
}
