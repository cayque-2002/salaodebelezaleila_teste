using Microsoft.EntityFrameworkCore;
using SalaoDeBelezaLeila.Application.Dtos;
using SalaoDeBelezaLeila.Application.Services;
using SalaoDeBelezaLeila.Domain.Entities;
using SalaoDeBelezaLeila.Infra.Data;

public class ServicoService : IServicoService
{
    private readonly AppDbContext _context;

    public ServicoService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<ServicoDto>> GetAll()
    {
        return await _context.Servicos
            .Select(c => new ServicoDto
            {
                Id = c.Id,
                Nome = c.Nome,
                Preco = c.Preco
            }).ToListAsync();
    }

    public async Task<ServicoDto?> GetById(int id)
    {
        var s = await _context.Servicos.FindAsync(id);

        if (s == null) return null;

        return new ServicoDto
        {
            Id = s.Id,
            Nome = s.Nome,
            Preco = s.Preco
        };
    }

    public async Task<ServicoDto> Create(ServicoDto dto)
    {
        var entity = new Servico
        {
            Nome = dto.Nome,
            Preco = dto.Preco
        };

        _context.Servicos.Add(entity);
        await _context.SaveChangesAsync();

        dto.Id = entity.Id;
        return dto;
    }

    public async Task<bool> Update(int id, ServicoDto dto)
    {
        var entity = await _context.Servicos.FindAsync(id);

        if (entity == null) return false;

        entity.Nome = dto.Nome;
        entity.Preco = dto.Preco;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Delete(int id)
    {
        var entity = await _context.Servicos.FindAsync(id);

        if (entity == null) return false;

        _context.Servicos.Remove(entity);
        await _context.SaveChangesAsync();

        return true;
    }
}