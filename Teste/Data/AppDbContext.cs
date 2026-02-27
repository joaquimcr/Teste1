using Microsoft.EntityFrameworkCore;
using Teste.Model;

namespace Teste.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)        
            => optionsBuilder.UseSqlite(connectionString:"DataSource=app.db;Cache=Shared");
        

    }
}
