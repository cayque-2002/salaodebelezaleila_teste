using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using SalaoDeBelezaLeila.Domain.Entities;

namespace SalaoDeBelezaLeila.Infra.Data;

public class AppDbContext : DbContext
{
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Servico> Servicos { get; set; }
    public DbSet<Agendamento> Agendamentos { get; set; }    
    public DbSet<Usuario> Usuarios { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
}