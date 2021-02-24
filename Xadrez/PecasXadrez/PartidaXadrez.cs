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
        public Tabuleiro TabuleiroXadrez { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool Terminada { get; private set; }
        private HashSet<Peca> Pecas;
        private HashSet<Peca> PecasCapturadas;
        public bool Xeque { get; private set; }
        public Peca VulneravelEnPassant { get; private set; }

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
            VulneravelEnPassant = null;
        }

        public Peca ExecutarMovimento(Posicao origem, Posicao destino)
        {
            Peca peca = TabuleiroXadrez.RetirarPeca(origem);
            peca.IncrementarQuantidadeMovimento();
            Peca pecaCapturada = TabuleiroXadrez.RetirarPeca(destino);
            TabuleiroXadrez.ColocarPeca(peca, destino);
            if (pecaCapturada != null)
                PecasCapturadas.Add(pecaCapturada);

            //roque pequeno o-o
            if (peca is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca TorreRoque = TabuleiroXadrez.RetirarPeca(origemTorre);
                TorreRoque.IncrementarQuantidadeMovimento();
                TabuleiroXadrez.ColocarPeca(TorreRoque, destinoTorre);
            }

            //roque grande o-o-o
            if (peca is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca TorreRoque = TabuleiroXadrez.RetirarPeca(origemTorre);
                TorreRoque.IncrementarQuantidadeMovimento();
                TabuleiroXadrez.ColocarPeca(TorreRoque, destinoTorre);
            }

            //En passant
            if (peca is Peao)
                if (origem.Coluna != destino.Coluna && pecaCapturada == null)
                {
                    Posicao posicaoPeao;
                    if (peca.CorPeca == Cor.Branco)
                        posicaoPeao = new Posicao(destino.Linha + 1, destino.Coluna);
                    else
                        posicaoPeao = new Posicao(destino.Linha - 1, destino.Coluna);
                    pecaCapturada = TabuleiroXadrez.RetirarPeca(posicaoPeao);
                    PecasCapturadas.Add(pecaCapturada);
                }


            return pecaCapturada;
        }

        public void DesfazerMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca peca = TabuleiroXadrez.RetirarPeca(destino);
            peca.DecrementarQuantidadeMovimento();
            if (pecaCapturada != null)
            {
                TabuleiroXadrez.ColocarPeca(peca, destino);
                PecasCapturadas.Remove(pecaCapturada);
            }

            TabuleiroXadrez.ColocarPeca(peca, origem);

            //roque pequeno o-o
            if (peca is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca TorreRoque = TabuleiroXadrez.RetirarPeca(destinoTorre);
                TorreRoque.DecrementarQuantidadeMovimento();
                TabuleiroXadrez.ColocarPeca(TorreRoque, origemTorre);
            }

            //roque grande o-o-o
            if (peca is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca TorreRoque = TabuleiroXadrez.RetirarPeca(destinoTorre);
                TorreRoque.DecrementarQuantidadeMovimento();
                TabuleiroXadrez.ColocarPeca(TorreRoque, origemTorre);
            }

            // en passant
            if(peca is Peao)
            {
                if (origem.Coluna != destino.Coluna && pecaCapturada == VulneravelEnPassant)
                {
                    Peca peao = TabuleiroXadrez.RetirarPeca(destino);
                    Posicao posicaoPeao;
                    if (peca.CorPeca == Cor.Branco)
                        posicaoPeao = new Posicao(3, destino.Coluna);
                    else
                        posicaoPeao = new Posicao(4, destino.Coluna);

                    TabuleiroXadrez.ColocarPeca(peao, posicaoPeao);
                }
            }

        }

        public void RealizarJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = ExecutarMovimento(origem, destino);

            if (ReiEstaEmXeque(JogadorAtual))
            {
                DesfazerMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em Xeque");
            }

            if (ReiEstaEmXeque(CorAdversaria(JogadorAtual)))
                Xeque = true;
            else
                Xeque = false;

            Peca peca = TabuleiroXadrez.RetornarPeca(destino);

            //promocao
            if(peca is Peao)
            {
                if ((peca.CorPeca == Cor.Branco && destino.Linha == 0) || (peca.CorPeca == Cor.Preto && destino.Linha == 7))
                {
                    peca = TabuleiroXadrez.RetirarPeca(destino);
                    Pecas.Remove(p);
                    Peca dama = new Dama(TabuleiroXadrez, peca.CorPeca);
                    TabuleiroXadrez.ColocarPeca(dama, destino);
                    Pecas.Add(dama);
                }
            }


            if (VerificarXequeMate(CorAdversaria(JogadorAtual)))
                Terminada = true;
            else
            {
                Turno++;
                MudarJogador();
            }


            //en passant
            if (peca is Peao && destino.Linha == origem.Linha - 2 || destino.Linha == origem.Linha + 2)
                VulneravelEnPassant = peca;
            else
                VulneravelEnPassant = null;



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

            if (JogadorAtual != TabuleiroXadrez.RetornarPeca(posicao).CorPeca)
                throw new TabuleiroException($"A peca escolhida deve ser {JogadorAtual}.");

            if (!TabuleiroXadrez.RetornarPeca(posicao).ExisteMovimentoPossivel())
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
            foreach (Peca item in PecasCapturadas)
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
            foreach (var item in ConjuntoPecasEmJogo(cor))
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

            foreach (var item in ConjuntoPecasEmJogo(CorAdversaria(cor)))
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

            foreach (var item in ConjuntoPecasEmJogo(cor))
            {
                bool[,] matriz = item.MovimentosPossiveis();

                for (int i = 0; i < TabuleiroXadrez.Linhas; i++)
                {
                    for (int j = 0; j < TabuleiroXadrez.Colunas; j++)
                    {
                        if (matriz[i, j])
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
            ColocarNovaPeca('e', 1, new Rei(TabuleiroXadrez, Cor.Branco, this));
            ColocarNovaPeca('f', 1, new Bispo(TabuleiroXadrez, Cor.Branco));
            ColocarNovaPeca('g', 1, new Cavalo(TabuleiroXadrez, Cor.Branco));
            ColocarNovaPeca('h', 1, new Torre(TabuleiroXadrez, Cor.Branco));

            ColocarNovaPeca('a', 2, new Peao(TabuleiroXadrez, Cor.Branco, this));
            ColocarNovaPeca('b', 2, new Peao(TabuleiroXadrez, Cor.Branco, this));
            ColocarNovaPeca('c', 2, new Peao(TabuleiroXadrez, Cor.Branco, this));
            ColocarNovaPeca('d', 2, new Peao(TabuleiroXadrez, Cor.Branco, this));
            ColocarNovaPeca('e', 2, new Peao(TabuleiroXadrez, Cor.Branco, this));
            ColocarNovaPeca('f', 2, new Peao(TabuleiroXadrez, Cor.Branco, this));
            ColocarNovaPeca('g', 2, new Peao(TabuleiroXadrez, Cor.Branco, this));
            ColocarNovaPeca('h', 2, new Peao(TabuleiroXadrez, Cor.Branco, this));

            //negras
            ColocarNovaPeca('a', 8, new Torre(TabuleiroXadrez, Cor.Preto));
            ColocarNovaPeca('b', 8, new Cavalo(TabuleiroXadrez, Cor.Preto));
            ColocarNovaPeca('c', 8, new Bispo(TabuleiroXadrez, Cor.Preto));
            ColocarNovaPeca('d', 8, new Dama(TabuleiroXadrez, Cor.Preto));
            ColocarNovaPeca('e', 8, new Rei(TabuleiroXadrez, Cor.Preto, this));
            ColocarNovaPeca('f', 8, new Bispo(TabuleiroXadrez, Cor.Preto));
            ColocarNovaPeca('g', 8, new Cavalo(TabuleiroXadrez, Cor.Preto));
            ColocarNovaPeca('h', 8, new Torre(TabuleiroXadrez, Cor.Preto));

            ColocarNovaPeca('a', 7, new Peao(TabuleiroXadrez, Cor.Preto, this));
            ColocarNovaPeca('b', 7, new Peao(TabuleiroXadrez, Cor.Preto, this));
            ColocarNovaPeca('c', 7, new Peao(TabuleiroXadrez, Cor.Preto, this));
            ColocarNovaPeca('d', 7, new Peao(TabuleiroXadrez, Cor.Preto, this));
            ColocarNovaPeca('e', 7, new Peao(TabuleiroXadrez, Cor.Preto, this));
            ColocarNovaPeca('f', 7, new Peao(TabuleiroXadrez, Cor.Preto, this));
            ColocarNovaPeca('g', 7, new Peao(TabuleiroXadrez, Cor.Preto, this));
            ColocarNovaPeca('h', 7, new Peao(TabuleiroXadrez, Cor.Preto, this));
        }
    }
}
