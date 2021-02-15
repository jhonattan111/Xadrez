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
        public static void imprimirTabuleiro(Tabuleiro tabuleiro)
        {
            for (int i = 0; i < tabuleiro.Linhas; i++)
            {
                Console.Write($"{8 - i} ");
                for (int j = 0; j < tabuleiro.Colunas; j++)
                {
                    if (tabuleiro.RetornarPeca(i, j) == null)
                        Console.Write($"- ");
                    else
                        Tela.ImprimirPeca(tabuleiro.RetornarPeca(i,j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  A B C D E F G H");
        }

        public static void ImprimirPeca(Peca peca)
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

        public static PosicaoXadrez LerPosicaoXadrez()
        {
            string s = Console.ReadLine();
            char coluna = s[0];
            int linha = int.Parse($"{s[1]}");
            return new PosicaoXadrez(coluna, linha);
        }
    }
}
