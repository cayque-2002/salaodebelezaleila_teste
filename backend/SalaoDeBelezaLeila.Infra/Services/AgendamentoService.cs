using Microsoft.EntityFrameworkCore;
using SalaoDeBelezaLeila.Application.Dtos;
using SalaoDeBelezaLeila.Application.Services;
using SalaoDeBelezaLeila.Domain.Entities;
using SalaoDeBelezaLeila.Domain.Enums;
using SalaoDeBelezaLeila.Infra.Data;
using System.Text.RegularExpressions;

public class AgendamentoService : IAgendamentoService
{
    private readonly AppDbContext _context;
    private readonly IClienteService _clienteService;

    public AgendamentoService(AppDbContext context, IClienteService clienteService)
    {
        _context = context;
        _clienteService = clienteService;
    }

    public async Task<List<AgendamentoDto>> GetAll()
    {
        return await _context.Agendamentos
            .Include(a => a.Cliente)
            .Include(a => a.Servicos)
                .ThenInclude(s => s.Servico)
            .Select(a => new AgendamentoDto
            {
                Id = a.Id,
                ClienteId = a.ClienteId,
                Cliente = a.Cliente.Nome,
                Servicos = a.Servicos
                    .Select(s => s.Servico.Nome)
                    .ToList(),
                ServicosIds = a.Servicos
                    .Select(s => s.ServicoId)
                    .ToList(),
                Total = a.Servicos.Sum(s => s.Servico.Preco),
                DataHora = a.DataHora,
                Status = a.Status
            }).ToListAsync();
    }

    public async Task<AgendamentoDto?> GetById(int id)
    {
        var agendamento = await _context.Agendamentos
            .Where(a => a.Id == id)
            .Include(a => a.Cliente)
            .Include(a => a.Servicos)
                .ThenInclude(s => s.Servico)
            .Select(a => new AgendamentoDto
            {
                Id = a.Id,
                ClienteId = a.ClienteId,
                Cliente = a.Cliente.Nome,
                Servicos = a.Servicos
                    .Select(s => s.Servico.Nome)
                    .ToList(),
                ServicosIds = a.Servicos
                    .Select(s => s.ServicoId)
                    .ToList(),
                Total = a.Servicos.Sum(s => s.Servico.Preco),
                DataHora = a.DataHora,
                Status = a.Status
            })
            .FirstOrDefaultAsync();

        return agendamento;
    }

    public async Task<List<AgendamentoDto?>> GetByServicoId(int id)
    {
        var a = await _context.Agendamentos
            .Include(a => a.Cliente)
            .Include(a => a.Servicos)
                .ThenInclude(s => s.Servico)
            .Select(a => new AgendamentoDto
            {
                Id = a.Id,
                ClienteId = a.ClienteId,
                Cliente = a.Cliente.Nome,
                Servicos = a.Servicos
                    .Select(s => s.Servico.Nome)
                    .ToList(),
                ServicosIds = a.Servicos
                    .Select(s => s.ServicoId)
                    .ToList(),
                Total = a.Servicos.Sum(s => s.Servico.Preco),
                DataHora = a.DataHora,
                Status = a.Status
            })
            .Where(x => x.ClienteId == id)
            .ToListAsync();

        if (a == null) return null;

        return a;
    }

    public async Task<List<AgendamentoDto?>> GetByClienteId(int id)
    {

        var a = await _context.Agendamentos
            .Include(a => a.Cliente)
            .Include(a => a.Servicos)
                .ThenInclude(s => s.Servico)
            .Select(a => new AgendamentoDto
            {
                Id = a.Id,
                ClienteId = a.ClienteId,
                Cliente = a.Cliente.Nome,
                Servicos = a.Servicos
                    .Select(s => s.Servico.Nome)
                    .ToList(),
                ServicosIds = a.Servicos
                    .Select(s => s.ServicoId)
                    .ToList(),
                Total = a.Servicos.Sum(s => s.Servico.Preco),
                DataHora = a.DataHora,
                Status = a.Status
            })
            .Where(x => x.ClienteId == id)
            .ToListAsync();


        if (a == null) return null;

        return a;
    }

    public async Task<List<AgendamentoDto>> ObterPorUsuarioCliente(int usuarioId)
    {
        var cliente = await _clienteService.ObterPorUsuarioId(usuarioId);

        if (cliente == null)
            return new List<AgendamentoDto>();

        return await _context.Agendamentos
            .Include(a => a.Cliente)
            .Include(a => a.Servicos)
                .ThenInclude(s => s.Servico)
            .Where(a => a.ClienteId == cliente.Id)
            .Select(a => new AgendamentoDto
            {
                Id = a.Id,
                ClienteId = a.ClienteId,
                Cliente = a.Cliente.Nome,
                Servicos = a.Servicos.Select(s => s.Servico.Nome).ToList(),
                ServicosIds = a.Servicos.Select(s => s.ServicoId).ToList(),
                DataHora = a.DataHora,
                Total = a.Servicos.Sum(s => s.Servico.Preco),
                Status = a.Status,
                UsuarioId = a.Cliente.UsuarioId ?? 0
            })
            .ToListAsync();
    }

