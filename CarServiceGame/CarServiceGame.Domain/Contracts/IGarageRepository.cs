using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CarServiceGame.Domain.Entities;

namespace CarServiceGame.Domain.Contracts
{
    public interface IGarageRepository
    {
        Garage GetGarage(string name, string password);
        void SetCashBalance(Guid garageId, int value);
    }
}
