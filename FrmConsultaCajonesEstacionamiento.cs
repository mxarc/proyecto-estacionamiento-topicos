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
    public partial class FrmConsultaCajonesEstacionamiento : Form
    {
        public FrmConsultaCajonesEstacionamiento()
        {
            InitializeComponent();
        }

        private void FrmConsultaCajonesEstacionamiento_Load(object sender, EventArgs e)
        {
            CapaPersistencia persistencia = new CapaPersistencia();
            List<CajonEstacionamiento> lista = persistencia.RegresaCajones();
            if (lista == null)
            {
                MessageBox.Show("Error al conectar con BD");
                foreach (SqlError err in CapaPersistencia.errores.Errors)
                {
                    MessageBox.Show(err.Message);
                }
            }
            else
            {
                foreach (CajonEstacionamiento lugar in lista)
                {
                    string estatus = lugar.GetOcupado() ? "Ocupado" : "Disponible";
                    this.dataGridView1.Rows.Add(lugar.GetClave(), estatus, lugar.GetDescripcion());
                }
            }
        }
    }
}