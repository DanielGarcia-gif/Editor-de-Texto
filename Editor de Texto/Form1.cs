using System.IO;

namespace Editor_de_Texto
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        bool archivoGuardado = false;
        string filePath;

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string textoFile = "";

            if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
            {
                textoFile = File.ReadAllText(filePath);
            }

            if (archivoGuardado == false && rtbEditor.Text.CompareTo("") == 0)
            {
                rtbEditor.Clear();
                archivoGuardado = false;
            }
            else if(archivoGuardado == false && rtbEditor.Text.CompareTo("") != 0)
            {
                DialogResult res = MessageBox.Show("¿Deseas guardar los cambios en un nuevo archivo?", "Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (res == DialogResult.Yes)
                {
                    guardarToolStripMenuItem_Click(null, null);
                    rtbEditor.Clear();
                    archivoGuardado = false;
                }
                else
                {
                    rtbEditor.Clear();
                    archivoGuardado = false;
                }
            }
            else if(archivoGuardado == true && rtbEditor.Text.CompareTo(textoFile) != 0)
            {
                DialogResult res = MessageBox.Show("¿Deseas guardar los nuevos cambios antes de generar un nuevo archivo?", "Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (res == DialogResult.Yes)
                {
                    guardarToolStripMenuItem_Click(null, null);
                    rtbEditor.Clear();
                    archivoGuardado = false;
                }
                else
                {
                    rtbEditor.Clear();
                    archivoGuardado = false;
                }
            }
            else if (archivoGuardado == true && rtbEditor.Text.CompareTo(textoFile) == 0)
            {
                rtbEditor.Clear();
                archivoGuardado = false;
            }
        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult resultado;

            if (archivoGuardado == false)
            {
                resultado = saveFileDialogEditor.ShowDialog();

                if (resultado == DialogResult.OK)
                {
                    filePath = saveFileDialogEditor.FileName;
                    string texto = rtbEditor.Text;

                    try
                    {
                        File.WriteAllText(filePath, texto);
                        MessageBox.Show("¡Archivo guardado correctamente!");
                        archivoGuardado = true;
                        saveFileDialogEditor.FileName = "";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al guardar el archivo" + ex.Message);
                    }
                }
            }
            else
            {
                try
                {
                    string texto = rtbEditor.Text;
                    File.WriteAllText(filePath, texto);
                    MessageBox.Show("¡Archivo guardado correctamente!");
                    saveFileDialogEditor.FileName = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al guardar el archivo" + ex.Message);
                }
            }
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialogEditor.FileName = "";
            DialogResult resultado;
            resultado = openFileDialogEditor.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                filePath = openFileDialogEditor.FileName;

                try
                {
                    string texto = File.ReadAllText(filePath);
                    rtbEditor.Text = texto;
                    archivoGuardado = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al abrir el archivo" + ex.Message);
                }
            }

        }

        private void guardarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult resultado;
            resultado = saveFileDialogEditor.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                filePath = saveFileDialogEditor.FileName;
                string texto = rtbEditor.Text;

                try
                {
                    File.WriteAllText(filePath, texto);
                    MessageBox.Show("¡Archivo guardado correctamente!");
                    archivoGuardado = true;
                    saveFileDialogEditor.FileName = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al guardar el archivo" + ex.Message);
                }
            }
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string texto = rtbEditor.Text;
            string textoFile = "";

            if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
            {
                textoFile = File.ReadAllText(filePath);
            }

            if (string.IsNullOrEmpty(filePath) && string.IsNullOrEmpty(texto))
            {
                this.Close();
            }
            else if(texto.CompareTo(textoFile) != 0)
            {
                DialogResult res = MessageBox.Show("¿Deseas guardar los cambios?", "Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question); 
                
                if(res == DialogResult.Yes)
                {
                    guardarToolStripMenuItem_Click(null, null);
                }
                else
                {
                    this.Close();
                }
            }
            
            this.Close();  
        }
    }
}
