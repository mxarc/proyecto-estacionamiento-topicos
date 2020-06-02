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
            int diffMinutos = capaPersistencia.DiferenciaMinutosCodigo(codigoEntrada);
            Console.WriteLine(diffMinutos);
            int costo = CalcularLosCostos(diffMinutos);
            Cursor = Cursors.Arrow;
            int horas = diffMinutos / 60;
            int minutosRestantes = diffMinutos % 60;
            MessageBox.Show("Total horas: " + horas + "\nFracción minutos: " + minutosRestantes + "\nSe van a cobrar en total: $" + costo,
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }

        private void ButtonContinuar_Click(object sender, EventArgs e)
        {
            string codigoEntrada = textBoxCodigoEntrada.Text.Trim();
            bool boletoPerdido = checkBoxBoletoPerdido.Checked;
            CapaPersistencia capaPersistencia = new CapaPersistencia();
            // abrir dialogo para buscar por matricula, flujo cuando hay boleto perdido
            /**
             * FLUJO BOLETO PERDIDO
             */
            if (boletoPerdido)
            {
                string matriculaAuto = Utilidades.ShowDialog("Introduce la matrícula").Trim();
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
                int diffMinutosBoletoPerdido = capaPersistencia.DiferenciaMinutosMatricula(matriculaAuto);
                Console.WriteLine("Diff minutos: " + diffMinutosBoletoPerdido);
                int costoBoletoPerdido = CalcularLosCostos(diffMinutosBoletoPerdido);
                Cursor = Cursors.Arrow;
                // aplicar penalización boleto perdido
                costoBoletoPerdido += 80;
                int horasBoletoPerdido = diffMinutosBoletoPerdido / 60;
                int minutosRestantesBoletoPerdido = diffMinutosBoletoPerdido % 60;
                MessageBox.Show("Total horas: " + horasBoletoPerdido + "\nFracción minutos: "
                    + minutosRestantesBoletoPerdido + " \nPenalización boleto: $80\nSe van a cobrar en total: $"
                    + costoBoletoPerdido,
                        "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Question);
                // confirmar que quiere salir 
                var confirmResultDialog = MessageBox.Show("Deseas registrar salida del automóvil?",
 "Confirmar salida",
 MessageBoxButtons.YesNo);
                if (confirmResultDialog == DialogResult.No)
                {
                    // cancelar todo
                    return;
                }
                // despachar y aplicar cambios en la capa persistencia
                capaPersistencia.SalidaVehiculoMatricula(matriculaAuto);
                MessageBox.Show("Auto despachado",
        "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarValores();
                return;
            }
            /**
             * FLUJO NORMAL POR CODIGO DE ENTRADA
             * */
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
            // validar que codigo esta estacionada
            Cursor = Cursors.WaitCursor;
            if (!capaPersistencia.CodigoEntradaExiste(codigoEntrada))
            {
                Cursor = Cursors.Arrow;
                MessageBox.Show("No hay ningún vehiculo con esa código de entrada estacionado",
                        "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int diffMinutos = capaPersistencia.DiferenciaMinutosCodigo(codigoEntrada);
            Console.WriteLine("Diff minutos: " + diffMinutos);
            int costo = CalcularLosCostos(diffMinutos);
            Cursor = Cursors.Arrow;
            // aplicar penalización boleto perdido
            costo += 80;
            int horas = diffMinutos / 60;
            int minutosRestantes = diffMinutos % 60;
            MessageBox.Show("Total horas: " + horas + "\nFracción minutos: " + minutosRestantes + "\nSe van a cobrar en total: $" + costo,
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Question);
            // confirmar que quiere salir 
            var confirmResult = MessageBox.Show("Deseas registrar salida del automóvil?",
"Confirmar salida",
MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.No)
            {
                // canclear todo
                return;
            }
            // despachar y aplicar cambios en la capa persistencia
            capaPersistencia.SalidaCodigo(codigoEntrada);
            MessageBox.Show("Auto despachado",
    "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LimpiarValores();
        }

    }
}
