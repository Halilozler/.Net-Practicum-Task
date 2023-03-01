using System;
using AutoMapper;
using Net_Core_Patika.DbOperations;

namespace Net_Core_Patika.Application.AuthorOperations.Commands
{
	public class GetAuthorDetailQuery
	{
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public int AuthorId { get; set; }

        public GetAuthorDetailQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public AuthorDetailViewModel Handle()
        {
            var author = _context.Authors.SingleOrDefault(x => x.Id == AuthorId);

            if(author is null)
            {
                throw new InvalidOperationException("Yaza Bulunamadı");
            }

            return _mapper.Map<AuthorDetailViewModel>(author);
        }
    }

    //Mappera ekledik.
    public class AuthorDetailViewModel
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public DateTime Birthday { get; set; }
    }
}


