using Microsoft.EntityFrameworkCore;
using SalaoDeBelezaLeila.Application.Dtos;
using SalaoDeBelezaLeila.Application.Services;
using SalaoDeBelezaLeila.Domain.Entities;
using SalaoDeBelezaLeila.Infra.Data;

public class ClienteService : IClienteService
{
    private readonly AppDbContext _context;

    public ClienteService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<ClienteDto>> GetAll()
    {
        return await _context.Clientes
            .Select(c => new ClienteDto
            {
                Id = c.Id,
                Nome = c.Nome,
                Telefone = c.Telefone
            }).ToListAsync();
    }

    public async Task<ClienteDto?> GetById(int id)
    {
        var c = await _context.Clientes.FindAsync(id);

        if (c == null) return null;

        return new ClienteDto
        {
            Id = c.Id,
            Nome = c.Nome,
            Telefone = c.Telefone
        };
    }

    public async Task<ClienteDto> Create(ClienteDto dto)
    {
        var entity = new Cliente
        {
            Nome = dto.Nome,
            Telefone = dto.Telefone
        };

        _context.Clientes.Add(entity);
        await _context.SaveChangesAsync();

        dto.Id = entity.Id;
        return dto;
    }

    public async Task<bool> Update(int id, ClienteDto dto)
    {
        var entity = await _context.Clientes.FindAsync(id);

        if (entity == null) return false;

        entity.Nome = dto.Nome;
        entity.Telefone = dto.Telefone;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Delete(int id)
    {
        var entity = await _context.Clientes.FindAsync(id);

        if (entity == null) return false;

        _context.Clientes.Remove(entity);
        await _context.SaveChangesAsync();

        return true;
    }
}