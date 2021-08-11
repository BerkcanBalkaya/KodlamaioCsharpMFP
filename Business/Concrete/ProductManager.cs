using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;

namespace Business.Concrete
{
    public class ProductManager:IProductService
    {
        IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public IDataResult<List<Product>> GetAll()
        {
            //iş kodları
            if (DateTime.Now.Hour==1)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(),Messages.ProductsListed);
        }

        public IDataResult<List<Product>> GetAllByCategory(int id)
        {
            return new SuccessDataResult<List<Product>>( _productDal.GetAll(p => p.CategoryId == id));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
        }

        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
        }
        [ValidationAspect(typeof(ProductValidator))]
        public IResult Add(Product product)
        {
            //Validation ve business kodları ayrıdır birbiriyle karıştırmamalız
            //Nesnenin yapısal olarak uygun olup olmadığını kontrol etmeye validation denir.(min karakter,sayı aralığı,null olmamalı vs vs)
            //İş kuralları ise mesela 70 almış mı krediye uygun mu finansal puanına bakmak vs vs dir.
            //if (product.UnitPrice<=0)
            //{
            //    return new ErrorResult(Messages.UnitPriceInvalid);
            //}

            //if (product.ProductName.Length<2)
            //{
            //    //magic strings denir bunlara ve iyi değillerdir.
            //    //return new ErrorResult("Ürün ismi en az 2 karakter olmalıdır.")
            //    return new ErrorResult(Messages.ProductNameInvalid);
            //}

            
            //ValidationTool.Validate(new ProductValidator(),product);
            //Loglama
            //Cache
            //Performance
            //Transaction
            //yetkilendirme
            //Biz bunlarla uğraşacağımıza yukarıya bir aspect yazacağız ve bunların hepsi aspectin içerisinde olacak

            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
        }
    }
}
