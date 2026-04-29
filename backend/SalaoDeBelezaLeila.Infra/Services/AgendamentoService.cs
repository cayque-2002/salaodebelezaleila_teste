using Microsoft.EntityFrameworkCore;
using SalaoDeBelezaLeila.Application.Dtos;
using SalaoDeBelezaLeila.Application.Services;
using SalaoDeBelezaLeila.Domain.Entities;
using SalaoDeBelezaLeila.Infra.Data;

public class AgendamentoService : IAgendamentoService
{
    private readonly AppDbContext _context;

    public AgendamentoService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<AgendamentoDto>> GetAll()
    {
        return await _context.Agendamentos
            .Select(a => new AgendamentoDto
            {
                Id = a.Id,
                ClienteId = a.ClienteId,
                Cliente = a.Cliente.Nome,
                ServicoId = a.ServicoId,
                Servico = a.Servico.Nome,
                DataHora = a.DataHora
            }).ToListAsync();
    }

    public async Task<AgendamentoDto?> GetById(int id)
    {
        var a = await _context.Agendamentos.FindAsync(id);

        var cliente = await _context.Clientes.FindAsync(a.ClienteId);
        var servico = await _context.Servicos.FindAsync(a.ServicoId);

        if (a == null) return null;

        return new AgendamentoDto
        {
            Id = a.Id,
            ClienteId = a.ClienteId,
            Cliente = cliente.Nome,
            ServicoId = a.ServicoId,
            Servico = servico.Nome,
            DataHora = a.DataHora
        };
    }

    public async Task<List<AgendamentoDto?>> GetByServicoId(int id)
    {
        var a = await _context.Agendamentos
            .Select(a => new AgendamentoDto
             {
                Id = a.Id,
                ClienteId = a.ClienteId,
                Cliente = a.Cliente.Nome,
                ServicoId = a.ServicoId,
                Servico = a.Servico.Nome,
                DataHora = a.DataHora
            }).Where(x => x.ServicoId == id).ToListAsync();


        if (a == null) return null;

        return a;
    }

    public async Task<List<AgendamentoDto?>> GetByClienteId(int id)
    {
        var a = await _context.Agendamentos
            .Select(a => new AgendamentoDto
            {
                Id = a.Id,
                ClienteId = a.ClienteId,
                Cliente = a.Cliente.Nome,
                ServicoId = a.ServicoId,
                Servico = a.Servico.Nome,
                DataHora = a.DataHora
            }).Where(x => x.ClienteId == id).ToListAsync();


        if (a == null) return null;

        return a;
    }

    public async Task<AgendamentoDto> Create(AgendamentoDto dto)
    {

        var cliente = await _context.Clientes.FindAsync(dto.ClienteId);
        if (cliente == null) throw new Exception("Cliente não encontrado");

        var servico = await _context.Servicos.FindAsync(dto.ServicoId);
        if (servico == null) throw new Exception("Serviço não encontrado");

        if (dto.DataHora < DateTime.Now)
            throw new Exception("Não é possível agendar no passado");

        var entity = new Agendamento
        {
            Cliente = cliente,
            ClienteId = dto.ClienteId,
            Servico = servico,
            ServicoId = dto.ServicoId,
            DataHora = dto.DataHora
        };

        _context.Agendamentos.Add(entity);
        await _context.SaveChangesAsync();

        dto.Id = entity.Id;
        return dto;
    }

    public async Task<bool> Update(int id, AgendamentoDto dto)
    {
        var cliente = await _context.Clientes.FindAsync(dto.ClienteId);
        if (cliente == null) throw new Exception("Cliente não encontrado");

        var servico = await _context.Servicos.FindAsync(dto.ServicoId);
        if (servico == null) throw new Exception("Serviço não encontrado");

        if (dto.DataHora < DateTime.Now)
            throw new Exception("Não é possível agendar no passado");

        var entity = await _context.Agendamentos.FindAsync(id);

        if (entity == null) return false;

        entity.ClienteId = cliente.Id;
        entity.Cliente = cliente;
        entity.ServicoId = servico.Id;
        entity.Servico = servico;
        entity.DataHora = dto.DataHora;

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
}