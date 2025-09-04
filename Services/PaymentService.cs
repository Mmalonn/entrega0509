using entrega_viernes_5_09.Data.Implements;
using entrega_viernes_5_09.Data.Repositorys;
using entrega_viernes_5_09.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entrega_viernes_5_09.Services
{
    public class PaymentService
    {
        private IPaymentRepository _paymentRepository;
        public PaymentService()
        {
            _paymentRepository = new PaymentRepository();
        }
        public List<Payment> GetPayments()
        {
            return _paymentRepository.GetAll();
        }
        public bool SavePayment(Payment payment)
        {
            return _paymentRepository.Save(payment);
        }
        public Payment? GetPayment(int id)
        {
            return _paymentRepository.GetById(id);
        }
        public bool DeletePayment(int id)
        {
            return _paymentRepository.Delete(id);
        }
    }
}
