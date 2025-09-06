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
    public class ArticleService
    {
        private IArticleRepository _articleRepository;
        public ArticleService()
        {
            _articleRepository = new ArticleRepository();
        }
        public List<Article> GetArticles()
        {
            return _articleRepository.GetAll();
        }
        public bool SaveArticle(Article article)
        {
            return _articleRepository.Save(article);
        }
        public Article? GetArticle(int id)
        {
            return _articleRepository.GetById(id);
        }
        public bool DeleteArticle(int id) {
            return _articleRepository.Delete(id);
        }
    }
}
