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

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite("Filename=MenorPreco.db");
        //}
    }
}
