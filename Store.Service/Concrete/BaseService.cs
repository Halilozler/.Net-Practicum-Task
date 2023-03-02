using System;
using AutoMapper;
using Movie_Store.Base;
using Movie_Store.Repository.Abstract;
using Store.Base.Response;
using Store.Data.UnitOfWork;
using Store.Service.Abstract;

namespace Store.Service.Concrete
{
	public class BaseService<Dto, Entity> : IBaseService<Dto, Entity> where Dto: class, IDto where Entity: class, IEntity 
	{
        private readonly IGenericRepository<Entity> genericRepository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public BaseService(IGenericRepository<Entity> genericRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.genericRepository = genericRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<IEnumerable<Dto>>> GetAllAsync()
        {
            // Get list record from DB
            var tempEntity = await genericRepository.GetAllAsync();
            // Mapping Entity to Resource
            var result = mapper.Map<IEnumerable<Entity>, IEnumerable<Dto>>(tempEntity);

            return BaseResponse<IEnumerable<Dto>>.Success(result, 200);
        }

        public Task<BaseResponse<Dto>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse<Dto>> InsertAsync(Dto insertResource)
        {
            try
            {
                // Mapping Resource to Entity
                var tempEntity = mapper.Map<Dto, Entity>(insertResource);

                await genericRepository.InsertAsync(tempEntity);
                await unitOfWork.CompleteAsync();

                var mapped = mapper.Map<Entity, Dto>(tempEntity);

                return BaseResponse<Dto>.Success(mapped, 201);
            }
            catch (Exception ex)
            {
                return BaseResponse<Dto>.Fail("Saving_Error",500);
            }
        }

        public Task<BaseResponse<Dto>> RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<Dto>> UpdateAsync(int id, Dto updateResource)
        {
            throw new NotImplementedException();
        }
    }
}

