using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CarServiceGame.Domain.Entities;

namespace CarServiceGame.Domain.Contracts
{
    public interface IOrderRepository
    {
        IEnumerable<RepairOrder> GetAvailableOrders(int skip, int take);
        IEnumerable<RepairProcess> GetHistoryOrders(Guid garageId, int skip, int take);

        void AssignOrder(Guid garageId, Guid orderId, Guid workerId, int stallNumber);
        void FinishOrder(Guid orderId);
        void CancelOrder(Guid orderId);
    }
}
