using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace editor_de_texto
{
    class cls_lectura_lineas
    {
        string token;
        long idtoken;
        object sentencia;
        string cadena_op;
        public cls_lectura_lineas(string token, long idtoken, object senetencia, string cadena_op)
        {
            this.token = token;
            this.idtoken = idtoken;
            this.sentencia = senetencia;
            this.cadena_op = cadena_op;
        }
        public string get_token()
        {
            return this.token;
        }
        public long get_idtoken()
        {
            return this.idtoken;
        }
        public object get_sentencia()
        {
            return this.sentencia;
        }
        public string get_cadena_op()
        {
            return this.cadena_op;
        }

    }
}
