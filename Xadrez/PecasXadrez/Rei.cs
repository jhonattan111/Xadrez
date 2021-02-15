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
        public Rei(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor)
        {

        }

        public override string ToString()
        {
            return "R ";
        }

        private bool PodeMover(Posicao posicao)
        {
            Peca peca = TabuleiroPeca.RetornarPeca(posicao);
            return peca == null || peca.CorPeca != this.CorPeca;
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] matriz = new bool[TabuleiroPeca.Linhas, TabuleiroPeca.Colunas];

            Posicao posicao = new Posicao(0, 0);

            //norte
            posicao.DefinirValores(PosicaoPeca.Linha - 1, PosicaoPeca.Coluna);
            if(TabuleiroPeca.ValidarPosicao(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            //nordeste
            posicao.DefinirValores(PosicaoPeca.Linha - 1, PosicaoPeca.Coluna + 1);
            if (TabuleiroPeca.ValidarPosicao(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            //direita
            posicao.DefinirValores(PosicaoPeca.Linha, PosicaoPeca.Coluna + 1);
            if (TabuleiroPeca.ValidarPosicao(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            //sudeste
            posicao.DefinirValores(PosicaoPeca.Linha + 1, PosicaoPeca.Coluna + 1);
            if (TabuleiroPeca.ValidarPosicao(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            //sul
            posicao.DefinirValores(PosicaoPeca.Linha + 1, PosicaoPeca.Coluna);
            if (TabuleiroPeca.ValidarPosicao(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            //sudoeste
            posicao.DefinirValores(PosicaoPeca.Linha + 1, PosicaoPeca.Coluna - 1);
            if (TabuleiroPeca.ValidarPosicao(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            //esquerda
            posicao.DefinirValores(PosicaoPeca.Linha, PosicaoPeca.Coluna - 1);
            if (TabuleiroPeca.ValidarPosicao(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            //noroeste
            posicao.DefinirValores(PosicaoPeca.Linha - 1, PosicaoPeca.Coluna - 1);
            if (TabuleiroPeca.ValidarPosicao(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
            }

            return matriz;
        }
    }
}
