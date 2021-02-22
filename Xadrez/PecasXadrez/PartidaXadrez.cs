using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xadrez.TabuleiroEntities;
using Xadrez.TabuleiroEntities.XadrezExceptions;

namespace Xadrez.PecasXadrez
{
    class PartidaXadrez
    {
        public Tabuleiro TabuleiroXadrez {get; private set;}
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool Terminada { get; private set; }
        private HashSet<Peca> Pecas;
        private HashSet<Peca> PecasCapturadas;
        public bool Xeque { get; private set; }

        public PartidaXadrez()
        {
            TabuleiroXadrez = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branco;
            Terminada = false;
            Pecas = new HashSet<Peca>();
            PecasCapturadas = new HashSet<Peca>();
            ColocarPecas();
            Xeque = false;
        }

        public Peca ExecutarMovimento(Posicao origem, Posicao destino)
        {
            Peca peca = TabuleiroXadrez.RetirarPeca(origem);
            peca.IncrementarQuantidadeMovimento();
            Peca pecaCapturada = TabuleiroXadrez.RetirarPeca(destino);
            TabuleiroXadrez.ColocarPeca(peca, destino);
            if(pecaCapturada != null)
                PecasCapturadas.Add(pecaCapturada);
            return pecaCapturada;
        }

        public void DesfazerMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca peca = TabuleiroXadrez.RetirarPeca(destino);
            peca.DecrementarQuantidadeMovimento();
            if(pecaCapturada != null)
            {
                TabuleiroXadrez.ColocarPeca(peca, destino);
                PecasCapturadas.Remove(pecaCapturada);
            }

            TabuleiroXadrez.ColocarPeca(peca, origem);

        }

        public void RealizarJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = ExecutarMovimento(origem, destino);

            if(ReiEstaEmXeque(JogadorAtual))
            {
                DesfazerMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em Xeque");
            }

            if(ReiEstaEmXeque(CorAdversaria(JogadorAtual)))
                Xeque = true;
            else
                Xeque = false;

