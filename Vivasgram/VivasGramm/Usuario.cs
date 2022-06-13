using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace VivasGramm
{
    class Usuario
    {
        private string nickUser;
        public string NickUser
        {
            set;get;
        }

        private string contrasenha;
        public string Contrasenha
        {
            set;get;
        }

        public NetworkStream stream;

        public StreamWriter streamwriter;

        public StreamReader streamreader;
    }
}
