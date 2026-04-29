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
    public int ServicoId { get; set; }
    public string Servico { get; set; } = string.Empty;
    public DateTime DataHora { get; set; }
}