            if (VerificarXequeMate(CorAdversaria(JogadorAtual)))
                Terminada = true;
            else
            {
            Turno++;
            MudarJogador();
            }

        }

        private void MudarJogador()
        {
            if (JogadorAtual == Cor.Branco)
                JogadorAtual = Cor.Preto;
            else
                JogadorAtual = Cor.Branco;
        }

        public void ValidarPosicaoOrigem(Posicao posicao)
        {
            if (TabuleiroXadrez.RetornarPeca(posicao) == null)
                throw new TabuleiroException("Não existe peça nesta posição.");

            if(JogadorAtual != TabuleiroXadrez.RetornarPeca(posicao).CorPeca)
                throw new TabuleiroException($"A peca escolhida deve ser {JogadorAtual}.");

            if(!TabuleiroXadrez.RetornarPeca(posicao).ExisteMovimentoPossivel())
                throw new TabuleiroException($"Não existe movimentos possíveis para esta peça.");
        }

        public void ValidarPosicaoDestino(Posicao origem, Posicao destino)
        {
            if (!TabuleiroXadrez.RetornarPeca(origem).PodeMoverPara(destino))
                throw new TabuleiroException("Posição de destino inválida.");
        }

        public HashSet<Peca> ConjuntoPecasCapturadas(Cor cor)
        {
            HashSet<Peca> controlePecas = new HashSet<Peca>();
            foreach(Peca item in PecasCapturadas)
            {
                if (item.CorPeca == cor)
                    controlePecas.Add(item);
            }

            return controlePecas;
        }
        
        private Cor CorAdversaria(Cor cor)
        {
            if (cor == Cor.Branco)
                return Cor.Preto;
            else
                return Cor.Branco;
        }

        private Peca Rei(Cor cor)
        {
            foreach(var item in ConjuntoPecasEmJogo(cor))
            {
                if (item is Rei)
                {
                    return item;
                }
            }

            return null;
        }

        public bool ReiEstaEmXeque(Cor cor)
        {
            Peca rei = Rei(cor);
            
            foreach(var item in ConjuntoPecasEmJogo(CorAdversaria(cor)))
            {
                bool[,] matriz = item.MovimentosPossiveis();

                if (matriz[rei.PosicaoPeca.Linha, rei.PosicaoPeca.Coluna])
                    return true;
            }
            return false;
        }
        public HashSet<Peca> ConjuntoPecasEmJogo(Cor cor)
        {
            HashSet<Peca> controlePecas = new HashSet<Peca>();
            foreach (Peca item in Pecas)
            {
                if (item.CorPeca == cor)
                    controlePecas.Add(item);
            }

            controlePecas.ExceptWith(ConjuntoPecasCapturadas(cor));
            return controlePecas;
        }

        public void ColocarNovaPeca(char coluna, int linha, Peca peca)
        {
            TabuleiroXadrez.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ConverterPosicao());
            Pecas.Add(peca);
        }

        public bool VerificarXequeMate(Cor cor)
        {
            if (!ReiEstaEmXeque(cor))
                return false;

            foreach(var item in ConjuntoPecasEmJogo(cor))
            {
                bool[,] matriz = item.MovimentosPossiveis();

                for(int i = 0; i < TabuleiroXadrez.Linhas; i++)
                {
                    for (int j = 0; j < TabuleiroXadrez.Colunas; j++)
                    {
                        if(matriz[i,j])
                        {
                            Posicao origem = item.PosicaoPeca;
                            Posicao destino = new Posicao(i, j);
                            //Peca pecaCapturada = ExecutarMovimento(item.PosicaoPeca, new Posicao(i, j));
                            Peca pecaCapturada = ExecutarMovimento(origem, destino);
                            bool verificarXeque = ReiEstaEmXeque(cor);
                            DesfazerMovimento(origem, destino, pecaCapturada);
                            if (!verificarXeque)
                                return false;
                        }
                    }
                }
            }
                return true;
        }
        private void ColocarPecas()
        {
            //brancas
            ColocarNovaPeca('a', 1, new Torre(TabuleiroXadrez, Cor.Branco));
            ColocarNovaPeca('b', 1, new Cavalo(TabuleiroXadrez, Cor.Branco));
            ColocarNovaPeca('c', 1, new Bispo(TabuleiroXadrez, Cor.Branco));
            ColocarNovaPeca('d', 1, new Dama(TabuleiroXadrez, Cor.Branco));
            ColocarNovaPeca('e', 1, new Rei(TabuleiroXadrez, Cor.Branco));
            ColocarNovaPeca('f', 1, new Bispo(TabuleiroXadrez, Cor.Branco));
            ColocarNovaPeca('g', 1, new Cavalo(TabuleiroXadrez, Cor.Branco));
            ColocarNovaPeca('h', 1, new Torre(TabuleiroXadrez, Cor.Branco));

            ColocarNovaPeca('a', 2, new Peao(TabuleiroXadrez, Cor.Branco));
            ColocarNovaPeca('b', 2, new Peao(TabuleiroXadrez, Cor.Branco));
            ColocarNovaPeca('c', 2, new Peao(TabuleiroXadrez, Cor.Branco));
            ColocarNovaPeca('d', 2, new Peao(TabuleiroXadrez, Cor.Branco));
            ColocarNovaPeca('e', 2, new Peao(TabuleiroXadrez, Cor.Branco));
            ColocarNovaPeca('f', 2, new Peao(TabuleiroXadrez, Cor.Branco));
            ColocarNovaPeca('g', 2, new Peao(TabuleiroXadrez, Cor.Branco));
            ColocarNovaPeca('h', 2, new Peao(TabuleiroXadrez, Cor.Branco));

            //negras
            ColocarNovaPeca('a', 8, new Torre(TabuleiroXadrez, Cor.Preto));
            ColocarNovaPeca('b', 8, new Cavalo(TabuleiroXadrez, Cor.Preto));
            ColocarNovaPeca('c', 8, new Bispo(TabuleiroXadrez, Cor.Preto));
            ColocarNovaPeca('d', 8, new Dama(TabuleiroXadrez, Cor.Preto));
            ColocarNovaPeca('e', 8, new Rei(TabuleiroXadrez, Cor.Preto));
            ColocarNovaPeca('f', 8, new Bispo(TabuleiroXadrez, Cor.Preto));
            ColocarNovaPeca('g', 8, new Cavalo(TabuleiroXadrez, Cor.Preto));
            ColocarNovaPeca('h', 8, new Torre(TabuleiroXadrez, Cor.Preto));

            ColocarNovaPeca('a', 7, new Peao(TabuleiroXadrez, Cor.Preto));
            ColocarNovaPeca('b', 7, new Peao(TabuleiroXadrez, Cor.Preto));
            ColocarNovaPeca('c', 7, new Peao(TabuleiroXadrez, Cor.Preto));
            ColocarNovaPeca('d', 7, new Peao(TabuleiroXadrez, Cor.Preto));
            ColocarNovaPeca('e', 7, new Peao(TabuleiroXadrez, Cor.Preto));
            ColocarNovaPeca('f', 7, new Peao(TabuleiroXadrez, Cor.Preto));
            ColocarNovaPeca('g', 7, new Peao(TabuleiroXadrez, Cor.Preto));
            ColocarNovaPeca('h', 7, new Peao(TabuleiroXadrez, Cor.Preto));
        }
    }
}
