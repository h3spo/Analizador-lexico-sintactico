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
           //cls_main obj_analizador = new cls_main();
            obj_analizador.analizar_Cadena(texto);
            //obj_analizador.generarLista();
            data_tokens.DataSource = obj_analizador.listado();
            //richTextBox1.Text = obj_analizador.getRetorno();
            data_errores.DataSource = obj_analizador.listado_errores();
            listBox1.DataSource = obj_analizador.lista_pila();
            listBox2.DataSource = obj_analizador.listalexemas();
            
            obj_analizador.analisis_sintactico();
            obj_analizador.Errores_Variables();

            list_posfijo.DataSource = obj_analizador.lista_posfijo();
            // obj_analizador.variables_duplicadas();
            dataGridView1.DataSource = obj_analizador.listado_erroresSintac();
            data_variables.DataSource= obj_analizador.listado_Variables();
            //dataGridView1.DataSource= obj_analizador.listado_erroresSintac();
            label3.Text=Convert.ToString( "Numero de caracteres--" +  obj_analizador.elementos_lista());
            label4.Text=Convert.ToString("Parenthesis abiertos---"+obj_analizador.parentesis_abre());
            label5.Text=Convert.ToString("Parenthesis cerrados---"+ obj_analizador.parentisis_cierra());
            

           listBox3.DataSource= obj_analizador.listavariablesx();
           
          
            

        }
        
       
        


        private void Btn_lexico_prueba_Click(object sender, EventArgs e)
        {
           // lexico(rich_principal.Text);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //ajustar_dataToken();

        }
        private void ajustar_dataToken()  
        {
            var height = data_tokens.ColumnHeadersHeight;
            foreach (DataGridViewRow dr in data_tokens.Rows)
            {
                height += dr.Height;
            }
            data_tokens.Height = height;
        }
        private void fuenteToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void deshacerToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void data_tokens_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void sinteticoToolStripMenuItem_Click(object sender, EventArgs e)
        {


          
            cls_analizador obj_analizador = new cls_analizador();
            obj_analizador.Errores_Variables();
            dataGridView1.DataSource = obj_analizador.listado_erroresSintac();
          
            

            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        int a;

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {


           
            

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void data_errores_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void rich_principal_TextChanged(object sender, EventArgs e)
        {

        }
    }

}