    public async Task<AgendamentoDto> Create(AgendamentoDto dto)
    {
        await ValidaAgendamento(dto);

        var usuario = await _context.Usuarios.FindAsync(dto.UsuarioId);

        var entity = new Agendamento
        {
            ClienteId = dto.ClienteId,
            DataHora = dto.DataHora,
            Status = StatusAgendamento.Pendente,
            Servicos = dto.ServicosIds.Select(id => new AgendamentoServico
            {
                ServicoId = id
            }).ToList()
            
        };

        _context.Agendamentos.Add(entity);
        await _context.SaveChangesAsync();

        return await GetById(entity.Id);
    }

    public async Task<bool> Update(int id, AgendamentoDto dto)
    {
        await ValidaAgendamento(dto);

        var usuario = await _context.Usuarios.FindAsync(dto.UsuarioId);
        if (usuario == null) throw new Exception("Usuário não encontrado");

        if (dto.DataHora < DateTime.Now.AddDays(2)
            && usuario.Tipo != TipoUsuario.Admin)
        {
            throw new Exception("Somente administradores podem alterar agendamentos com menos de 2 dias. Por favor nos contate por telefone.");
        }

        var entity = await _context.Agendamentos
            .Include(a => a.Servicos)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (entity == null) return false;

        entity.ClienteId = dto.ClienteId;
        entity.DataHora = dto.DataHora;

        entity.Servicos.Clear();
        entity.Servicos = dto.ServicosIds.Select(id => new AgendamentoServico
        {
            AgendamentoId = entity.Id,
            ServicoId = id
        }).ToList();

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Delete(int id)
    {
        var entity = await _context.Agendamentos.FindAsync(id);
        if (entity == null) return false;

        _context.Agendamentos.Remove(entity);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> ConfirmarAgendamento(int id, int usuarioId)
    {
        var agendamento = await _context.Agendamentos.FindAsync(id);
        if (agendamento == null)
            return false;

        var usuario = await _context.Usuarios.FindAsync(usuarioId);
        if (usuario == null)
            throw new Exception("Usuário não encontrado");

        if (usuario.Tipo != TipoUsuario.Admin)
            throw new Exception("Apenas administradores podem confirmar agendamentos");

        if (agendamento.Status == StatusAgendamento.Confirmado)
            throw new Exception("Agendamento já está confirmado");

        agendamento.Status = StatusAgendamento.Confirmado;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task ValidaAgendamento(AgendamentoDto dto)
    {
        if (dto.DataHora < DateTime.Now)
            throw new Exception("Não é possível agendar no passado");

        var clienteExiste = await _context.Clientes
            .AnyAsync(c => c.Id == dto.ClienteId);

        if (!clienteExiste)
            throw new Exception("Cliente não encontrado");

        var usuarioExiste = await _context.Usuarios
            .AnyAsync(u => u.Id == dto.UsuarioId);

        if (!usuarioExiste)
            throw new Exception("Usuário não encontrado");

        var servicosValidos = await _context.Servicos
            .Where(s => dto.ServicosIds.Contains(s.Id))
            .Select(s => s.Id)
            .ToListAsync();

        if (servicosValidos.Count != dto.ServicosIds.Count)
            throw new Exception("Um ou mais serviços são inválidos");
    }

    public async Task<DashboardSemanalDto> ObterDashboardSemanal(int semana, int ano)
    {
        var (inicio, fim) = ObterIntervaloSemana(ano, semana);

        var agendamentos = await _context.Agendamentos
            .Include(x => x.Servicos)
                .ThenInclude(x => x.Servico)
            .Where(x => x.DataHora >= inicio && x.DataHora <= fim)
            .ToListAsync();

        var totalAtendimentos = agendamentos.Count;

        var faturamentoTotal = agendamentos
            .SelectMany(a => a.Servicos)
            .Sum(s => s.Servico.Preco);

        var servicosMaisFeitos = agendamentos
            .SelectMany(a => a.Servicos)
            .GroupBy(s => s.Servico.Nome)
            .Select(g => new ServicoDashboardDto
            {
                Nome = g.Key,
                Quantidade = g.Count()
            })
            .OrderByDescending(x => x.Quantidade)
            .Take(5)
            .ToList();

        var faturamentoPorDia = agendamentos
            .GroupBy(a => a.DataHora.Date)
            .OrderBy(g => g.Key)
            .Select(g => new FaturamentoDiaDto
            {
                Dia = g.Key.ToString("ddd"),
                Valor = g.SelectMany(a => a.Servicos)
                         .Sum(s => s.Servico.Preco)
            })
            .ToList();

        return new DashboardSemanalDto
        {
            TotalAtendimentos = totalAtendimentos,
            FaturamentoTotal = faturamentoTotal,
            ServicosMaisFeitos = servicosMaisFeitos,
            FaturamentoPorDia = faturamentoPorDia
        };
    }

    private (DateTime inicio, DateTime fim) ObterIntervaloSemana(int ano, int semana)
    {
        var primeiroDiaAno = new DateTime(ano, 1, 1);

        var diferenca = DayOfWeek.Monday - primeiroDiaAno.DayOfWeek;
        var primeiraSegunda = primeiroDiaAno.AddDays(diferenca);

        var inicio = primeiraSegunda.AddDays((semana - 1) * 7);
        var fim = inicio.AddDays(6);

        return (inicio, fim);
    }



}