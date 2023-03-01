using System;
using Net_Core_Patika.DbOperations;
using Net_Core_Patika.Entity;

namespace WebApi.UnitTests.TestsSetup
{
    public static class Genres
    {
        public static void AddGenres(this BookStoreDbContext context)
        {
            //Genre için otomatik nesneler verdik.
            context.Genres.AddRange(
                new Genre { Name = "Personel Growth" },
                new Genre { Name = "Science Fiction" },
                new Genre { Name = "Romance" }
                );
        }
    }
}

