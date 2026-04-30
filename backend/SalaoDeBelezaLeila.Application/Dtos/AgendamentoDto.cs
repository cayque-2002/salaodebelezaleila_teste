using SalaoDeBelezaLeila.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaoDeBelezaLeila.Application.Dtos;

public class AgendamentoDto
{
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public string Cliente { get; set; } = string.Empty;
    public List<int> ServicosIds { get; set; } = new();
    public List<string> Servicos { get; set; } = new();
    public DateTime DataHora { get; set; }
    public int UsuarioId { get; set; }
    public StatusAgendamento Status { get; set; }
}
