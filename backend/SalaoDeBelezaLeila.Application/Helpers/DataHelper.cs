using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaoDeBelezaLeila.Application.Helpers
{
    public static class DataHelper
    {
        public static (DateTime inicio, DateTime fim) ObterIntervaloSemana(int ano, int semana)
        {
            var primeiroDiaAno = new DateTime(ano, 1, 1);

            var diferenca = DayOfWeek.Monday - primeiroDiaAno.DayOfWeek;
            var primeiraSegunda = primeiroDiaAno.AddDays(diferenca);

            var inicio = primeiraSegunda.AddDays((semana - 1) * 7);
            var fim = inicio.AddDays(6);

            return (inicio, fim);
        }
    }
}
