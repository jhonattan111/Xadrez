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

                PartidaXadrez partida = new PartidaXadrez();

                while(!partida.terminada)
                {
                    Console.Clear();
                    Tela.imprimirTabuleiro(partida.tabuleiro);

                    Console.WriteLine("Origem");
                    Posicao origem = Tela.LerPosicaoXadrez().ConverterPosicao();
                    Console.WriteLine("Destino");
                    Posicao destino = Tela.LerPosicaoXadrez().ConverterPosicao();

                    partida.ExecutarMovimento(origem, destino);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            Console.ReadKey();
        }
    }
}
