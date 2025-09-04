using entrega_viernes_5_09.Data.Helper;
using entrega_viernes_5_09.Data.Repositorys;
using entrega_viernes_5_09.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entrega_viernes_5_09.Data.Implements
{
    public class BillRepository : IBillRepository
    {
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Bill> GetAll()
        {
            throw new NotImplementedException();
        }

        public Bill? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Save(Bill bill)
        {
            return DataHelper.GetInstance().ExecuteBillTransaction(bill);
        }
    }
}
