﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Alura.LeilaoOnline.Core
{
    public enum EstadoLeilao
    {
        LeilaoAntesDoPregao,
        LeilaoEmAndamento,
        LeilaoFinalizado
    }
    public class Leilao
    {
        private readonly IList<Lance> _lances;
        private readonly IModalidadeAvaliacao _avaliador;
        private Interessada _ultimoCliente;
        public IEnumerable<Lance> Lances => _lances;
        public string Peca { get; }
        public Lance Ganhador { get; private set; }
        public EstadoLeilao Estado { get; private set; }

        public Leilao(string peca, IModalidadeAvaliacao avaliador)
        {
            Peca = peca;
            _lances = new List<Lance>();
            Estado = EstadoLeilao.LeilaoAntesDoPregao;
            _avaliador = avaliador;
        }

        private bool NovoLanceAceito(Interessada cliente, double valor)
        {
            return (Estado == EstadoLeilao.LeilaoEmAndamento)
                && (cliente != _ultimoCliente);
        }

        public void RecebeLance(Interessada cliente, double valor)
        {
            if(NovoLanceAceito(cliente, valor))
            {
                _lances.Add(new Lance(cliente, valor));
                _ultimoCliente = cliente;
            }
        }

        public void IniciaPregao()
        {
            Estado = EstadoLeilao.LeilaoEmAndamento;
        }

        public void TerminaPregao()
        {
            if(Estado != EstadoLeilao.LeilaoEmAndamento)
            {
                throw new InvalidOperationException("Não é possível terminar o pregão sem que ele tenha começado. Para isso, utilize o método IniciaPregao()");
            }
            Ganhador = _avaliador.Avalia(this);
            Estado = EstadoLeilao.LeilaoFinalizado;
        }
    }
}
