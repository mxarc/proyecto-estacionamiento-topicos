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
            List<clases.VehiculoEstacionado> lista = persistencia.RegresaCajones(true);
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
                foreach (clases.VehiculoEstacionado cajon in lista)
                {
                    comboBoxCajones.Items.Add(cajon.GetClave());
                    comboBoxCajones.SelectedIndex = 0;
                }
            }
            // fecha minima
            dateTimePickerFechaIngreso.Enabled = false;
            dateTimePickerFechaIngreso.Value = DateTime.Now;
            dateTimePickerFechaVencimiento.Value = DateTime.Now.AddDays(1);
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
            errorProvider1.SetError(textBoxMatriculaAuto, "");
            errorProvider1.SetError(textBoxModeloAuto, "");
            errorProvider1.SetError(textBoxApellidoPropietario, "");
            errorProvider1.SetError(textBoxNombrePropietario, "");
        }

        private void ButtonLimpiarDatos_Click(object sender, EventArgs e)
        {
            LimpiarValores();
        }

        private void TextBoxMatriculaAuto_KeyUp(object sender, KeyEventArgs e)
        {
            if (textBoxMatriculaAuto.Text.Length > 8)
            {
                errorProvider1.SetError(textBoxMatriculaAuto, "Max 8 caracteres");
                e.Handled = false;
                return;
            }
            else
            {
                errorProvider1.SetError(textBoxMatriculaAuto, "");
            }
        }

        private void TextBoxModeloAuto_KeyUp(object sender, KeyEventArgs e)
        {
            if (textBoxModeloAuto.Text.Length > 32)
            {
                errorProvider1.SetError(textBoxModeloAuto, "Max 32 caracteres");
                e.Handled = false;
                return;
            }
            else
            {
                errorProvider1.SetError(textBoxModeloAuto, "");
            }
        }

        private void TextBoxNombrePropietario_KeyUp(object sender, KeyEventArgs e)
        {
            if (textBoxNombrePropietario.Text.Length > 32)
            {
                errorProvider1.SetError(textBoxNombrePropietario, "Max 32 caracteres");
                e.Handled = false;
                return;
            }
            else
            {
                errorProvider1.SetError(textBoxNombrePropietario, "");
            }
        }

        private void TextBoxApellidoPropietario_KeyUp(object sender, KeyEventArgs e)
        {
            if (textBoxApellidoPropietario.Text.Length > 32)
            {
                errorProvider1.SetError(textBoxApellidoPropietario, "Max 32 caracteres");
                e.Handled = false;
                return;
            }
            else
            {
                errorProvider1.SetError(textBoxApellidoPropietario, "");
            }
        }


        private int CalcularCuotaPago()
        {
            DateTime fechaIngreso = dateTimePickerFechaIngreso.Value;
            fechaIngreso.AddDays(1);
            DateTime fechaSalida = dateTimePickerFechaVencimiento.Value;
            int diff = (fechaSalida - fechaIngreso).Days;
            if (diff < 15)
            {
                return 60 * diff;
            }
            return 40 * diff;
        }

        private void ButtonGuardar_Click(object sender, EventArgs e)
        {
            string matriculaAuto = textBoxMatriculaAuto.Text.Trim();
            string modeloAuto = textBoxModeloAuto.Text.Trim();
            string nombrePropietario = textBoxNombrePropietario.Text.Trim();
            string apellidoPropietario = textBoxApellidoPropietario.Text.Trim();
            if (comboBoxCajones.Text == "")
            {
                MessageBox.Show("Cajón no puede ir vacio", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int cajonID = int.Parse(comboBoxCajones.Text);
            // Realizar validaciones
            if (Validadores.ValidaNoVacio(matriculaAuto) ||
                Validadores.ValidaNoVacio(modeloAuto) ||
                Validadores.ValidaNoVacio(nombrePropietario) ||
                Validadores.ValidaNoVacio(apellidoPropietario))
            {
                MessageBox.Show("Debes llenar todos los campos de entrada", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (matriculaAuto.Length > 8)
            {
                MessageBox.Show("Matricula debe tener una longitud igual o menor a 8 caracteres", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (modeloAuto.Length > 32)
            {
                MessageBox.Show("Modelo debe tener una longitud igual o menor a 32 caracteres", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (nombrePropietario.Length > 32)
            {
                MessageBox.Show("Nombre propietario debe tener una longitud igual o menor a 32 caracteres", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (apellidoPropietario.Length > 32)
            {
                MessageBox.Show("Apellido propietario debe tener una longitud igual o menor a 32 caracteres", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Cursor = Cursors.WaitCursor;
            // checar que matricula no este estacionada
            CapaPersistencia capaPersistencia = new CapaPersistencia();
            if (capaPersistencia.MatriculaYaEstaEstacionada(matriculaAuto))
            {
                MessageBox.Show("La matrícula que deseas registrar se encuentra estacionada", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // checar que no este pensionado antes
            if (capaPersistencia.MatriculaYaEstaPensionada(matriculaAuto))
            {
                MessageBox.Show("La matrícula que deseas registrar se encuentra pensionada", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // calcular cuota
            int cuota = CalcularCuotaPago();
            DateTime fechaIngreso = dateTimePickerFechaIngreso.Value;
            DateTime fechaVencimiento = dateTimePickerFechaVencimiento.Value;
            // ahora registrar pensión
            Pension pension = new Pension(matriculaAuto,
                modeloAuto, nombrePropietario,
                apellidoPropietario,
                fechaIngreso, fechaVencimiento, cuota, cajonID);
            // guardar pensión en BD
            capaPersistencia.AgregarPension(pension);
            Cursor = Cursors.Arrow; 
            MessageBox.Show("Pensión agregada con éxito", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LimpiarValores();
        }

        private void DateTimePickerFechaVencimiento_ValueChanged(object sender, EventArgs e)
        {
            labelCuotaPagar.Text = "$" + CalcularCuotaPago() + ".00";
        }
    }
}
