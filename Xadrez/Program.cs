using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xadrez.PecasXadrez;
using Xadrez.TabuleiroEntities;

namespace Xadrez
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {

            Tabuleiro tabuleiro = new Tabuleiro(8, 8);

            tabuleiro.ColocarPeca(new Torre(tabuleiro, Cor.Branco), new Posicao(0, 0));
            tabuleiro.ColocarPeca(new Rei(tabuleiro, Cor.Branco), new Posicao(0, 1));

            Tela.imprimirTabuleiro(tabuleiro);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            Console.ReadKey();
        }
    }
}
