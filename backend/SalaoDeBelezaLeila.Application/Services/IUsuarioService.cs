using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalaoDeBelezaLeila.Application.Dtos;

namespace SalaoDeBelezaLeila.Application.Services;

public interface IUsuarioService
{
    Task<List<UsuarioResponseDto>> GetAll();
    Task<UsuarioResponseDto?> GetById(int id);
    Task<UsuarioCreateDto> Create(UsuarioCreateDto dto);
    Task<bool> Update(int id, UsuarioCreateDto dto);
    Task<bool> Delete(int id);
}

