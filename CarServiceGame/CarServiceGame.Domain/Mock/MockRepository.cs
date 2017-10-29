using CarServiceGame.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarServiceGame.Domain.Entities;

namespace CarServiceGame.Domain.Mock
{
    public class MockRepository : IGarageRepository, IOrderRepository, IWorkerRepository
    {
        private static List<Worker> availableWorkers = new List<Worker>()
        {
            new Worker(Guid.NewGuid(), "Worker 1", 10, 150),
            new Worker(Guid.NewGuid(), "Worker 2", 20, 300),
            new Worker(Guid.NewGuid(), "Worker 3", 5, 50),
            new Worker(Guid.NewGuid(), "Worker 4", 5, 120),
            new Worker(Guid.NewGuid(), "Worker 5", 2, 60),
            new Worker(Guid.NewGuid(), "Worker 6", 100, 500),
        };

        private static List<RepairOrder> availableOrders = new List<RepairOrder>()
        {
            new RepairOrder(Guid.NewGuid(), "Audi A6", 200, 1000, "Broken car"),
            new RepairOrder(Guid.NewGuid(), "Audi A5", 100, 300, "Broken car"),
            new RepairOrder(Guid.NewGuid(), "BMW M5", 500, 5000, "Broken car"),
            new RepairOrder(Guid.NewGuid(), "BMW i8", 100, 3000, "Broken car"),
        };

        private static Garage garage = new Garage(Guid.NewGuid(), 5000, new []
        {
            new Worker(Guid.NewGuid(), "Worker 7", 50, 200), 
            new Worker(Guid.NewGuid(), "Worker 8", 20, 100),
        });

        private static List<RepairProcess> finishedProcesses = new List<RepairProcess>();

        public Garage GetGarage(string name, string password)
        {
            return new Garage(garage.GarageId, garage.CashBalance, garage.EmployeedWorkers);
        }

        public void SetCashBalance(Guid garageId, int value)
        {
            garage.SetCashBalance(value);
        }

        public IEnumerable<RepairOrder> GetAvailableOrders(int skip, int take)
        {
            return new List<RepairOrder>(availableOrders.Skip(skip).Take(take).ToArray());
        }

        public void AssignOrder(Guid garageId, Guid orderId, Guid workerId)
        {
            var order = availableOrders.FirstOrDefault(x => x.RepairOrderId == orderId);
            availableOrders.RemoveAll(x => x.RepairOrderId == orderId);
            var worker = garage.EmployeedWorkers.FirstOrDefault(x => x.WorkerId == workerId);
            garage.RepairProcesses.Add(new RepairProcess(order, worker));
        }

        public IEnumerable<Worker> GetUnemployedWorkers(int skip, int take)
        {
            return new List<Worker>(availableWorkers.Skip(skip).Take(take).ToArray());
        }

        public void FireWorker(Guid workerId)
        {
            Worker worker = garage.EmployeedWorkers.FirstOrDefault(x => x.WorkerId == workerId);
            garage.EmployeedWorkers.Remove(worker);
            availableWorkers.Add(worker);
        }

        public void EmployWorker(Guid garageId, Guid workerId)
        {
            Worker worker = availableWorkers.FirstOrDefault(x => x.WorkerId == workerId);
            availableWorkers.RemoveAll(x => x.WorkerId == workerId);
            garage.EmployeedWorkers.Add(worker);
        }

        public IEnumerable<RepairProcess> GetHistoryOrders(Guid garageId, int skip, int take)
        {
            return new List<RepairProcess>(finishedProcesses.Skip(skip).Take(take).ToArray());
        }

        public void FinishOrder(Guid orderId)
        {
            var order = garage.RepairProcesses.FirstOrDefault(x => x.Order.RepairOrderId == orderId);
            finishedProcesses.Add(order);
            garage.RepairProcesses.RemoveAll(x => x.Order.RepairOrderId == orderId);
        }
    }
}
