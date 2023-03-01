using System;
using AutoMapper;
using Net_Core_Patika.DbOperations;
using Net_Core_Patika.Entity;
using static Net_Core_Patika.BookOperations.CreateBook.CreateBookCommand;

namespace Net_Core_Patika.Application.UserOperations.Commands.CreateUser
{
    public class CreateUserCommand
    {
        public CreateUserModel Model { get; set; }

        private readonly IBookStoreDbContext context;

        //Mapperları oluşutrudk.
        private readonly IMapper mapper;

        public CreateUserCommand(IBookStoreDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public void Handle()
        {
            var user = context.Users.SingleOrDefault(x => x.Email == Model.Email);

            if (user is not null)
            {
                throw new InvalidOperationException("Kullanıcı zaten mevcut");
            }

            //Modeli User objesine dönüştür dedik.
            user = mapper.Map<User>(Model);

            context.Users.Add(user);
            context.SaveChanges();
        }
    }

    public class CreateUserModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

