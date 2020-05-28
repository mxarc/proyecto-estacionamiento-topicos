namespace ProyectoEstacionamientos.clases
{
    class Pension
    {
        private string matriculaAuto, nombrePropietario, fechaIngreso, fechaVencimiento;
        private int cuota, modelo;

        public Pension(string matriculaAuto, int modelo,
            string nombrePropietario, string fechaIngreso,
            string fechaVencimiento, int cuota)
        {
            this.matriculaAuto = matriculaAuto;
            this.modelo = modelo;
            this.nombrePropietario = nombrePropietario;
            this.fechaIngreso = fechaIngreso;
            this.fechaVencimiento = fechaVencimiento;
            this.cuota = cuota;
        }

        public string GetMatriculaAuto()
        {
            return matriculaAuto;
        }

        public void SetMatriculaAuto(string matriculaAuto)
        {
            this.matriculaAuto = matriculaAuto;
        }

        public string GetNombrePropietario()
        {
            return nombrePropietario;
        }

        public void SetNombrePropietario(string nomPropietario)
        {
            this.nombrePropietario = nomPropietario;
        }

        public string GetFechaIngreso()
        {
            return fechaIngreso;
        }

        public void SetFechaIngreso(string fechaIngreso)
        {
            this.fechaIngreso = fechaIngreso;
        }

        public string GetFechaVencimiento()
        {
            return fechaVencimiento;
        }

        public void SetFechaVencimiento(string fechaVencimiento)
        {
            this.fechaVencimiento = fechaVencimiento;
        }

        public int GetCuota()
        {
            return cuota;
        }

        public void SetCuota(int cuota)
        {
            this.cuota = cuota;
        }

        public int GetModelo()
        {
            return modelo;
        }

        public void SetModelo(int modelo)
        {
            this.modelo = modelo;
        }
    }
}
