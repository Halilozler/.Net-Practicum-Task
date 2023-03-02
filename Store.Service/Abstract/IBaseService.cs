using System;
using Movie_Store.Base;
using Store.Base.Response;

namespace Store.Service.Abstract
{
	public interface IBaseService<Dto, Entity> where Dto : class, IDto where Entity : class, IEntity
	{
        Task<BaseResponse<Dto>> GetByIdAsync(int id);
        Task<BaseResponse<IEnumerable<Dto>>> GetAllAsync();
        Task<BaseResponse<Dto>> InsertAsync(Dto insertResource);
        Task<BaseResponse<Dto>> UpdateAsync(int id, Dto updateResource);
        Task<BaseResponse<Dto>> RemoveAsync(int id);
    }
}

