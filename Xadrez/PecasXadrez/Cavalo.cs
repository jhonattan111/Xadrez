using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xadrez.TabuleiroEntities;

namespace Xadrez.PecasXadrez
{
    class Cavalo : Peca
    {
        public Cavalo(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor)
        {

        }

        public override string ToString()
        {
            return "C ";
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] matriz = new bool[TabuleiroPeca.Linhas, TabuleiroPeca.Colunas];

            Posicao posicao = new Posicao(0, 0);

            posicao.DefinirValores(PosicaoPeca.Linha - 1, PosicaoPeca.Coluna - 2);
            if (TabuleiroPeca.PosicaoValida(posicao) && PodeMover(posicao))
                matriz[posicao.Linha, posicao.Coluna] = true;

            posicao.DefinirValores(PosicaoPeca.Linha - 2, PosicaoPeca.Coluna - 1);
            if (TabuleiroPeca.PosicaoValida(posicao) && PodeMover(posicao))
                matriz[posicao.Linha, posicao.Coluna] = true;

            posicao.DefinirValores(PosicaoPeca.Linha - 2, PosicaoPeca.Coluna + 1);
            if (TabuleiroPeca.PosicaoValida(posicao) && PodeMover(posicao))
                matriz[posicao.Linha, posicao.Coluna] = true;

            posicao.DefinirValores(PosicaoPeca.Linha - 1, PosicaoPeca.Coluna + 2);
            if (TabuleiroPeca.PosicaoValida(posicao) && PodeMover(posicao))
                matriz[posicao.Linha, posicao.Coluna] = true;

            posicao.DefinirValores(PosicaoPeca.Linha + 1, PosicaoPeca.Coluna + 2);
            if (TabuleiroPeca.PosicaoValida(posicao) && PodeMover(posicao))
                matriz[posicao.Linha, posicao.Coluna] = true;

            posicao.DefinirValores(PosicaoPeca.Linha + 2, PosicaoPeca.Coluna + 1);
            if (TabuleiroPeca.PosicaoValida(posicao) && PodeMover(posicao))
                matriz[posicao.Linha, posicao.Coluna] = true;

            posicao.DefinirValores(PosicaoPeca.Linha + 2, PosicaoPeca.Coluna - 1);
            if (TabuleiroPeca.PosicaoValida(posicao) && PodeMover(posicao))
                matriz[posicao.Linha, posicao.Coluna] = true;

            posicao.DefinirValores(PosicaoPeca.Linha + 1, PosicaoPeca.Coluna - 2);
            if (TabuleiroPeca.PosicaoValida(posicao) && PodeMover(posicao))
                matriz[posicao.Linha, posicao.Coluna] = true;

            return matriz;
        }
    }
}
