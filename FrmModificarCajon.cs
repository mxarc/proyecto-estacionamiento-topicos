using ProyectoEstacionamientos.clases;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ProyectoEstacionamientos
{
    public partial class FrmModificarCajon : Form
    {
        public FrmModificarCajon()
        {
            InitializeComponent();
        }

        private void FrmModificarCajon_Load(object sender, EventArgs e)
        {
            CapaPersistencia persistencia = new CapaPersistencia();
            // obtener cajones DISPONIBLES
            List<clases.VehiculoEstacionado> lista = persistencia.RegresaCajones();
            if (lista == null)
            {
                MessageBox.Show("Error al conectar con la base de datos");
                foreach (SqlError err in CapaPersistencia.errores.Errors)
                {
                    MessageBox.Show(err.Message);
                }
            }
            else
            {
                if (lista.Count < 1)
                {
                    MessageBox.Show("No hay ningún cajón de estacionamiento disponible", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Close();
                    return;
                }
                foreach (clases.VehiculoEstacionado cajon in lista)
                {
                    comboBoxCajones.Items.Add(cajon.GetClave());
                    comboBoxCajones.SelectedIndex = 0;
                }
                textBoxDescripcion.Text = persistencia.RegresaDescripcionCajon(int.Parse(comboBoxCajones.Text));
            }
        }


        private void LimpiarValores()
        {
            comboBoxCajones.SelectedIndex = -1;
            textBoxDescripcion.Text = "";
        }

        private void ButtonLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarValores();
        }

        private void TextBoxDescripcion_KeyUp(object sender, KeyEventArgs e)
        {
            if (textBoxDescripcion.Text.Length > 32)
            {
                errorProvider1.SetError(textBoxDescripcion, "Max 8 caracteres");
                e.Handled = false;
                return;
            }
            else
            {
                errorProvider1.SetError(textBoxDescripcion, "");
            }
        }

        private void ComboBoxCajones_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxCajones.SelectedIndex >= 0)
            {
                CapaPersistencia persistencia = new CapaPersistencia();
                textBoxDescripcion.Text = persistencia.RegresaDescripcionCajon(int.Parse(comboBoxCajones.Text));
            }
        }

        private void ButtonGuardar_Click(object sender, EventArgs e)
        {
            string descripcion = textBoxDescripcion.Text.Trim();
            if (comboBoxCajones.Text == "")
            {
                MessageBox.Show("Clave cajón no puede ir vacio", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int cajonID = int.Parse(comboBoxCajones.Text);
            // realizar validaciones
            if (descripcion.Length > 32)
            {
                MessageBox.Show("Descripción debe tener una longitud igual o menor a 32 caracteres", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (Validadores.ValidaNoVacio(descripcion))
            {
                MessageBox.Show("Descripción no puede ir vacio", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Cursor = Cursors.WaitCursor;
            // continuar a modificar
            CapaPersistencia capaPersistencia = new CapaPersistencia();
            capaPersistencia.ModificarDescripcionCajon(cajonID, descripcion);
            Cursor = Cursors.Arrow;
            MessageBox.Show("Cajón modificado con éxito", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LimpiarValores();
        }

        private void ButtonSalir_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
