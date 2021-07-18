using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserImageDal : EfEntityRepositoryBase<Entities.Concrete.UserImage, NorthwindContext>, IUserImageDal
    {
        public object ImagePath => throw new System.NotImplementedException();
    }
}