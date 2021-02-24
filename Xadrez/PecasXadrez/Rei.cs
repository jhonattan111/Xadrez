using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xadrez.TabuleiroEntities;

namespace Xadrez.PecasXadrez
{
    class Rei : Peca
    {
        public Rei(Tabuleiro tabuleiro, Cor cor, PartidaXadrez partida) : base(tabuleiro, cor)
        {
            this.Partida = partida;
        }

        private PartidaXadrez Partida{ get; set; }

        public override string ToString()
        {
            return "R ";
        }

        private bool TesteTorreParaRoque(Posicao posicao)
        {
            Peca peca = TabuleiroPeca.RetornarPeca(posicao);
            return peca != null && peca is Torre && peca.CorPeca == CorPeca && peca.QuantidadeMovimentos == 0;
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] matriz = new bool[TabuleiroPeca.Linhas, TabuleiroPeca.Colunas];

            Posicao posicao = new Posicao(0, 0);

            //norte
            posicao.DefinirValores(PosicaoPeca.Linha - 1, PosicaoPeca.Coluna);
            if(TabuleiroPeca.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            //nordeste
            posicao.DefinirValores(PosicaoPeca.Linha - 1, PosicaoPeca.Coluna + 1);
            if (TabuleiroPeca.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            //direita
            posicao.DefinirValores(PosicaoPeca.Linha, PosicaoPeca.Coluna + 1);
            if (TabuleiroPeca.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            //sudeste
            posicao.DefinirValores(PosicaoPeca.Linha + 1, PosicaoPeca.Coluna + 1);
            if (TabuleiroPeca.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            //sul
            posicao.DefinirValores(PosicaoPeca.Linha + 1, PosicaoPeca.Coluna);
            if (TabuleiroPeca.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            //sudoeste
            posicao.DefinirValores(PosicaoPeca.Linha + 1, PosicaoPeca.Coluna - 1);
            if (TabuleiroPeca.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            //esquerda
            posicao.DefinirValores(PosicaoPeca.Linha, PosicaoPeca.Coluna - 1);
            if (TabuleiroPeca.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            //noroeste
            posicao.DefinirValores(PosicaoPeca.Linha - 1, PosicaoPeca.Coluna - 1);
            if (TabuleiroPeca.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            //roque
            if (QuantidadeMovimentos == 0 && !Partida.Xeque)
            {
                //roque pequeno o-o
                Posicao posicaoTorre = new Posicao(posicao.Linha, posicao.Coluna + 3);
                if(TesteTorreParaRoque(posicaoTorre))
                {
                    Posicao posicao1 = new Posicao(posicao.Linha, posicao.Coluna + 1);
                    Posicao posicao2 = new Posicao(posicao.Linha, posicao.Coluna + 2);

                    if (TabuleiroPeca.RetornarPeca(posicao1) == null && TabuleiroPeca.RetornarPeca(posicao2) == null)
                        matriz[posicao.Linha, posicao.Coluna + 2] = true;
                }

                //roque grande o-o-o
                Posicao posicaoTorre2 = new Posicao(posicao.Linha, posicao.Coluna - 4);
                if (TesteTorreParaRoque(posicaoTorre2))
                {
                    Posicao posicao1 = new Posicao(posicao.Linha, posicao.Coluna - 1);
                    Posicao posicao2 = new Posicao(posicao.Linha, posicao.Coluna - 2);
                    Posicao posicao3 = new Posicao(posicao.Linha, posicao.Coluna - 3);

                    if (TabuleiroPeca.RetornarPeca(posicao1) == null && TabuleiroPeca.RetornarPeca(posicao2) == null && TabuleiroPeca.RetornarPeca(posicao3) == null)
                        matriz[posicao.Linha, posicao.Coluna - 2] = true;
                }
            }

            return matriz;
        }
    }
}
 