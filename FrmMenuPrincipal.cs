using System;
using System.Windows.Forms;

namespace ProyectoEstacionamientos
{
    public partial class FrmMenuPrincipal : Form
    {
        public FrmMenuPrincipal()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void CajónDeEstacionamientoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAltaCajon frmAlta = new FrmAltaCajon();
            frmAlta.Show();
        }

        private void LugaresParaEstacionarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            FrmConsultaCajonesEstacionamiento frmConsultaCajones = new FrmConsultaCajonesEstacionamiento();
            frmConsultaCajones.Show();
            Cursor = Cursors.Arrow; // change cursor to normal type

        }

        private void PensionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            FrmConsultaPensiones frmConsultaPensiones = new FrmConsultaPensiones();
            frmConsultaPensiones.Show();
            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void PensiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            FrmAltaPensiones frmAltaPensiones = new FrmAltaPensiones();
            frmAltaPensiones.Show();
            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void SalirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AcercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Programa realizado por: \nAlfonso Reyes Cortés\nAngulo Licerio Lester Armando",
                "Acerca de", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void EntradaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            FrmAltaVehiculo frmAltaVehiculo = new FrmAltaVehiculo();
            frmAltaVehiculo.Show();
            Cursor = Cursors.Arrow; // change cursor to normal type
        }

        private void AutosEstacionadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // change cursor to hourglass type
            FrmConsultaAutosEstacionados frmConsultaAutosEstacionados = new FrmConsultaAutosEstacionados();
            frmConsultaAutosEstacionados.Show();
            Cursor = Cursors.Arrow; // change cursor to normal type
        }
    }
}
