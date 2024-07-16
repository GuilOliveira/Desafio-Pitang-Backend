using DesafioPitang.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace DesafioPitang.UnitTests
{
    public class BaseUnitTest
    {
        private readonly IServiceCollection ServiceCollection = new ServiceCollection();
        protected Context _context;

        protected void Register<I, T>() where I : class where T : class, I
          => ServiceCollection.AddSingleton<I, T>();

        protected I GetService<I>() where I : class
          => ServiceCollection.BuildServiceProvider().GetService<I>();

        protected void RegisterObject<Tp, T>(Tp type, T _object) where Tp : Type where T : class
           => ServiceCollection.AddSingleton(type, _object);


        [OneTimeSetUp]
        public void OneTimeSetUpBase()
        {
            ConfigureInMemoryDataBase();
            DatabaseSeeder.Seed(_context);
        }

        [OneTimeTearDown]
        public void OneTimeTearDownBase()
        {
            _context.Dispose();
        }

        private void ConfigureInMemoryDataBase()
        {
            var options = new DbContextOptionsBuilder<Context>()
                              .UseInMemoryDatabase("InMemoryDatabase")
                              .Options;

            _context = new Context(options);

            if (_context.Database.IsInMemory())
                _context.Database.EnsureDeleted();

            _context.Database.EnsureCreated();
        }
    }
}
