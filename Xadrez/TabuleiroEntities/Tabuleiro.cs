using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xadrez.TabuleiroEntities.XadrezExceptions;

namespace Xadrez.TabuleiroEntities
{
    class Tabuleiro
    {
        public int Linhas { get; set; }
        public int Colunas { get; set; }
        private Peca[,] Pecas { get; set; }

        public Tabuleiro(int linhas, int colunas)
        {
            this.Linhas = linhas;
            this.Colunas = colunas;
            this.Pecas = new Peca[linhas, colunas];
        }

        public Peca RetornarPeca(int linha, int coluna)
        {
            return Pecas[linha, coluna];
        }

        public Peca RetornarPeca(Posicao posicao)
        {
            return Pecas[posicao.Linha, posicao.Coluna];
        }

        public void ColocarPeca(Peca peca, Posicao posicao)
        {
            if (ExistePeca(posicao))
                throw new TabuleiroException("Já existe uma peça nesta posição");
            Pecas[posicao.Linha, posicao.Coluna] = peca;
            peca.PosicaoPeca = posicao;
        }

        public Peca RetirarPeca(Posicao posicao)
        {
            if (RetornarPeca(posicao) == null)
                return null;

            Peca aux = RetornarPeca(posicao);
            aux.PosicaoPeca = null;
            Pecas[posicao.Linha, posicao.Coluna] = null;
            return aux;
        }

        public bool ValidarPosicao(Posicao posicao)
        {
            if (posicao.Linha < 0 || posicao.Linha >= Linhas || posicao.Coluna < 0 || posicao.Coluna >= Colunas)
                throw new TabuleiroException("Posicao Invalida");

            return true;
        }

        public bool ExistePeca(Posicao posicao)
        {
            ValidarPosicao(posicao);
            return RetornarPeca(posicao) != null;
        }
    }

}
