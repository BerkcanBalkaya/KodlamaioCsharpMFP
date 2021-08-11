using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryProductDal:IProductDal
    {
        //global değişkenleri _ ile kullanılır classlarda genelde
        private List<Product> _products;

        public InMemoryProductDal()
        {
            _products = new List<Product>()
            {
                new Product{ProductId = 1,CategoryId = 1,ProductName = "Bardak",UnitPrice = 15,UnitsInStock = 15},
                new Product{ProductId = 2,CategoryId = 1,ProductName = "Kamera",UnitPrice = 500,UnitsInStock = 3},
                new Product{ProductId = 3,CategoryId = 2,ProductName = "Telefon",UnitPrice = 1500,UnitsInStock = 2},
                new Product{ProductId = 4,CategoryId = 2,ProductName = "Klavye",UnitPrice = 150,UnitsInStock = 65},
                new Product{ProductId = 5,CategoryId = 2,ProductName = "Fare",UnitPrice = 85,UnitsInStock = 1}
            };
        }
        public List<Product> GetAll()
        {
            return _products;

        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public void Add(Product product)
        {
            _products.Add(product);
        }

        public void Update(Product product)
        {
            //Gönderdiğim ürün id sine sahip olan listedeki ürünü bul diyoruz biz aşağıda 
            Product productToUpdate = _products.SingleOrDefault(p => p.ProductId == product.ProductId);
            productToUpdate.ProductName = product.ProductName;
            productToUpdate.CategoryId = product.CategoryId;
            productToUpdate.UnitPrice = product.UnitPrice;
            productToUpdate.UnitsInStock = product.UnitsInStock;
        }

        public void Delete(Product product)
        {
            //Product productToDelete;
            //foreach (var p in _products)
            //{
            //    if (product.ProductId == p.ProductId)
            //    {
            //        productToDelete = p;
            //    }
            //}
            //  => bunun anlamı for each gibi çalışmasıdır ve aşağıda yaptığımız kod ile üstteki kodların hepsini tek satırda yapmış oluruz.
            //single or defaultta birden fazla veri gelirse hata verir yani atama yapmaz
            Product productToDelete = _products.SingleOrDefault(p=>p.ProductId==product.ProductId);

        }

        public List<ProductDetailDto> GetProductDetails()
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAllCategory(int categoryId)
        {
            //where koşuluna uyanları yeni bir listeye çevirip bunu döndürür
            return _products.Where(p => p.CategoryId == categoryId).ToList();
        }
    }
}
