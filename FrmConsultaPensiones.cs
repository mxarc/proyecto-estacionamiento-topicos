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
    public partial class FrmConsultaPensiones : Form
    {
        public FrmConsultaPensiones()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void FrmConsultaPensiones_Load(object sender, EventArgs e)
        {
            CapaPersistencia persistencia = new CapaPersistencia();
            List<Pension> lista = persistencia.RegresaPensiones();
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
                foreach (Pension pension in lista)
                {
                    dataGridView1.Rows.Add(pension.GetMatriculaAuto(), pension.GetModelo(),
                        pension.GetFechaIngreso(), pension.GetFechaVencimiento(),
                        pension.GetCuota(), pension.GetNombrePropietario(),
                        pension.GetApellidoPropietario());
                }
            }
        }
    }
}
