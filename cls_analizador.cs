using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;

namespace editor_de_texto
{
    class cls_analizador
    {
        Stack pila = new Stack();
        List<int> lista_sintac = new List<int>();

        //!los numero de token , numero de error estan en archivo Diccionario.txt
        ArrayList tokens;
        ArrayList tipos;
       // int carga = 0;
        //se crea una lista para guardar los tokens la estructura y caracteristicas estan en
        //cls_token
        ArrayList lista_operaciones;

        static private List<cls_Token> lista_tokens;
        //static private List<cls_sintac> lista_sintac;

        public List<cls_lectura_lineas> ejecutar_lineas;
        public List<cls_gr_ejecutar> gr_ejecutar;
        private String retorno;
        public int estado_token;

        static private List<cls_error> lista_errores;
        static private List <cls_sintacError> lista_errorSintac;
        public cls_analizador()
        {
            lista_tokens = new List<cls_Token>();
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

        

       

        public void addError(String lexema, int idToken, int linea, int columna)
        {
            cls_error errtok = new cls_error(lexema, idToken, linea, columna);
            lista_errores.Add(errtok);
        }

        public void addErrorSintac(string mesagge,int idToken, int code_token)
        {
            cls_sintacError error = new cls_sintacError(mesagge,idToken,code_token);
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
                            add_Token(lexema, 160, fila, columna, i - lexema.Length);
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
                            add_Token(lexema, 150, fila, columna, i - lexema.Length);
                            lexema = "";
                           // lista_sintac.Add(150);
                        }
                        else if (c == ')')
                        {
                            //)=18
                            lexema += c;
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
                            add_Token(lexema, 5, fila, columna, i - lexema.Length);
                            lexema = "";
                        }
                        else if (c == '+')
                        {
                            lexema += c;
                            add_Token(lexema, 120, fila, columna, i);
                            lexema = "";
                        }
                        else if (c == '-')
                        {
                            lexema += c;
                            add_Token(lexema, 121, fila, columna, i);
                            lexema = "";
                        }
                        else if (c == '*')
                        {
                            lexema += c;
                            add_Token(lexema, 122, fila, columna, i);
                            lexema = "";
                        }
                        else if (c == '/')
                        {
                            lexema += c;
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
                            add_Token(lexema, 101, fila, columna, i - lexema.Length);
                            estado = 0;
                            lexema = "";
                        }


                        else if (c == ',')
                        {
                            lexema += c;

                            add_Token(lexema, 4, fila, columna, i - lexema.Length);
                            estado = 0;
                            lexema = "";
                        }

                        break;
                    case 8:
                        if (c == '\'')
                        {
                            lexema += c;
                            add_Token(lexema, 135, fila, columna, i - lexema.Length);
                            estado = 0;
                            lexema = "";
                        }
                        else if (c == ',')
                        {
                            lexema += c;

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

                            addError(lexema, 301, fila, columna);
                            estado = 0;
                            lexema = "";
                            fila ++;
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
                            add_Token(lexema, 131, fila, columna, i - lexema.Length);
                            estado = 0;
                            lexema = "";
                            //i--;
                            //fila++;
                        }
                        else
                        {
                            lexema += c;
                            add_Token(lexema, 134, fila, columna, i - lexema.Length);
                            estado = 0;
                            lexema = "";
                        }
                        break;
                    case 16:
                        if (c != '=')
                        {
                            //lexema += c;
                            add_Token(lexema, 132, fila, columna, i - lexema.Length);
                            estado = 0;
                            lexema = "";
                            i--;
                            // fila++;
                        }
                        else
                        {
                            lexema += c;
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
                            add_Token(lexema, 162, fila, columna, i - lexema.Length);
                            estado = 0;
                            lexema = "";
                            //fila++;
                        }
                        if (c == '=')
                        {
                            lexema += c;
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
       


        public void analisis_sintactico() 
        {
           
            int estado = 0;
         
            
            
            //?es la pila que ya contienen todos los tokens
           // int Stack = pila.Count;
            for (int i = 0; i < lista_sintac.Count; i++)
            {
                 
                 int actual = lista_sintac[i];
                //int actual = Stack;
                switch (estado)
                {
                    case 0:
                         
                        if (actual==500)
                        {
                            
                            estado = 1;   
                        }
                        else 
                        {
                            addErrorSintac("Se esperaba started" ,1001,actual);
                            estado =99;
                        }
                        break;
                    case 1:
                        if (actual == 110)
                        {
                            estado = 2;
                        }
                        else
                        {
                            addErrorSintac("Se esperaba un nombre", 1000, actual);
                            estado = 99;
                        }
                        break;
                    //? se da incio al control de variables 
                    //!varaible = {(vare) (:)  (inter|decim|text) (varaible) (;)}
                    case 2:
                        if(actual==501)
                        {
                            //500=(vare)
                            estado =3;
                        }
                        else
                        {
                            addErrorSintac("Se esperaba vare",1000,actual);
                            estado =999;
                        }
                    break;
                    case 3:
                        if(actual ==162)
                        {
                            // (vare) (:)
                            estado = 4;
                        }
                        else
                        {
                          addErrorSintac("Se esperaba :",1000, actual);
                          estado =999;
                        }
                    break;
                    case 4:
                        if(actual >=502 && actual <= 504)
                        {
                            // (vare) (:) (inter 502|decim 503|text 504)
                            estado=5;

                        }
                        else
                        {
                            addErrorSintac("Se esperaba un tipo",1000, actual);
                            estado =999;
                        }
                    break;
                    case 5:
                        if(actual ==110)
                        {
                            // (vare) (:) (inter 502|decim 503|text 504) (variable)
                            estado =6;
                        }
                        else 
                        {
                            addErrorSintac("Se esperaba un Nombre", 1000,actual);
                            estado =999;
                        }
                    break;
                    case 6:
                        if(actual ==161)
                        {
                            //;
                            estado =7;
                        }
                        else
                        {
                            addErrorSintac("Se esperaba ;",1000,actual);
                            estado = 999;
                        }
                    break;
                    //todo repetimos todo el proceso de nuevo por si llegan mas declariciones de variables
                    case 7:
                        if(actual == 501)
                        {
                            i--;
                            estado = 2;
                        }
                        //!COMIENZA EL CONTROL PARA METODOS 
                         //! metodo= { (procedure)(variable)"("((variable)(,)*)* ")"(start)(proposicion)*(finish)(;)}
                        else if(actual == 505)//todo esperamos la llamada a un metodo
                        {
                            // metodo = {(procedure 505)}
                            estado = 8;

                        }
                        else 
                        {
                            //!MODIFICAR ESTA ESPERANDO A QUE SI GUE DE DECLARACION DE VARIABLES
                            addErrorSintac("se esperaba la llamada a un metodo ",1000, actual);
                            estado = 999;
                        }
                    break;
                       
                    case 8:
                       if(actual == 110 )
                         {
                         // metodo = {(prodcedure 505) (variable)}
                           estado =9;
                            
                         }
                        else 
                        {
                            addErrorSintac("Se esperaba un nombre",1000,actual);
                            estado = 999;
                        }
                    
                    break;
                    case 9:
                      if(actual == 150)
                      {
                          // metodo= {(procedure 505) (variable 110) "(")}
                          
                          estado =10;

                      }
                      else 
                      {
                          addErrorSintac("Se esperaba ( ",1000,actual);
                          estado = 999;
                      }
                    break;
                    case 10:
                    if(actual ==110  )
                    {
                         // metodo= {(procedure 505) (variable 110) "(") (variable)}
                         estado =11;
                         
                    }
                    
                    else
                    {
                        addErrorSintac("se espera una variable",1000,actual);
                        estado=999;
                    }
                    break;
                    case 11:
                      if(actual==160)
                      {
                           // metodo= {(procedure 505) (variable 110) "(") (variable) (, 160) (variable) }
                           estado =10;
                             
                      }
                     

                      else 
                      {  if(actual != 151)
                         {
                             addErrorSintac("Error Sintax",1000,actual);
                         }
                         else
                         {
                             estado =12;
                             i--;

                         }
                         
                         //addErrorSintac("error",1000,actual);
                      }
                    break;
                    case 12:
                     if(actual==151)
                     {
                         // metodo= {(procedure 505) (variable 110) "(") (variable) (, 160) (variable) ")"}
                         
                         estado = 13;
                     }
                     else
                     {
                         addErrorSintac("Se esperaba )",1000,actual);
                     }
                    break;
                    case 13:
                       if(actual == 506)
                       {
                         // metodo= {(procedure 505) (variable 110) "(") (variable) (, 160) (variable) ")"  (start)}
                         estado =14;   

                       }
                       else
                       {
                           addErrorSintac ("Se esperaba start",1000,actual);
                           estado = 999;
                       }
                    break;
                    case 14: //todo AQUI INICA EL FUNCIONAMIENTO DE UN METODO AQUI VAN LOS CASOS DE INCIO DE UN METODO
                    //preposicon= { (igualacion)|(cond if)|ciclo while)|(read line)| write line)|(inc-dec)|(llamada metodo) }
                     //? igualacion = (variable) (:|:=) (varaible) o numero o LETRA (;)
                     //? igualacion = (variable 110)
                     
                     if(actual==110)//todo llego una igualacion
                     {
                         estado = 15;
                     }
                     else if(actual==507)//todo llego un if (istrue)
                     {
                         estado = 18;
                     }

                     else //todo Error compartido de sintaxis 
                     {
                         addErrorSintac("Error sintax Metodo",1000,actual);

                     }
                     //si detras no hay un ; error

                    break;
                    //!IGUALACION
                    case 15:
                       if(actual == 162 || actual == 141)
                       {
                            //? igualacion = (variable 110) (: 162 || := 141) 
                            estado =16;
                       }
                       else
                       {
                           addErrorSintac("Se esperaba una igualacion",1000,actual);
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
                       if(actual==110||actual==101||actual==102||actual==103||actual==104||actual ==105)
                       {
                           estado =17;
                           
                       }
                       else if(actual==150)//todo llego el parentesis
                       {
                           //parentesis_abre ++;
                           //estado =16;
                           estado=16;
                           lista_parentesis_abre.Add(actual);

                       }
                       else if(actual==151)//todo llego el parentesis que cierra
                       {
                          
                               //parentisis_cierra ++;
                               //estado = 16;
                               estado=17;
                               lista_parentesis_cierra.Add(actual);

                       }
                       //else if(actual==120 || actual==121||actual==122||actual==123)
                       //{
                         // estado = 20; //?varible mas un operador aritmetico + - / *
                       //}
                       //else if(actual ==161 )
                       //{
                         // estado =14;
                       //}
                      
                       
                       
                       else
                       {
                           addErrorSintac("Error sintax igualacion",1000,actual);
                           estado=999;

                       }
                       

                    break;

                    case 17: 
                      if(actual ==161 )
                      {
                          //todo comprovamos que sea igual la cantidad de parentesis que se abren y se cierran
                          int abre = lista_parentesis_abre.Count;
                          int cierra = lista_parentesis_cierra.Count;

                          
                          if(abre==cierra)
                          {
                              estado=14; //todo vuelve a un estado de inicio
                          }
                          else
                          {
                               addErrorSintac("ERROR PARENTESIS ",1000,actual);
                          }
                      }

                      else if(actual==120 || actual==121||actual==122||actual==123)
                      {
                          estado = 16; //?varible mas un operador aritmetico + - / *
                      }
                      else if(actual==150)//todo paraentesis que abre
                      {
                          estado=17;
                           lista_parentesis_abre.Add(actual);

                      }
                      else if (actual==151)
                      {
                          estado=17;
                          lista_parentesis_cierra.Add(actual);
                      }
                      
                      else 
                      {
                          addErrorSintac("Sintaxis error",1000,actual);
                          estado = 999;
                      }

                    break;
                    case 18://! Inicio el condicional if ( istrue)
                    //condicional if = (istrue) "(" (expresion) ")" (preposicion)* ({) [(else)(preposicion) (}) (finish) ] }
                     if(actual == 150)
                     {
                         estado =19;
                     }
                     else
                     {
                         addErrorSintac("Se esperaba ( ",1000,actual);
                         estado =  9999;
                     }
                    break;


                }
            }
            
        }
        
        
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
            string[] reservada = { "started","vare", "inter", "decim", "text" ,"procedure","start","istrue"};
            int encontrado = 0;
            for (int p  = 0; p < reservada.Length; p++)
            {
                if (reservada[p].ToString()==lexema)
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
                tabla.Rows.Add(actual.get_mesagge(),actual.get_IdToken(),actual.get_codetoken());

            }
            return tabla;
        

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
        
        //!INICIA EL ANALISIS SINTACTICO
        



    }
}
/* RESERVADAS
 * SI
 *  ENTONCES
 *  SINO
 *  FIN SI
 *  MIENTRAS
 *  FIN MIENTRAS    
 *  DECLARA
 *  ENTERO
 *  DOBLE
 *  CADENA
 */
 /*ERRORES
  * 300 NUMEROS ERROE EN NUMERO
  * 301 CARACTER CARACTER NO IDENTIFICADO
  * 302 ENTER EN LA CADENA
  * 303 VARIABLES MUY LARGAS
  */