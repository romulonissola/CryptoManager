using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CryptoManager.Repository.DatabaseContext
{
    class EntityContextFactory : IDesignTimeDbContextFactory<EntityContext>
    {
        public EntityContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<EntityContext>();
            builder.UseSqlServer("Data Source=(LocalDB)\\mssqllocaldb;Initial Catalog=CryptoManagerDB;Integrated Security=True;");

            return new EntityContext(builder.Options);
        }
    }
}
