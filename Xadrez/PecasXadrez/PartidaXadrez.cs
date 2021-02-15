using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xadrez.TabuleiroEntities;

namespace Xadrez.PecasXadrez
{
    class PartidaXadrez
    {
        public Tabuleiro TabuleiroXadrez {get; private set;}
        public int Turno { get; set; }
        private Cor JogadorAtual { get; set; }
        public bool Terminada { get; private set; }

        public PartidaXadrez()
        {
            TabuleiroXadrez = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branco;
            Terminada = false;
            ColocarPecas();
        }

        public void ExecutarMovimento(Posicao origem, Posicao destino)
        {
            Peca peca = tabuleiro.RetirarPeca(origem);
            peca.IncrementarQuantidadeMovimento();
            Peca pecaCapturada = tabuleiro.RetirarPeca(destino);
            tabuleiro.ColocarPeca(peca, destino);
        }

        private void ColocarPecas()
        {
            tabuleiro.ColocarPeca(new Torre(tabuleiro, Cor.Branco), new PosicaoXadrez('c',1).ConverterPosicao());
            tabuleiro.ColocarPeca(new Torre(tabuleiro, Cor.Branco), new PosicaoXadrez('c',2).ConverterPosicao());
        }
    }
}
