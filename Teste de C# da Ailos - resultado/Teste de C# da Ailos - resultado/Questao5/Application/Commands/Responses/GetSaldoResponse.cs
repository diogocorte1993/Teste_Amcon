namespace Questao5.Application.Commands.Responses
{
    public class GetSaldoResponse
    {
        public int NumeroDaConta { get; set; }
        public string TitularContaCorrente { get; set; }
        public DateTime DataHora { get; set; }
        public double Saldo { get; set; }
    }
}