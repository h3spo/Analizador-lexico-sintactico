using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace editor_de_texto
{
    public partial class Form1 : Form
    {

        int[,] arreglo =
               {
               { 1,2,802,3,1,11,12,13,14,70,18,19,21,20,22,23,10,8,61,60,62,0,0,0,800,54,55,58,59,56,57 },
               { 1,1,1,50,1,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50,50 },
               { 51,2,51,3,5,51,51,51,51,51,51,51,51,51,51,51,51,51,51,51,51,51,51,51,51,51,51,51,51,51,51 },
               { 802,4,802,802,802,802,802,802,802,802,802,802,802,802,802,802,802,802,802,802,802,802,802,802,802,802,802,802,802,802,802 },
               { 52,4,52,52,5,52,52,52,52,52,52,52,52,52,52,52,52,52,52,52,52,52,52,52,52,52,52,52,52,52,52 },
               { 802,7,802,802,802,6,6,802,802,802,802,802,802,802,802,802,802,802,802,802,802,802,802,802,802,802,802,802,802,802,802 },
               { 802,7,802,802,802,802,802,802,802,802,802,802,802,802,802,802,802,802,802,802,802,802,802,802,802,802,802,802,802,802,802 },
               { 53,7,53,53,53,53,53,53,53,53,53,53,53,53,53,53,53,53,53,53,53,53,53,53,53,53,53,53,53,53,53 },
               { 9,9,9,9,9,9,9,9,9,9,9,9,9,9,9,9,9,9,9,9,9,801,9,9,9,9,9,9,9,9,9 },
               { 801,801,801,801,801,801,801,801,801,801,801,801,801,801,801,801,801,64,801,801,801,801,801,801,801,801,801,801,801,801,801 },
               { 10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,10,65,10,10,10,10,801,10,10,10,10,10,10,10,10,10 },
               { 66,66,66,66,66,71,66,66,66,66,66,66,73,66,66,66,66,66,66,66,66,66,66,66,66,66,66,66,66,66,66 },
               { 67,67,67,67,67,67,72,67,67,67,67,67,74,67,67,67,67,67,67,67,67,67,67,67,67,67,67,67,67,67,67 },
               { 68,68,68,68,68,68,68,68,68,68,68,68,76,68,68,68,68,68,68,68,68,68,68,68,68,68,68,68,68,68,68 },
               { 69,69,69,69,69,69,69,16,15,69,69,69,75,69,69,69,69,69,69,69,69,69,69,69,69,69,69,69,69,69,69 },
               { 15,15,15,15,15,15,15,15,15,15,15,15,15,15,15,15,15,15,15,15,15,77,15,15,15,15,15,15,15,15,15 },
               { 16,16,16,16,16,16,16,17,16,16,16,16,16,16,16,16,16,16,16,16,16,16,16,16,16,16,16,16,16,16,16 },
               { 16,16,16,16,16,16,16,16,77,16,16,16,16,16,16,16,16,16,16,16,16,16,16,16,16,16,16,16,16,16,16 },
               { 79,79,79,79,79,79,79,79,79,79,79,79,79,78,79,79,79,79,79,79,79,79,79,79,79,79,79,79,79,79,79 },
               { 81,81,81,81,81,81,81,81,81,81,81,81,80,81,81,81,81,81,81,81,81,81,81,81,81,81,81,81,81,81,81 },
               { 85,85,85,85,85,85,85,85,85,85,85,85,82,85,85,85,85,85,85,85,85,85,85,85,85,85,85,85,85,85,85 },
               { 84,84,84,84,84,84,84,84,84,84,84,84,83,84,84,84,84,84,84,84,84,84,84,84,84,84,84,84,84,84,84 },
               { 800,800,800,800,800,800,800,800,800,800,800,800,800,800,86,800,800,800,800,800,800,800,800,800,800,800,800,800,800,800,800 },
               { 800,800,800,800,800,800,800,800,800,800,800,800,800,800,800,87,800,800,800,800,800,800,800,800,800,800,800,800,800,800,800 }
            };
        public Form1()
        {
            InitializeComponent();
        }

        private void NuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rich_principal.Clear();
        }

        private void AbrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirDocumentoo();
        }


        private void CerrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rich_principal.Clear();
        }



        private void GuardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GuardarArchivo();
        }
        //Abri archivo
        private void AbrirDocumentoo()
        {
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.Title = "Seleccionar un archivo";
            if (openfile.ShowDialog() == DialogResult.OK)
            {
                rich_principal.Clear();
                using (StreamReader sr = new StreamReader(openfile.FileName))
                {
                    rich_principal.Text = sr.ReadToEnd();
                    sr.Close();
                }

            }
        }
        //Guardar Archivo
        public void GuardarArchivo()
        {
            SaveFileDialog savefile = new SaveFileDialog();
            savefile.Title = "Guardar archivo como...";
            if (savefile.ShowDialog() == DialogResult.OK)
            {
                StreamWriter txtx_output = new StreamWriter(savefile.FileName + ".txt");
                txtx_output.Write(rich_principal.Text);
                txtx_output.Close();
            }
        }

        private void SalirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();


        }
        private void CortarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rich_principal.Cut();
        }
        private void CopiarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rich_principal.Copy();
        }
        private void PegarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rich_principal.Paste();
        }
        private void SeleccionarTodoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rich_principal.SelectAll();
        }
        private void Btn_desahacer_Click(object sender, EventArgs e)
        {
            rich_principal.Undo();
        }
        private void Reacer_Click(object sender, EventArgs e)
        {
            rich_principal.Redo();
        }
        private void VerdanaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rich_principal.SelectionFont = new Font("Verdana", Convert.ToInt32(txt_tamañoletra.Text), FontStyle.Bold);

        }

        private void ArialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rich_principal.SelectionFont = new Font("Arial", Convert.ToInt32(txt_tamañoletra.Text), FontStyle.Bold);
        }

        private void MSOutlookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rich_principal.SelectionFont = new Font("MS Outlook", Convert.ToInt32(txt_tamañoletra.Text), FontStyle.Bold);
        }

        private void PerpetuaTitlingMTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rich_principal.SelectionFont = new Font("Perpetua Titling MT", Convert.ToInt32(txt_tamañoletra.Text), FontStyle.Bold);
        }

        private void AcercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Partida camacho jesus fabiel  \n  version 1 editor de texto");
        }

        private void LexicoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string texto = rich_principal.Text;
            cls_analizador obj_analizador = new cls_analizador();
            obj_analizador.analizar_Cadena(texto);
            //obj_analizador.generarLista();
            dataGridView1.DataSource = obj_analizador.listado();
            //richTextBox1.Text = obj_analizador.getRetorno();
            data_errores.DataSource = obj_analizador.listado_errores();
            

        }
        public void lexico(string cadena)
        {
            int estado = 0;
            int columna = 0;
            int longitud = rich_principal.Text.Length;
            int cont = 0;
            List<int> columnas = new List<int>();
            List<int> estados = new List<int>();
            do
            {
                char caracter = rich_principal.Text[cont];
                cont++;
                columna = col(caracter);
                columnas.Add(columna);
                estado = est(columna);
                if (estado <= 23)
                {
                    estados.Add(estado);
                }
                else
                {
                    estados.Add(1000);
                }

            } while (cont < rich_principal.Text.Length);

            //list_caracter.DataSource = columnas;
            //listBox1.DataSource = estados;


        }
        public int col(char caracter)
        {
            char[] caracteres = { 'a', '5', '_', '.', 'e', '+', '-', '*', '/', '%', '>', '<', '=', '!',
                '&', '|', '\"', '\'',',',';', ':', '\n', '\t', '\b', ' ', '(', ')', '[', ']', '{', '}'};
            int columna = 0;
            for (int i = 0; i < caracteres.Length; i++)
            {
                if (char.IsLetter(caracter) == true && caracter != 'e')
                {
                    columna = 0;
                    break;
                }
                else if (char.IsDigit(caracter) == true)
                {
                    columna = 1;
                    break;
                }
                else if (caracter == 'e')
                {
                    columna = 4;
                    break;
                }
                else
                {
                    if (caracter == caracteres[i])
                    {
                        columna = i;
                        break;
                    }
                }
            }



            return columna;
        }
        private int est(int columna)
        {

            int estado = arreglo[0, columna];
            return estado;
        }


        private void Btn_lexico_prueba_Click(object sender, EventArgs e)
        {
            lexico(rich_principal.Text);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void fuenteToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void deshacerToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }

}
