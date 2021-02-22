using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xadrez.TabuleiroEntities;

namespace Xadrez.PecasXadrez
{
    class Dama : Peca
    {
        public Dama(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor)
        {

        }

        public override string ToString()
        {
            return "D ";
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] matriz = new bool[TabuleiroPeca.Linhas, TabuleiroPeca.Colunas];

            Posicao posicao = new Posicao(0, 0);

            //NO
            posicao.DefinirValores(PosicaoPeca.Linha - 1, PosicaoPeca.Coluna - 1);
            while (TabuleiroPeca.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
                if (TabuleiroPeca.RetornarPeca(posicao) != null && TabuleiroPeca.RetornarPeca(posicao).CorPeca != CorPeca)
                    break;
                posicao.Linha--;
                posicao.Coluna--;
            }

            //NE
            posicao.DefinirValores(PosicaoPeca.Linha - 1, PosicaoPeca.Coluna + 1);
            while (TabuleiroPeca.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
                if (TabuleiroPeca.RetornarPeca(posicao) != null && TabuleiroPeca.RetornarPeca(posicao).CorPeca != CorPeca)
                    break;
                posicao.Linha--;
                posicao.Coluna++;
            }

            //SE
            posicao.DefinirValores(PosicaoPeca.Linha + 1, PosicaoPeca.Coluna + 1);
            while (TabuleiroPeca.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
                if (TabuleiroPeca.RetornarPeca(posicao) != null && TabuleiroPeca.RetornarPeca(posicao).CorPeca != CorPeca)
                    break;
                posicao.Linha++;
                posicao.Coluna++;
            }

            //SO
            posicao.DefinirValores(PosicaoPeca.Linha + 1, PosicaoPeca.Coluna - 1);
            while (TabuleiroPeca.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
                if (TabuleiroPeca.RetornarPeca(posicao) != null && TabuleiroPeca.RetornarPeca(posicao).CorPeca != CorPeca)
                    break;
                posicao.Linha++;
                posicao.Coluna--;
            }

            //norte
            posicao.DefinirValores(PosicaoPeca.Linha - 1, PosicaoPeca.Coluna);
            while (TabuleiroPeca.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
                if (TabuleiroPeca.RetornarPeca(posicao) != null && TabuleiroPeca.RetornarPeca(posicao).CorPeca != CorPeca)
                    break;
                posicao.Linha--;
            }

            //sul
            posicao.DefinirValores(PosicaoPeca.Linha + 1, PosicaoPeca.Coluna);
            while (TabuleiroPeca.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
                if (TabuleiroPeca.RetornarPeca(posicao) != null && TabuleiroPeca.RetornarPeca(posicao).CorPeca != CorPeca)
                    break;
                posicao.Linha++;
            }

            //leste
            posicao.DefinirValores(PosicaoPeca.Linha, PosicaoPeca.Coluna + 1);
            while (TabuleiroPeca.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
                if (TabuleiroPeca.RetornarPeca(posicao) != null && TabuleiroPeca.RetornarPeca(posicao).CorPeca != CorPeca)
                    break;
                posicao.Coluna++;
            }

            //oeste
            posicao.DefinirValores(PosicaoPeca.Linha, PosicaoPeca.Coluna - 1);
            while (TabuleiroPeca.PosicaoValida(posicao) && PodeMover(posicao))
            {
                matriz[posicao.Linha, posicao.Coluna] = true;
                if (TabuleiroPeca.RetornarPeca(posicao) != null && TabuleiroPeca.RetornarPeca(posicao).CorPeca != CorPeca)
                    break;
                posicao.Coluna--;
            }

            return matriz;
        }
    }
}
