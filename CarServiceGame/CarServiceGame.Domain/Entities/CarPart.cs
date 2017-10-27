using System;
using System.Collections.Generic;
using System.Text;
using CarServiceGame.Domain.Enums;

namespace CarServiceGame.Domain.Entities
{
    public class CarPart
    {
        public Guid CartPart { get; }
        public CarPartsEnum Type { get; }
        public decimal Price { get; }
    }
}
