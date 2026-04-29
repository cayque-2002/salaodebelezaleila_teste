using Microsoft.AspNetCore.Mvc;
using SalaoDeBelezaLeila.Application.Dtos;
using SalaoDeBelezaLeila.Application.Services;
using SalaoDeBelezaLeila.Domain.Entities;

[ApiController]
[Route("api/[controller]")]
public class AgendamentoController : ControllerBase
{
    private readonly IAgendamentoService _service;

    public AgendamentoController(IAgendamentoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
        => Ok(await _service.GetAll());

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var agendamento = await _service.GetById(id);
        if (agendamento == null) return NotFound();
        return Ok(agendamento);
    }

    [HttpGet("servico/{servicoId}")]
    public async Task<IActionResult> GetByServico(int servicoId)
    {
        var agendamento = await _service.GetByServicoId(servicoId);
        if (agendamento == null) return NotFound();
        return Ok(agendamento);
    }

    [HttpGet("cliente/{clienteId}")]
    public async Task<IActionResult> GetByCliente(int clienteId)
    {
        var agendamento = await _service.GetByClienteId(clienteId);
        if (agendamento == null) return NotFound();
        return Ok(agendamento);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] AgendamentoDto dto)
    {
        var result = await _service.Create(dto);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] AgendamentoDto dto)
    {
        var updated = await _service.Update(id, dto);
        if (!updated) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.Delete(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}