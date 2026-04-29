using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalaoDeBelezaLeila.Application.Dtos;

namespace SalaoDeBelezaLeila.Application.Services;

public interface IServicoService
{
    Task<List<ServicoDto>> GetAll();
    Task<ServicoDto?> GetById(int id);
    Task<ServicoDto> Create(ServicoDto dto);
    Task<bool> Update(int id, ServicoDto dto);
    Task<bool> Delete(int id);
}

