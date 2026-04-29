using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using SalaoDeBelezaLeila.Domain.Entities;

public class AppDbContext : DbContext
{
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Servico> Servicos { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
}