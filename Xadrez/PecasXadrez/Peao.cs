using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xadrez.TabuleiroEntities;

namespace Xadrez.PecasXadrez
{
    class Peao : Peca
    {
        public Peao(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor)
        {

        }

        public override string ToString()
        {
            return "P ";
        }

        private bool ExisteInimigo(Posicao posicao)
        {
            Peca peca = TabuleiroPeca.RetornarPeca(posicao);
            return peca != null && peca.CorPeca != CorPeca;
        }

        private bool PosicaoLivre(Posicao posicao)
        {
            return TabuleiroPeca.RetornarPeca(posicao) == null;
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] matriz = new bool[TabuleiroPeca.Linhas, TabuleiroPeca.Colunas];

            Posicao posicao = new Posicao(0, 0);

            if (CorPeca == Cor.Branco)
            {
                posicao.DefinirValores(PosicaoPeca.Linha - 1, PosicaoPeca.Coluna);
                if (TabuleiroPeca.PosicaoValida(posicao) && PosicaoLivre(posicao))
                    matriz[posicao.Linha, posicao.Coluna] = true;

                posicao.DefinirValores(PosicaoPeca.Linha - 2, PosicaoPeca.Coluna);
                Posicao p2 = new Posicao(PosicaoPeca.Linha - 1, PosicaoPeca.Coluna);

                if (TabuleiroPeca.PosicaoValida(p2) && PosicaoLivre(p2) && TabuleiroPeca.PosicaoValida(posicao) && PosicaoLivre(posicao) && QuantidadeMovimentos == 0)
                    matriz[posicao.Linha, posicao.Coluna] = true;

                posicao.DefinirValores(PosicaoPeca.Linha - 1, PosicaoPeca.Coluna - 1);
                if (TabuleiroPeca.PosicaoValida(posicao) && ExisteInimigo(posicao))
                    matriz[posicao.Linha, posicao.Coluna] = true;

                posicao.DefinirValores(PosicaoPeca.Linha - 1, PosicaoPeca.Coluna + 1);
                if (TabuleiroPeca.PosicaoValida(posicao) && ExisteInimigo(posicao))
                    matriz[posicao.Linha, posicao.Coluna] = true;
            }
            else
            {
                posicao.DefinirValores(PosicaoPeca.Linha + 1, PosicaoPeca.Coluna);
                if (TabuleiroPeca.PosicaoValida(posicao) && PosicaoLivre(posicao))
                    matriz[posicao.Linha, posicao.Coluna] = true;

                posicao.DefinirValores(PosicaoPeca.Linha + 2, PosicaoPeca.Coluna);
                Posicao p2 = new Posicao(PosicaoPeca.Linha + 1, PosicaoPeca.Coluna);
                if (TabuleiroPeca.PosicaoValida(p2) && PosicaoLivre(p2) && TabuleiroPeca.PosicaoValida(posicao) && PosicaoLivre(posicao) && QuantidadeMovimentos == 0)
                    matriz[posicao.Linha, posicao.Coluna] = true;

                posicao.DefinirValores(PosicaoPeca.Linha + 1, PosicaoPeca.Coluna - 1);
                if (TabuleiroPeca.PosicaoValida(posicao) && ExisteInimigo(posicao))
                    matriz[posicao.Linha, posicao.Coluna] = true;

                posicao.DefinirValores(PosicaoPeca.Linha + 1, PosicaoPeca.Coluna + 1);
                if (TabuleiroPeca.PosicaoValida(posicao) && ExisteInimigo(posicao))
                    matriz[posicao.Linha, posicao.Coluna] = true;
            }
            return matriz;
        }
    }
}
