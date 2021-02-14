using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xadrez.TabuleiroEntities;

namespace Xadrez
{
    class Tela
    {
        public static void imprimirTabuleiro(Tabuleiro tabuleiro)
        {
            for(int i = 0; i < tabuleiro.Linhas; i++)
            {
                for(int j = 0; j < tabuleiro.Colunas; j++)
                {
                    if(tabuleiro.RetornarPeca(i,j) == null)
                        Console.Write($"- ");
                    else
                        Console.Write($"{tabuleiro.RetornarPeca(i,j)} ");
                }
                Console.WriteLine();
            }
        }
    }
}
