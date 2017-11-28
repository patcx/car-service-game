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
        Garage CreateGarage(string name, string password);
        decimal GetGarageBalance(Guid garageId);
        IEnumerable<GarageRanking> GetGaragesRanking(int count);
    }
}
