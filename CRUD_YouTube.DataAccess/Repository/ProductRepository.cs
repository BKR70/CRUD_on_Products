using CRUD_YouTube.DataAccess.Data;
using CRUD_YouTube.DataAccess.Repository.IRepository;
using CRUD_YouTube.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_YouTube.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Product prod)
        {
            _db.Products.Update(prod);
        }
    }
}
