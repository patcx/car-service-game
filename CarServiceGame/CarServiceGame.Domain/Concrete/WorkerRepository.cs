using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarServiceGame.Domain.Contracts;
using CarServiceGame.Domain.Entities;
using Db = CarServiceGame.Domain.Database;

namespace CarServiceGame.Domain.Concrete
{
    public class WorkerRepository : IWorkerRepository
    {

        private Func<Db.CarServiceContext> GetContext;

        public WorkerRepository(Func<Db.CarServiceContext> contextFactory = null)
        {
            if (contextFactory == null)
                GetContext = () => new Db.CarServiceContext();
            else
                GetContext = contextFactory;
        }

        public IEnumerable<Worker> GetUnemployedWorkers(int skip, int take)
        {
            using (var context = GetContext())
            {
                var workers = (from w in context.Worker
                               where w.GarageId == null
                               select new Worker
                               {
                                   WorkerId = w.WorkerId,
                                   Salary = w.Salary,
                                   Efficiency = w.Efficiency,
                                   Name = w.Name

                               }).Skip(skip).Take(take).ToArray();
                if (workers.Length < 3) GenerateNewWorkers();
                return workers;
            }
        }

        private void GenerateNewWorkers()
        {
            String[] names = { "Karol", "Patryk", "Adam", "Mateusz", "Wojciech", "Zenon", "Wincenty", "Donald", "Paweł", "Maciej", "Klaudia", "Anna", "Patrycja", "Jakub", "Ewelina" };

            Random r = new Random();

            using (var context = GetContext())
            {
                for (int i = 0; i < 7; i++)
                {
                    Db.Worker worker = new Db.Worker
                    {
                        Name = names[r.Next(names.Length)],
                        Efficiency = r.Next(10, 101),
                        Salary = r.Next(5, 100) * 10,
                        WorkerId = Guid.NewGuid(),
                    };
                    context.Worker.Add(worker);
                }
                context.SaveChanges();
            }
        }

        public void FireWorker(Guid garageId, Guid workerId)
        {
            using (var context = GetContext())
            {
                bool isBusy = (from rp in context.RepairProcess
                    where rp.WorkerId == workerId &&
                          rp.GarageId == garageId &&
                          rp.IsPickedUp == false && rp.IsCancelled == false
                    select rp.RepairOrderId).Any();

                if(isBusy)
                    throw new Exception("worker is working");

                var worker = (from w in context.Worker
                              where w.WorkerId == workerId && w.GarageId == garageId
                              select w).FirstOrDefault();

                worker.GarageId = null;
                context.SaveChanges();
            }
        }

        public void UpgradeWorker(Guid garageId, Guid workerId, decimal cost)
        {
            using (var context = GetContext())
            {
                bool isBusy = (from rp in context.RepairProcess
                               where rp.WorkerId == workerId &&
                                     rp.GarageId == garageId &&
                                     rp.IsPickedUp == false && rp.IsCancelled == false
                               select rp.RepairOrderId).Any();

                if (isBusy)
                    throw new Exception("worker is working");

                var worker = (from w in context.Worker
                              where w.WorkerId == workerId && w.GarageId == garageId
                              select w).FirstOrDefault();

                if(worker.Efficiency + 10 > 100)
                    throw new Exception("Worker cannot be upgrades");

                worker.Efficiency += 10;
                context.SaveChanges();
            }

            using (var context = GetContext())
            {
                Db.WorkerUpgrade wu = new Db.WorkerUpgrade
                {
                    WorkerUpgradeId = Guid.NewGuid(),
                    Cost = cost,
                    EfficiencyIncrease = 10,
                    GarageId = garageId,
                    WorkerId = workerId,
                };
                context.WorkerUpgrade.Add(wu);
                context.SaveChanges();
            }
        }


        public void EmployWorker(Guid garageId, Guid workerId)
        {
            using (var context = GetContext())
            {
                var worker = (from w in context.Worker
                              where w.WorkerId == workerId && w.GarageId == null
                              select w).FirstOrDefault();

                worker.GarageId = garageId;
                context.SaveChanges();
            }
        }
    }
}
