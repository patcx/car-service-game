using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using CarServiceGame.Domain.Contracts;
using CarServiceGame.Domain.Entities;
using Db = CarServiceGame.Domain.Database;

namespace CarServiceGame.Domain.Concrete
{
    public class GarageRepository : IGarageRepository
    {
        private Func<Db.CarServiceContext> GetContext;

        public GarageRepository(Func<Db.CarServiceContext> contextFactory = null)
        {
            if (contextFactory == null)
                GetContext = () => new Db.CarServiceContext();
            else
                GetContext = contextFactory;
        }

        public Garage GetGarage(string name, string password)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(password))
                return null;

            var hashedPassword = GetSha256FromString(password);

            using (var context = GetContext())
            {
                var garage = (from x in context.Garage
                              where x.Name == name && x.Password == hashedPassword
                              select new Garage()
                              {
                                  GarageId = x.GarageId,
                                  EmployeedWorkers = new List<Worker>(from worker in x.Worker
                                                                      select new Worker()
                                                                      {
                                                                          WorkerId = worker.WorkerId,
                                                                          Salary = worker.Salary,
                                                                          Efficiency = worker.Efficiency,
                                                                          Name = worker.Name
                                                                      }),
                                  RepairProcesses = new List<RepairProcess>((from repairProcess in x.RepairProcess
                                                                             where repairProcess.IsPickedUp == false
                                                                             select new RepairProcess
                                                                             {
                                                                                 Order = new RepairOrder()
                                                                                 {
                                                                                     RepairOrderId = repairProcess.RepairOrderId,
                                                                                     RequiredWork = repairProcess.RepairOrder.RequiredWork,
                                                                                     Description = repairProcess.RepairOrder.Description,
                                                                                     Reward = repairProcess.RepairOrder.Reward,
                                                                                     CarName = repairProcess.RepairOrder.CarName,
                                                                                 },
                                                                                 AssignedWorker = new Worker()
                                                                                 {
                                                                                     WorkerId = repairProcess.WorkerId,
                                                                                     Salary = repairProcess.Worker.Salary,
                                                                                     Efficiency = repairProcess.Worker.Efficiency,
                                                                                     Name = repairProcess.Worker.Name
                                                                                 },
                                                                                 CreatedDate = repairProcess.CreatedDate,
                                                                                 StallNumber = repairProcess.StallNumber
                                                                             })).ToList(),
                                  CashBalance = (from rp in x.RepairProcess
                                                 where rp.IsPickedUp == true
                                                 select rp.RepairOrder.Reward - rp.Worker.Salary).Sum()
                              }).AsEnumerable();

                return garage.FirstOrDefault();
            }

        }

        public Garage CreateGarage(string name, string password)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(password))
                return null;
            var hashedPassword = GetSha256FromString(password);

            using (var context = GetContext())
            {
                {
                    var garage = (from x in context.Garage
                                  where x.Name == name && x.Password == hashedPassword
                                  select new Garage
                                  {
                                      GarageId = x.GarageId
                                  });
                    if (garage.ToArray().Length != 0)
                    {
                        return null;
                    }
            }
            }

            using (var context = GetContext())
            {
                Db.Garage garage = new Db.Garage
                {
                    Name = name,
                    Password = hashedPassword,
                    GarageId = Guid.NewGuid(),
                };
                context.Garage.Add(garage);
                context.SaveChanges();
                return new Garage
                {
                    GarageId = garage.GarageId,
                    EmployeedWorkers = new List<Worker>(),
                    RepairProcesses = new List<RepairProcess>(),
                    CashBalance = 0
                };
            }
        }

        public static string GetSha256FromString(string strData)
        {
            var message = Encoding.ASCII.GetBytes(strData);
            SHA256Managed hashString = new SHA256Managed();
            string hex = "";

            var hashValue = hashString.ComputeHash(message);
            foreach (byte x in hashValue)
            {
                hex += String.Format("{0:x2}", x);
            }
            return hex;
        }
    }
}
