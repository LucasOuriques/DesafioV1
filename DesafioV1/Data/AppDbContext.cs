using DesafioV1.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DesafioV1.Data {
    public class AppDbContext : DbContext {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Caixa> Caixa => Set<Caixa>();

    }

}
