using Microsoft.EntityFrameworkCore;
using SalaoDeBelezaLeila.Application.Dtos;
using SalaoDeBelezaLeila.Application.Services;
using SalaoDeBelezaLeila.Domain.Entities;
using SalaoDeBelezaLeila.Infra.Data;
using SalaoDeBelezaLeila.Application.Helpers;

public class UsuarioService : IUsuarioService
{
    private readonly AppDbContext _context;

    public UsuarioService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<UsuarioResponseDto>> GetAll()
    {
        return await _context.Usuarios
            .Select(u => new UsuarioResponseDto
            {
                Id = u.Id,
                Nome = u.Nome,
                Email = u.Email,
                Tipo = u.Tipo
            }).ToListAsync();
    }

    public async Task<UsuarioResponseDto?> GetById(int id)
    {
        var u = await _context.Usuarios.FindAsync(id);

        if (u == null) return null;

        return new UsuarioResponseDto
        {
            Id = u.Id,
            Nome = u.Nome,
            Email = u.Email,
            Tipo = u.Tipo
        };
    }

    public async Task<UsuarioCreateDto> Create(UsuarioCreateDto dto)
    {
        if (_context.Usuarios.Any(u => u.Email == dto.Email))
            throw new Exception("Email já cadastrado");

        var entity = new Usuario
        {
            Nome = dto.Nome,
            Email = dto.Email,
            Tipo = dto.Tipo,
            SenhaHash =  PasswordHelper.Hash(dto.Senha)
        };

        _context.Usuarios.Add(entity);
        await _context.SaveChangesAsync();

        dto.Id = entity.Id;
        return dto;
    }

    public async Task<bool> Update(int id, UsuarioCreateDto dto)
    {
        var entity = await _context.Usuarios.FindAsync(id);

        if (entity == null) throw new Exception("Usuário não encontrado");

        if (_context.Usuarios.Any(u => u.Email == dto.Email))
            throw new Exception("Email já cadastrado");

        //if (entity == null) return false;

        entity.Nome = dto.Nome;
        entity.Email = dto.Email;
        entity.Tipo = dto.Tipo;
        if (dto.Senha != null || dto.Senha != string.Empty)
        {
            entity.SenhaHash = PasswordHelper.Hash(dto.Senha); // Em um cenário real, você deveria hash a senha antes de salvar
        }

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Delete(int id)
    {
        var entity = await _context.Usuarios.FindAsync(id);

        if (entity == null) throw new Exception("Usuário não encontrado");

        //if (entity == null) return false;

        _context.Usuarios.Remove(entity);
        await _context.SaveChangesAsync();

        return true;
    }
}