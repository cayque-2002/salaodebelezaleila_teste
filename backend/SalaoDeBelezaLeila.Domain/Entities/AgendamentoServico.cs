using SalaoDeBelezaLeila.Domain.Entities;

namespace SalaoDeBelezaLeila.Domain.Entities;

public class AgendamentoServico
{
    public int Id { get; set; }

    public int AgendamentoId { get; set; }
    public Agendamento Agendamento { get; set; }

    public int ServicoId { get; set; }
    public Servico Servico { get; set; }
}