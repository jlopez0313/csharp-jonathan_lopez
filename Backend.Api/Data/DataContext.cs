using Backend.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Api.Data
{
    public class DataContext: DbContext
    {

        public DataContext(DbContextOptions <DataContext> options): base(options) { }

        public DbSet<Areas> Areas { get; set; }
        public DbSet<Subareas> Subareas { get; set; }
        public DbSet<Empleados> Empleados { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }

    }
}