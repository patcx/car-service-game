using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarServiceGame.Domain.Contracts;
using CarServiceGame.Domain.Infrastructure;
using Db=CarServiceGame.Domain.Database;

namespace CarServiceGame.Domain.Concrete
{
    public class PaymentRepository : IPaymentRepository
    {
        private IMailClient _mailClient;
        private Func<Db.CarServiceContext> GetContext;

        public PaymentRepository(IMailClient mailClient)
        {
            _mailClient = mailClient;
        }

        public PaymentRepository(IMailClient mailClient, Func<Db.CarServiceContext> contextFactory = null)
        {
            _mailClient = mailClient;

            if (contextFactory == null)
                GetContext = () => new Db.CarServiceContext();
            else
                GetContext = contextFactory;
        }

        public void SendMail(Guid garageId, string mail)
        {
            var code = Guid.NewGuid().ToString();
            _mailClient.SendMail(mail, $"CODE: {code}", "CarServiceGame - Bonus");

            using (var context = GetContext())
            {
                context.Payment.Add(new Db.Payment
                {
                    GarageId = garageId,
                    BonusValue = 5000,
                    Code = code,
                    Email = mail,
                    GarageBonusId = Guid.NewGuid(),
                    IsActivated = false,
                });

                context.SaveChanges();
            }
        }

        public void ProcessCode(Guid garageId, string code)
        {
            using (var context = GetContext())
            {
                var payment = context.Payment.First(x => x.GarageId == garageId && x.Code == code);
                payment.IsActivated = true;
                context.SaveChanges();
            }
        }
    }
}
