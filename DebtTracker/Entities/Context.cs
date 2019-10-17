using Microsoft.EntityFrameworkCore;

namespace DebtTracker.Entities
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
            Database.Migrate();
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Debt> Debts { get; set; }
    }
}
