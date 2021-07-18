using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface IBasketService 
    {
        IDataResult<List<BasketDetailDto>> GetBasketDetails();
        IDataResult<List<BasketDetailDto>> GetBasketDetailsByUserId(int userId);
        IResult Add(Basket basket);
        IResult Delete(Basket basket);
        IDataResult<List<Basket>> GetAll();

        IResult Update(Basket basket);


    }
}