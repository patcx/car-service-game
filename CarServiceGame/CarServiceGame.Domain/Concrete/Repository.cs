using CarServiceGame.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CarServiceGame.Domain.Entities;

namespace CarServiceGame.Domain.Concrete
{
    public class Repository : IGarageRepository, IOrderRepository, IWorkerRepository
    {
        public IEnumerable<RepairOrder> GetAvailableOrders(int skip, int take)
        {
            throw new NotImplementedException();
        }

        public void AssignOrder(Guid garageId, Guid orderId, Guid workerId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Worker> GetUnemployedWorkers(int skip, int take)
        {
            throw new NotImplementedException();
        }

        public void FireWorker(Guid workerId)
        {
            throw new NotImplementedException();
        }

        public void EmployWorker(Guid garageId, Guid workerId)
        {
            throw new NotImplementedException();
        }

        public Garage GetGarage(string name, string password)
        {
            throw new NotImplementedException();
        }

        public void SetCashBalance(Guid garageId, int value)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RepairProcess> GetHistoryOrders(Guid garageId, int skip, int take)
        {
            throw new NotImplementedException();
        }

        public void FinishOrder(Guid orderId)
        {
            throw new NotImplementedException();
        }
    }
}
