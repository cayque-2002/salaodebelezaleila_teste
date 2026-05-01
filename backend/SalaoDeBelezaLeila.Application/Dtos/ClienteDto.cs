using SalaoDeBelezaLeila.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaoDeBelezaLeila.Application.Dtos;

public class ClienteDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public TipoUsuario TipoUsuario { get; set; }
    public int? UsuarioId { get; set; }
}