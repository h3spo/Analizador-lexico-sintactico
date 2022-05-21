using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace editor_de_texto
{
    class cls_Token
    {
        private string lexema;
        private string idtoken;
        private int linea;
        private int columna;
        private int indice;
        public cls_Token(string lexema, string idtoken, int linea, int columna, int indice)
        {
            this.lexema = lexema;
            this.idtoken = idtoken;
            this.linea = linea;
            this.columna = columna;
            this.indice = indice;
        }
        public int get_indice()
        {
            return this.indice;
        }
        public string get_lexema()
        {
            return this.lexema;
        }
        public string get_idtoken()
        {
            return this.idtoken;
        }
        public int get_linea()
        {
            return this.linea;
        }
        public int get_columna()
        {
            return this.columna;
        }


    }
}
