namespace ProyectoEstacionamientos
{
    partial class FrmSalidaVehiculo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSalidaVehiculo));
            this.buttonCalcularCosto = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.buttonLimpiar = new System.Windows.Forms.Button();
            this.buttonSalir = new System.Windows.Forms.Button();
            this.buttonContinuar = new System.Windows.Forms.Button();
            this.textBoxCodigoEntrada = new System.Windows.Forms.TextBox();
            this.labelClave = new System.Windows.Forms.Label();
            this.checkBoxBoletoPerdido = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonCalcularCosto
            // 
            this.buttonCalcularCosto.Location = new System.Drawing.Point(109, 146);
            this.buttonCalcularCosto.Name = "buttonCalcularCosto";
            this.buttonCalcularCosto.Size = new System.Drawing.Size(75, 23);
            this.buttonCalcularCosto.TabIndex = 43;
            this.buttonCalcularCosto.Text = "Calcular";
            this.buttonCalcularCosto.UseVisualStyleBackColor = true;
            this.buttonCalcularCosto.Click += new System.EventHandler(this.ButtonCalcularCosto_Click);
            // 
            // label7
            // 
            this.label7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label7.Location = new System.Drawing.Point(72, 37);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(235, 34);
            this.label7.TabIndex = 42;
            this.label7.Text = "Rellena los campos que se te piden para calcular el costo de la estancia de un au" +
    "tomóvil";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold);
            this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label6.Location = new System.Drawing.Point(71, 9);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 26);
            this.label6.TabIndex = 41;
            this.label6.Text = "Salida";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(11, 9);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(56, 54);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 40;
            this.pictureBox1.TabStop = false;
            // 
            // buttonLimpiar
            // 
            this.buttonLimpiar.Location = new System.Drawing.Point(12, 146);
            this.buttonLimpiar.Name = "buttonLimpiar";
            this.buttonLimpiar.Size = new System.Drawing.Size(75, 23);
            this.buttonLimpiar.TabIndex = 39;
            this.buttonLimpiar.Text = "Limpiar";
            this.buttonLimpiar.UseVisualStyleBackColor = true;
            this.buttonLimpiar.Click += new System.EventHandler(this.ButtonLimpiar_Click);
            // 
            // buttonSalir
            // 
            this.buttonSalir.Location = new System.Drawing.Point(291, 11);
            this.buttonSalir.Name = "buttonSalir";
            this.buttonSalir.Size = new System.Drawing.Size(75, 23);
            this.buttonSalir.TabIndex = 38;
            this.buttonSalir.Text = "Volver";
            this.buttonSalir.UseVisualStyleBackColor = true;
            this.buttonSalir.Click += new System.EventHandler(this.ButtonSalir_Click);
            // 
            // buttonContinuar
            // 
            this.buttonContinuar.Location = new System.Drawing.Point(291, 146);
            this.buttonContinuar.Name = "buttonContinuar";
            this.buttonContinuar.Size = new System.Drawing.Size(75, 23);
            this.buttonContinuar.TabIndex = 37;
            this.buttonContinuar.Text = "Continuar";
            this.buttonContinuar.UseVisualStyleBackColor = true;
            this.buttonContinuar.Click += new System.EventHandler(this.ButtonContinuar_Click);
            // 
            // textBoxCodigoEntrada
            // 
            this.textBoxCodigoEntrada.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBoxCodigoEntrada.Location = new System.Drawing.Point(12, 95);
            this.textBoxCodigoEntrada.Name = "textBoxCodigoEntrada";
            this.textBoxCodigoEntrada.Size = new System.Drawing.Size(218, 20);
            this.textBoxCodigoEntrada.TabIndex = 36;
            // 
            // labelClave
            // 
            this.labelClave.AutoSize = true;
            this.labelClave.BackColor = System.Drawing.Color.Transparent;
            this.labelClave.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelClave.Location = new System.Drawing.Point(8, 72);
            this.labelClave.Margin = new System.Windows.Forms.Padding(5, 7, 3, 0);
            this.labelClave.Name = "labelClave";
            this.labelClave.Size = new System.Drawing.Size(157, 20);
            this.labelClave.TabIndex = 35;
            this.labelClave.Text = "Código de entrada";
            // 
            // checkBoxBoletoPerdido
            // 
            this.checkBoxBoletoPerdido.AutoSize = true;
            this.checkBoxBoletoPerdido.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.checkBoxBoletoPerdido.Location = new System.Drawing.Point(241, 94);
            this.checkBoxBoletoPerdido.Name = "checkBoxBoletoPerdido";
            this.checkBoxBoletoPerdido.Size = new System.Drawing.Size(135, 21);
            this.checkBoxBoletoPerdido.TabIndex = 44;
            this.checkBoxBoletoPerdido.Text = "¿Boleto perdido?";
            this.checkBoxBoletoPerdido.UseVisualStyleBackColor = true;
            // 
            // FrmSalidaVehiculo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(388, 186);
            this.Controls.Add(this.checkBoxBoletoPerdido);
            this.Controls.Add(this.buttonCalcularCosto);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.buttonLimpiar);
            this.Controls.Add(this.buttonSalir);
            this.Controls.Add(this.buttonContinuar);
            this.Controls.Add(this.textBoxCodigoEntrada);
            this.Controls.Add(this.labelClave);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmSalidaVehiculo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Salida de vehículo";
            this.Load += new System.EventHandler(this.FrmSalidaVehiculo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCalcularCosto;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button buttonLimpiar;
        private System.Windows.Forms.Button buttonSalir;
        private System.Windows.Forms.Button buttonContinuar;
        private System.Windows.Forms.TextBox textBoxCodigoEntrada;
        private System.Windows.Forms.Label labelClave;
        private System.Windows.Forms.CheckBox checkBoxBoletoPerdido;
    }
}