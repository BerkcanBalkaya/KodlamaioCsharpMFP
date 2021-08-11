using System;
using Business.Concrete;
using System.Linq;
using System.Linq.Expressions;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;

namespace ConsoleUI
{
    class Program
    {
        //SOLİD
        //O : Open Close principle : Geliştirilebilir ama değiştirilemez.
        //DTO : Data Transformation Object
        static void Main(string[] args)
        {
            ProductTest();
            //şimdilik newliyoruz ama ileride ioc container kullanacağız
            //CategoryTest();

        }

        private static void CategoryTest()
        {
            CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
            foreach (var category in categoryManager.GetAll())
            {
                Console.WriteLine(category.CategoryName);
            }
        }

        private static void ProductTest()
        {
            ProductManager productManager = new ProductManager(new EfProductDal());

            var result = productManager.GetProductDetails();

            if (result.Success==true)
            {
                foreach (var product in result.Data)
                {
                    Console.WriteLine(product.ProductName + " / " + product.CategoryName);
                }
            }
            else
            {
                Console.WriteLine(result.Message);
            }
            
        }
    }
}
