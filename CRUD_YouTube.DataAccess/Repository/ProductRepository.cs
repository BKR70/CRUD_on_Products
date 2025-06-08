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

        public void Update(Product obj)
        {
            // explicitly update product
            var objFromDb = _db.Products.FirstOrDefault(u => u.Id == obj.Id);
            objFromDb.Title = obj.Title;
            objFromDb.ISBN = obj.ISBN;
            objFromDb.Price = obj.Price;
            objFromDb.Description = obj.Description;
            objFromDb.Price50 = obj.Price50;
            objFromDb.Price100 = obj.Price100;
            objFromDb.ListPrice = obj.ListPrice;
            objFromDb.CategoryId = obj.CategoryId;
            objFromDb.Author = obj.Author;
            if(obj.ImageUrl != null)
            {
                objFromDb.ImageUrl = obj.ImageUrl;
            }
        }
    }
}
