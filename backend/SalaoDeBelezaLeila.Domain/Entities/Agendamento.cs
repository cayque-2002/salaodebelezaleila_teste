using SalaoDeBelezaLeila.Domain.Entities;
using SalaoDeBelezaLeila.Domain.Enums;

namespace SalaoDeBelezaLeila.Domain.Entities;

public class Agendamento
{
    public int Id { get; set; }

    public int ClienteId { get; set; }
    public Cliente Cliente { get; set; }
    public DateTime DataHora { get; set; }
    public StatusAgendamento Status { get; set; }


    public List<AgendamentoServico> Servicos { get; set; }

}