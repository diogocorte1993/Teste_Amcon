using System;
using System.Globalization;
using System.Xml.Linq;

namespace Questao1
{
    internal class ContaBancaria
    {
        private int NumerDaConta;
        private string NomeDoTitular;
        private double Saldo;

        public ContaBancaria(int numero, string titular)
        {
            this.NumerDaConta = numero;
            this.NomeDoTitular = titular;
            this.Saldo = 0;
        }

        public ContaBancaria(int numero, string titular, double depositoInicial)
        {
            this.NumerDaConta = numero;
            this.NomeDoTitular = titular;
            this.Saldo = depositoInicial;
        }

        internal double ObterValorTarifaSaque()
        {
            return 3.50;
        }

        internal void Deposito(double quantia)
        {
            Saldo += quantia;
        }

        internal void Saque(double quantia)
        {
            if (quantia < 0) quantia *= -1;
            Saldo -= (ObterValorTarifaSaque() + quantia);
        }
        public override string ToString() => String.Format($"Conta {NumerDaConta}, Titular: {NomeDoTitular}, Saldo: $ {Saldo:N2}");
    }
}
