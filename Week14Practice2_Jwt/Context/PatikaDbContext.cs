using Microsoft.EntityFrameworkCore;
using Week14Practice2_Jwt.Entities;

namespace Week14Practice2_Jwt.Context
{
    public class PatikaDbContext : DbContext
    {
        public PatikaDbContext(DbContextOptions<PatikaDbContext> options) : base(options) 
        {
            
        }

        public DbSet<UserEntity> Users => Set<UserEntity>();
    }
}
