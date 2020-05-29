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
    public partial class FrmSalidaVehiculo : Form
    {
        public FrmSalidaVehiculo()
        {
            InitializeComponent();
        }

        private void ButtonSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LimpiarValores()
        {
            checkBoxBoletoPerdido.Checked = false;
            textBoxCodigoEntrada.Text = "";
        }

        private void ButtonLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarValores();
        }

        private void ButtonCalcularCosto_Click(object sender, EventArgs e)
        {
            CapaPersistencia capaPersistencia = new CapaPersistencia();
        }
    }
}
