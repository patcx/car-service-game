using System;
using System.Collections.Generic;

namespace CarServiceGame.Domain.Database
{
    public partial class Payment
    {
        public Guid GarageBonusId { get; set; }
        public Guid GarageId { get; set; }
        public string Email { get; set; }
        public string Code { get; set; }
        public bool? IsActivated { get; set; }
        public int BonusValue { get; set; }

        public Garage Garage { get; set; }
    }
}
