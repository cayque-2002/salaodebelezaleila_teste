using Microsoft.EntityFrameworkCore;
using SalaoDeBelezaLeila.Application.Dtos;
using SalaoDeBelezaLeila.Application.Helpers;
using SalaoDeBelezaLeila.Application.Services;
using SalaoDeBelezaLeila.Domain.Entities;
using SalaoDeBelezaLeila.Infra.Data;
using System.Text.RegularExpressions;

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
        await ValidaUsuario(dto);

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

        if (entity.Email == dto.Email)
            throw new Exception("Email já cadastrado");

        await ValidaUsuario(dto);

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

        _context.Usuarios.Remove(entity);
        await _context.SaveChangesAsync();

        return true;
    }


    public async Task<UsuarioResponseDto> Login(LoginDto dto)
    {
        var usuario = await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Email == dto.Email);

        if (usuario == null)
            throw new Exception("Usuário ou senha inválidos");

        if (!PasswordHelper.Verify(dto.Senha, usuario.SenhaHash))
            throw new Exception("Usuário ou senha inválidos");

        return new UsuarioResponseDto
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Email = usuario.Email,
            Tipo = usuario.Tipo
        };
    }

    public async Task<bool> ValidaUsuario(UsuarioCreateDto dto)
    {

      //Validações Nome
        if (dto.Nome == null || dto.Nome == string.Empty)
            throw new Exception("Nome é obrigatório");

      //Validações Email
        if (dto.Email == null || dto.Email == string.Empty)
            throw new Exception("Email é obrigatório");

        if (!Regex.IsMatch(dto.Email, @"^[^\s@]+@[^\s@]+\.[^\s@]+$"))
        {
            throw new Exception("Email inválido");
        }

        return true;
    }

}