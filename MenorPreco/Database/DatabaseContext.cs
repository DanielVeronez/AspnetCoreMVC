using MenorPreco.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MenorPreco.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<ProdutoModel> Produtos { get; set; }
        public DbSet<EstabelecimentoModel> Estabelecimentos { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=blogging.db");

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
