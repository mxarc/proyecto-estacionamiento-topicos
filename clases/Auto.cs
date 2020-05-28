namespace ProyectoEstacionamientos
{
    class Auto
    {
        private string matriculaAuto, nombrePropietario;
        private int modelo;

        public Auto(string matriculaAuto, string nombrePropietario, int modelo)
        {
            SetMatriculaAuto(matriculaAuto);
            SetNombrePropietario(nombrePropietario);
            SetModelo(modelo);
        }

        public void SetMatriculaAuto(string matriculaAuto)
        {
            this.matriculaAuto = matriculaAuto;
        }

        public void SetNombrePropietario(string nombrePropietario)
        {
            this.nombrePropietario = nombrePropietario;
        }

        public void SetModelo(int modelo)
        {
            this.modelo = modelo;
        }

        public string GetMatriculaAuto()
        {
            return matriculaAuto;
        }

        public string GetNombrePropietario()
        {
            return nombrePropietario;
        }

        public int GetModelo()
        {
            return modelo;
        }

    }
}
