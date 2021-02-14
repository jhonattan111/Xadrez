using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xadrez.TabuleiroEntities.Excecoes
{
    class TabuleiroExcecoes : Exception
    {
        public TabuleiroExcecoes(string mensagem) : base(mensagem)
        {

        }
    }
}
