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

        public GarageRepository() { }

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
                                  GarageLevel = x.GarageLevel,
                                  EmployeedWorkers = new List<Worker>(from worker in x.Worker
                                                                      select new Worker()
                                                                      {
                                                                          WorkerId = worker.WorkerId,
                                                                          Salary = worker.Salary,
                                                                          Efficiency = worker.Efficiency,
                                                                          Name = worker.Name
                                                                      }),
                                  RepairProcesses = new List<RepairProcess>((from repairProcess in x.RepairProcess
                                                                             where repairProcess.IsPickedUp == false  && !repairProcess.IsCancelled
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
                                  CashBalance = (from gb in context.GarageBalance
                                                 where gb.GarageId == x.GarageId
                                                 select gb.Balance).FirstOrDefault()
                              }).AsEnumerable();

                return garage.FirstOrDefault();
            }

        }

        public decimal GetGarageBalance(Guid garageId)
        {
            using (var context = new Db.CarServiceContext())
            {
                var balance = (from gb in context.GarageBalance
                               where gb.GarageId == garageId
                               select gb.Balance).FirstOrDefault();

                return balance;
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
                    GarageLevel = 4
                };
                context.Garage.Add(garage);
                try
                {
                    context.SaveChanges();

                }
                catch (Exception e)
                {
                    return null;
                }
                return new Garage
                {
                    GarageId = garage.GarageId,
                    EmployeedWorkers = new List<Worker>(),
                    RepairProcesses = new List<RepairProcess>(),
                    CashBalance = 0,
                    GarageLevel = 4
                };
            }
        }

        public IEnumerable<GarageRanking> GetGaragesRanking(int count)
        {
            using (var context = GetContext())
            {
                var ranking =  new List<GarageRanking>(from garage in context.Garage
                                                                     select new GarageRanking()
                                                                     {
                                                                         GarageId = garage.GarageId,
                                                                         Name = garage.Name,
                                                                         CashBalance = (from gb in context.GarageBalance
                                                                                        where gb.GarageId == garage.GarageId
                                                                                        select gb.Balance).FirstOrDefault()
                                                                     }).Take(count);
                return ranking.OrderByDescending(x => x.CashBalance);
            }
        }

        public void UpgradeGarage(Guid garageId, decimal cost)
        {
            using (var context = GetContext())
            {
                var garage = (from g in context.Garage
                               where g.GarageId == garageId
                               select g).FirstOrDefault();
                garage.GarageLevel += 2;
                int level = garage.GarageLevel;
                context.SaveChanges();

                Db.GarageUpgrade gu = new Db.GarageUpgrade
                {
                    GarageUpgradeId = Guid.NewGuid(),
                    Cost = cost,
                    GarageId = garageId,
                    ResultLevel = level
                };
                context.GarageUpgrade.Add(gu);
                context.SaveChanges();
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
