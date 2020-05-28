using ProyectoEstacionamientos.clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoEstacionamientos
{
    public partial class FrmAltaPensiones : Form
    {
        public FrmAltaPensiones()
        {
            InitializeComponent();
        }

        private void FrmAltaPensiones_Load(object sender, EventArgs e)
        {
            CapaPersistencia persistencia = new CapaPersistencia();
            // obtener cajones DISPONIBLES
            List<CajonEstacionamiento> lista = persistencia.RegresaCajones(true);
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
               if (lista.Count < 1) {
                    MessageBox.Show("No hay ningún cajón de estacionamiento disponible", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Close();
                    return;
               }
                foreach (CajonEstacionamiento cajon in lista)
                {
                    comboBoxCajones.Items.Add(cajon.GetClave());
                    comboBoxCajones.SelectedIndex = 0;
                }
            }
            // fecha minima
            dateTimePickerFechaIngreso.Enabled = false;
            dateTimePickerFechaIngreso.Value = DateTime.Now;
        }

        private void ButtonVolver_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void LimpiarValores()
        {
            comboBoxCajones.SelectedIndex = 0;
            textBoxMatriculaAuto.Text = "";
            textBoxModeloAuto.Text = "";
            textBoxMatriculaAuto.Text = "";
            textBoxNombrePropietario.Text = "";
            textBoxApellidoPropietario.Text = "";
            labelCuotaPagar.Text = "$0.00";
            dateTimePickerFechaIngreso.Value = DateTime.Now;
            dateTimePickerFechaVencimiento.Value = DateTime.Now;
        }

        private void ButtonLimpiarDatos_Click(object sender, EventArgs e)
        {
            LimpiarValores();
        }
    }
}
