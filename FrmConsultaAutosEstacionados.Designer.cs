namespace ProyectoEstacionamientos
{
    partial class FrmConsultaAutosEstacionados
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConsultaAutosEstacionados));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.CodigoEntrada = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MatriculaAuto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HoraEntrada = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cajon = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CodigoEntrada,
            this.MatriculaAuto,
            this.HoraEntrada,
            this.Cajon});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(600, 366);
            this.dataGridView1.TabIndex = 0;
            // 
            // CodigoEntrada
            // 
            this.CodigoEntrada.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.CodigoEntrada.HeaderText = "Código entrada";
            this.CodigoEntrada.Name = "CodigoEntrada";
            this.CodigoEntrada.ReadOnly = true;
            // 
            // MatriculaAuto
            // 
            this.MatriculaAuto.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.MatriculaAuto.HeaderText = "Matrícula";
            this.MatriculaAuto.Name = "MatriculaAuto";
            this.MatriculaAuto.ReadOnly = true;
            this.MatriculaAuto.Width = 77;
            // 
            // HoraEntrada
            // 
            this.HoraEntrada.HeaderText = "Hora de entrada";
            this.HoraEntrada.Name = "HoraEntrada";
            this.HoraEntrada.ReadOnly = true;
            // 
            // Cajon
            // 
            this.Cajon.HeaderText = "Clave de cajón";
            this.Cajon.Name = "Cajon";
            this.Cajon.ReadOnly = true;
            // 
            // FrmConsultaAutosEstacionados
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 366);
            this.Controls.Add(this.dataGridView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "FrmConsultaAutosEstacionados";
            this.Text = "Consulta autos estacionados";
            this.Load += new System.EventHandler(this.FrmConsultaAutosEstacionados_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodigoEntrada;
        private System.Windows.Forms.DataGridViewTextBoxColumn MatriculaAuto;
        private System.Windows.Forms.DataGridViewTextBoxColumn HoraEntrada;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cajon;
    }
}