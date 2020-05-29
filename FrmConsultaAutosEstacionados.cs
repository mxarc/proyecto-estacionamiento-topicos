using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ProyectoEstacionamientos
{
    public partial class FrmConsultaAutosEstacionados : Form
    {
        public FrmConsultaAutosEstacionados()
        {
            InitializeComponent();
        }

        private void FrmConsultaAutosEstacionados_Load(object sender, EventArgs e)
        {
            CapaPersistencia persistencia = new CapaPersistencia();
            List<VehiculoEstacionado> lista = persistencia.RegresaVehiculosEstacionados();
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
                foreach (VehiculoEstacionado vehiculo in lista)
                {
                    dataGridView1.Rows.Add(vehiculo.CodigoEntrada, vehiculo.MatriculaAuto, vehiculo.HoraEntrada, vehiculo.CajonID);
                }
            }
        }
    }
}
