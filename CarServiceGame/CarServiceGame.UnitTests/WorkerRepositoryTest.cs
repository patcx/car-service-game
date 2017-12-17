using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarServiceGame.Domain.Concrete;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Db=CarServiceGame.Domain.Database;

namespace CarServiceGame.UnitTests
{
    public class WorkerRepositoryTest
    {
        DbContextOptions<Domain.Database.CarServiceContext> options = new DbContextOptionsBuilder<Domain.Database.CarServiceContext>()
              .UseInMemoryDatabase(databaseName: "OrderTestDb")
              .Options;

        private Guid workerId = Guid.NewGuid();
        private Guid garageId = Guid.NewGuid();

        public WorkerRepositoryTest()
        {
            using (var context = new Db.CarServiceContext(options))
            {
                context.Garage.Add(new Db.Garage()
                {
                    Name = "warsztat",
                    Password = "password",
                    GarageId = garageId
                });
                context.Worker.Add(new Db.Worker
                {
                    WorkerId = workerId,
                });

                context.SaveChanges();
            }
        }

        [Fact]
        void HireWorkerTest()
        {
            WorkerRepository workerRepository = new WorkerRepository(() => new Db.CarServiceContext(options));
            workerRepository.EmployWorker(garageId, workerId);

            Guid? workerGarageId = null;

            using (var context = new Db.CarServiceContext(options))
            {
                workerGarageId = (from x in context.Worker
                    where x.WorkerId == workerId
                    select x.GarageId).FirstOrDefault();
            }

            Assert.Equal(garageId, workerGarageId);

        }

        [Fact]
        void FireWorkerTest()
        {
            WorkerRepository workerRepository = new WorkerRepository(() => new Db.CarServiceContext(options));

            using (var context = new Db.CarServiceContext(options))
            {
                var worker = (from x in context.Worker
                                  where x.WorkerId == workerId
                                  select x).FirstOrDefault();

                worker.GarageId = garageId;
                context.SaveChanges();
            }

            workerRepository.FireWorker(garageId, workerId);

            Guid? workerGarageId;
            using (var context = new Db.CarServiceContext(options))
            {
                workerGarageId = (from x in context.Worker
                              where x.WorkerId == workerId
                              select x.GarageId).FirstOrDefault();
            }

            Assert.Null(workerGarageId);
        }
    }
}
