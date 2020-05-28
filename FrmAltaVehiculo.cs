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
    public partial class FrmAltaVehiculo : Form
    {
        readonly ErrorProvider errorP = new ErrorProvider();

        public FrmAltaVehiculo()
        {
            InitializeComponent();
        }

        private void ButtonSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LimpiarValores()
        {
            textBoxCodigoEntrada.Text = "";
            textBoxMatricula.Text = "";
            comboBoxCajones.SelectedIndex = 0;
        }

        private void ButtonLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarValores();
        }

        private void FrmAltaVehiculo_Load(object sender, EventArgs e)
        {
            checarDisponibilidadCajones();
            textBoxCodigoEntrada.Text = Utilidades.RandomString(8, false);
        }

        private void checarDisponibilidadCajones()
        {
            CapaPersistencia persistencia = new CapaPersistencia();
            List<CajonEstacionamiento> lista = persistencia.RegresaCajones(true);
            if (lista == null)
            {
                MessageBox.Show("Error al conectar con la base de datos");
                foreach (SqlError err in CapaPersistencia.errores.Errors)
                {
                    MessageBox.Show(err.Message);
                    Close();
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
                foreach (CajonEstacionamiento cajon in lista)
                {
                    comboBoxCajones.Items.Add(cajon.GetClave());
                    comboBoxCajones.SelectedIndex = 0;
                }
            }
            
        }

        private void TextBoxMatricula_KeyUp(object sender, KeyEventArgs e)
        {
            if (textBoxMatricula.Text.Length > 8)
            {
                errorP.SetError(textBoxMatricula, "Max 8 caracteres");
                e.Handled = false;
                return;
            }
            else
            {
                errorP.SetError(textBoxMatricula, "");
            }
        }

        private void ButtonGuardar_Click(object sender, EventArgs e)
        {
            string matricula = textBoxMatricula.Text.Trim();
            string codigo = textBoxCodigoEntrada.Text.Trim();
            string cajon = comboBoxCajones.Text;
            // realizar validaciones
            if (Validadores.ValidaNoVacio(matricula) || Validadores.ValidaNoVacio(codigo))
            {
                MessageBox.Show("Debes llenar todos los campos de entrada", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (matricula.Length > 8)
            {
                MessageBox.Show("Matrícula debe tener una longitud igual o menor a 8 caracteres", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (codigo.Length > 8)
            {
                MessageBox.Show("Código debe tener una longitud igual o menor a 8 caracteres", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // validar que cajon sea num por si acaso
            if (!Validadores.ValidaNumero(cajon))
            {
                MessageBox.Show("Cajón combo debe ser un número", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Cursor = Cursors.WaitCursor;
            CapaPersistencia capaPersistencia = new CapaPersistencia();
            // pero checar que el codigo sea unica
            if (capaPersistencia.CodigoEntradaExiste(codigo))
            {
                Cursor = Cursors.Arrow; // change cursor to normal type
                MessageBox.Show("Este código de entrada ya existe en la BD", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // todo bien, guardar entrada vehiculo:
            Console.WriteLine(codigo);
            Console.WriteLine(matricula);
            Console.WriteLine(cajon);
            capaPersistencia.AgregarEntradaVehiculo(codigo, matricula, int.Parse(cajon));
            Cursor = Cursors.Arrow;
            MessageBox.Show("Entrada añadida con éxito", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LimpiarValores();
            checarDisponibilidadCajones();
        }

        private void textBoxCodigoEntrada_KeyUp(object sender, KeyEventArgs e)
        {
            if (textBoxCodigoEntrada.Text.Length > 8)
            {
                errorP.SetError(textBoxCodigoEntrada, "Max 8 caracteres");
                e.Handled = false;
                return;
            }
            else
            {
                errorP.SetError(textBoxCodigoEntrada, "");
            }
        }
    }
}
