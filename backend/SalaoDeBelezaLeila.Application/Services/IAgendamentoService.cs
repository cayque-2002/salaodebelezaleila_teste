using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalaoDeBelezaLeila.Application.Dtos;

namespace SalaoDeBelezaLeila.Application.Services;

public interface IAgendamentoService
{
    Task<List<AgendamentoDto>> GetAll();
    Task<AgendamentoDto?> GetById(int id);
    Task<List<AgendamentoDto?>> GetByServicoId(int id);
    Task<List<AgendamentoDto?>> GetByClienteId(int id);
    Task<AgendamentoDto> Create(AgendamentoDto dto);
    Task<bool> Update(int id, AgendamentoDto dto);
    Task<bool> Delete(int id);
    Task<bool> ConfirmarAgendamento(int id, int usuarioId);
    Task<DashboardSemanalDto> ObterDashboardSemanal(int semana, int ano);
}

