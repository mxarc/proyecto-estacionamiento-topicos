namespace ProyectoEstacionamientos.clases
{
    class VehiculoEstacionado
    {
        private int clave;
        private string descripcion;
        private bool ocupado;

        public VehiculoEstacionado(int clave, string descripcion, bool ocupado)
        {
            this.clave = clave;
            this.descripcion = descripcion;
            this.ocupado = ocupado;
        }

        public int GetClave()
        {
            return clave;
        }

        public void SetClave(int clave)
        {
            this.clave = clave;
        }

        public string GetDescripcion()
        {
            return descripcion;
        }

        public void SetDescripcion(string descripcion)
        {
            this.descripcion = descripcion;
        }

        public bool GetOcupado()
        {
            return ocupado;
        }

        public void SetOcupado(bool ocupado)
        {
            this.ocupado = ocupado;
        }

    }
}
