using Microsoft.AspNetCore.Mvc;
using SalaoDeBelezaLeila.Application.Dtos;
using SalaoDeBelezaLeila.Application.Services;

[ApiController]
[Route("api/[controller]")]
public class ServicoController : ControllerBase
{
    private readonly IServicoService _service;

    public ServicoController(IServicoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
        => Ok(await _service.GetAll());

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var servico = await _service.GetById(id);
        if (servico == null) return NotFound();
        return Ok(servico);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ServicoDto dto)
    {
        var result = await _service.Create(dto);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] ServicoDto dto)
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