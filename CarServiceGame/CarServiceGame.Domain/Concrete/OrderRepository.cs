using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarServiceGame.Domain.Contracts;
using CarServiceGame.Domain.Entities;
using Db = CarServiceGame.Domain.Database;

namespace CarServiceGame.Domain.Concrete
{
    public class OrderRepository : IOrderRepository
    {
        private Func<Db.CarServiceContext> GetContext;

        public OrderRepository(Func<Db.CarServiceContext> contextFactory = null)
        {
            if (contextFactory == null)
                GetContext = () => new Db.CarServiceContext();
            else
                GetContext = contextFactory;
        }

        public IEnumerable<RepairOrder> GetAvailableOrders(int skip, int take)
        {
            using (var context = GetContext())
            {
                var orders = (from o in context.RepairOrder
                    where o.RepairProcess == null
                    select new RepairOrder
                    {
                        RequiredWork = o.RequiredWork,
                        RepairOrderId = o.RepairOrderId,
                        Description = o.Description,
                        Reward = o.Reward,
                        CarName = o.CarName
                    }).Skip(skip).Take(take).ToArray();

                return orders;
            }
        }

        public IEnumerable<RepairProcess> GetHistoryOrders(Guid garageId, int skip, int take)
        {
            using (var context = GetContext())
            {
                var repairProcesses = (from rp in context.RepairProcess
                              where rp.GarageId == garageId && rp.IsPickedUp == true
                              select new RepairProcess
                              {
                                  CreadteDate = rp.CreatedDate,
                                  StallNumber = rp.StallNumber,
                                  Order = new RepairOrder
                                  {
                                      RequiredWork = rp.RepairOrder.RequiredWork,
                                      RepairOrderId = rp.RepairOrder.RepairOrderId,
                                      Description = rp.RepairOrder.Description,
                                      Reward = rp.RepairOrder.Reward,
                                      CarName = rp.RepairOrder.CarName
                                  },
                                  AssignedWorker = new Worker
                                  {
                                      WorkerId = rp.WorkerId,
                                      Salary = rp.Worker.Salary,
                                      Efficiency = rp.Worker.Efficiency,
                                      Name = rp.Worker.Name
                                  }
                              }
                              
                             ).Skip(skip).Take(take).ToArray();

                return repairProcesses;
            }
        }

        public void AssignOrder(Guid garageId, Guid orderId, Guid workerId, int stallNumber)
        {
            Db.RepairProcess repairProcess = new Db.RepairProcess
            {
                WorkerId = workerId,
                CreatedDate = DateTime.Now,
                GarageId = garageId,
                IsPickedUp = false,
                RepairOrderId = orderId,
                RepairProcessId = Guid.NewGuid(),
                StallNumber = stallNumber,
            };

            using (var context = GetContext())
            {
                context.RepairProcess.Add(repairProcess);
                context.SaveChanges();
            }
        }

        public void FinishOrder(Guid orderId)
        {
            using (var context = GetContext())
            {
                var repairProcess = (from rp in context.RepairProcess
                                     where rp.RepairOrderId == orderId
                                     select rp).FirstOrDefault();

                repairProcess.IsPickedUp = true;
                context.SaveChanges();
            }

        }
    }
}
