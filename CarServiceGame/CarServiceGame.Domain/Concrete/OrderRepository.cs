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
        private readonly int generatingCount = 5;
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

                if (orders.Length < 3) GenerateNewOrders();
                return orders;
            }
        }

        private void GenerateNewOrders()
        {
            String[] names = { "Seat Ibiza", "Audi A4", "BMW M5", "Opel Insignia", "BMW X6", "Ford Focus", "Fiat Punto", "Fiat Multipla", "Citroen C4" , "Toyota Auris" };
            String[] desctiptions = { "Broken Engine", "Need to replace brakes", "Need to replace gearbox", "Broken Wheel", "Cannot start the car" };

            Random r = new Random();
            using (var context = GetContext())
            {
                for (int i = 0; i < generatingCount; i++)
                {
                    Db.RepairOrder order = new Db.RepairOrder
                    {
                        RepairOrderId = Guid.NewGuid(),
                        Reward = r.Next(2, 101) * 100,
                        CarName = names[r.Next(names.Length)],
                        Description = desctiptions[r.Next(desctiptions.Length)],
                        RequiredWork = r.Next(10001) ,
                    };
                    context.RepairOrder.Add(order);
                }
                context.SaveChanges();
            }
        }

        public IEnumerable<RepairProcess> GetHistoryOrders(Guid garageId, int skip, int take)
        {
            using (var context = GetContext())
            {
                var repairProcesses = (from rp in context.RepairProcess
                                       where rp.GarageId == garageId && rp.IsPickedUp == true && !rp.IsCancelled
                                       select new RepairProcess
                                       {
                                           CreatedDate = rp.CreatedDate,
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

        public void FinishOrder(Guid garageId, Guid orderId)
        {
            using (var context = GetContext())
            {
                var models = (from rp in context.RepairProcess
                                     where rp.RepairOrderId == orderId && rp.GarageId == garageId && rp.IsCancelled == false
                                     select new
                                     {
                                         repairProcess = rp,
                                         worker = rp.Worker,
                                         order = rp.RepairOrder
                                     }).FirstOrDefault();

                if (models.order.RequiredWork / models.worker.Efficiency >
                    (int) DateTime.Now.Subtract(models.repairProcess.CreatedDate).TotalSeconds)
                {
                    throw new Exception("Order cannot be finished");
                }
                else
                {
                    models.repairProcess.IsPickedUp = true;
                    context.SaveChanges();
                }
            }

        }

        public void CancelOrder(Guid orderId)
        {
            using (var context = GetContext())
            {
                var repairProcess = (from rp in context.RepairProcess
                                     where rp.RepairOrderId == orderId
                                     select rp).FirstOrDefault();

                repairProcess.IsCancelled = true;
                context.SaveChanges();
            }
        }
    }
}
