using System.Drawing;
using System;

namespace Questao5.Domain.Entities
{
    public class Movimento
    {
        public string idmovimento { get; set; }
        public string idcontacorrente { get; set; }
        public DateTime datamovimento { get; set; }
        public char tipomovimento { get; set; }
        public double valor { get; set; }
    }
}
