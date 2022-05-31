using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace editor_de_texto
{
    class cls_sintacError
    {
        private string message;
        private int id_Token;
        private int code_Token;
       
        public cls_sintacError(string mesagge, int idToken, int code_Token)
        {


            this.id_Token = idToken;
            this.message = mesagge;
            this.code_Token = code_Token;
        }
        public string get_mesagge() 
        {
            return this.message;
        }
       
        public int get_IdToken()
        {
            return this.id_Token;
        }
        public int get_codetoken() 
        {
            return this.code_Token;
        }
       

    }
}
