using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace editor_de_texto
{
    class cls_error
    {
        private String lexema;
        private String id_Token;
        private int linea;
        private int columna;
        public cls_error(String lexema, String idToken, int linea, int columna)
        {

            this.lexema = lexema;
            this.id_Token = idToken;
            this.linea = linea;
            this.columna = columna;
        }
        public String get_Lexema()
        {
            return this.lexema;
        }
        public String get_IdToken()
        {
            return this.id_Token;
        }
        public int get_Linea()
        {
            return this.linea;
        }
        public int get_Columna()
        {
            return this.columna;
        }

    }
}
