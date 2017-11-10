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
    public class OrderRepositoryTest
    {
        DbContextOptions<Domain.Database.CarServiceContext> options = new DbContextOptionsBuilder<Domain.Database.CarServiceContext>()
              .UseInMemoryDatabase(databaseName: "OrderTestDb")
              .Options;

        private Guid workerId = Guid.NewGuid();
        private Guid repairOrderId = Guid.NewGuid();
        private Guid garageId = Guid.NewGuid();

        public OrderRepositoryTest()
        {
            var password = GarageRepository.GetSha256FromString("1234");
            using (var context = new Db.CarServiceContext(options))
            {
                context.Garage.Add(new Db.Garage()
                {
                    Name = "warsztat",
                    Password = password,
                    GarageId = garageId
                });
                context.RepairOrder.Add(new Db.RepairOrder()
                {
                    RepairOrderId = repairOrderId,
                    Reward = 1000
                });
                context.Worker.Add(new Db.Worker
                {
                    WorkerId = workerId,
                    Salary = 100
                });
                context.SaveChanges();
            }
        }

        [Fact]
        void AssignOrderTest()
        {
            OrderRepository orderRepository = new OrderRepository(() => new Db.CarServiceContext(options));

            orderRepository.AssignOrder(garageId, repairOrderId, workerId, 0);

            using (var context = new Db.CarServiceContext(options))
            {
                var repairProcesses = context.RepairProcess.Select(x => new
                {
                    workerId = x.WorkerId,
                    garageId = x.GarageId,
                    repairOrderId = x.RepairOrderId
                }).ToList();

                var repairProcess = repairProcesses.FirstOrDefault();

                Assert.Equal(1, repairProcesses.Count);
                Assert.Equal(repairProcess.workerId, workerId);
                Assert.Equal(repairProcess.garageId, garageId);
                Assert.Equal(repairProcess.repairOrderId, repairOrderId);
            }
        }
    }
}
