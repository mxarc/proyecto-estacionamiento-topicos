using ProyectoEstacionamientos.clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoEstacionamientos
{
    public partial class FrmAltaCajon : Form
    {
        readonly ErrorProvider errorP = new ErrorProvider();
        public FrmAltaCajon()
        {
            InitializeComponent();
        }

        private void FrmAltaCajon_Load(object sender, EventArgs e)
        {
        }

        private void ButtonSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ButtonGuardar_Click(object sender, EventArgs e)
        {
            string clave = textBoxClave.Text.Trim();
            string descripcion = textBoxDescripcion.Text.Trim();
            bool checkEstatus = checkBoxEstatus.Checked;

            // realizar validaciones
            if (Validadores.ValidaNoVacio(clave) || Validadores.ValidaNoVacio(descripcion))
            {
                MessageBox.Show("Debes llenar todos los campos de entrada", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!Validadores.ValidaNumero(clave))
            {
                MessageBox.Show("Clave debe ser un valor númerico", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (clave.Length > 3)
            {
                MessageBox.Show("Clave debe tener una longitud igual o menor a 3 caracteres", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (descripcion.Length > 32)
            {
                MessageBox.Show("Descripción debe tener una longitud igual o menor a 32 caracteres", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Cursor = Cursors.WaitCursor;
            // pasaron validaciones, hora de crear objeto cajón estacionamiento
            CajonEstacionamiento lugarEstacionamiento = new CajonEstacionamiento(int.Parse(clave), descripcion, checkEstatus);
            // guardar lugar estacionamiento en la base de datos
            CapaPersistencia capaPersistencia = new CapaPersistencia();
            // pero checar que la clave sea unica
            if (capaPersistencia.ClaveCajonExiste(clave))
            {
                Cursor = Cursors.Arrow; // change cursor to normal type
                MessageBox.Show("Esa clave de cajón ya existe en la BD", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            capaPersistencia.AgregarCajonEstacionamiento(lugarEstacionamiento);
            Cursor = Cursors.Arrow; // change cursor to normal type
            MessageBox.Show("Lugar agregado con éxito", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LimpiarValores();
        }

        private void LimpiarValores()
        {
            textBoxClave.Text = "";
            textBoxDescripcion.Text = "";
            checkBoxEstatus.Checked = false;
            errorP.SetError(textBoxDescripcion, "");
            errorP.SetError(textBoxClave, "");
        }
        private void ButtonLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarValores();
        }

        private void TextBoxDescripcion_KeyUp(object sender, KeyEventArgs e)
        {
            if (textBoxDescripcion.Text.Length > 32)
            {
                errorP.SetError(textBoxDescripcion, "Max 32 caracteres");
                e.Handled = false;
                return;
            }
            else
            {
                errorP.SetError(textBoxDescripcion, "");
            }
        }

        private void TextBoxClave_KeyUp(object sender, KeyEventArgs e)
        {
            if (textBoxClave.Text.Length > 3)
            {
                errorP.SetError(textBoxClave, "Max 3 caracteres");
                e.Handled = false;
                return;
            }
            else
            {
                errorP.SetError(textBoxClave, "");
            }
        }
    }
}
