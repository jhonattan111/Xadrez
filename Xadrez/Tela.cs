using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xadrez.PecasXadrez;
using Xadrez.TabuleiroEntities;

namespace Xadrez
{
    class Tela
    {
        public static void ImprimirTabuleiro(Tabuleiro tabuleiro)
        {
            for (int i = 0; i < tabuleiro.Linhas; i++)
            {
                Console.Write($"{8 - i} ");
                for (int j = 0; j < tabuleiro.Colunas; j++)
                {
                    Tela.ImprimirPeca(tabuleiro.RetornarPeca(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  A B C D E F G H");
        }

        public static void ImprimirTabuleiro(Tabuleiro tabuleiro, bool[,] posicoesPossiveis)
        {
            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoMovimentosPossiveis = ConsoleColor.DarkCyan;

            for (int i = 0; i < tabuleiro.Linhas; i++)
            {
                Console.Write($"{8 - i} ");
                for (int j = 0; j < tabuleiro.Colunas; j++)
                {
                    if (posicoesPossiveis[i, j])
                        Console.BackgroundColor = fundoMovimentosPossiveis;

                    Tela.ImprimirPeca(tabuleiro.RetornarPeca(i, j));
                    Console.BackgroundColor = fundoOriginal;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  A B C D E F G H");
        }

        public static void ImprimirPeca(Peca peca)
        {

            if (peca == null)
                Console.Write($"- ");
            else
            {
                if (peca.CorPeca == Cor.Branco)
                    Console.Write(peca);
                else
                {
                    ConsoleColor consoleColor = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(peca);
                    Console.ForegroundColor = consoleColor;
                }

            }
        }

        public static void ImprimirPartida(PartidaXadrez partida)
        {
            ImprimirTabuleiro(partida.TabuleiroXadrez);
            ImprimirPecasCapturadas(partida);
            Console.WriteLine($"\nTurno: {partida.Turno}");
            Console.WriteLine($"Aguardando jogada: {partida.JogadorAtual}");

            if(partida.Xeque)
                Console.WriteLine("VOCÊ ESTÁ EM XEQUE!");
        }

        public static void ImprimirPecasCapturadas(PartidaXadrez partida)
        {
            Console.WriteLine("\nPeças Capturadas");
            Console.Write($"Brancas: ");
            ImprimirConjunto(partida.ConjuntoPecasCapturadas(Cor.Branco));
            Console.Write("Pretas: ");
            var aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            ImprimirConjunto(partida.ConjuntoPecasCapturadas(Cor.Preto));
            Console.ForegroundColor = aux;
        }

        public static void ImprimirConjunto(HashSet<Peca> conjunto)
        {
            Console.Write($"[");
            foreach(var item in conjunto)
            {
                Console.Write($"{item} ");
            }
            Console.WriteLine($"]");
        }

        public static PosicaoXadrez LerPosicaoXadrez()
        {
            string s = Console.ReadLine();
            char coluna = s[0];
            int linha = int.Parse($"{s[1]}");
            return new PosicaoXadrez(coluna, linha);
        }
    }
}
