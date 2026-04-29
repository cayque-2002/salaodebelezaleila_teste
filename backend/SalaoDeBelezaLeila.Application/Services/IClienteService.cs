using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalaoDeBelezaLeila.Application.Dtos;

namespace SalaoDeBelezaLeila.Application.Services;

public interface IClienteService
{
    Task<List<ClienteDto>> GetAll();
    Task<ClienteDto?> GetById(int id);
    Task<ClienteDto> Create(ClienteDto dto);
    Task<bool> Update(int id, ClienteDto dto);
    Task<bool> Delete(int id);
}

