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

        public void DecrementarQuantidadeMovimento()
        {
            QuantidadeMovimentos--;
        }

        public bool ExisteMovimentoPossivel()
        {
            bool[,] matriz = MovimentosPossiveis();
            for(int i = 0; i < TabuleiroPeca.Linhas; i++)
            {
                for (int j = 0; j < TabuleiroPeca.Colunas; j++)
                {
                    if (matriz[i, j])
                        return true;
                }
            }
            return false;
        }

        public bool PodeMoverPara(Posicao posicao)
        {
            return MovimentosPossiveis()[posicao.Linha, posicao.Coluna];
        }

        public abstract bool[,] MovimentosPossiveis();

        protected bool PodeMover(Posicao posicao)
        {
            Peca peca = TabuleiroPeca.RetornarPeca(posicao);
            return peca == null || peca.CorPeca != this.CorPeca;
        }
    }
}
