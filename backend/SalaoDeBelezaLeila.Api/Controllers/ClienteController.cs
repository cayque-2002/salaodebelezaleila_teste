using Microsoft.AspNetCore.Mvc;
using SalaoDeBelezaLeila.Application.Dtos;
using SalaoDeBelezaLeila.Application.Services;

[ApiController]
[Route("api/[controller]")]
public class ClienteController : ControllerBase
{
    private readonly IClienteService _service;

    public ClienteController(IClienteService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
        => Ok(await _service.GetAll());

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var cliente = await _service.GetById(id);
        if (cliente == null) return NotFound();
        return Ok(cliente);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ClienteDto dto)
    {
        var result = await _service.Create(dto);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] ClienteDto dto)
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