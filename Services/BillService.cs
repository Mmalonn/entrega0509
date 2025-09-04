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
    public class BillService
    {
        private IBillRepository _billRepository;
        public BillService()
        {
            _billRepository = new BillRepository();
        }
        public List<Bill> GetBills()
        {
            return _billRepository.GetAll();
        }
        public bool SaveBill(Bill bill)
        {
            return _billRepository.Save(bill);
        }
        public Bill? GetArticle(int id)
        {
            return _billRepository.GetById(id);
        }
        public bool DeleteBill(int id)
        {
            return _billRepository.Delete(id);
        }
    }
}
