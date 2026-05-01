using SalaoDeBelezaLeila.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaoDeBelezaLeila.Application.Dtos;

public class DashboardSemanalDto
{
    public int TotalAtendimentos { get; set; }
    public decimal FaturamentoTotal { get; set; }

    public List<ServicoDashboardDto> ServicosMaisFeitos { get; set; }
    public List<FaturamentoDiaDto> FaturamentoPorDia { get; set; }
}

public class ServicoDashboardDto
{
    public string Nome { get; set; }
    public int Quantidade { get; set; }
}

public class FaturamentoDiaDto
{
    public string Dia { get; set; }
    public decimal Valor { get; set; }
}