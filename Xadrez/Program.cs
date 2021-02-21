using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xadrez.PecasXadrez;
using Xadrez.TabuleiroEntities;
using Xadrez.TabuleiroEntities.XadrezExceptions;

namespace Xadrez
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                PartidaXadrez partida = new PartidaXadrez();


                while (!partida.Terminada)
                {
                    try
                    {
                        Console.Clear();
                        Tela.ImprimirPartida(partida);

                        Console.Write("Origem: ");
                        Posicao origem = Tela.LerPosicaoXadrez().ConverterPosicao();
                        partida.ValidarPosicaoOrigem(origem);
                        bool[,] PosicoesPossiveis = partida.TabuleiroXadrez.RetornarPeca(origem).MovimentosPossiveis();

                        Console.Clear();

                        Tela.ImprimirTabuleiro(partida.TabuleiroXadrez, PosicoesPossiveis);

                        Console.WriteLine($"\nTurno: {partida.Turno}");
                        Console.WriteLine($"Aguardando jogada: {partida.JogadorAtual}");
                        Console.Write("Destino: ");

                        Posicao destino = Tela.LerPosicaoXadrez().ConverterPosicao();
                        partida.ValidarPosicaoDestino(origem, destino);

                        partida.RealizarJogada(origem, destino);
                    }
                    catch (TabuleiroException ex)
                    {
                        Console.WriteLine($"{ex.Message}, aperte Enter para continuar");
                        Console.ReadLine();
                    }
                }

            }
            catch (TabuleiroException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }
    }
}
