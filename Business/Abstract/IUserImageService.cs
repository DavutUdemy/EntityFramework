using System;
using System.Collections.Generic;
using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;

namespace Business.Abstract
{
    public interface IUserImageService
    {
        IResult Add(IFormFile file, UserImage userImage);
        IResult Delete(UserImage userImage);
        IResult Update(IFormFile file,UserImage userImage);
        IDataResult<UserImage> Get(int id);
        IDataResult<List<UserImage>> GetAll();
        IDataResult<List<UserImage>> GetImagesByTId(int id);

    }
}
