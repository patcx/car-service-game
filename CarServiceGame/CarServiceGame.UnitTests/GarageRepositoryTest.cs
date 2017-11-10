using System;
using System.Collections.Generic;
using System.Text;
using CarServiceGame.Domain.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Xunit;
using Db = CarServiceGame.Domain.Database;

namespace CarServiceGame.UnitTests
{

    public class GarageRepositoryTest
    {
        DbContextOptions<Db.CarServiceContext> options = new DbContextOptionsBuilder<Db.CarServiceContext>()
               .UseInMemoryDatabase(databaseName: "GarageTestDb")
               .Options;

        public GarageRepositoryTest()
        {
            var password = GarageRepository.GetSha256FromString("1234");
            using (var context = new Db.CarServiceContext(options))
            {
                context.Garage.Add(new Db.Garage()
                {
                    Name = "warsztat",
                    Password = password,
                    GarageId = Guid.NewGuid()
                });
                context.SaveChanges();
            }
        }

        [Theory]
        [InlineData("warsztat", "1234")]

        void GetExisitingGarage(string username, string password)
        {
            GarageRepository garageRepository = new GarageRepository(() => new Db.CarServiceContext(options));
            var garage = garageRepository.GetGarage(username, password);
            Assert.True(garage != null);
        }

        [Theory]
        [InlineData("warsz", "1234")]
        [InlineData("warsztat", "12345")]
        void GetNonExisitingGarage(string username, string password)
        {
            GarageRepository garageRepository = new GarageRepository(() => new Db.CarServiceContext(options));
            var garage = garageRepository.GetGarage(username, password);

            Assert.True(garage == null);
        }
    }
}
