using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CryptoManager.Repository.DatabaseContext
{
    class ApplicationIdentityContextFactory : IDesignTimeDbContextFactory<ApplicationIdentityDbContext>
    {
        public ApplicationIdentityDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ApplicationIdentityDbContext>();
            builder.UseSqlServer("Data Source=(LocalDB)\\mssqllocaldb;Initial Catalog=CryptoManagerDB;Integrated Security=True;");

            return new ApplicationIdentityDbContext(builder.Options);
        }
    }
}
