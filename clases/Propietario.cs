namespace ProyectoEstacionamientos
{
    class Propietario
    {
        private string nombre, apellido;
        private int modelo;

        public Propietario(string matriculaAuto, string nombrePropietario, int modelo)
        {
            SetMatriculaAuto(nombre);
            SetNombrePropietario(apellido);
            SetModelo(modelo);
        }

        public void SetMatriculaAuto(string matriculaAuto)
        {
            this.nombre = matriculaAuto;
        }

        public void SetNombrePropietario(string nomPropietario)
        {
            this.apellido = nomPropietario;
        }

        public void SetModelo(int modelo)
        {
            this.modelo = modelo;
        }

        public string GetMatriculaAuto()
        {
            return nombre;
        }

        public string GetNombrePropietario()
        {
            return apellido;
        }

        public int GetModelo()
        {
            return modelo;
        }

    }
}
