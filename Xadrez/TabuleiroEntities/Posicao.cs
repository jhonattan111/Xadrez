using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xadrez.TabuleiroEntities
{
    class Posicao
    {
        public int Linha { get; set; }
        public int Coluna { get; set; }
        
        public Posicao(int linha, int coluna)
        {
            this.Linha = linha;
            this.Coluna = coluna;
        }

        public override string ToString()
        {
            return $"{Linha}, {Coluna}";        
        }
    }
}
