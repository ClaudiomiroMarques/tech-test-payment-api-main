using Microsoft.EntityFrameworkCore;
using tech_test_payment_api.Models;
using System.Text.Json;

namespace tech_test_payment_api.Context{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { 

        }

        public DbSet<Vendedor> Vendedores { get; set; }
        public DbSet<Venda> Vendas { get; set; }
        public DbSet<Produto> Produtos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Venda>().Property(v => v.Produtos)
            .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions)default),
                v => JsonSerializer.Deserialize<List<Produto>>(v, (JsonSerializerOptions)default));


            base.OnModelCreating(modelBuilder);
        modelBuilder.Ignore <List<string>>();
        modelBuilder.Ignore <ICollection<String>>();
        }

    }
}