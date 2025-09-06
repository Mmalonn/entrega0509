using entrega_viernes_5_09.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entrega_viernes_5_09.Data.Repositorys
{
    public interface IArticleRepository
    {
        List<Article> GetAll();
        Article? GetById(int id);
        bool Save(Article article);
        bool Delete(int id);
    }
}
