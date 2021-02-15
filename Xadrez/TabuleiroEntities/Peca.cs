using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xadrez.TabuleiroEntities
{
    abstract class Peca
    {
        public Posicao PosicaoPeca { get; set; }
        public Cor CorPeca { get; protected set; }
        public int QuantidadeMovimentos { get; protected set; }
        public Tabuleiro TabuleiroPeca { get; set; }

        public Peca(Tabuleiro tabuleiro, Cor cor)
        {
            this.PosicaoPeca = null;
            this.TabuleiroPeca = tabuleiro;
            this.CorPeca = cor;
            this.QuantidadeMovimentos = 0;
        }

        public void IncrementarQuantidadeMovimento()
        {
            QuantidadeMovimentos++;
        }

        public abstract bool[,] MovimentosPossiveis();
    }
}
