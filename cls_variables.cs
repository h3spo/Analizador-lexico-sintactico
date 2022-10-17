using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace editor_de_texto
{
    class cls_variables
    {
        private string lexema;
        private int id_token;

        private string tipo;

        private string valor;

        public cls_variables(string lexema , int idtoken,string tipo, string valor )
        {
            this.lexema=lexema;
            this.id_token=idtoken;
            this.tipo=tipo;
            this.valor=valor;
        }
        public String get_lexema()
        {
            return this.lexema;
        }
        public int get_idtoken()
        {
            return this.id_token;
        }

        public String get_tipo()
        {
            return this.tipo;
        }

        public String get_valor()
        {
            return this.valor;
        }

    }
}
