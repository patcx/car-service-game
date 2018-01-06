using System;
using System.Collections.Generic;
using System.Text;

namespace CarServiceGame.Domain.Contracts
{
    public interface IPaymentRepository
    {
        void SendMail(Guid garageId, string mail);
        void ProcessCode(Guid garageId, string code);
    }
}
