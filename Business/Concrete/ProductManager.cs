using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.CCS;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }


        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]
        [PerformanceAspect(30)]
     
        public IResult Add(Product product)
        {

            IResult result = BusinessRules.Run(CheckIfProductNameExists(product.ProductName)
            ,CheckIfProductNameDoesNotBreakeRules(product.ProductName));

            if (result != null)
            {
                return result;
            }

            _productDal.Add(product);

            return new SuccessResult(Messages.ProductAdded);

        }


        [CacheAspect]
         public IDataResult<List<Product>> GetAll()
        {
            if (DateTime.Now.Hour == 1)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }

            return new SuccessDataResult<List<Product>>(_productDal.GetAll(), Messages.ProductsListed);
        }



        [CacheAspect]
         public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
        }


        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Update(Product product)
        {

            throw new NotImplementedException();
        }



        private IResult CheckIfProductNameExists(string productName)
        {
            var result = _productDal.GetAll(p => p.ProductName == productName).Any();
            if (result)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists);
            }
            return new SuccessResult();
        }



        public IResult AddTransactionalTest(Product product)
        {

            Add(product);
            if (product.UnitPrice < 10)
            {
                throw new Exception("");
            }

            Add(product);

            return null;
        }

        [SecuredOperation("products.add,admin")]
        public IDataResult<Product> GetProductsByCategory(string CategoryName)
        {
            BusinessRules.Run(CheckIfCategoryNameExists(CategoryName));
            return new SuccessDataResult<Product>(_productDal.Get(p => p.CategoryName == CategoryName));
        }

        private IResult CheckIfCategoryNameExists(string categoryName)
        {
            bool IsValidCategory = _productDal.Get(p => p.CategoryName == categoryName).Equals(true);
            if (!IsValidCategory)
            {
                return new ErrorResult("Product Category Does Not Exists,Please Try Again");
            }
            return new SuccessResult();

        }
        private IResult CheckIfProductNameDoesNotBreakeRules(string ProductName)
        {
            string[] cars = { "Sigara", "Tutun", "Bira" };
            foreach (string i in cars) {
                if (ProductName.Contains(i)) {
                    return new ErrorResult("this behavior against our rules,Product Name can not be :" + i);
                }
            }
            return new SuccessResult();
        }
   

    }
}
