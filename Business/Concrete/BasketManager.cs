using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Abstract;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Concrete
{
    public class BasketManager:IBasketService
    {
        private IBasketDal _basketDal;

        public BasketManager(IBasketDal basketDal)
        {
            _basketDal = basketDal;
        }

        public IDataResult<List<BasketDetailDto>> GetBasketDetails()
        {
            return new SuccessDataResult<List<BasketDetailDto>>(_basketDal.GetBasketDetails());

        }

        public IDataResult<List<BasketDetailDto>> GetBasketDetailsByUserId(int userId)
        {
            return new SuccessDataResult<List<BasketDetailDto>>(_basketDal.GetBasketDetails(c=>c.UserId==userId));
        }

        public IResult Add(Basket basket)
        {
            _basketDal.Add(basket);
            return new SuccessResult("Successfully, Added To Basket");

        }

        public IResult Delete(Basket basket)
        {
             _basketDal.Delete(basket);
            return new SuccessResult("Successfully, Deleted From Basket");
        }

        public IDataResult<List<Basket>> GetAll()
        {
            return new SuccessDataResult<List<Basket>>(_basketDal.GetAll());

        }

        public IResult Update(Basket basket)
        {
             _basketDal.Update(basket);
            return new SuccessResult("Successfully,Updated Basket");
        }
    }
}