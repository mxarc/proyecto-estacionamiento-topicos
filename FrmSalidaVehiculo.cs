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
    public partial class FrmSalidaVehiculo : Form
    {
        readonly ErrorProvider errorP = new ErrorProvider();
        public FrmSalidaVehiculo()
        {
            InitializeComponent();
        }

        private void FrmSalidaVehiculo_Load(object sender, EventArgs e)
        {

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

        private int CalcularLosCostos(int minutos)
        {

            int costo = 15; // costo inicial
            int horas = (int)(minutos / 60); // convertir a horas
                                             // sumar la hora
            for (int i = 0; i < horas; i++)
            {
                costo += 10;
            }

            return costo;
        }

        private void ButtonCalcularCosto_Click(object sender, EventArgs e)
        {
            string codigoEntrada = textBoxCodigoEntrada.Text.Trim();
            // validaciones
            if (Validadores.ValidaNoVacio(codigoEntrada))
            {
                errorP.SetError(textBoxCodigoEntrada, "No puede estar vacio");
                MessageBox.Show("El código de entrada está vacio, no se puede consultar costo sin el",
            "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (codigoEntrada.Length > 8)
            {
                errorP.SetError(textBoxCodigoEntrada, "Muy largo");
                MessageBox.Show("El código de entrada no tiene más de 8 caracteres de longitud",
            "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            CapaPersistencia capaPersistencia = new CapaPersistencia();
            // validar que el código si esté estacionada
            if (!capaPersistencia.CodigoEntradaExiste(codigoEntrada))
            {
                errorP.SetError(textBoxCodigoEntrada, "No existe");
                MessageBox.Show("No se encontró este código de entrada en la BD",
            "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Cursor = Cursors.WaitCursor;
            Console.WriteLine(textBoxCodigoEntrada.Text);
            int diffMinutos = capaPersistencia.DiferenciaMinutos(codigoEntrada);
            Console.WriteLine(diffMinutos);
            int costo = CalcularLosCostos(diffMinutos);
            Cursor = Cursors.Arrow;
            int horas = diffMinutos / 60;
            int minutosRestantes = diffMinutos % 60;
            var confirmResult = MessageBox.Show("Deseas registrar salida del automóvil?",
                         "Confirmar salida",
                         MessageBoxButtons.YesNo);
            MessageBox.Show("Total horas: " + horas + "\nFracción minutos: " + minutosRestantes + "\nSe van a cobrar en total: $" + costo,
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Question);
            if (confirmResult == DialogResult.Yes)
            {
             // quitarlo de la BD
            }
 
        }

        private void ButtonContinuar_Click(object sender, EventArgs e)
        {
            string codigoEntrada = textBoxCodigoEntrada.Text.Trim();
            bool boletoPerdido = checkBoxBoletoPerdido.Checked;
            CapaPersistencia capaPersistencia = new CapaPersistencia();
            // abrir dialogo para buscar por matricula
            if (boletoPerdido)
            {
                string matriculaAuto = Prompt.ShowDialog("Introduce la matrícula").Trim();
                // validar que matricula no sea vacio:
                if (Validadores.ValidaNoVacio(matriculaAuto))
                {
                    MessageBox.Show("No puedes dejar el valor de matrícula vacio",
                            "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // validar que matricula esta estacionada
                Cursor = Cursors.WaitCursor;
                if (!capaPersistencia.MatriculaYaEstaEstacionada(matriculaAuto))
                {
                    Cursor = Cursors.Arrow;
                    MessageBox.Show("No hay ningún vehiculo con esa matrícula estacionado",
                            "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                int diffMinutos = capaPersistencia.DiferenciaMinutosMatricula(matriculaAuto);
                Console.WriteLine("Diff minutos: " + diffMinutos);
                int costo = CalcularLosCostos(diffMinutos);
                Cursor = Cursors.Arrow;
                // aplicar penalización boleto perdido
                costo += 80;
                int horas = diffMinutos / 60;
                int minutosRestantes = diffMinutos % 60;
                MessageBox.Show("Total horas: " + horas + "\nFracción minutos: " + minutosRestantes + " \nPenalización boleto: $80\nSe van a cobrar en total: $" + costo,
                        "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Question);
                return;
            }
            // flujo normal
            // validaciones
            if (Validadores.ValidaNoVacio(codigoEntrada))
            {
                errorP.SetError(textBoxCodigoEntrada, "No puede estar vacio");
                MessageBox.Show("El código de entrada está vacio, no se puede consultar costo sin el",
    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (codigoEntrada.Length > 8)
            {
                errorP.SetError(textBoxCodigoEntrada, "Muy largo");
                MessageBox.Show("El código de entrada no tiene más de 8 caracteres de longitud",
    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

    }
    public static class Prompt
    {
        public static string ShowDialog(string caption)
        {
            Form prompt = new Form()
            {
                Width = 290,
                Height = 120,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterParent
            };
            TextBox textBox = new TextBox() { Left = 10, Top = 10, Width = 250 };
            Button confirmation = new Button() { Text = "Continuar", Left = 200, Width = 60, Top = 40, DialogResult = DialogResult.OK };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
        }
    }
}
