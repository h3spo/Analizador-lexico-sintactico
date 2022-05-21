using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace editor_de_texto
{
    class cls_gr_ejecutar
    {
        String Token_n;
        long id_Token;
        Object linea_sententencia;
        String cadena_oper;

        public cls_gr_ejecutar(long idToken, String Token_n, Object linea_sentencia, String cadena_oper)
        {

            this.Token_n = Token_n;
            this.id_Token = idToken;
            this.linea_sententencia = linea_sentencia;
            this.cadena_oper = cadena_oper;

        }

        public String getCadena_oper()
        {
            return this.cadena_oper;
        }

        public String getToken_n()
        {
            return this.Token_n;
        }
        public long getIdToken()
        {
            return this.id_Token;
        }
        public Object getlin_senten()
        {
            return this.linea_sententencia;
        }

    }
}
