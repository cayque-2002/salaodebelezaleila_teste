using Microsoft.EntityFrameworkCore;
using SalaoDeBelezaLeila.Application.Dtos;
using SalaoDeBelezaLeila.Application.Services;
using SalaoDeBelezaLeila.Domain.Entities;
using SalaoDeBelezaLeila.Domain.Enums;
using SalaoDeBelezaLeila.Infra.Data;

public class ClienteService : IClienteService
{
    private readonly AppDbContext _context;
    private readonly IUsuarioService _usuarioService;

    public ClienteService(AppDbContext context, IUsuarioService usuarioService)
    {
        _context = context;
        _usuarioService = usuarioService;
    }

    public async Task<List<ClienteDto>> GetAll()
    {
        return await _context.Clientes
            .Select(c => new ClienteDto
            {
                Id = c.Id,
                Nome = c.Nome,
                Telefone = c.Telefone,
                TipoUsuario = c.UsuarioId.HasValue ? TipoUsuario.Comum : TipoUsuario.Admin,
                UsuarioId = c.UsuarioId
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
            Telefone = c.Telefone,
            TipoUsuario = c.UsuarioId.HasValue ? TipoUsuario.Comum : TipoUsuario.Admin,
            UsuarioId = c.UsuarioId

        };
    }

    public async Task<ClienteDto?> ObterPorUsuarioId(int usuarioId)
    {
        var c = await _context.Clientes
            .FirstOrDefaultAsync(c => c.UsuarioId == usuarioId);

        return new ClienteDto
        {
            Id = c.Id,
            Nome = c.Nome,
            Telefone = c.Telefone,
            TipoUsuario = c.UsuarioId.HasValue ? TipoUsuario.Comum : TipoUsuario.Admin,
            UsuarioId = c.UsuarioId
        };
    }

    public async Task<ClienteDto> Create(ClienteDto dto)
    {

        var entity = new Cliente
        {
            Nome = dto.Nome,
            Telefone = dto.Telefone,
            UsuarioId = dto.UsuarioId ?? null
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
        entity.UsuarioId = dto.UsuarioId ?? null;

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