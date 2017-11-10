using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarServiceGame.Domain.Contracts;
using CarServiceGame.Domain.Entities;
using Db=CarServiceGame.Domain.Database;

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

                return workers;
            }
        }

        public void FireWorker(Guid workerId)
        {
            using (var context = GetContext())
            {
                var worker = (from w in context.Worker
                    where w.WorkerId == workerId
                    select w).FirstOrDefault();

                worker.GarageId = null;
                context.SaveChanges();
            }
        }

        public void EmployWorker(Guid garageId, Guid workerId)
        {
            using (var context = GetContext())
            {
                var worker = (from w in context.Worker
                              where w.WorkerId == workerId
                              select w).FirstOrDefault();

                worker.GarageId = garageId;
                context.SaveChanges();
            }
        }
    }
}
