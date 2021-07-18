using System;
using System.Collections.Generic;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfBasketDal:EfEntityRepositoryBase<Basket,NorthwindContext>,IBasketDal
    {
        public List<BasketDetailDto> GetBasketDetails(Expression<Func<BasketDetailDto, bool>> filter = null)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var result = from basket in context.Baskets
                    join product in context.product on basket.ProductId equals product.ProductId
                    join user in context.User on basket.UserId equals user.Id
                    select new BasketDetailDto
                    {
                        Id = basket.Id,
                        UserId = user.Id,
                        ProductId = product.ProductId,
                        ProductName = product.ProductName,
                        UserFullName = $"{user.FirstName} {user.LastName}",
                        Price = product.UnitPrice,
                        Count = basket.Count,
                        CreateDate = basket.CreateDate,
                        Active = basket.Active
                    };
                return filter == null ?  result.ToList() :  result.Where(filter).ToList();
            }
        }
    }
}