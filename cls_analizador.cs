using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace editor_de_texto
{
    class cls_analizador
    {
        //Stack pila_posfijo = new Stack();
        Stack pila = new Stack();
        List<int> lista_sintac = new List<int>();
        List<String> temporal = new List<string>();

        List<int> listacontador_cilcos = new List<int>();

        List<string> lista_lexemas = new List<string>();

        //!los numero de token , numero de error estan en archivo Diccionario.txt
        ArrayList tokens;
        ArrayList tipos;
        // int carga = 0;
        //se crea una lista para guardar los tokens la estructura y caracteristicas estan en
        //cls_token
        ArrayList lista_operaciones;
        ArrayList variables;

        static private List<cls_Token> lista_tokens;
        static private List<cls_variables> lista_Varibles;
        //static private List<cls_sintac> lista_sintac;

        public List<cls_lectura_lineas> ejecutar_lineas;
        public List<cls_gr_ejecutar> gr_ejecutar;
        private String retorno;
        public int estado_token;

        static private List<cls_error> lista_errores;
        static private List<cls_sintacError> lista_errorSintac;
        public cls_analizador()
        {
            lista_tokens = new List<cls_Token>();
            lista_Varibles = new List<cls_variables>();
            //lista_sintac = new List<cls_sintac>();
            tokens = new ArrayList();
            tipos = new ArrayList();
            tokens.Add("Resultado");


            tipos.Add("Valor");
            tipos.Add("Operador");
            tipos.Add("IZQ");
            tipos.Add("DER");

            lista_operaciones = new ArrayList();
            ejecutar_lineas = new List<cls_lectura_lineas>();
            gr_ejecutar = new List<cls_gr_ejecutar>();

            lista_errores = new List<cls_error>();
            lista_errorSintac = new List<cls_sintacError>();

        }
        public void add_Token(String lexema, int idToken, int linea, int columna, int indice)
        {
            cls_Token nuevo = new cls_Token(lexema, idToken, linea, columna, indice);
            lista_tokens.Add(nuevo);
        }

        public void add_varaible(string lexema, int idtoken, string valor, string tipo)
        {
            cls_variables nuevo = new cls_variables(lexema, idtoken, tipo, valor);
            lista_Varibles.Add(nuevo);
        }





        public void addError(String lexema, int idToken, int linea, int columna)
        {
            cls_error errtok = new cls_error(lexema, idToken, linea, columna);
            lista_errores.Add(errtok);
        }

        public void addErrorSintac(string mesagge, int idToken, int code_token)
        {
            cls_sintacError error = new cls_sintacError(mesagge, idToken, code_token);
            lista_errorSintac.Add(error);
        }
        public void analizar_Cadena(string entrada)
        {
            int estado = 0;
            int columna = 0;
            int fila = 1;
            string lexema = "";
            Char c;
            entrada = entrada + " ";
            for (int i = 0; i < entrada.Length; i++)
            {
                c = entrada[i];
                columna++;
                switch (estado)
                {
                    case 0:
                        if (Char.IsLetter(c))
                        {
                            estado = 1;
                            lexema += c;
                        }
                        else if (Char.IsDigit(c))
                        {
                            estado = 2;
                            lexema += c;
                        }
                        else if (c == '"')
                        {
                            //lexema += c;
                            estado = 4;
                            i--;
                            columna--;
                        }
                        else if (c == '\'')
                        {
                            estado = 11;
                            i--;
                            columna--;
                        }
                        else if (c == ',')
                        {
                            lexema += c;
                            lista_lexemas.Add(lexema);
                            add_Token(lexema, 160, fila, columna, i - lexema.Length);
                            //lista_lexemas.Add(lexema);
                            lexema = "";
                            //estado = 6;
                            //i--;
                            //columna--;
                        }
                        else if (c == ' ')
                        {
                            estado = 0;
                        }
                        else if (c == '\n')
                        {
                            columna = 0;
                            fila++;
                            estado = 0;
                            //add_Token(lexema,1,fila,columna, i-lexema.Length);
                        }
                        /* else if (c == '{')
                         {
                             lexema += c;

                             //{=19
                             add_Token(lexema, "19", fila, columna, i - lexema.Length);
                             lexema = "";
                         }
                         else if (c == '}')
                         {
                             //}=20
                             lexema += c;
                             add_Token(lexema, "20", fila, columna, i - lexema.Length);
                             lexema = "";
                         }*/
                        else if (c == '(')
                        {
                            //(=17
                            lexema += c;
                            lista_lexemas.Add(lexema);
                            add_Token(lexema, 150, fila, columna, i - lexema.Length);
                            lexema = "";
                            // lista_sintac.Add(150);
                        }
                        else if (c == ')')
                        {
                            //)=18
                            lexema += c;
                            lista_lexemas.Add(lexema);
                            add_Token(lexema, 151, fila, columna, i - lexema.Length);
                            lexema = "";
                        }
                        else if (c == ',')
                        {
                            lexema += c;
                            lexema = "";
                        }
                        else if (c == ';')
                        {
                            //;=27
                            lexema += c;
                            lista_lexemas.Add(lexema);
                            add_Token(lexema, 161, fila, columna, i - lexema.Length);
                            lexema = "";
                        }
                        else if (c == '<')
                        {
                            //<=11
                            //lexema += c;
                            //add_Token(lexema, "11", fila, columna, i - lexema.Length);
                            //lexema = "";
                            lexema += c;
                            estado = 14;
                            i--;
                            columna--;


                        }
                        else if (c == '>')
                        {
                            //>=10
                            //lexema += c;
                            //add_Token(lexema, "10", fila, columna, i - lexema.Length);
                            //lexema = "";
                            lexema += c;
                            estado = 14;
                            i--;
                            columna--;
                        }
                        else if (c == '.')
                        {
                            //.=5
                            lexema += c;
                            lista_lexemas.Add(lexema);
                            add_Token(lexema, 5, fila, columna, i - lexema.Length);
                            lexema = "";
                        }
                        else if (c == '+')
                        {
                            lexema += c;
                            lista_lexemas.Add(lexema);
                            add_Token(lexema, 120, fila, columna, i);
                            lexema = "";
                        }
                        else if (c == '-')
                        {
                            lexema += c;
                            lista_lexemas.Add(lexema);
                            add_Token(lexema, 121, fila, columna, i);
                            lexema = "";
                        }
                        else if (c == '*')
                        {
                            lexema += c;
                            lista_lexemas.Add(lexema);
                            add_Token(lexema, 122, fila, columna, i);
                            lexema = "";
                        }
                        else if (c == '/')
                        {
                            lexema += c;
                            lista_lexemas.Add(lexema);
                            add_Token(lexema, 123, fila, columna, i);
                            lexema = "";
                            //lexema += c;
                            //estado = 11;
                            //i--;
                            //columna--;
                        }
                        else if (c == '=')
                        {
                            lexema += c;
                            lista_lexemas.Add(lexema);
                            add_Token(lexema, 135, fila, columna, i);
                            lexema = "";
                            //fila++;
                        }


                        /*else if (c == '%')
                        {
                            lexema += c;
                            add_Token(lexema, "10", fila, columna, i);
                            lexema = "";
                        }*/
                        else if (c == ':')
                        {
                            lexema += c;
                            estado = 17;
                            i--;
                            columna--;
                        }
                        else
                        {
                            /*//otra cosa
                            estado = -99;
                            i--;
                            columna--;*/
                            //lexema += c;
                            //add_Token(lexema, "1000", fila, columna, i - lexema.Length);
                            //lexema = "";
                            lexema += c;
                            //addError(lexema, "500", fila, columna);
                            lista_lexemas.Add(lexema);
                            addError(lexema, 1000, fila, columna);
                            estado = 0;
                            lexema = "";
                            //fila++;
                        }
                        break;
                    case 1:
                        int encontrado = verificar_reservada(lexema);
                        if (Char.IsLetterOrDigit(c) || c == '_')
                        {
                            lexema += c;
                            estado = 1;
                        }
                        else
                        {

                            //lexema = "";
                            //i--;
                            //columna--;
                            //estado = 0;
                            if (encontrado >= 500)
                            {
                                int valor = encontrado;
                                //reservadas =500
                                lexema += c;
                                lista_lexemas.Add(lexema);
                                add_Token(lexema, valor, fila, columna, i - lexema.Length);
                                lexema = "";
                                i--;
                                columna--;
                                estado = 0;
                                //fila++;
                            }
                            else if (encontrado == 0)
                            {


                                //lexema += c;
                                lista_lexemas.Add(lexema);

                                add_Token(lexema, 110, fila, columna, i - lexema.Length);
                                lexema = "";
                                i--;
                                columna--;
                                estado = 0;
                                //fila++;
                            }
                            //fila++;


                        }




                        //fila++;
                        //lexema = " ";

                        break;
                    case 2:
                        if (Char.IsDigit(c))
                        {
                            lexema += c;
                            estado = 2;
                        }

                        else if (c == '.')
                        {
                            estado = 9;
                            lexema += c;
                        }
                        else if (c == 'e')
                        {
                            estado = 19;
                            lexema += c;
                        }

                        else
                        {
                            //digitos
                            lista_lexemas.Add(lexema);
                            add_Token(lexema, 102, fila, columna, i - lexema.Length);
                            lexema = "";
                            i--;
                            columna--;
                            estado = 0;
                        }
                        break;
                    case 3:
                        if (Char.IsDigit(c))
                        {
                            lexema += c;
                            estado = 2;
                        }
                        else
                        {
                            estado = -99;
                            i = i - 2;
                            columna = columna - 2;
                            lexema = "";
                        }
                        break;
                    case 4:
                        if (c == '"')
                        {
                            lexema += c;
                            estado = 5;
                        }
                        break;
                    case 5:
                        if (c == '\n')
                        {
                            lexema += c;
                            estado = 99;
                            i--;
                            columna--;
                            //lexema = "";
                        }
                        else if (c != '"')
                        {
                            lexema += c;
                            estado = 5;
                        }


                        else
                        {
                            estado = 7;
                            i--;
                            columna--;
                        }
                        break;
                    case 6:
                        if (c != '\'')
                        {
                            lexema += c;
                            estado = 6;
                        }

                        else
                        {
                            estado = 8;
                            i--;
                            columna--;
                        }
                        break;
                    case 7:

                        if (c == '"')
                        {
                            lexema += c;
                            //cadena doble
                            lista_lexemas.Add(lexema);
                            add_Token(lexema, 101, fila, columna, i - lexema.Length);
                            estado = 0;
                            lexema = "";
                        }


                        else if (c == ',')
                        {
                            lexema += c;
                            lista_lexemas.Add(lexema);

                            add_Token(lexema, 4, fila, columna, i - lexema.Length);
                            estado = 0;
                            lexema = "";
                        }

                        break;
                    case 8:
                        if (c == '\'')
                        {
                            lexema += c;
                            lista_lexemas.Add(lexema);
                            add_Token(lexema, 135, fila, columna, i - lexema.Length);
                            estado = 0;
                            lexema = "";
                        }
                        else if (c == ',')
                        {
                            lexema += c;
                            lista_lexemas.Add(lexema);

                            add_Token(lexema, 4, fila, columna, i - lexema.Length);
                            estado = 0;
                            lexema = "";
                        }
                        break;

                    case 9:
                        if (Char.IsDigit(c))
                        {
                            estado = 10;
                            lexema += c;
                        }
                        else if (c == 'e')
                        {
                            estado = 2;
                            lexema += c;
                        }
                        else
                        {
                            lista_lexemas.Add(lexema);
                            addError(lexema, 301, fila, columna);
                            estado = 0;
                            lexema = "";
                            fila++;
                        }
                        break;
                    case 10:
                        if (Char.IsDigit(c))
                        {
                            estado = 10;
                            lexema += c;
                        }
                        else if (c == 'e')
                        {
                            estado = 19;
                            lexema += c;
                        }
                        else
                        {
                            lista_lexemas.Add(lexema);

                            add_Token(lexema, 103, fila, columna, i - lexema.Length);
                            lexema = "";
                            i--;
                            columna--;
                            estado = 0;
                        }
                        break;

                    case 11:
                        if (c == '\'')
                        {
                            lexema += c;
                            estado = 12;

                        }
                        break;
                    case 12:
                        if (c != '\n')
                        {
                            lexema += c;
                            estado = 12;
                        }
                        else
                        {
                            estado = 13;
                            i--;
                            //columna--;
                        }
                        break;
                    case 13:
                        if (c == '\n')
                        {
                            lexema += c;
                            lista_lexemas.Add(lexema);
                            add_Token(lexema, 170, fila, columna, i - lexema.Length);
                            estado = 0;
                            lexema = "";
                            fila++;
                        }

                        break;
                    case 14:
                        if (c == '<')
                        {

                            estado = 15;

                        }
                        else if (c == '>')
                        {
                            estado = 16;
                        }
                        break;

                    case 15:
                        if (c != '=' && c != '>')
                        {
                            //lexema += c;
                            lista_lexemas.Add(lexema);
                            add_Token(lexema, 130, fila, columna, i - lexema.Length);
                            estado = 0;
                            lexema = "";
                            i--;
                            //columna--;
                            //fila++;
                        }
                        else if (c == '=')
                        {
                            lexema += c;
                            lista_lexemas.Add(lexema);
                            add_Token(lexema, 131, fila, columna, i - lexema.Length);
                            estado = 0;
                            lexema = "";
                            //i--;
                            //fila++;
                        }
                        else
                        {
                            lexema += c;
                            lista_lexemas.Add(lexema);
                            add_Token(lexema, 134, fila, columna, i - lexema.Length);
                            estado = 0;
                            lexema = "";
                        }
                        break;
                    case 16:
                        if (c != '=')
                        {
                            //lexema += c;
                            lista_lexemas.Add(lexema);
                            add_Token(lexema, 132, fila, columna, i - lexema.Length);
                            estado = 0;
                            lexema = "";
                            i--;
                            // fila++;
                        }
                        else
                        {
                            lexema += c;
                            lista_lexemas.Add(lexema);
                            add_Token(lexema, 133, fila, columna, i - lexema.Length);
                            estado = 0;
                            lexema = "";
                            //i--;
                            // fila++;
                        }
                        break;
                    case 17:
                        if (c == ':')
                        {
                            estado = 18;

                        }
                        break;
                    case 18:
                        if (c != '=')
                        {
                            lexema += c;
                            lista_lexemas.Add(lexema);
                            add_Token(lexema, 162, fila, columna, i - lexema.Length);
                            estado = 0;
                            lexema = "";
                            //fila++;
                        }
                        if (c == '=')
                        {
                            lexema += c;
                            lista_lexemas.Add(lexema);
                            add_Token(lexema, 141, fila, columna, i - lexema.Length);
                            estado = 0;
                            lexema = "";
                            // fila++;
                        }
                        break;
                    case 19:
                        if (Char.IsDigit(c))
                        {
                            estado = 20;
                            lexema += c;
                        }
                        else if (c == '-')
                        {
                            estado = 21;
                            lexema += c;
                        }
                        else
                        {
                            lista_lexemas.Add(lexema);
                            addError(lexema, 301, fila, columna);
                            estado = 0;
                            lexema = "";
                        }
                        break;
                    //potenciacion
                    case 20:
                        if (Char.IsDigit(c))
                        {
                            estado = 20;
                            lexema += c;
                        }
                        else
                        {
                            lista_lexemas.Add(lexema);

                            add_Token(lexema, 104, fila, columna, i - lexema.Length);
                            lexema = "";
                            i--;
                            columna--;
                            estado = 0;
                        }
                        break;

                    case 21:
                        if (Char.IsDigit(c))
                        {
                            estado = 21;
                            lexema += c;
                        }
                        else
                        {
                            lista_lexemas.Add(lexema);

                            add_Token(lexema, 105, fila, columna, i - lexema.Length);
                            lexema = "";
                            i--;
                            columna--;
                            estado = 0;
                        }
                        break;


                    case 99:
                        lexema += c;
                        //addError(lexema, "500", fila, columna);
                        lista_lexemas.Add(lexema);
                        addError(lexema, 1000, fila, columna);
                        estado = 0;
                        lexema = "";
                        fila++;
                        break;

                }
            }
        }


        //todo metodo para provar el conteo de los ()
        List<int> lista_parentesis_abre = new List<int>();
        List<int> lista_parentesis_cierra = new List<int>();
        List<int> lista_condicional = new List<int>();
        List<int> lista_condicional2 = new List<int>();



        public void analisis_sintactico()
        {

            int estado = 0;
            string tipo = " ";
            string valor = "";

            int ciclos_open = 0;
            int ciclos_close = 0;




            //?es la pila que ya contienen todos los tokens
            // int Stack = pila.Count;
            for (int i = 0; i < lista_sintac.Count; i++)
            {

                int actual = lista_sintac[i];
                string lexema = lista_lexemas[i];//obtener variable

                //int actual = Stack;
                switch (estado)
                {
                    case 0:

                        if (actual == 500)//todo llego la reservada de incio
                        {

                            estado = 1;
                        }
                        else
                        {
                            addErrorSintac("Se esperaba inicio", 1000, actual);
                            estado = 99;
                        }
                        break;
                    case 1:

                        //i++;
                        if (actual == 110) //todo llego un nombre
                        {
                            estado = 2;
                            add_varaible(lexema, 110, "0", "Solucion");
                            // dt_variables.Rows.Add(lexema,0,"Solucion");
                        }
                        else
                        {
                            addErrorSintac("Se  esperaba un nombre", 1027, actual);
                            estado = 99;
                        }
                        break;
                    //? se da incio al control de variables 
                    //!varaible = {(vare) (:)  (inter|decim|text) (varaible) (;)}
                    case 2:
                        if (actual == 501)
                        {
                            //500=(vare)
                            //llego el nombre para la variable
                            estado = 3;
                        }
                        else
                        {
                            addErrorSintac("Syntax error", 1028, actual);
                            estado = 999;
                        }
                        break;
                    case 3:
                        if (actual == 162)
                        {
                            // (vare) (:)
                            estado = 4;
                        }
                        else
                        {
                            addErrorSintac("Syntax error", 1029, actual);
                            estado = 999;
                        }
                        break;
                    case 4:
                        if (actual >= 502 && actual <= 504)
                        {
                            // (vare) (:) (inter 502|decim 503|text 504)
                            estado = 5;



                        }
                        else
                        {
                            addErrorSintac("Syntax error", 1030, actual);
                            estado = 999;
                        }
                        break;
                    case 5:
                        if (actual == 110)
                        {
                            // (vare) (:) (inter 502|decim 503|text 504) (variable)
                            estado = 6;
                        }
                        else
                        {
                            addErrorSintac("Syntax error", 1031, actual);
                            estado = 999;
                        }
                        break;
                    case 6:
                        if (actual == 161)
                        {
                            //;asd
                            estado = 7;
                            tipo = Convert.ToString(lista_lexemas[i - 2]);
                            lexema = Convert.ToString(lista_lexemas[i - 1]);
                            add_varaible(lexema, 110, "0", tipo);
                            //i = i +3;

                        }
                        else if (actual == 141)
                        {

                            estado = 39;//?mandamos a estado de igualacion para variables


                        }
                        else
                        {
                            addErrorSintac("Syntax error ", 1032, actual);
                            estado = 999;
                        }
                        break;
                    //todo repetimos todo el proceso de nuevo por si llegan mas declariciones de variables
                    case 7://todo estado incial de todo


                        if (actual == 501)
                        {
                            i--;
                            estado = 2;
                        }
                        //!COMIENZA EL CONTROL PARA METODOS 
                        //! metodo= { (procedure)(variable)"("((variable)(,)*)* ")"(start)(proposicion)*(finish)(;)}
                        else if (actual == 505)//todo esperamos la llamada a un metodo
                        {
                            // metodo = {(procedure 505)}
                            estado = 8;

                        }
                        else
                        {
                            //!MODIFICAR ESTA ESPERANDO A QUE SI GUE DE DECLARACION DE VARIABLES
                            addErrorSintac("Syntax error ", 1033, actual);
                            estado = 999;
                        }
                        break;

                    case 8:
                        if (actual == 110)
                        {
                            // metodo = {(prodcedure 505) (variable)}
                            estado = 9;

                        }
                        else
                        {
                            addErrorSintac("Syntax error", 1034, actual);
                            estado = 999;
                        }

                        break;
                    case 9:
                        if (actual == 150)
                        {
                            // metodo= {(procedure 505) (variable 110) "(")}

                            estado = 10;

                        }
                        else
                        {
                            addErrorSintac("Syntax error", 1035, actual);
                            estado = 999;
                        }
                        break;
                    case 10:
                        if (actual == 110)
                        {
                            // metodo= {(procedure 505) (variable 110) "(") (variable)}
                            estado = 11;

                        }
                        else if (actual == 151)
                        {
                            estado = 11;
                        }

                        else
                        {
                            addErrorSintac("Syntax error", 1036, actual);
                            estado = 999;
                        }
                        break;
                    case 11:
                        if (actual == 160)
                        {
                            // metodo= {(procedure 505) (variable 110) "(") (variable) (, 160) (variable) }
                            estado = 10;

                        }
                        else if (actual == 506)
                        {
                            ciclos_open++;
                            estado = 14;
                        }


                        else
                        {
                            if (actual != 151)
                            {
                                addErrorSintac("Syntax error", 1037, actual);
                            }
                            else
                            {
                                estado = 12;
                                i--;

                            }

                            //addErrorSintac("error",1000,actual);
                        }
                        break;
                    case 12:
                        if (actual == 151)
                        {
                            // metodo= {(procedure 505) (variable 110) "(") (variable) (, 160) (variable) ")"}

                            estado = 13;
                        }
                        else
                        {
                            addErrorSintac("Syntax error", 1038, actual);
                        }
                        break;
                    case 13:
                        if (actual == 506)
                        {
                            ciclos_open++;
                            // metodo= {(procedure 505) (variable 110) "(") (variable) (, 160) (variable) ")"  (start)}
                            estado = 14;

                        }
                        else
                        {
                            addErrorSintac("Syntax error", 1039, actual);
                            estado = 999;
                        }
                        break;
                    case 14: //todo AQUI INICA EL FUNCIONAMIENTO DE UN METODO AQUI VAN LOS CASOS DE INCIO DE UN METODO
                             //preposicon= { (igualacion)|(cond if)|ciclo while)|(read line)| write line)|(inc-dec)|(llamada metodo) }
                             //? igualacion = (variable) (:|:=) (varaible) o numero o LETRA (;)
                             //? igualacion = (variable 110)

                        if (actual == 110)//todo llego una igualacion
                        {
                            estado = 15;
                        }
                        else if (actual == 521) //todo termina el metodo
                        {
                            estado = 39;
                        }
                        else if (actual == 507)//todo llego un if (istrue)
                        {
                            estado = 19;
                        }
                        else if (actual == 509)//todo llego un fn_istrue finalizo un if
                        {
                            ciclos_close++;
                            estado = 14;
                        }
                        else if (actual == 510)//todo llego un else 
                        {
                            estado = 22;
                        }
                        else if (actual == 512)//todo finalizo un else
                        {
                            ciclos_close++;
                            estado = 14;
                        }
                        else if (actual == 513)//todo llego la llamada a un metodo
                        {
                            estado = 24;

                        }
                        else if (actual == 514)//todo se llamo a un ciclo while
                        {
                            estado = 29;
                        }
                        else if (actual == 516)//todo finaliza ciclo while during
                        {
                            estado = 14;
                        }
                        else if (actual == 517)//todo llego write input
                        {
                            estado = 31;
                        }
                        else if (actual == 518)//todo llego el read  line
                        {
                            estado = 33;
                        }
                        else if (actual == 519)//todo llgo in Increases
                        {
                            estado = 35;

                        }
                        else if (actual == 520)//todo llego un decremento
                        {
                            estado = 37;

                        }


                        else //todo Error compartido de sintaxis 
                        {
                            addErrorSintac("Syntax error", 1000, actual);

                        }
                        //si detras no hay un ; error

                        break;
                    //!IGUALACION
                    case 15:
                        if (actual == 162 || actual == 141)
                        {
                            //? igualacion = (variable 110) (: 162 || := 141) 
                            estado = 16;
                        }
                        else
                        {
                            addErrorSintac("Syntax error", 1001, actual);
                        }

                        break;
                    //!Posibles estados 
                    //varaible 16
                    // cadenas 17
                    // numero decimal 18
                    // numero exponecial 19

                    //! ESTADO QUE VALIDA QUE TERMINO BIEN = 20


                    case 16://todo Que va llegar despues de la igualacion
                            //? igualacion = (variable) (:|:=) (varaible) o numero o LETRA
                            //? variable = 110   cadena = 101  numero = 102
                        if (actual == 110 || actual == 101 || actual == 102 || actual == 103 || actual == 104 || actual == 105)
                        {
                            estado = 17;

                        }
                        else if (actual == 150)//todo llego el parentesis
                        {
                            //parentesis_abre ++;
                            //estado =16;
                            estado = 16;
                            lista_parentesis_abre.Add(actual);

                        }
                        else if (actual == 151)//todo llego el parentesis que cierra
                        {

                            //parentisis_cierra ++;
                            //estado = 16;
                            estado = 17;
                            lista_parentesis_cierra.Add(actual);

                        }
                        else
                        {
                            addErrorSintac("Syntax error", 1002, actual);
                            estado = 999;

                        }


                        break;

                    case 17:
                        if (actual == 161)
                        {
                            //todo comprovamos que sea igual la cantidad de parentesis que se abren y se cierran
                            int abre = lista_parentesis_abre.Count;
                            int cierra = lista_parentesis_cierra.Count;


                            if (abre == cierra || abre == 0 && cierra == 0)
                            {
                                estado = 14; //todo vuelve a un estado de inicio
                            }
                            else
                            {
                                addErrorSintac("Syntax error", 1003, actual);
                            }
                        }

                        else if (actual == 521)
                        {
                            ciclos_close++;
                            estado = 39;
                        }

                        else if (actual == 120 || actual == 121 || actual == 122 || actual == 123)
                        {
                            estado = 16; //?varible mas un operador aritmetico + - / *
                        }
                        else if (actual == 150)//todo paraentesis que abre
                        {
                            estado = 17;
                            lista_parentesis_abre.Add(actual);

                        }
                        else if (actual == 151)
                        {
                            estado = 17;
                            lista_parentesis_cierra.Add(actual);
                        }

                        else
                        {
                            addErrorSintac("Syntax error", 1003, actual);
                            estado = 999;
                        }

                        break;

                    //! inicia el condicional if (istrue)
                    case 18://! Inicio el condicional if ( istrue)
                            //condicional if = (istrue) "(" (expresion) ")" (preposicion)* ({) [(else)(preposicion) (}) (finish) ] }
                        if (actual == 150)
                        {
                            estado = 19;
                            lista_parentesis_abre.Add(actual);
                        }

                        else
                        {
                            addErrorSintac("Syntax error ", 1005, actual);
                            estado = 9999;
                        }
                        break;


                    case 19:
                        //?variable o numero
                        if (actual == 110 || actual == 101 || actual == 102 || actual == 103 || actual == 104 || actual == 105)
                        {
                            estado = 20;

                        }
                        else if (actual == 150)//todo llego el parentesis
                        {
                            estado = 19;
                            lista_parentesis_abre.Add(actual);
                        }
                        else if (actual == 151)//?se cierra parentesis
                        {
                            estado = 20;
                            lista_parentesis_cierra.Add(actual);
                        }
                        else if (actual == 130 || actual == 131 || actual == 132 || actual == 133 || actual == 134 || actual == 135)
                        {
                            lista_condicional.Add(actual);
                            estado = 19;

                        }
                        else
                        {
                            addErrorSintac("Syntax error", 1006, actual);
                        }

                        break;
                    case 20:
                        if (actual == 508) //todo da inicio lo que va dentro del if = st_istrue
                        {
                            ciclos_open++;
                            //todo comprovamos que sea igual la cantidad de parentesis que se abren y se cierran
                            int abre = lista_parentesis_abre.Count;
                            int cierra = lista_parentesis_cierra.Count;



                            if (abre == cierra)
                            {
                                estado = 14; //todo vuelve a un estado de inicio
                            }
                            else
                            {
                                addErrorSintac("Syntax error", 1007, actual);
                            }
                        }
                        else if (actual == 521)
                        {
                            ciclos_close++;
                            estado = 39;
                        }
                        else if (actual == 509)//todo fn_istrue
                        {
                            ciclos_close++;
                            estado = 21;
                        }
                        else if (actual == 120 || actual == 121 || actual == 122 || actual == 123)
                        {
                            estado = 19; //?varible mas un operador aritmetico + - / *
                        }
                        else if (actual == 151)//?se cierra parentesis
                        {
                            estado = 19;
                            lista_parentesis_cierra.Add(actual);
                        }
                        else if (actual == 150)//todo llego el parentesis
                        {
                            estado = 19;
                            lista_parentesis_abre.Add(actual);
                        }
                        else if (actual == 130 || actual == 131 || actual == 132 || actual == 133 || actual == 134 || actual == 135)
                        {
                            estado = 19;
                            lista_condicional.Add(actual);

                        }
                        else
                        {
                            addErrorSintac("Syntax error", 1008, actual);
                        }

                        break;

                    case 21:
                        //i--;
                        if (actual == 510)//todo llego un else
                        {
                            estado = 22;
                        }
                        else if (actual == 521)
                        {
                            ciclos_close++;
                            estado = 39;
                        }
                        else
                        {
                            estado = 14;
                            //lista_condicional.Clear();
                            //addErrorSintac("Error else2",100,actual);
                        }


                        break;
                    //!else 
                    case 22:

                        if (actual == 511)//todo st_else
                        {
                            ciclos_open++;
                            estado = 23;

                        }
                        else
                        {
                            addErrorSintac("Syntax error", 1009, actual);
                        }

                        break;

                    case 23:
                        if (actual == 512)//todo fn_else
                        {
                            ciclos_close++;

                            estado = 14;
                        }

                        else if (actual == 521)
                        {
                            ciclos_close++;
                            estado = 39;
                        }

                        else
                        {
                            i--;
                            estado = 14;
                            // addErrorSintac("Error en else",100,actual);
                        }

                        break;
                    //!llamada al metodo
                    case 24:
                        if (actual == 110)
                        {
                            estado = 25;//llego el nombre del metodo
                        }
                        else
                        {
                            addErrorSintac("Sintax error", 1010, actual);
                        }
                        break;
                    case 25:
                        if (actual == 150)
                        {
                            estado = 26;  //function nombre ( sa)
                        }
                        else
                        {
                            addErrorSintac("Syntax error", 1011, actual);
                        }
                        break;
                    case 26:
                        if (actual == 110)
                        {
                            estado = 27;
                        }
                        else
                        {
                            addErrorSintac("Syntax error", 1012, actual);
                        }
                        break;
                    case 27:
                        if (actual == 160)
                        {
                            estado = 26;
                        }
                        else if (actual == 151)
                        {
                            estado = 28;
                        }
                        else
                        {
                            addErrorSintac("Syntax error", 1013, actual);
                        }
                        break;
                    case 28:
                        if (actual == 161)
                        {
                            estado = 14;
                        }
                        else if (actual == 521)
                        {
                            ciclos_close++;
                            estado = 39;
                        }
                        else
                        {
                            addErrorSintac("Syntax error", 1014, actual);
                        }
                        break;
                    //!CICLO WHILE
                    //TODO Ciclo while = { (during) (expresion) st_durin (preposicion) (fn_during) (;) }
                    case 29:
                        //?variable o numero
                        if (actual == 110 || actual == 101 || actual == 102 || actual == 103 || actual == 104 || actual == 105)
                        {
                            estado = 30;

                        }
                        else if (actual == 150)//todo llego el parentesis
                        {
                            estado = 29;
                            lista_parentesis_abre.Add(actual);
                        }
                        else if (actual == 151)//?se cierra parentesis
                        {
                            estado = 30;
                            lista_parentesis_cierra.Add(actual);
                        }
                        else if (actual == 130 || actual == 131 || actual == 132 || actual == 133 || actual == 134 || actual == 135)
                        {
                            lista_condicional.Add(actual);
                            estado = 29;

                        }
                        else
                        {
                            addErrorSintac("Syntax error", 1015, actual);
                        }

                        break;
                    case 30:
                        if (actual == 515) //todo da inicio lo que va dentro del while = st_during
                        {
                            ciclos_open++;
                            //todo comprovamos que sea igual la cantidad de parentesis que se abren y se cierran
                            int abre = lista_parentesis_abre.Count;
                            int cierra = lista_parentesis_cierra.Count;
                            //int condicion = lista_condicional.Count;


                            if (abre == cierra)
                            {
                                estado = 14; //todo vuelve a un estado de inicio
                            }
                            else
                            {
                                addErrorSintac("Syntax error", 1017, actual);
                            }
                        }
                        else if (actual == 521)
                        {
                            ciclos_close++;
                            estado = 39;
                        }
                        else if (actual == 120 || actual == 121 || actual == 122 || actual == 123)
                        {
                            estado = 29; //?varible mas un operador aritmetico + - / *
                        }
                        else if (actual == 151)//?se cierra parentesis
                        {
                            estado = 30;
                            lista_parentesis_cierra.Add(actual);
                        }
                        else if (actual == 150)//todo llego el parentesis
                        {
                            estado = 29;
                            lista_parentesis_abre.Add(actual);
                        }
                        else if (actual == 130 || actual == 131 || actual == 132 || actual == 133 || actual == 134 || actual == 135)
                        {
                            estado = 29;
                            lista_condicional.Add(actual);

                        }
                        else
                        {
                            addErrorSintac("Syntax error", 1017, actual);
                        }
                        break;
                    //!inicioa write input
                    case 31:
                        if (actual == 110 || actual == 101)
                        {
                            estado = 32;
                        }
                        else
                        {
                            addErrorSintac("Syntax error", 1018, actual);
                        }

                        break;
                    case 32:
                        if (actual == 161)
                        {
                            estado = 14;
                        }
                        else if (actual == 521)
                        {
                            ciclos_close++;
                            estado = 39;
                        }
                        else
                        {
                            addErrorSintac("Syntax error", 1019, actual);
                        }
                        break;
                    case 33:
                        if (actual == 110 || actual == 101 || actual == 102 || actual == 103)
                        {
                            estado = 34;
                        }
                        else
                        {
                            addErrorSintac("Syntax error", 1020, actual);
                        }
                        break;
                    case 34:
                        if (actual == 161)
                        {
                            estado = 14;
                        }
                        else if (actual == 521)
                        {
                            estado = 39;
                        }
                        else
                        {
                            addErrorSintac("Syntax error", 1021, actual);
                        }
                        break;
                    //!Increases
                    case 35:
                        if (actual == 110)
                        {
                            estado = 36;
                        }
                        else
                        {
                            addErrorSintac("Syntax error", 1022, actual);
                        }
                        break;
                    case 36:
                        if (actual == 161)
                        {
                            estado = 14;
                        }
                        else if (actual == 521)
                        {
                            ciclos_close++;
                            estado = 39;
                        }
                        else
                        {
                            addErrorSintac("Syntax error", 1023, actual);

                        }
                        break;
                    //! decremento
                    case 37:
                        if (actual == 110)
                        {
                            estado = 38;
                        }
                        else
                        {
                            addErrorSintac("Syntax error", 1024, actual);
                        }
                        break;
                    case 38:
                        if (actual == 161)
                        {
                            estado = 14;
                        }
                        else if (actual == 521)
                        {
                            ciclos_close++;
                            estado = 41;
                        }
                        else
                        {
                            addErrorSintac("Syntax", 1025, actual);

                        }

                        break;
                    //!Igualacion de variables

                    case 39:
                        if (actual == 110 || actual == 101 || actual == 102 || actual == 103 || actual == 104 || actual == 105)
                        {
                            estado = 40;

                        }
                        break;
                    case 40:
                        if (actual == 161)
                        {
                            //todo comprovamos que sea igual la cantidad de parentesis que se abren y se cierran
                            //int abre = lista_parentesis_abre.Count;
                            //int cierra = lista_parentesis_cierra.Count;


                            //if(abre==cierra)
                            tipo = Convert.ToString(lista_lexemas[i - 4]);
                            lexema = Convert.ToString(lista_lexemas[i - 3]);
                            valor = Convert.ToString(lista_lexemas[i - 1]);
                            add_varaible(lexema, 110, valor, tipo);
                            estado = 2; //todo vuelve a un estado de inicio

                            //}

                        }
                        else
                        {
                            addErrorSintac("Se esperaba ;", 1003, actual);

                        }

                        break;
                    case 41:
                        if (actual == 522)//TODO TERMINO EL PROGRAMA CON EXITO
                        {
                            if(ciclos_close != ciclos_open) 
                            {
                                addErrorSintac("Finaliza bien los ciclos cual nose suerte", 1025, actual);
                            }

                            estado = 40;
                        }
                        else
                        {
                            i--;
                            estado = 7;
                        }

                        break;
                }





            }

        }

        //controla la cantidad de parentesis abiertos o cerrados
        public int parentesis_abre()
        {
            int total = lista_parentesis_abre.Count;
            return total;
        }
        public int parentisis_cierra()
        {
            int total = lista_parentesis_cierra.Count;
            return total;
        }


        public bool comprobador(String sente)
        {
            bool enco = false;
            for (int i = 0; i < tokens.Count; ++i)
            {

                if (sente.ToString() == tokens[i].ToString())
                {
                    enco = true;
                    estado_token = i;
                    return enco;
                }
                else { enco = false; }

            }
            return enco;
        }
        public int verificar_reservada(string lexema)
        {
            //!MOVER EL ORDEN DE LAS RESERVADAS AFECTA TODO LAS CONDICIONES
            string[] reservada = { "inicio","vare", "inter", "decim", "text" ,"procedure","st_procedure","istrue","st_istrue","fn_istrue","else","st_else","fn_else",
            "function","during","st_during","fn_during","write_input","read_output","increases","decrease","fn_procedure","endterm"};
            int encontrado = 0;
            for (int p = 0; p < reservada.Length; p++)
            {
                if (reservada[p].ToString() == lexema)
                {
                    encontrado = 500;
                    return encontrado + p;

                }
                else
                {
                    encontrado = 0;
                    //return encontrado;
                }

                //!H3spo 
            }
            return encontrado;

        }

        public void generarLista()
        {
            for (int i = 0; i < lista_tokens.Count; i++)
            {
                cls_Token actual = lista_tokens.ElementAt(i);
                retorno += "[Lexema: " + actual.get_lexema() + " \t IdToken: " + actual.get_idtoken() + " \t Linea: " + actual.get_linea() + "]" + Environment.NewLine;

                //lista_sintac.Add(actual.get_idtoken());
            }
        }
        public DataTable listado()
        {
            DataTable tabla = new DataTable();
            //DataRow renglones;

            tabla.Columns.Add("Lexema");
            tabla.Columns.Add("Token");
            tabla.Columns.Add("Linea");

            for (int i = 0; i < lista_tokens.Count; i++)
            {

                cls_Token actual = lista_tokens.ElementAt(i);
                tabla.Rows.Add(actual.get_lexema(), actual.get_idtoken(), actual.get_linea());
                //pila.Push(actual.get_idtoken());
                lista_sintac.Add(actual.get_idtoken());


            }

            return tabla;
        }
        //Guardamos todos los numeros de tokens en una pila
        public List<int> lista_pila()
        {
            List<int> lista = new List<int>();
            // int contador = pila.Count;
            for (int i = 0; i < lista_sintac.Count; i++)
            {
                //lista.Add(Convert.ToInt32( pila.Pop()));
                lista.Add(lista_sintac[i]);
            }
            return lista;
        }

        public List<string> listalexemas()
        {
            List<string> lista = new List<string>();
            // int contador = pila.Count;
            for (int i = 0; i < lista_lexemas.Count; i++)
            {
                //lista.Add(Convert.ToInt32( pila.Pop()));
                lista.Add(lista_lexemas[i]);
            }
            return lista;
        }


        public int elementos_lista()
        {
            List<int> lista = new List<int>();
            // int contador = pila.Count;
            for (int i = 0; i < lista_sintac.Count; i++)
            {
                //lista.Add(Convert.ToInt32( pila.Pop()));
                lista.Add(lista_sintac[i]);
            }
            int total = lista.Count;
            return total;

        }



        public DataTable listado_errores()
        {
            DataTable tabla = new DataTable();
            tabla.Columns.Add("Lexema");
            tabla.Columns.Add("Token");
            tabla.Columns.Add("Linea");
            tabla.Columns.Add("Columna");
            for (int i = 0; i < lista_errores.Count; i++)
            {
                cls_error actual = lista_errores.ElementAt(i);
                tabla.Rows.Add(actual.get_Lexema(), actual.get_IdToken(), actual.get_Linea(), actual.get_Columna());

            }
            return tabla;

        }
        public DataTable listado_erroresSintac()
        {
            DataTable tabla = new DataTable();
            tabla.Columns.Add("Tipo");
            tabla.Columns.Add("Num Error");
            tabla.Columns.Add("Token ");

            for (int i = 0; i < lista_errorSintac.Count; i++)
            {
                cls_sintacError actual = lista_errorSintac.ElementAt(i);
                tabla.Rows.Add(actual.get_mesagge(), actual.get_IdToken(), actual.get_codetoken());

            }
            return tabla;
        }


        //!lista para guardar solo las variables
        List<string> listaVARE = new List<string>();
       

        public DataTable listado_Variables()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Variable");
            table.Columns.Add("Id token");
            table.Columns.Add("Tipo");
            table.Columns.Add("Valor");
            for (int i = 0; i < lista_Varibles.Count; i++)
            {
                cls_variables actual = lista_Varibles.ElementAt(i);
                table.Rows.Add(actual.get_lexema(), actual.get_idtoken(), actual.get_tipo(), actual.get_valor());
                listaVARE.Add(actual.get_lexema());

            }
            return table;
        }



        public String getRetorno()
        {
            return this.retorno;
        }
        public List<cls_Token> getListaTokens()
        {
            return lista_tokens;
        }
        public List<cls_error> getlistaErrrores()
        {
            return lista_errores;
        }

        public List<cls_sintacError> getlistaErrorSintac()
        {
            return lista_errorSintac;
        }

        public List<cls_variables> getlistavariables()
        {
            return lista_Varibles;
        }

        //!INICIA EL ANALISIS SINTACTICO

        //metodo para el control de variables
        public void Errores_Variables()
        {


            //?vare|501|
            //?|inter|502|
            //?|decim|503|
            //?|text|504|
            int estado = 0;
            List<string> lista_ciclos_close = new List<string>();
            for (int i = 0; i < lista_sintac.Count; i++)
            {
                string lexema = lista_lexemas[i];
                int token = lista_sintac[i];
                //int estado=0;

                switch (estado)
                {
                    case 0:
                        if (token == 501)//vare
                        {
                            estado = 1;
                        }
                        break;
                    case 1:
                        if (token == 161)//:
                        {
                            estado = 2;
                        }
                        break;
                    case 2:
                        if (token == 502)//inter
                        {
                            estado = 3;
                        }
                        else if (token == 503)//decim
                        {
                            estado = 6;
                        }
                        else if (token == 504)//text
                        {
                            estado = 9;
                        }
                        break;
                    //? ////////////////////////////////////////////////////////////////////
                    case 3:
                        if (token == 110)// inter variable 
                        {
                            
                            estado = 4;

                        }
                        break;
                    case 4:
                        if (token == 141)// inter := 
                        {
                            estado = 5;

                        }
                        break;
                    case 5:
                        if (token == 102 || token == 103)//12  12.2
                        {
                            estado = 12;

                        }
                        else
                        {
                            addErrorSintac("Se espera un numero", 2001, token);
                            estado = 2;
                        }
                        break;
                    //? ////////////////////////////////////////////////////////////////////
                    case 6:
                        if (token == 110)// DECIM variable 
                        {
                            estado = 7;

                        }
                        break;
                    case 7:
                        if (token == 141)// DECIM := 
                        {
                            estado = 8;

                        }
                        break;
                    case 8:
                        if (token == 103)//DECIM  12.2
                        {
                            estado = 12;

                        }
                        else
                        {
                            //addErrorSintac("Se espera un numero decimal", 2002, token);
                            estado = 2;
                        }
                        break;
                    //? ////////////////////////////////////////////////////////////////////
                    case 9:
                        if (token == 110)// text variable 
                        {
                            estado = 10;

                        }
                        break;
                    case 10:
                        if (token == 141)// text := 
                        {
                            estado = 11;

                        }
                        break;
                    case 11:
                        if (token == 101)//text  "AS"
                        {
                            estado = 12;

                        }
                        else
                        {
                            addErrorSintac("Se espera una cadena", 2003, token);
                            estado = 2;
                        }
                        break;




                    case 12:

                        if (token == 161)
                        {


                            estado = 2;
                        }
                        break;
                }
            }
        }




        public List<string> listavariablesx()
        {
            List<string> lista = new List<string>();
            List<string> list_pila = new List<string>();

            // int contador = pila.Count;
            for (int i = 0; i < listaVARE.Count; i++)
            {
                //lista.Add(Convert.ToInt32( pila.Pop()));
                lista.Add(listaVARE[i]);
            }
            return lista;
        }
        //verifica q
       


        //todo estado 1 y 2 son de inicio
        //todo no meter ala lista cuando inciia un metodo los ()
        //metodo consiste en hacer un rebote para cada lexema que llega el rebote se hace de un estado a otro 
        //del 1 a 2 2 a 1 2 a2 1 a 1 se pasan el lexema lo gurda carga o rechaza
        public List<string> lista_posfijo()
        {
            //ARREGLOS DE PRIORIDADES 
           

            string[] bandera = new string[1000];
            //string[] operadores = new string [] {"<" ,">" , "<=", ">=" ,"<>" ,"=", "+", "-", "*", "/" };

            string[] tres = new string[] { "+", "-" };

            string[] cuatro = new string[] { "*", "/" };
            string[] cinco = new string[] { "^" };

            int estado = 0;
            List<string> lista = new List<string>();
            List<string> lista_ciclos_open = new List<string>();
            List<string> lista_ciclos_close = new List<string>();
          
            Stack pila = new Stack();

            int cont_ciclos = 0;

            for (int i = 0; i < lista_sintac.Count(); i++)
            {
                int token = lista_sintac[i];
                string lexema = lista_lexemas[i];
                switch (estado)
                {
                    case 0:
                        // llegada de procedure
                        if (token == 505)
                        {
                            lista.Add(lexema);
                            pila.Push(lexema);
                            
                            estado = 1;
                        }
                        break;
                    case 1:
                        if (token == 506)
                        {
                            lista.Add(lexema);
                            lista_ciclos_open.Add(lexema);
                            estado = 2;
                        }
                        break;

                    //Token de llegada 1
                    //istrue = 507 durin 514
                    case 2:
                        if (token == 507 || token == 514)
                        {
                            //agrega el contador a cada ciclo
                            cont_ciclos++;
                            string cadena = lexema + cont_ciclos.ToString();
                            

                            cadena = cadena.Replace(" ", string.Empty);
                            
                            bandera[cont_ciclos] = lexema; 
                            lista.Add(cadena); pila.Push(cadena);
                            estado = 3;
                        }
                        //decremento
                        if(token == 519) 
                        {
                            //lista.Add(lexema);
                            pila.Push(lexema);
                            estado = 3;
                        }
                        if(token == 101) 
                        {
                            lista.Add(lexema);
                            estado = 3;
                        }
                        //variable
                        if(token == 110 ) 
                        {
                            bool exits = listaVARE.Contains(lexema);


                            if (exits) 
                            {
                                lista.Add(lexema);
                                estado = 3;
                            }
                            else 
                            {
                                addErrorSintac("Variable no declarada", 2005, token);
                                lista.Add(lexema);
                                estado = 3;
                            }
                        }
                        //condicionales < <
                        else if(token == 130 || token == 133) 
                        {
                            pila.Push(lexema);
                            estado =  3;
                        }
                        //Operador + -
                        if (token == 120 || token == 121)
                        {
                            pila.Push(lexema);
                            estado = 3;
                        }

                        //finalizadores de ciclos
                        if(token ==509||token==516||token == 521) 
                        {

                           
                                lista.Add(lexema);
                            lista_ciclos_close.Add(lexema);
                            
                           
                            estado = 2;
                        }
                        //Finalizador supremo
                        if(token == 522) 
                        {
                            int a = lista_ciclos_open.Count();
                            int b = lista_ciclos_close.Count();
                            if(a < b || a > b) 
                            {
                                addErrorSintac("Cierra el ciclo cual nose mira tu", 2010, token);
                                lista.Add(lexema);
                            }
                            lista.Add(lexema);

                        }
                        break;
                    //tokem de llegada 2
                    
                    case 3:
                        //variable = 110
                        if (token == 102 || token == 103||token==101) 
                        {
                            lista.Add (lexema);
                            estado =3; 
                        }
                        if (token == 110)
                        {


                            bool exits = listaVARE.Contains(lexema);


                            if (exits)
                            {
                                lista.Add(lexema);
                                estado = 3;
                            }
                            else
                            {
                                addErrorSintac("Variable no declarada", 2005, token);
                                lista.Add(lexema);
                                estado = 3;
                            }








                        }

                        //condicional
                        if (token >= 130 && token <= 135)
                        {
                            pila.Push(lexema);
                            estado = 3;
                        }
                        //igualacion
                         if (token ==140|| token == 141)
                        {
                            pila.Push(lexema);
                            estado = 3;
                        }
                        //Operador + -
                        if (token == 120 || token == 121)
                        {
                            pila.Push(lexema);
                            estado = 3;
                        }
                        //finalizador de condiciones
                        if (token == 508 || token == 515 ) 
                        {
                            lista_ciclos_open.Add(lexema);
                            lista.Add(pila.Pop().ToString());
                            lista.Add(lexema);
                            estado = 2;

                        }
                        // ;
                         if ( token == 161)
                        {
                            string[] operadores = new string[] { "<", ">", "<=", ">=", "<>", "=", "+", "-", "*", "/",":=" };
                            for (int p = 0; p < pila.Count; p++)
                            {

                                for (int a = 0; a < operadores.Length; a++)
                                {
                                    if (pila.Contains(operadores[a])) 
                                    {
                                        lista.Add(pila.Pop().ToString());
                                    }
                                }
                                

                            }
                           
                            //lista.Add(lexema);
                            estado = 2;

                        }
                        break;
                       
                }


            }

            return lista;

        }






    }
}
//  eliminar espacios
//agregar el contador al incio y al final 