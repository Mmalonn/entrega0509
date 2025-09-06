using entrega_viernes_5_09.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entrega_viernes_5_09.Data.Repositorys
{
    public interface IBillRepository
    {
        List<Bill> GetAll();
        Bill? GetById(int id);
        bool Save(Bill bill);
        bool Delete(int id);
    }
}
